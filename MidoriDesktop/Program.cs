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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run();
            NotifyIcon ico = new NotifyIcon();
            ico.Text = "Midori";
            ico.Icon = Properties.Resources.icon;

            ContextMenu cm = new ContextMenu();

            MenuItem i1 = new MenuItem("Exit...");
            i1.DefaultItem = true;
            i1.Click += delegate { closing = true; };
            cm.MenuItems.Add(i1);

            ico.ContextMenu = cm;

            ico.Visible = true;

            ico.ShowBalloonTip(3, "Info", "Midori's running now!", ToolTipIcon.Info);

            while (!closing) Application.DoEvents();

            ico.Visible = false;
            ico.Dispose();
        }
    }
}
