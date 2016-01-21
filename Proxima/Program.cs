using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Proxima
{
    static class Program
    {
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        public static string ProxyInfo = "";

        public static string setProxy(string proxyhost, bool proxyEnabled)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey="Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            const string keyName = userRoot+ "\\" + subkey;
            
            //set registry values
            Registry.SetValue(keyName, "ProxyServer", proxyhost);
            Registry.SetValue(keyName, "ProxyEnable", 0);
            var IsProxyEnabled=Registry.GetValue(keyName, "ProxyEnable", "2");


            //implement the interface and force OS to refresh the settings
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero,0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero,0);
            string retVal = String.Format("Proxy Settings: Enabled:{0}, Server:{1}", IsProxyEnabled, proxyhost);
            return retVal;
        } // eof setProxy

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            setProxy("10.10.10.255:1000", false);
            Application.Run(new frmMain());
          
        }
    }
}
