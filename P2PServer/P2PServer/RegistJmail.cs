using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace P2PServer
{
    class RegistJmail
    {
        public void DoUpdate()
        {
            //在注册路径前后加上双引号，以免注册时路径出现空格，发生崩溃
            string strPath = "\"" + Application.StartupPath;
            /*
            IniFile ini = new IniFile(strPath + @"/sys.ini" + "\"");
            //注册Jmail
            if (ini.IniReadValue("System", "JmailReg").ToString() != "1")
            {
                if (JmailReg(strPath))
                {
                    ini.IniWriteValue("System", "JmailReg", "1");
                }
                else
                {
                    MessageBox.Show("没有自动注册Jmail，请手动注册");
                }
            }
             */
            JmailReg(strPath);
        }
        public static Boolean JmailReg(string targetdir)
        {
            try
            {
                //参见：http://blog.csdn.net/qdrush/article/details/3404552
                /*
                ProcessStartInfo processInfo = new ProcessStartInfo("regsvr32");
                //processInfo.WindowStyle=ProcessWindowStyle.Normal;
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processInfo.Arguments = " /s " + Char.ToString('"') + targetdir + "jmail.dll" + Char.ToString('"');
                try
                {
                    Process osql = Process.Start(processInfo);
                    //Wait till it is done...
                    osql.WaitForExit();
                    osql.Dispose();
                    processInfo = null;
                }
                catch
                {
                    return false;
                }
                */

                //启动cmd.exe执行命令，regsvr32 路径\jmail.dll 

                try
                {
                    string registJmail = "regsvr32 /s " + targetdir + "\\" + "jmail.dll" + "\"";
                    Process p = new Process();
                    p.StartInfo.FileName = @"C:\WINDOWS\system32\cmd.exe ";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.StandardInput.WriteLine(@registJmail);　　//执行注册Jmail的命令
                    p.StandardInput.WriteLine("exit");
                    p.WaitForExit();
                    p.Close();
                    p.Dispose();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("没有自动注册Jmail，请手动注册");
                return false;

            }
        }
    }
}
