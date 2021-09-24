using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace auto_inicio
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

            Process[] _process = null;
            _process = Process.GetProcessesByName("firefox");
            foreach (Process proces in _process)
            {
                proces.Kill();
            }





      
        }
    }

}
