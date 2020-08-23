using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace ADSLConnecter
{
    public class ADSLIB
    {
        static String _temppath = "temp.bat";
        public static String temppath
        {
            get { return ADSLIB._temppath; }
            set { ADSLIB._temppath = value; }
        }
        private static StringBuilder sb = new StringBuilder();

        public static int delay = 1;
        public static void ChangeIp(String ADSL_Name = "", String ADSL_UserName = "", String ADSL_PassWord = "")
        {
            sb.Clear();
            sb.AppendLine("@echo off");
            sb.AppendLine("set adslmingzi=" + ADSL_Name);
            sb.AppendLine("set adslzhanghao=" + ADSL_UserName);
            sb.AppendLine("set adslmima=" + ADSL_PassWord);
            sb.AppendLine("Rasdial %adslmingzi% /disconnect");
            sb.AppendLine("Rasdial %adslmingzi% %adslzhanghao% %adslmima%");

            using (StreamWriter sw = new StreamWriter(temppath, false, Encoding.Default))
            {
                sw.Write(sb.ToString());
            }
            Process.Start(temppath);
            System.Threading.Thread.Sleep(delay * 1000);
            /*
            while (!HttpMethod.CheckIp(null))
            {
                Process.Start(temppath);
                System.Threading.Thread.Sleep(2 * delay * 1000);
            }
            */
            File.Delete(temppath);
        }
        public static String GetIP()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
    }
}
