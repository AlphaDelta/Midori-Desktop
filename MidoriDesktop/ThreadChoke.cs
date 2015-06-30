using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MidoriDesktop
{
    public partial class ThreadChoke : Form
    {
        public ThreadChoke()
        {
            InitializeComponent();

            this.Opacity = 0;
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.Width = 0;
            this.Height = 0;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }
    }
}
