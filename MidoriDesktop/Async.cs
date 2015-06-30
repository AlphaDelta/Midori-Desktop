using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MidoriDesktop
{
    class Async
    {
        public static void StartAsync(Action a)
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += delegate { a(); };
            bg.RunWorkerCompleted += delegate { bg.Dispose(); };
            bg.RunWorkerAsync();
        }
    }

    public delegate void Action();
}
