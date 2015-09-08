using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Diagnostics;




namespace P2PServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void deleteAndReset()
        {
            string PhoneNumO, PhoneIDO, ComTimeO;
            OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
            connectStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + "server.mdb";
            connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
            DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(connectStringBuilder.ConnectionString);
            conn.Open();
            string sql = "delete * from Command";
            OleDbCommand myCmd = new OleDbCommand(sql, conn);
            myCmd.ExecuteNonQuery();

            string sql_2 = "select * from client";
            OleDbCommand myCmd_2 = new OleDbCommand(sql_2, conn);
            OleDbDataReader datareader = myCmd_2.ExecuteReader();
           
            while (datareader.Read()) {

                PhoneNumO = datareader["PhoneNum"].ToString();
                PhoneIDO = datareader["PhoneID"].ToString();

                ComTimeO = System.DateTime.Now.ToString();
                string sql_3 = "insert into Command(PhoneID, PhoneIMSI, Content, ComTime, Flag) values ('" +
                    PhoneIDO + "', '" + PhoneNumO + "','reset','" + ComTimeO + "','On')";
                OleDbCommand myCmd_3 = new OleDbCommand(sql_3, conn);
                myCmd_3.ExecuteNonQuery();

            }
            conn.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            P2Pip.Text=getIPAddress();
            P2Pport.Text =MainPort.ToString();
           
            //将所有未取走的命令删除，并给所有客户端一个reset命令
            deleteAndReset();

            OnLine();
            OrderTran();
            Twenjian();
            TfileDown();
            Tyuyin();          
            YuyinKZ();
            LocationTran();
        }


        #region    
        int MainPort = 6000; //主听端口
        int OrdPort = 2005;  //命令端口
        int RecePort = 2006;  //接受手机文件的端口
        int TranPort = 5000; //转发端口 
        int FileDownPort = 7000; //文件下载端口
        int LocationPort = 9987; //接收位置信息端口
        FileStream MyFileStream;
        private IPAddress localAddr = IPAddress.Any;
        private static object syncroot = new object(); //线程同步，加锁标识

        Socket zhuanfaS = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket zhuanfaM = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket zhuanfaA = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        Socket Tran = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);     //主听socket

        Socket MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Socket TranSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//文件连接线
        Socket LocationSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //位置连接Socket
        Socket OrderSocket;                                                                   //接受手机端传来信息的Socket
        Socket MobSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//命令连接线 到手机 
     
        private string MonIP =  "192.101.155.157";     //控制端的IP       12.17    
        private int MonPort = 0;            //控制端的端口
     
        #endregion

        #region 线程的定义
        Thread ThreadTrany;
        Thread ThreadTranw;
        Thread ThreadTran1;
        Thread ThreadListen;
        Thread ThreadTranS;
        Thread ThreadTranA;
        Thread ThreadTranF;
        Thread ThreadListenS;
        Thread ThreadListenM;
        Thread ThreadListenA;
        public void OnLine() //监听服务器端上线没
        {
            ThreadListen = new Thread(new ThreadStart(listen));
            ThreadListen.IsBackground = true;
            ThreadListen.Start();

        }
        private void OrderTran()  //命令的转发
        {
            ThreadTran1 = new Thread(new ThreadStart(TOrder1));
            ThreadTran1.IsBackground = true;
            ThreadTran1.Start();
        }
        public void Twenjian() //文件的转发
        {
            //TCP的转发
            ThreadTranw = new Thread(new ThreadStart(Trans));
            ThreadTranw.IsBackground = true;
            ThreadTranw.Start();
        }

        public void LocationTran() //位置的转发
        {
            //TCP的转发
            ThreadTranw = new Thread(new ThreadStart(locationTransport));
            ThreadTranw.IsBackground = true;
            ThreadTranw.Start(); 
        }

        private void TfileDown()  //控制端下载文件处理
        {
            ThreadTranF = new Thread(new ThreadStart(TFileDown));
            ThreadTranF.IsBackground = true;
            ThreadTranF.Start();

        }
        public void YuyinKZ() //监听服务器端上线没
        {
            ThreadListenS = new Thread(new ThreadStart(ListenYuyinS));
            ThreadListenS.IsBackground = true;
            ThreadListenS.Start();
            ThreadListenM = new Thread(new ThreadStart(ListenYuyinM));
            ThreadListenM.IsBackground = true;
            ThreadListenM.Start();
            ThreadListenA = new Thread(new ThreadStart(ListenYuyinA));
            ThreadListenA.IsBackground = true;
            ThreadListenA.Start();
        }
        private void Tyuyin()  //语音的转发  WindowsMobile
        {
           
            ThreadTrany = new Thread(new ThreadStart(TransYuyinM));
            ThreadTrany.IsBackground = true;
            ThreadTrany.Start();
            ThreadTranS = new Thread(new ThreadStart(TransYuyinS));
            ThreadTranS.IsBackground = true;
            ThreadTranS.Start();
            ThreadTranA = new Thread(new ThreadStart(TransYuyinA));
            ThreadTranA.IsBackground = true;
            ThreadTranA.Start();
        }  
       
       
        #endregion       
        private static string getIPAddress()
        {
            System.Net.IPAddress addr;
            // 获得本机局域网IP地址 
            addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            return addr.ToString();
        } 

        #region 第三方与控制端连接线
        public void listen()   
        {
            try
            {
                IPEndPoint main = new IPEndPoint(IPAddress.Any, MainPort);//主听端口
                ShowMoniter.Text = "控制端下线";
                TcpListener lisnerL = new TcpListener(main);
                lisnerL.Start();                
             
                while (true)
                {                   
                    try
                    {
                        while (true)
                        {
                            if (!lisnerL.Pending())
                            {
                                ShowMoniter.Text = "控制端下线";
                                MoniterIP.Text = null;
                                MoniterPort.Text = null;
                                Thread.Sleep(1000);
                                continue;
                            }
                            Tran = lisnerL.AcceptSocket();
                            //设置第三方超时
                            Tran.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 20 * 1000);
                            Tran.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 60 * 1000);
                            //等待接收次数
                            //int recvCount = 0;
                            while (true)
                            {
                                byte[] receiveBytes = TransferFiles.ReceiveVarData(Tran);
                                string sTemp1 = Encoding.ASCII.GetString(receiveBytes);
                                IPEndPoint kz = (IPEndPoint)Tran.RemoteEndPoint;


                                //获取来自控制端的注册信息，并显示出控制端注册的IP和端口
                                if (sTemp1.IndexOf("Moniter") != -1)            
                                {
                                    MonIP =((IPEndPoint)Tran.RemoteEndPoint).Address.ToString();
                                    MonPort = ((IPEndPoint)Tran.RemoteEndPoint).Port;
                                    MoniterIP.Text = MonIP;
                                    MoniterPort.Text = MonPort.ToString();
                                    ShowMoniter.Text = "控制端上线";
                                    TransferFiles.SendVarData(Tran, receiveBytes); //返回相同信息
                                }

                                //获取来自控制端对手机端的命令，记入数据库 命令格式Order@PhoneNum@PhoneID@Content
                                if (sTemp1.IndexOf("Order") != -1)             
                                {
                                    string[] pinfor = sTemp1.Split('@');   //更改1031                      
                                  
                                    string PhoneNum = pinfor[1];  //IMSI号
                                    string PhoneID = pinfor[2];   //序列号
                                    string Content = pinfor[3];
                                    string ComTime = System.DateTime.Now.ToString();
                                    string Flag = "Doing";
                                    string sql = "insert into Command(PhoneIMSI,PhoneID,Content,ComTime,Flag) values('" + PhoneNum + "','" + PhoneID + "','" + Content + "','" + ComTime + "','" + Flag + "')";
                                    HandleDatabase.IntoDatabase1(PhoneNum, PhoneID, Content, ComTime, Flag, sql);//插入任务到“执行中”列表中
                                    string sql1 = "insert into ALLCommand(PhoneIMSI,PhoneID,Content,ComTime,Flag) values('" + PhoneNum + "','" + PhoneID + "','" + Content + "','" + ComTime + "','" + Flag + "')";
                                    HandleDatabase.IntoDatabase1(PhoneNum, PhoneID, Content, ComTime, Flag, sql1);//插入任务到“全部任务”列表中
                                  //  MessageBox.Show(sTemp1);
                                }                                
                            }
                        }

                    }
                   catch
                    {
                        ShowMoniter.Text = "控制端下线";
                        MoniterIP.Text = null;
                        MoniterPort.Text = null;
                    }
                }               
            }
            catch 
            {             
            }
        }
       
        #endregion

        #region 转发程序    服务器9987端口接收手机位置信息并转发给控制端
        public void locationTransport()
        {

            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, LocationPort);//2006
            TcpListener lisner = new TcpListener(TranFrom);
            lisner.Start();
            while (true) {

                try {
                    while (true) {
                        if (!lisner.Pending()) {
                            Thread.Sleep(1000);
                            continue;
                        }

                        //接受手机端传来位置信息的Socket
                        LocationSocket = lisner.AcceptSocket();
                        LocationSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                        //每接收一个手机端位置信息连接，就创建一个对应线程循环接收位置信息，主线程继续监听连接
                        ParameterizedThreadStart pts = new ParameterizedThreadStart(LocationSub);
                        Thread ThreadReceive = new Thread(pts);
                        ThreadReceive.IsBackground = true;
                        ThreadReceive.Start(LocationSocket);
                    }

                }
                catch {

                }
            }
        }
        public void LocationSub(object obj)
        {
            Socket client = (Socket)obj;
            toolStripStatusLabel1.Text = "手机定位已连接";
            try {
                while (true) {
                    byte[] temp = TransferFiles.ReceiveVarData1(client);
                    if (temp.Length == 0)
                        break;
                    string SendFileName = System.Text.Encoding.ASCII.GetString(temp);
   
                    if (String.Compare(SendFileName, String.Empty) == 0)
                        break;
                    //将位置信息转发给控制端 格式:Location@RegInfo@经度@纬度
                    TransferFiles.SendVarData(Tran, temp);
                    client.Close();
                    toolStripStatusLabel1.Text = "手机定位信息己发送";
                    break;
                }
            }
            catch//(Exception e)
            {
                //   MessageBox.Show(e.ToString());
            }
        }

        #endregion

        #region 转发程序    服务器2006端口接收手机端的文件,存入本地，将接收完的文件名传给控制端
        public void Trans()
        {
            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, RecePort);//2006
            IPEndPoint TranObj = new IPEndPoint(IPAddress.Any, TranPort);//5000
           
            TcpListener lisner = new TcpListener(TranFrom);
            lisner.Start();
            while (true)
            {

                try
                {
                    while (true)
                    {
                        if (!lisner.Pending())
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        //接受手机端传来文件信息的Socket
                        TranSocket = lisner.AcceptSocket();
                        TranSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                        //每接收一个手机端文件信息连接，就创建一个对应线程循环接收文件信息，主线程继续监听连接
                        ParameterizedThreadStart pts = new ParameterizedThreadStart(TranSub);
                        Thread ThreadReceive = new Thread(pts);
                        ThreadReceive.IsBackground = true;
                        ThreadReceive.Start(TranSocket);                        
                    }

                }
                catch
                {                   

                }
            }


        }      

        public void TranSub(object obj)
        {
            Socket client = (Socket)obj;
            toolStripStatusLabel1.Text = "手机发送文件已连接";
            try
            {
                while (true)
                {
                    byte[] temp = TransferFiles.ReceiveVarData1(client);
                     if (temp.Length == 0)
                        break;
                     string SendFileName = System.Text.Encoding.ASCII.GetString(temp);
                    //获得[文件名]                    
                    //  string SendFileName = System.Text.Encoding.ASCII.GetString(TransferFiles.ReceiveVarData1(client));
                    toolStripStatusLabel1.Text = "文件名收到";
                    if (String.Compare(SendFileName, String.Empty) == 0)
                         break;
                        
                    //获得[手机号]
                    string info = System.Text.Encoding.ASCII.GetString(TransferFiles.ReceiveVarData1(client));
                    string[] pinfor = info.Split('@');   //更改1031                   
                    string PhoneNum = pinfor[0];
                    string PhoneID = pinfor[1];
                    //创建一个新文件
                    string path = FileCreateSub(PhoneID);
                    string RTime = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SendFileName = PhoneID+"@"+PhoneNum + "@" + RTime +"@"+ SendFileName;  //第三方的文件名比控制端多加了个PhoneID
                    string fileFullName = path + "\\" + SendFileName;
                    byte[] FilenameSend = Encoding.UTF8.GetBytes("FileName"+"@"+SendFileName);
                    //  FileStream
                    MyFileStream = new FileStream(fileFullName, FileMode.Create, FileAccess.Write);


                    //已发送包的个数
                    int SendedCount = 0;
                    byte[] data;
                    int TotalSize = 0;
                    while (true)
                    {
                        data = TransferFiles.ReceiveVarData1(client);
                        if (data.Length == 0)
                        {
                            break;
                        }
                        else
                        {
                            TotalSize += data.Length;
                            SendedCount++;
                            //将接收到的数据包写入到文件流对象
                            MyFileStream.Write(data, 0, data.Length);
                            MyFileStream.Flush();
                            MyFileStream.Close();
                            break;
                        }
                    }
                
                 
                   TransferFiles.SendVarData(Tran, FilenameSend);
               
                //    MessageBox.Show("文件名给控制端" + SendFileName);
                    //关闭文件流
                    if (MyFileStream != null)
                        MyFileStream.Close();
                    client.Close();                   
                           
                    break;
                }
            }
            catch//(Exception e)
            {
             //   MessageBox.Show(e.ToString());
            }
        }
        public string FileCreateSub(string file)
        {
            //创建每个手机对应的文件夹
            string path = AppDomain.CurrentDomain.BaseDirectory + "ReceviceFile\\" + file;
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "ReceviceFile\\" + file))
            {
                //  MessageBox.Show("directory exists");  //C#创建文件夹  
            }
            else
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "ReceviceFile\\" + file);
                // MessageBox.Show("done");
            }
            return path;

        }
       #endregion     

        #region 处理控制端的文件下载
        public void TFileDown()
        { 
            while(true)
        {
            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, FileDownPort);//  监听文件下载端口              
            TcpListener lisner;          

            while (true)
            {
                Socket FileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                lisner = new TcpListener(TranFrom);
               
                lisner.Start();
                try
                {
                    while (true)
                    {
                        if (!lisner.Pending())
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        //接受控制端传来文件名的Socket                       
                        FileSocket = lisner.AcceptSocket();                      
                        //每接收一个控制端的文件名，就创建一个对应的线程传送相应的文件
                        ParameterizedThreadStart pts = new ParameterizedThreadStart(TFileSub);
                        Thread ThreadReceive = new Thread(pts);
                        ThreadReceive.IsBackground = true;
                        ThreadReceive.Start(FileSocket);                       
                    }
                }
                catch
                {                
                  
                   if (lisner != null)
                       lisner.Stop();
                }
                finally
                {                  
                    if (lisner != null)
                       lisner.Stop();
                }        
           }
        }     
        
        }     

        public void TFileSub(object obj)
        {          
            Socket FileSocket = (Socket)obj;
            try
            {

                while (true)
                {
                    //接收到控制端发来的文件名
                    byte[] temp = TransferFiles.ReceiveVarData(FileSocket);
                    if (temp.Length == 0)
                        break;
                    string temp2 = System.Text.Encoding.ASCII.GetString(temp, 0, temp.Length);
               //     MessageBox.Show("控制端ASK" + temp2);
                    if (temp2.IndexOf("FileAsk") == -1)
                        break;
                    //获得手机信息，准备添加入表格  信息格式 （FileAsk@PhoneID@PhoneNum@Rtime@文件名）
                    string[] pinfor = temp2.Split('@');
                    string PhoneID = pinfor[1];
                    string PhoneNum = pinfor[2];
                    string Rtime = pinfor[3];
                    string name = pinfor[4];
                    string filename =PhoneID+"@"+ PhoneNum + "@" + Rtime + "@" + name;
                    string FullName = AppDomain.CurrentDomain.BaseDirectory + "ReceviceFile\\" + PhoneID + "\\" + filename;
                //    MessageBox.Show("得到"+FullName);
                    //传输文件
                    if (File.Exists(FullName))
                    {
                     //   MessageBox.Show("存在" + FullName);                       

                        //创建一个文件对象   
                        FileInfo EzoneFile = new FileInfo(FullName);
                        //打开文件流   
                        FileStream EzoneStream = EzoneFile.OpenRead();
                        //包的大小   
                        int PacketSize = int.Parse("50000");
                        //包的数量   
                        int PacketCount = (int)(EzoneStream.Length / ((long)PacketSize));
                        //最后一个包的大小   
                        int LastDataPacket = (int)(EzoneStream.Length - ((long)(PacketSize * PacketCount)));


                        //发送[文件名]到客户端   
                        TransferFiles.SendVarData(FileSocket, System.Text.Encoding.Unicode.GetBytes(EzoneFile.Name));
                        //发送[包的大小]到客户端   
                        TransferFiles.SendVarData(FileSocket, System.Text.Encoding.Unicode.GetBytes(PacketSize.ToString()));
                        //发送[包的总数量]到客户端   
                        TransferFiles.SendVarData(FileSocket, System.Text.Encoding.Unicode.GetBytes(PacketCount.ToString()));
                        //发送[最后一个包的大小]到客户端   
                        TransferFiles.SendVarData(FileSocket, System.Text.Encoding.Unicode.GetBytes(LastDataPacket.ToString()));

                        //数据包   
                        byte[] data = new byte[PacketSize];
                        //开始循环发送数据包   
                        for (int i = 0; i < PacketCount; i++)
                        {
                            //从文件流读取数据并填充数据包   
                            EzoneStream.Read(data, 0, data.Length);
                            //发送数据包   
                            TransferFiles.SendVarData(FileSocket, data);                          
                           
                        }

                        //如果还有多余的数据包,则应该发送完毕!   
                        if (LastDataPacket != 0)
                        {
                            data = new byte[LastDataPacket];
                            EzoneStream.Read(data, 0, data.Length);
                             TransferFiles.SendVarData(FileSocket, data);
                           
                        }

                        //关闭套接字   
                        FileSocket.Close();                     
                        //关闭文件流   
                        EzoneStream.Close();  
                        //删除已经发送的文件
                        File.Delete(FullName);          
                    }
                
                    break;
                }
            }
            catch
            {
                if (FileSocket != null)
                    FileSocket.Close();
            }
            finally
            {
                if (FileSocket != null)
                    FileSocket.Close();
            }
        }
        #endregion

        #region 语音转发    
        public void TransYuyinS()
        {          
            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, 2007);//         
            UdpClient receive = new UdpClient(TranFrom);     
            IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {                   
                    while (true)
                    {
                        Byte[] receiveBytes = receive.Receive(ref RemoteIPEndPoint);
                        if (receiveBytes.Length == 0)
                            break;
                        IPAddress jianting = IPAddress.Parse(MonIP);
                        IPEndPoint TranTo = new IPEndPoint(jianting, 2007);

                        toolStripStatusLabel1.Text = "Symbian手机连接";
                        try {
                            TransferFiles.SendVarData(zhuanfaS, receiveBytes);
                            toolStripStatusLabel1.Text = "Sybian语音转发中";
                        } catch {
                            toolStripStatusLabel1.Text = "Sybian语音出错，等待重连中... ...";
                        }
                    }
                }
                catch//(Exception e)
                {
                   // MessageBox.Show("S语音" + e.ToString());
                }
            }          
        }
        public void TransYuyinM()
        {
            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, 10001);//         
            UdpClient receive = new UdpClient(TranFrom);
            IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    while (true)
                    {
                        Byte[] receiveBytes = receive.Receive(ref RemoteIPEndPoint);
                        if (receiveBytes.Length == 0)
                            break;
                        IPAddress jianting = IPAddress.Parse(MonIP);
                        IPEndPoint TranTo = new IPEndPoint(jianting, 10001);

                        toolStripStatusLabel1.Text = "Mobile手机连接";
                        try {
                            TransferFiles.SendVarData(zhuanfaM, receiveBytes);
                            toolStripStatusLabel1.Text = "Mobile语音转发中";
                        }
                        catch {
                            toolStripStatusLabel1.Text = "Mobile语音出错，等待重联中... ...";
                        }
                    }
                }
                catch//(Exception e)
                {
                 //   MessageBox.Show("Mobile语音" + e.ToString());
                }
            }
        }

        public void TransYuyinA()
        {
            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, 10003);//         
            UdpClient receive = new UdpClient(TranFrom);
            IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    while (true)
                    {
                        Byte[] receiveBytes = receive.Receive(ref RemoteIPEndPoint);
                        if (receiveBytes.Length == 0)
                            break;
                        IPAddress jianting = IPAddress.Parse(MonIP);
                        IPEndPoint TranTo = new IPEndPoint(jianting, 10003);

                        try {
                            TransferFiles.SendVarData(zhuanfaA, receiveBytes);  //发送超时为5s
                            toolStripStatusLabel1.Text = "Android语音转发中";
                        }
                        catch {
                            //从这个地方向控制端发送重新连接请求
                            Byte[] regInfo = new Byte[39];
                            for (int i = 0; i < 39; ++i) {
                                regInfo[i] = receiveBytes[i];
                            }
                            String reg = Encoding.ASCII.GetString(regInfo);
                            //MessageBox.Show(reg);
                            TransferFiles.SendVarData(Tran, regInfo); //发送给控制端，将其来联线接收信息
                            Thread.Sleep(3 * 1000); //等待3s
                            toolStripStatusLabel1.Text = "Android语音出错，等待重联中... ...";
                        }
                    }
                }
                catch//(Exception e)
                {
                    //   MessageBox.Show("Mobile语音" + e.ToString());
                }
            }
        }       
        public void ListenYuyinS()
        {
            try
            {
                IPEndPoint main = new IPEndPoint(IPAddress.Any, 2008);//主听端口
              
                TcpListener lisnerL = new TcpListener(main);
                lisnerL.Start();

                while (true)
                {
                    
                    try
                    {
                        while (true)
                        {
                            if (!lisnerL.Pending())
                            {
                                Thread.Sleep(1000);
                                continue;
                            }
                            zhuanfaS = lisnerL.AcceptSocket();
                            zhuanfaS.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 20 * 1000);
                            zhuanfaS.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 20 * 1000);


                            while (true)
                            {
                                byte[] receiveBytes = TransferFiles.ReceiveVarData(zhuanfaS);
                                string sTemp1 = Encoding.ASCII.GetString(receiveBytes);
                                IPEndPoint kz = (IPEndPoint)Tran.RemoteEndPoint;

                                //获取来自控制端的注册信息，并显示出控制端注册的IP和端口
                                if (sTemp1.IndexOf("MonVoice") != -1)
                                {
                                  //  KZyuyinS.Text = "S语音已连接";
                                }                            
                            }
                        }

                    }
                    catch
                    {
                       
                    }
                }
            }
            catch
            {
            }
        }
        public void ListenYuyinM()
        {
            try
            {
                IPEndPoint main = new IPEndPoint(IPAddress.Any, 10002);//主听端口

                TcpListener lisnerL = new TcpListener(main);
                lisnerL.Start();

                while (true)
                {
                    try
                    {
                        while (true)
                        {
                            if (!lisnerL.Pending())
                            {
                                Thread.Sleep(1000);
                                continue;
                            }
                            zhuanfaM = lisnerL.AcceptSocket();
                            zhuanfaM.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 20 * 1000);
                            zhuanfaM.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 20 * 1000);
                            while (true)
                            {
                                byte[] receiveBytes = TransferFiles.ReceiveVarData(zhuanfaM);
                                string sTemp1 = Encoding.ASCII.GetString(receiveBytes);
                                IPEndPoint kz = (IPEndPoint)Tran.RemoteEndPoint;

                                //获取来自控制端的注册信息，并显示出控制端注册的IP和端口
                                if (sTemp1.IndexOf("MonVoice") != -1)
                                {
                                    KZyuyinM.Text = "M语音已连接";
                                }
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }
            catch
            {
            }
        }
        public void ListenYuyinA()
        {
            try
            {
                IPEndPoint main = new IPEndPoint(IPAddress.Any, 10004);//主听端口

                TcpListener lisnerL = new TcpListener(main);
                lisnerL.Start();

                while (true)
                {
                    try
                    {
                        while (true)
                        {
                            if (!lisnerL.Pending())
                            {
                                Thread.Sleep(1000);
                                continue;
                            }
                            zhuanfaA = lisnerL.AcceptSocket();
                            zhuanfaA.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 20 * 1000);
                            zhuanfaA.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 5 * 1000);
                            while (true)
                            {
                                byte[] receiveBytes = TransferFiles.ReceiveVarData(zhuanfaA);
                                string sTemp1 = Encoding.ASCII.GetString(receiveBytes);
                                IPEndPoint kz = (IPEndPoint)Tran.RemoteEndPoint;

                                //获取来自控制端的注册信息，并显示出控制端注册的IP和端口
                                if (sTemp1.IndexOf("MonVoice") != -1)
                                {
                                  //  KZyuyinM.Text = "M语音已连接";
                                }
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }
            catch
            {
            }
        }
        #endregion
        #region  接收手机注册 进行转发

        public void TOrder1()
        {
            while (true)
            {
                TOrder();
               
            }
        
        }
        
        //多个线程的接收函数
        public void TOrder()
        { 
            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, OrdPort);//2005
            IPEndPoint TranObj = new IPEndPoint(IPAddress.Any, TranPort);//5000
           
            TcpListener lisner;           
          

            while (true)
            {
                OrderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                lisner = new TcpListener(TranFrom);
               
                lisner.Start();
                try
                {

                    while (true)
                    {

                        if (!lisner.Pending())
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        //接受手机端传来信息的Socket                       
                        OrderSocket = lisner.AcceptSocket();
                      
                        //每接收一个手机端连接，就创建一个对应的线程循环接收手机端发来的信息
                        ParameterizedThreadStart pts = new ParameterizedThreadStart(TOrderSub);
                        Thread ThreadReceive = new Thread(pts);
                        ThreadReceive.IsBackground = true;
                        ThreadReceive.Start(OrderSocket);
                       
                    }
                }
                catch
                {                
                  
                   if (lisner != null)
                       lisner.Stop();
                }
                finally
                {                  
                    if (lisner != null)
                       lisner.Stop();
                }
        
           }
        }
        public void TOrderSub(object obj)
        {
            Socket OrderSocket = (Socket)obj;
            try {
                String MobIPZC = ((IPEndPoint)OrderSocket.RemoteEndPoint).Address.ToString();
                int MobPortZC = ((IPEndPoint)OrderSocket.RemoteEndPoint).Port;

                while (true) {
                    //服务器接受手机端传来的注册信息

                    //将接受到的手机信息利用控制端注册时建立的连接转发到监控端,

                    //byte[] temp = TransferFiles.ReceiveVarData1(OrderSocket);
                    byte[] temp = TransferFiles.ReceiveVarData1(OrderSocket);
                    if (temp.Length == 0)
                        break;
                    string temp2 = System.Text.Encoding.ASCII.GetString(temp, 0, temp.Length);
                    byte[] tempKZ = Encoding.UTF8.GetBytes("PhoneZC" + "@" + temp2);

                    //获得手机信息，准备添加入表格
                    string[] pinfor = temp2.Split('@');
                    string OS = pinfor[0];

                    string PhoneNum = pinfor[1];
                    string PhoneID = pinfor[2];
                    if (PhoneNum == null) PhoneNum = "NotGet";
                    if (PhoneID == null) PhoneID = "NotGet";

                    bool close = false;
                    if (PhoneNum.Length != 15) close = true;
                    if (OS.Length < 6 || OS.Length > 7) close = true;
                    if (PhoneID.Length != 15) close = true;
                    if (close) {
                        if (OrderSocket != null)
                            OrderSocket.Close();
                        break;
                    }

                    string ComTime = System.DateTime.Now.ToString();

                    ClientOnline(PhoneNum, PhoneID, OS);

                    //写文件
                    //MessageBox.Show(temp2);

                    while (true) {
                        try {
                            if (MonIP == null)  //控制端没上线 或者已经下线
                          {
                                break;
                            }
                            else                //将手机端注册信息发送到控制端
                          {
                                //Tran.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 20 * 1000);
                                TransferFiles.SendVarData(Tran, tempKZ);
                                break;
                            }
                        }
                        catch {

                        }
                    }


                    //从数据库中取命令，发送到手机端    
                    string[] command = new string[] { };
                    command = HandleDatabase.GetCommand(PhoneID);
                    if (command != null)                        //有此手机的命令时
                  {
                        int i = 0;
                        while (command[i] != null) {
                            byte[] msg = Encoding.UTF8.GetBytes(command[i]);
                            //MessageBox.Show("向手机端" + PhoneID + "发送命令" + command[i]);             
                            OrderSocket.Send(msg);
                            i++;
                        }
                    }
                }

            }
            catch {

                if (OrderSocket != null)
                    OrderSocket.Close();

            }
            finally {
                if (OrderSocket != null)
                    OrderSocket.Close();
            }
        }
         
        #endregion           

        #region  向表格添加元素的函数
        private delegate void DelAddRow(object o1, object o2, object o3, object o4, object o5);

        private void AddRow(object o1, object o2, object o3, object o4, object o5)
        {
            if (InvokeRequired)
            {
                DelAddRow dar = new DelAddRow(AddRow);
                this.Invoke(dar, o1, o2, o3, o4,o5);
                return;
            }
       //     this.dataGridView1.Rows.Add(o1, o2, o3, o4,o5);
        }
        #endregion

       


        public void ClientOnline(string PhoneNum, string PhoneID, string OS)
        {
            string PhoneNumO, PhoneIDO, TimeO, FlagO, OSO;
            OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
            connectStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + "server.mdb";
            connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
            DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(connectStringBuilder.ConnectionString);
            conn.Open();
            string sql = "select * from Client ";
            OleDbCommand myCmd = new OleDbCommand(sql, conn);
            OleDbDataReader datareader = myCmd.ExecuteReader();
            string ComTime = System.DateTime.Now.ToString();
            string Flag = "On";
            bool hv = false;
            while (datareader.Read())
            {

                PhoneNumO = datareader["PhoneNum"].ToString();
                PhoneIDO = datareader["PhoneID"].ToString();
                TimeO = datareader["SendTime"].ToString();
                FlagO = datareader["Flag"].ToString();
                OSO = datareader["OS"].ToString();
                string sqlT = "update Client set Flag ='" + "On" + "', SendTime = '" + ComTime + "',OS='" + OS + "'where  PhoneID = '" + PhoneIDO + "'and PhoneNum='" + PhoneNumO + "'";
                if (PhoneNumO.CompareTo(PhoneNum) == 0 && PhoneIDO.CompareTo(PhoneID) == 0 && OSO.CompareTo(OS) == 0)
                {
                    OleDbCommand myCmd1 = new OleDbCommand(sqlT, conn);
                    myCmd1.ExecuteNonQuery();
                    hv = true;

                }

            }
            conn.Close();

            if (hv == false)  //没有记录时
            {

                string sqlI = "insert into client (PhoneNum,PhoneID,SendTime,Flag,OS) values('" + PhoneNum + "','" + PhoneID + "','" + ComTime + "','" + Flag + "','" + OS + "')";
                HandleDatabase.IntoDatabase1(PhoneNum, PhoneID, ComTime, Flag, OS, sqlI);
            }

        }

        public void GetTreeTest()
        {
            treeView2.Nodes.Clear();
            string PhoneNum, PhoneID, Flag, Time, OS;
            OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
            connectStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + "server.mdb";
            connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
            DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(connectStringBuilder.ConnectionString);
            conn.Open();
            string sql = "select * from Client Order by SendTime DESC";
            OleDbCommand myCmd = new OleDbCommand(sql, conn);
            OleDbDataReader datareader = myCmd.ExecuteReader();
            int num = 0;

            while (datareader.Read())
            {
                PhoneNum = datareader["PhoneNum"].ToString();
                PhoneID = datareader["PhoneID"].ToString();
                Time = datareader["SendTime"].ToString();
                Flag = datareader["Flag"].ToString();
                OS = datareader["OS"].ToString();
                PhoneNum = PhoneNum + " " + PhoneID + " " + OS;
                string sqlT = "update Client set Flag ='" + "Off" + "' where  SendTime = '" + Time + "'";
                if (Flag.CompareTo("On") == 0)
                {
                    //检查是否过期 超过二分钟 则设为Off
                    System.DateTime date3 = Convert.ToDateTime(Time);
                    System.DateTime ComTime = System.DateTime.Now;
                    System.TimeSpan diff2 = ComTime - date3;

                    if (diff2.Minutes > 2)
                    {
                        OleDbCommand myCmd1 = new OleDbCommand(sqlT, conn);
                        myCmd1.ExecuteNonQuery();
                     // treeView2.Nodes.Add(PhoneNum, PhoneNum, 17);
                        num++;
                    }
                    else
                    {
                        treeView2.Nodes.Add(PhoneNum, PhoneNum);  //只加载在线的
                        num++;                    

                    }
                }
                else
                {
                  //  treeView2.Nodes.Add(PhoneNum, PhoneNum, 17);
                    num++;
                }


            }

            conn.Close();



        }
        private void timer1_Tick(object sender, EventArgs e)        {
          
            GetTreeTest();
        }

        private void MoniterPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ShowMoniter_TextChanged(object sender, EventArgs e)
        {

        }

    }
}