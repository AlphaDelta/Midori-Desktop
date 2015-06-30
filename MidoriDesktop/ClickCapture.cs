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
        Overlay overlay;
        public int X, Y, W, H;
        int L, T, R, B;
        public ClickCapture(Overlay overlay)
        {
            this.overlay = overlay;

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

            overlay.Size = this.Size = new Size(width, height);
            overlay.W = width;
            overlay.H = height;
            overlay.SetSize(width, height);

            this.Cursor = Cursors.Cross;

            bool dragging = false;
            this.MouseDown += (object sender, MouseEventArgs e) =>
            {
                dragging = true;

                this.L = e.X;
                this.T = e.Y;
                this.R = e.X;
                this.B = e.Y;

                overlay.L = e.X;
                overlay.T = e.Y;
                overlay.R = e.X;
                overlay.B = e.Y;

                overlay.Update();
                overlay.Show();
            };
            this.MouseMove += (object sender, MouseEventArgs e) =>
            {
                if (!dragging) return;

                this.R = e.X;
                this.B = e.Y;

                overlay.R = e.X;
                overlay.B = e.Y;

                overlay.Update();
            };
            this.MouseUp += delegate { this.Close(); };
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
