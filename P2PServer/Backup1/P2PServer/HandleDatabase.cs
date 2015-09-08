using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.IO;



///数据库操纵类///
namespace P2PServer
{
    class HandleDatabase
    {
        public static void IntoDatabase1(string t1, string t2, string t3, string t4, string t5, string sql)
        {
            try
            {
                OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                connectStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + "server.mdb";
                connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                OleDbConnection conn = new OleDbConnection(connectStringBuilder.ConnectionString);
                conn.Open();
           
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.ExecuteNonQuery();              
                conn.Close();
            }
            catch (System.Data.OleDb.OleDbException)
            {
            }
        }
        public static void IntoDatabase(string t1, string t2, string t3, string t4, string sql)
        {
            try
            {
                OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                connectStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + "server.mdb";
                connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                OleDbConnection conn = new OleDbConnection(connectStringBuilder.ConnectionString);
                conn.Open();
                // string sql = "insert into client (PhoneNum,PhoneID,SendTime,Flag) values('" + PhoneNum + "','" + PhoneID + "','" + SendTime + "','" +Flag + "')";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.ExecuteNonQuery();              
                conn.Close();
            }
            catch (System.Data.OleDb.OleDbException)
            {
            }
        }

        public static void delete(string sql)
        {
            OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
            connectStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + "server.mdb";
            connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
            DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(connectStringBuilder.ConnectionString);
            conn.Open();

            OleDbCommand myCmd = new OleDbCommand(sql, conn);
            OleDbDataReader datareader = myCmd.ExecuteReader();
            datareader.Close();
            conn.Close();
        }
        //获取命令
        public static string[] GetCommand(string PhoneID)
        {
            string[] CommandDo = new string[100];
            string PhoneNum, Content;
            int i = 0;
            OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
            connectStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + "server.mdb";
            connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
            DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(connectStringBuilder.ConnectionString);
            conn.Open();
            string sql = "select * from Command  where PhoneID = '" + PhoneID + "'";
            OleDbCommand myCmd = new OleDbCommand(sql, conn);
            OleDbDataReader datareader = myCmd.ExecuteReader();
            while (datareader.Read())
            {              
                PhoneNum = datareader["PhoneIMSI"].ToString();
                Content = datareader["Content"].ToString();
                string task = PhoneNum + "@" + Content;             
                CommandDo[i++] = Content;
                deleteCommand(PhoneID, Content);

                //每次只取一第命令发给手机
                break;
            }
            conn.Close();

            return CommandDo;
        }
        //删除命令
        public static void deleteCommand(string PhoneID, string content)
        {
            string sql = "delete from Command where PhoneID=" + "'" + PhoneID + "'and Content=" + "'" + content + "'";       
            delete(sql);        

        }
    }
}
