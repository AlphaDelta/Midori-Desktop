using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
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
            bool active = true;
            this.MouseDown += (object sender, MouseEventArgs e) =>
            {
                if (!active) return;

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
                if (!active) return;
                if (!dragging) return;

                bool flag = false;
                foreach(Screen scr in Screen.AllScreens)
                    if (e.X > scr.Bounds.Right - 8 && e.X < scr.Bounds.Right + 8)
                    {
                        this.R = scr.Bounds.Right;
                        flag = true;
                        break;
                    }
                
                if(!flag) this.R = e.X;
                this.B = e.Y;

                if (this.R < this.L)
                {
                    overlay.L = this.R;
                    overlay.R = this.L;
                }
                else
                {
                    overlay.L = this.L;
                    overlay.R = this.R;
                }
                if (this.B < this.T)
                {
                    overlay.T = this.B;
                    overlay.B = this.T;
                }
                else
                {
                    overlay.T = this.T;
                    overlay.B = this.B;
                }

                overlay.Update();
            };
            this.MouseUp += delegate
            {
                dragging = false;
                active = false;

                this.X = (this.R < this.L ? this.R : this.L);
                this.Y = (this.B < this.T ? this.B : this.T);
                this.W = (this.R < this.L ? this.L - this.R : this.R - this.L);
                this.H = (this.B < this.T ? this.T - this.B : this.B - this.T);

                this.Close();
            };
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
