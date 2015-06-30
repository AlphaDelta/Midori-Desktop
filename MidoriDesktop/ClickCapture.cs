using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MidoriDesktop
{
    public partial class ClickCapture : Form
    {
        public ClickCapture()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.TopLevel = true;
            this.TopMost = true;
            this.ShowInTaskbar = false;

            this.Location = new Point(0, 0);

            int height = 0, width = 0;
            foreach (Screen scr in Screen.AllScreens)
            {
                width += scr.Bounds.Width;
                if (scr.Bounds.Height > height) height = scr.Bounds.Height;
            }

            this.Size = new Size(width, height);

            this.Cursor = Cursors.Cross;
            this.MouseDown += delegate { this.Close(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}
