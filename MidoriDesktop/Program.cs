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
                //Application.Run();
                ico.Text = "Midori";
                ico.Icon = Properties.Resources.icon;

                ContextMenu cm = new ContextMenu();

                MenuItem i1 = new MenuItem("Settings");
                i1.DefaultItem = true;
                i1.Click +=
                delegate {
                    throw new Exception("Test exception");
                };
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
