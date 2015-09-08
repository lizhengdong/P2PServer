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




namespace P2PServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            P2Pip.Text=getIPAddress();
            P2Pport.Text =MainPort.ToString();
           
            OnLine();
            OrderTran();
            Twenjian();
            Tyuyin();
            TyuyinS();

        }


        #region    

        int MainPort = 6000; //主听端口
        int OrdPort = 2005;  //命令端口
        int RecePort = 2006;  //接受手机文件的端口
        int TranPort = 5000; //转发端口
        int Count = 0;       //手机连接数
        string[] Order;
        private IPAddress localAddr = IPAddress.Any;
        //IPEndPoint TranToZC = new IPEndPoint(jianting, 4000);
        //IPEndPoint TranToWJ = new IPEndPoint(jianting, 2005);  //修改IP
        Socket MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Socket TranSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//文件连接线  
        Socket OrderSocket;//= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//接受手机端传来信息的Socket
        Socket MobSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//命令连接线 到手机 
        private Thread ThreadRead;
        private string MonIP =  "192.101.155.157";     //控制端的IP       12.17
        private string MobIP = null;        //手机端的IP
        private int MonPort = 0;            //控制端的端口
        private int MobPort = 0;                //手机端的端口  12.17
        private UdpClient client = null;
        private UdpClient receive;
        private NetworkStream networkstream = null;
        bool monitor ;                          //判断控制端的标记变量
        DateTime lastTime;
        DateTime nowTime;
        #endregion

        private static string getIPAddress()
        {
            System.Net.IPAddress addr;
            // 获得本机局域网IP地址 
            addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            return addr.ToString();
        } 

        #region 接收控制端上线注册信息的函数   
      
        public void listen()   
        {

            try
            {

                IPEndPoint main = new IPEndPoint(IPAddress.Any, MainPort);//主听端口
                ShowMoniter.Text = "控制端下线";
                TcpListener lisnerL = new TcpListener(main);
                lisnerL.Start();  
              
                Socket Tran = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
                            Tran = lisnerL.AcceptSocket();
                            while (true)
                            {
                                byte[] receiveBytes = TransferFiles.ReceiveVarData(Tran);
                                string sTemp1 = Encoding.ASCII.GetString(receiveBytes);
                                IPEndPoint kz = (IPEndPoint)Tran.RemoteEndPoint;
                                if (sTemp1.IndexOf("Moniter") != -1)            //获取来自控制端的注册信息，并显示出控制端注册的IP和端口
                                {
                                    MonIP =((IPEndPoint)Tran.RemoteEndPoint).Address.ToString();
                                    MonPort = ((IPEndPoint)Tran.RemoteEndPoint).Port;
                                    MoniterIP.Text = MonIP;
                                    MoniterPort.Text = MonPort.ToString();
                                    ShowMoniter.Text = "控制端上线";                                   
                                }
                                TransferFiles.SendVarData(Tran, receiveBytes);
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
        
        #region 转发程序    服务器2006端口接收手机端的文件，转发到监控端的2005端口     
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
            Socket TranSocket = (Socket)obj;
            toolStripStatusLabel1.Text = "手机发送文件已连接";
            //与监控端进行连接，准备转发
            //发送到监控端的Socket
            try
            {
                Socket Tran = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//转发文件                       
                //Tran.Connect(TranToWJ);
                while (true)
                {
                    
                    try
                    {
                        if (Tran.Connected||MonIP==null)
                        {

                            break;
                        }
                        else
                        {
                            IPAddress jianting = IPAddress.Parse(MonIP);
                            IPEndPoint TranToWJ = new IPEndPoint(jianting, 2005); 
                            Tran.Connect(TranToWJ);

                        }
                    }
                    catch
                    {

                    }
                }    
                byte[] temp;
                while (true)
                {
                    //服务器接受手机端传来的文件
                 
                       temp = TransferFiles.ReceiveVarData1(TranSocket);
                   if (temp.Length == 0)
                     break;
                   else
                    //将接受到的文件转发到监控端
                    {
                        TransferFiles.SendVarData(Tran, temp);
                        toolStripStatusLabel1.Text = "文件转发中";
                    }
                }
                TranSocket.Close();
                if(Tran!=null)
               // Tran.Disconnect(false);   1220
                Tran.Close();
            }
            catch 
            { 
                      
            
             
               if (TranSocket != null)
                  TranSocket.Close();
              
             
            }
        }
       #endregion     

        #region 语音转发
        public void TransYuyin()
        {
            //MessageBox.Show("进入语音转发");
            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, 10001);//2007
            IPEndPoint TranObj = new IPEndPoint(IPAddress.Any, TranPort);//5000
            //IPEndPoint TranTo = new IPEndPoint(jianting, 10001);
          

            UdpClient receive = new UdpClient(TranFrom);
            UdpClient zhuanfa = new UdpClient();

            IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {             

                try
                {
                 
                        //MessageBox.Show("进入语音转发");
                       

                        while (true)
                        {
                            Byte[] receiveBytes = receive.Receive(ref RemoteIPEndPoint);
                            IPAddress jianting = IPAddress.Parse(MonIP);
                            IPEndPoint TranTo = new IPEndPoint(jianting, 10001);
                            toolStripStatusLabel1.Text = "手机发送文件已连接";
                            zhuanfa.Send(receiveBytes, receiveBytes.Length, TranTo);
                            toolStripStatusLabel1.Text = "语音转发中";

                        }
                    


                }



                catch
                {                    
                   

                }


            }
        }
        public void TransYuyinS()
        {
            //MessageBox.Show("进入语音转发");
            IPEndPoint TranFrom = new IPEndPoint(IPAddress.Any, 2007);//10001
            IPEndPoint TranObj = new IPEndPoint(IPAddress.Any, TranPort);//5000
            //IPEndPoint TranTo = new IPEndPoint(jianting, 10001);


            UdpClient receive = new UdpClient(TranFrom);
            UdpClient zhuanfa = new UdpClient();

            IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {

                try
                {                
                       

                        while (true)
                        {
                           
                            Byte[] receiveBytes = receive.Receive(ref RemoteIPEndPoint);
                            IPAddress jianting = IPAddress.Parse(MonIP);
                            IPEndPoint TranTo = new IPEndPoint(jianting, 2007);

                            toolStripStatusLabel1.Text = "Symbian手机连接";
                            zhuanfa.Send(receiveBytes, receiveBytes.Length, TranTo);
                            toolStripStatusLabel1.Text = "语音转发中";

                        }
                    


                }



                catch
                {


                }


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
            //Socket Tran;  12.17

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
        string temp3;
        public void TOrderSub(object obj)
        {
            Socket OrderSocket = (Socket)obj;
             try
             {   
              String MobIPZC = ((IPEndPoint)OrderSocket.RemoteEndPoint).Address.ToString();
              int MobPortZC = ((IPEndPoint)OrderSocket.RemoteEndPoint).Port;          
              Socket Tran = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);            
              while (true)
              {
                 //服务器接受手机端传来的注册信息
               
                 //将接受到的手机信息转发到监控端
                                   
                   byte[] temp = TransferFiles.ReceiveVarData1(OrderSocket);
                   if (temp.Length == 0)
                          break;
                   string temp2 = System.Text.Encoding.ASCII.GetString(temp, 0, temp.Length);
                  
                   //获得手机信息，准备添加入表格
                   //1230
                   string[] pinfor = temp2.Split('@');
                   string PhoneNum = pinfor[1];
                   string XuLie = pinfor[2];
                   string ComTime = System.DateTime.Now.ToString();
                   //查看是否是同一手机发来的信息，是则更新
                   for(int i=0;i<dataGridView1.Rows.Count-1;i++)
                    {

                        if (XuLie.CompareTo(dataGridView1.Rows[i].Cells[0].Value.ToString()) == 0)                           
                            dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                                  
                      
                    }
                    AddRow(XuLie, PhoneNum, MobIPZC, MobPortZC.ToString(),ComTime);   
                      
                   while (true)
                   {
                    
                     try
                     {
                         if (Tran.Connected ||MonIP== null)  //控制端没上线 或者已经下线
                        {
                            
                            break;
                        }
                        else
                        {
                            IPAddress jianting = IPAddress.Parse(MonIP);
                            IPEndPoint TranToZC = new IPEndPoint(jianting, 4000);
                            Tran.Connect(TranToZC);

                        }
                      }
                      catch
                      {

                      }
                   }                               
                   TransferFiles.SendVarData(Tran, temp);
                  
                   //测试 接收响应 接收控制端发回的命令信息
                  //Tran发送完后来接收控制端的回复
                  byte[] tempKZ;                             
                  while (true)
                  {
                     
                      //接受控制端的信息
                       if (Tran.Available != 0)
                           tempKZ = TransferFiles.ReceiveVarData(Tran);
                       else break;                         
                          
                      //将信息发送到手机                              
                       string temp1 = System.Text.Encoding.ASCII.GetString(tempKZ);
                    
                              
                       OrderSocket.Send(tempKZ);
                                        
                   }
                  
                 
               }
               OrderSocket.Close();
               //Tran.Disconnect(true);
               if(Tran!=null)
                 Tran.Close();
             //  lisner.Stop();
              // break;                      
           }              
            
          catch
          {
             
            if (OrderSocket != null)
             OrderSocket.Close();           
           
           }
          finally
          {
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
            this.dataGridView1.Rows.Add(o1, o2, o3, o4,o5);
        }
        #endregion

        #region 检查手机信息是否过期，过期则删除
        public void UpDate()
        {
           // string ComTime = System.DateTime.Now.ToString();
            System.DateTime ComTime = System.DateTime.Now;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                System.DateTime date3 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[4].Value);
                    //new System.DateTime(Convert..ToDateTime(dataGridView1.Rows[i].Cells[4]);

                System.TimeSpan diff2 = ComTime- date3;
               
                if (diff2.Seconds > 40)
                {
                    
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                }


            }
        
        
        }


        #endregion

        #region 线程的定义
        Thread ThreadTrany;
        Thread ThreadTranw;
        Thread ThreadTran1;
        Thread ThreadListen;
        Thread ThreadTranS;
        public void OnLine() //监听服务器端上线没
        {
            ThreadListen = new Thread(new ThreadStart(listen));
            ThreadListen.IsBackground = true;
            ThreadListen.Start();
        
        }
        public void Twenjian() //文件的转发
        {
            //TCP的转发
            ThreadTranw = new Thread(new ThreadStart(Trans));
            ThreadTranw.IsBackground = true;
            ThreadTranw.Start();
        }

        private void Tyuyin()  //语音的转发  WindowsMobile
        {
            //UDP的转发TransYuyin()
            ThreadTrany = new Thread(new ThreadStart(TransYuyin));
            ThreadTrany.IsBackground = true;
            ThreadTrany.Start();
        }

        private void OrderTran()  //命令的转发
        {
            ThreadTran1 = new Thread(new ThreadStart(TOrder1));
            ThreadTran1.IsBackground = true;
            ThreadTran1.Start();
        }
        private void TyuyinS()  //语音的转发  Symbian
        {
            //UDP的转发TransYuyinS()
            ThreadTranS = new Thread(new ThreadStart(TransYuyinS));
            ThreadTranS.IsBackground = true;
            ThreadTranS.Start();
        }
        #endregion       

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpDate();
        }

       

        
      

     

      








    }
}