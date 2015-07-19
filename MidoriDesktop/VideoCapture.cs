using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using Gma.UserActivityMonitor;

namespace MidoriDesktop
{
    class VideoCapture
    {
        static volatile bool running = true;
        public static void Initialize(Overlay o, Rectangle rect) {
            HookManager.KeyDown += OnKeyDown;
            Async.StartAsync((Action)delegate
            {
                Temp tmp = new Temp();
                Bitmap img = new Bitmap(rect.Width, rect.Height);
                Graphics g = Graphics.FromImage(img);
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

                long ticks = DateTime.Now.Ticks, next = 0, miliseconds = TimeSpan.TicksPerMillisecond * 33;
                while (running)
                {
                    Thread.Sleep(1);
                    ticks = DateTime.Now.Ticks;
                    if (ticks > next)
                    {
                        next = ticks + miliseconds;

                        g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                        g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);

                        WinAPI.CURSORINFO pci;
                        pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinAPI.CURSORINFO));

                        if (WinAPI.GetCursorInfo(out pci))
                        {
                            if (pci.flags == WinAPI.CURSOR_SHOWING)
                            {
                                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                                WinAPI.DrawIcon(g.GetHdc(), pci.ptScreenPos.x - rect.Left, pci.ptScreenPos.y - rect.Top, pci.hCursor);
                                g.ReleaseHdc();
                            }
                        }

                        tmp.AddImage(img);
                    }
                }

                g.Dispose();
                img.Dispose();

                o.Invoke((Action)delegate
                {
                    o.Close();
                    o.Dispose();
                });
            });
            o.Visible = false;
            o.ShowDialog(); //Overlay needs to be choked like the dirty whore it is for it to not crash
        }

        static void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode != System.Windows.Forms.Keys.Escape)
                return;

            running = false;
            e.SuppressKeyPress = true;
        }
    }
}
