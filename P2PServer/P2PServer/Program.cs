using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace P2PServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //禁止程序重复启动两次
                bool createdNew;
                //系统能够识别有名称的互斥，因此可以使用它禁止程序启动两次
                //第二个参数可以设置为产品的名称：Application.ProductName
                //每次启动应用程序，都会验证名称为P2PServer的互斥是否存在
                Mutex mutex = new Mutex(false, "P2PServer", out createdNew);

                //如果已经运行，则在前端显示
                //createdNew == false,说明程序已经运行
                if (!createdNew)
                {
                    Process instance = GetExistProcess();
                    if (instance != null)
                    {
                        SetForeground(instance);
                        Application.Exit();
                        return;
                    }

                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch(Exception e)
            {

            }
            
        }

        //查看程序是否已经运行
        private static Process GetExistProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if ((process.Id != currentProcess.Id) && (Assembly.GetExecutingAssembly().Location == currentProcess.MainModule.FileName))
                {
                    return process;
                }
            }
            return null;
        }

        //使程序前端显示
        private static void SetForeground(Process instance)
        {
            IntPtr mainFormHandle = instance.MainWindowHandle;
            if (mainFormHandle != IntPtr.Zero)
            {
                ShowWindowAsync(mainFormHandle, 1);
                SetForegroundWindow(mainFormHandle);
            }
        }
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWind,int cmdShow);
    }
}