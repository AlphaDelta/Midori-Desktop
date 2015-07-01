using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Gma.UserActivityMonitor;

namespace MidoriDesktop
{
    static class Program
    {
        static NotifyIcon ico;
        static SynchronizationContext STAThread;
        public static volatile bool closing = false;
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Settings.Initialize();

                SettingsForm settings = null;
                EventHandler evt = new EventHandler(delegate(object sender, EventArgs e)
                {
                    if (settings != null && !settings.IsDisposed)
                    {
                        if (settings.WindowState == FormWindowState.Minimized) settings.WindowState = FormWindowState.Normal;
                        settings.BringToFront();
                        settings.Focus();
                    }
                    else
                    {
                        settings = new SettingsForm();
                        settings.Show();
                    }
                });

                ico = new NotifyIcon();
                ico.Text = "Midori";
                ico.Icon = Properties.Resources.icon;
                ico.DoubleClick += evt;

                ContextMenu cm = new ContextMenu();

                MenuItem i1 = new MenuItem("Settings");
                i1.DefaultItem = true;
                i1.Click += evt;
                cm.MenuItems.Add(i1);

                cm.MenuItems.Add(new MenuItem("-"));

                MenuItem i2 = new MenuItem("Exit");
                i2.Click += delegate { Application.Exit(); };
                cm.MenuItems.Add(i2);

                ico.ContextMenu = cm;
                ico.Visible = true;

                HookManager.KeyDown += KeyDown;
                HookManager.KeyUp += KeyUp;
                ico.ShowBalloonTip(3, "Info", "Midori's running now!", ToolTipIcon.Info);

                ThreadChoke choke = new ThreadChoke();
                STAThread = SynchronizationContext.Current;
                choke.Dispose();

                Application.Run();
                /*while (!closing)
                {
                    Application.DoEvents();

                    if (intermediateimgflag)
                    {
                        Clipboard.SetImage(intermediateimg);
                        intermediateimg.Dispose();
                        intermediateimgflag = false;
                    }

                    Thread.Sleep(50);
                }*/
            }
            catch (Exception e)
            {
                Error error = new Error(e.ToString());
            }
            finally
            {
                if (ico != null)
                {
                    ico.Visible = false;
                    ico.Dispose();
                }

                HookManager.KeyDown -= KeyDown;
                HookManager.KeyUp -= KeyUp;
            }
        }

        static bool ctrl = false, alt = false, shift = false, incapture = false;

        static ClickCapture tmpcap = null;
        static void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey) ctrl = true;
            else if (e.KeyCode == Keys.Alt) alt = true;
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey) shift = true;
            else if (e.KeyValue == Settings.HotkeyImage && ctrl == Settings.HotkeyImageCtrl && alt == Settings.HotkeyImageAlt && shift == Settings.HotkeyImageShift)
            {
                e.SuppressKeyPress = true;
                if (incapture)
                {
                    tmpcap.Invoke((Action)delegate
                    {
                        WinAPI.SetWindowPos(tmpcap.Handle, new IntPtr(-1), 0, 0, tmpcap.Width, tmpcap.Height, 0);
                    });
                    return;
                }
                Async.StartAsync(delegate
                {
                    incapture = true;
                    Overlay overlay = new Overlay();
                    ClickCapture cap = new ClickCapture(overlay);
                    tmpcap = cap;
                    cap.ShowDialog();
                    cap.Dispose();
                    overlay.Close();
                    overlay.Dispose();

                    if (cap.W > 0 && cap.H > 0)
                    {
                        //MessageBox.Show(cap.X + ":" + cap.Y + ":" + cap.W + "x" + cap.H);
                        Bitmap img = new Bitmap(cap.W, cap.H);
                        using (Graphics g = Graphics.FromImage(img)) g.CopyFromScreen(cap.X, cap.Y, 0, 0, img.Size, CopyPixelOperation.SourceCopy);

                        if (Settings.PostCapture == 0)
                            STAThread.Send(delegate { Clipboard.SetImage(img); }, null);
                        else if (Settings.PostCapture == 1)
                        {
                            STAThread.Send(delegate
                            {
                                SaveFileDialog sfd = new SaveFileDialog();
                                sfd.Filter = "PNG Image (*.png)|*.png";
                                if (sfd.ShowDialog() == DialogResult.OK)
                                    img.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            }, null);
                        }
                        else if (Settings.PostCapture == 2)
                        {
                            try
                            {
                                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://midori.moe/upload.php?apikey=" + Settings.APIKey);

                                req.Method = WebRequestMethods.Http.Post;

                                req.UserAgent = "Midori-Desktop v1.0";

                                req.Expect = "";
                                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                                req.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
                                req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                                req.Headers.Add(HttpRequestHeader.Pragma, "no-cache");
                                req.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");

                                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", System.Globalization.NumberFormatInfo.InvariantInfo);
                                req.ContentType = "multipart/form-data; boundary=" + boundary;
                                boundary = "--" + boundary;

                                Stream str = req.GetRequestStream();

                                StringBuilder file = new StringBuilder();

                                byte[] buffer = Encoding.ASCII.GetBytes(boundary + "\r\nContent-Disposition: form-data; name=\"files[]\"; filename=\"midoridesktop.png\"\r\nContent-Type: image/png\r\n\r\n");
                                str.Write(buffer, 0, buffer.Length);

                                img.Save(str, System.Drawing.Imaging.ImageFormat.Png);

                                buffer = Encoding.ASCII.GetBytes("\r\n" + boundary + "--\r\n");
                                str.Write(buffer, 0, buffer.Length);
                                str.Close();
                                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                                string response;
                                using (Stream s = resp.GetResponseStream())
                                using (GZipStream inflate = new GZipStream(s, CompressionMode.Decompress))
                                using (StreamReader reader = new StreamReader(inflate))
                                    response = reader.ReadToEnd();

                                resp.Close();
                                str.Dispose();

                                if (response.EndsWith(".png"))
                                {
                                    STAThread.Send(delegate {
                                        Clipboard.SetText("https://i.midori.moe/" + response);
                                    ico.ShowBalloonTip(3, "Info", "Link copied to clipboard", ToolTipIcon.Info);
                                    }, null);
                                }
                            }
                            catch (Exception ex)
                            {
                                Error error = new Error(ex.ToString());
                            }
                        }
                        img.Dispose();
                    }

                    incapture = false;
                });
            }
            else if (e.KeyValue == Settings.HotkeyVideo && ctrl == Settings.HotkeyImageCtrl && alt == Settings.HotkeyImageAlt && shift == Settings.HotkeyImageShift)
            {
                if (incapture) return;
                ico.ShowBalloonTip(3, "Info", "Video capture invoked!", ToolTipIcon.Info);
            }
        }
        static void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey) ctrl = false;
            else if (e.KeyCode == Keys.Alt) alt = false;
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey) shift = false;
        }
    }
}
