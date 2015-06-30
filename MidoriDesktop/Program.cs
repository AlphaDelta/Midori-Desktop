using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace MidoriDesktop
{
    static class Program
    {
        public static volatile bool closing = false;
        [STAThread]
        static void Main()
        {
            NotifyIcon ico = new NotifyIcon();
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
            }
        }
    }
}
