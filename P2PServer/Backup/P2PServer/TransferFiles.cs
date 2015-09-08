using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace P2PServer
{
    class TransferFiles
    {
        public static int SendData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }

            return total;
        }

        public static byte[] ReceiveData(Socket s, int size)
        {
            int total = 0;
            int dataleft = size;
            byte[] data = new byte[size];
            int recv;
            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, SocketFlags.None);
                if (recv == 0)
                {
                    data = null;
                    break;
                }

                total += recv;
                dataleft -= recv;
            }
            recv = s.Receive(data, total, dataleft, SocketFlags.None);

            return data;
        }

        public static int SendVarData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            byte[] datasize = new byte[4];
            datasize = BitConverter.GetBytes(size);
            sent = s.Send(datasize);

            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }

            return total;
        }

        public static byte[] ReceiveVarData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            recv = s.Receive(datasize, 0, 4, SocketFlags.None);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];
            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, SocketFlags.None);
                if (recv == 0)
                {
                    data = null;
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }
        public static byte[] ReceiveVarData1(Socket s)      //接收手机端信息时的接收函数
        {

            byte[] content = new byte[9];

            // 一次性接收8个字节  不管超出的部分
            s.Receive(content);
            //     byte[] a = Encoding.ASCII.GetBytes("Moniter");
            //    s.Send(a);
            //  MessageBox.Show("接收到大小");

            string temp2 = System.Text.Encoding.ASCII.GetString(content, 0, content.Length);
            string temp1 = System.Text.Encoding.Unicode.GetString(content, 0, content.Length);
            int size = 0;
            int tempch = 0;
            for (int i = 0; i < 9; i++)
            {

                tempch = (int)content[i] - 48;
                if (tempch == -48) break;
                // MessageBox.Show(tempch.ToString());
                //MessageBox.Show("-----------------");
                size = size * 10 + tempch;
                // MessageBox.Show(size.ToString());
            }

          

            //  byte[] data = new byte[8];
            //s.Receive(data);
            //return data;

            int total = 0;
            int recv;
            int dataleft = size;
            byte[] data = new byte[size];
            while (total < size)
            {


                recv = s.Receive(data, total, dataleft, SocketFlags.None);
               
                if (recv == 0)
                {
                    data = null;
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;

        }

    }
}
