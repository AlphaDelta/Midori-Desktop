using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MidoriDesktop
{
    public partial class Error : Form
    {
        string[] subtitles =
        {
            "Something evil happened!",
            "Something just plain wrong happened!",
            "Oops I did it again!",
            "it was a axeident!",
            "HE DINDU NUFFIN!",
            "Fuck, I dropped it!",
            "Son of a WHORE!",
            "Fix it fix it fix it!",
            "Someone get a towel!",
            "Cleanup on line 34!",
            "Oh the humanity!",
            "HELP, POLICE!",
            "Fire in the disco!",
            "The jews are to blame for this!"
        };

        public Error(string error, bool rand = true)
        {
            InitializeComponent();

            txtError.Text = error;

            if (rand)
            {
                Random rnd = new Random();

                lbSubtext.Text = subtitles[rnd.Next(subtitles.Length)];
            }

            this.ShowDialog();
        }
    }
}
