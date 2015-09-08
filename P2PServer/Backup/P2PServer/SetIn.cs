using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace P2PServer
{
    public partial class SetIn : Form
    {
        public SetIn()
        {
            InitializeComponent();
            Thread TempThread = new Thread(new ThreadStart(this.commun));
            TempThread.IsBackground = true;
            TempThread.Start();
        }

        TcpListener lisner;
        Socket client;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 3000);
        private void commun()
        {

          
            lisner = new TcpListener(ipep);
            lisner.Start();

            try
            {
                while (true)
                {
                    if (!lisner.Pending())//查看是否有挂起的连接请求
                    {
                        Thread.Sleep(1000);
                        continue;

                    }

                    client = lisner.AcceptSocket();//从连接请求队列中提取一个连接进行接受
                    IPEndPoint PhoneInfo = (IPEndPoint)client.RemoteEndPoint;
                    //获得手机操作系统
                    string os = System.Text.Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));

                    AddRow(PhoneInfo.Address, PhoneInfo.Port, os);
                }
            }
            catch 
            {
               

            }
          

        }
        private delegate void DelAddRow(object o1, object o2, object o3);

        private void AddRow(object o1, object o2, object o3)
        {
            if (InvokeRequired)
            {
                DelAddRow dar = new DelAddRow(AddRow);
                this.Invoke(dar, o1, o2, o3);
                return;
            }
            this.dataGridView1.Rows.Add(o1, o2, o3);
        }

        private void button3_Click(object sender, EventArgs e)
        {           
          
           
            if (client != null)
            {
                IPAddress cus = IPAddress.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                int port = Int32.Parse(dataGridView1.SelectedCells[1].Value.ToString());
                IPEndPoint stemp2 = new IPEndPoint(cus, port);
                string message = "exit";
                client.Connect(stemp2);
                NetworkStream stream = new NetworkStream(client);
                StreamWriter sw = new StreamWriter(stream);
                //  byte[] send = System.Text.Encoding.UTF8.GetBytes(message);
                sw.WriteLine(message);
                sw.Flush();
                //MessageBox.Show("已经发送");
                stream.Close();
                client.Disconnect(true);

            }
            else
                MessageBox.Show("错误操作！");
        }

       
    }
}