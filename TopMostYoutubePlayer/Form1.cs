using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopMostYoutubePlayer
{
    public partial class frmPlayer : Form
    {
        public frmPlayer()
        {
            InitializeComponent();
            this.TopMost = true;
        }
        protected override void OnLoad(EventArgs e)
        {
            PlaceLowerRight();
            base.OnLoad(e);
        }

        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            string html = "<html> <head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}?autoplay=1' width='715' height='350' frameborder='0' allowfullscreen='allowfullscreen'></iframe>";
            html += "</head></html> ";
            wbPlayerBrowser.DocumentText = string.Format(html, txtUrl.Text.Split('=')[1]);
        }
    }
}
