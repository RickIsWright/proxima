using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Proxima
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            //timer settings
            timer1.Tick += new EventHandler(SetProxy);
            timer1.Interval = (1000) * (900); // default 900 seconds;
            timer1.Enabled = true;
            timer1.Start();
            string info = Program.setProxy("10.10.10.255:1000", false);
            lblCurrentSettings.Text = info + "\nLast Check: " + DateTime.Now.ToString();
            lblCurrentSettings.ForeColor = Color.Green;

        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            string info=Program.setProxy("10.10.10.255:1000", false);
            lblCurrentSettings.Text = info + "\nLast Check: " + DateTime.Now.ToString();
            lblCurrentSettings.ForeColor = Color.Green;

            this.WindowState = FormWindowState.Minimized;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Proxima";
            notifyIcon1.BalloonTipText = "Proxima is running and  minimized";
            
            notifyIcon1.ShowBalloonTip(2000);
            this.Visible = false;
        }


        private void SetProxy(object sender,EventArgs e)
        {
            btnActivate_Click(sender, e); 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }
    }
}
