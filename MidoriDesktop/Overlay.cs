using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MidoriDesktop
{
    public class Overlay : Form
    {
        public Overlay()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this.TopMost = true;
            this.ShowInTaskbar = false;

            this.Location = new Point(0, 0);

            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public int L, T, R, B, W, H;
        Brush fill = new SolidBrush(Color.FromArgb(0x66, 0x00, 0x00, 0x00));
        Brush filltransparent = new SolidBrush(Color.Transparent);
        Font font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
        Color text = Color.FromArgb(0xFF, 0x33, 0x33, 0x33);
        Brush btext = new SolidBrush(Color.FromArgb(0xFF, 0x33, 0x33, 0x33));
        Pen border = new Pen(Color.FromArgb(0xFF, 0x33, 0x33, 0x33));
        Bitmap bitmap = null;
        Graphics g = null;
        int oldL = 0, oldR = 0, oldT = 0, oldB = 0;
        public void SetSize(int Width, int Height)
        {
            bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(bitmap);
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        }
        new public void Update()
        {
            g.FillRectangle(fill, oldL, oldB, 80, 20);
            if (oldL == 0 && oldR == 0 && oldT == 0 && oldB == 0)
            {
                g.FillRectangle(fill, 0, 0, W, H);
                g.FillRectangle(filltransparent, L, T, R - L, B - T);
            }
            else
            {
                if (oldL > L) g.FillRectangle(filltransparent, L, T, (oldL - L) + 1, (B - T) + 1);
                else if (oldL < L) g.FillRectangle(fill, oldL, T, (L - oldL) + 1, (B - T) + 1);
                if (oldR > R) g.FillRectangle(fill, R, T, (oldR - R) + 1, (B - T) + 1);
                else if (oldR < R) g.FillRectangle(filltransparent, oldR, T, (R - oldR) + 1, (B - T) + 1);

                if (oldT > T) g.FillRectangle(filltransparent, L, T, R - L, (oldT - T) + 1);
                else if (oldT < T) g.FillRectangle(fill, oldL, oldT, (oldR - oldL) + 1, (T - oldT) + 1);
                if (oldB > B) g.FillRectangle(fill, oldL, B, (oldR - oldL) + 1, (oldB - B) + 1);
                else if (oldB < B) g.FillRectangle(filltransparent, L, oldB, R - L, (B - oldB) + 1);
            }
            g.DrawRectangle(border, L, T, R - L, B - T);
            
            string txt = (R - L) + "x" + (B - T);
            TextRenderer.DrawText(g, txt, font, new Point(L - 1, B + 5), Color.Black);
            TextRenderer.DrawText(g, txt, font, new Point(L + 1, B + 5), Color.Black);
            TextRenderer.DrawText(g, txt, font, new Point(L, B + 6), Color.Black);
            TextRenderer.DrawText(g, txt, font, new Point(L, B + 4), Color.Black);
            TextRenderer.DrawText(g, txt, font, new Point(L, B + 5), Color.White);

            oldL = L;
            oldR = R;
            oldT = T;
            oldB = B;

            IntPtr screenDc = WinAPI.GetDC(IntPtr.Zero);
            IntPtr memDc = WinAPI.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = WinAPI.SelectObject(memDc, hBitmap);

                WinAPI.Size size = new WinAPI.Size(bitmap.Width, bitmap.Height);
                WinAPI.Point pointSource = new WinAPI.Point(0, 0);
                WinAPI.Point topPos = new WinAPI.Point(Left, Top);
                WinAPI.BLENDFUNCTION blend = new WinAPI.BLENDFUNCTION();
                blend.BlendOp = WinAPI.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = 0xFF;
                blend.AlphaFormat = WinAPI.AC_SRC_ALPHA;

                WinAPI.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, WinAPI.ULW_ALPHA);
            }
            finally
            {
                WinAPI.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    WinAPI.SelectObject(memDc, oldBitmap);
                    WinAPI.DeleteObject(hBitmap);
                }
                WinAPI.DeleteDC(memDc);
            }
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WinAPI.WS_EX_LAYERED | WinAPI.WS_EX_TRANSPARENT;
                return cp;
            }
        }
    }
}
