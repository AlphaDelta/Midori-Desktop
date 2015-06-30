using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Gma.UserActivityMonitor;

namespace MidoriDesktop
{
    static class Program
    {
        static NotifyIcon ico;
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
                i2.Click += delegate { closing = true; };
                cm.MenuItems.Add(i2);

                ico.ContextMenu = cm;
                ico.Visible = true;

                HookManager.KeyDown += KeyDown;
                HookManager.KeyUp += KeyUp;
                ico.ShowBalloonTip(3, "Info", "Midori's running now!", ToolTipIcon.Info);

                while (!closing) Application.DoEvents();
            }
            catch (Exception e)
            {
                Error error = new Error(e.Message);
            }
            finally
            {
                ico.Visible = false;
                ico.Dispose();

                HookManager.KeyDown -= KeyDown;
                HookManager.KeyUp -= KeyUp;
            }
        }

        static bool ctrl = false, alt = false, shift = false;
        static void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey) ctrl = true;
            else if (e.KeyCode == Keys.Alt) alt = true;
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey) shift = true;
            else if (e.KeyValue == Settings.HotkeyImage && ctrl == Settings.HotkeyImageCtrl && alt == Settings.HotkeyImageAlt && shift == Settings.HotkeyImageShift)
            {
                ClickCapture cap = new ClickCapture();
                cap.Show();
            }
            else if (e.KeyValue == Settings.HotkeyVideo && ctrl == Settings.HotkeyImageCtrl && alt == Settings.HotkeyImageAlt && shift == Settings.HotkeyImageShift)
            {
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
