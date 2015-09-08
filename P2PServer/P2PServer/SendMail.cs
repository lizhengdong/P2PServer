using System;
using System.Collections.Generic;
using System.Text;
using jmail;
using System.Runtime.InteropServices;
namespace P2PServer
{
    class SendMail
    {
        //参考文章http://www.cnblogs.com/liping19851014/archive/2007/06/24/793989.html
        public Boolean SendMailByJmail(String aSendMailAdd, String aPassword, String aReceiveMailAdd, String aNewIP)
        {
            string[] SendMailAdd = aSendMailAdd.Split(new char[] { '@' });
            /**/
            ///建立发邮件类
            jmail.MessageClass oJmailMessage = new jmail.MessageClass();

            /**/
            /// 字符集
            oJmailMessage.Charset = "GB2312";

            /**/
            ///附件的编码格式
            oJmailMessage.Encoding = "BASE64";
            oJmailMessage.ContentType = "text/html";

            /**/
            ///是否将信头编码成iso-8859-1字符集
            oJmailMessage.ISOEncodeHeaders = false;

            /**/
            /// 优先级
            oJmailMessage.Priority = Convert.ToByte(1);

            /**/
            ///发送人邮件地址
            oJmailMessage.From = aSendMailAdd;

            /**/
            ///发送人姓名
            oJmailMessage.FromName = "IPChange";

            /**/
            /// 邮件主题
            oJmailMessage.Subject = aNewIP;

            /**/
            ///身份验证的用户名
            oJmailMessage.MailServerUserName = SendMailAdd[0];

            /**/
            ///用户密码
            oJmailMessage.MailServerPassWord = aPassword;

            /**/
            ///添加一个收件人，抄送人和密送人的添加和该方法是一样的，只是分别使用AddRecipientCC和RecipientBCC两个属性
            ///要是需要添加多个收件人，则重复下面的语句即可。添加多个抄送和密送人的方法一样
            oJmailMessage.AddRecipient(aReceiveMailAdd, "", "");

            /**/
            ///邮件内容
            oJmailMessage.Body = aNewIP;

            string aSmtpServer = "smtp." + SendMailAdd[1];
            try
            {
                if (oJmailMessage.Send(aSmtpServer, false))
                {
                    oJmailMessage = null;
                    return true;
                }
                else
                {
                    oJmailMessage = null;
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
