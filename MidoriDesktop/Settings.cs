using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gma.UserActivityMonitor;

namespace MidoriDesktop
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        bool ctrl = false, alt = false, shift = false;
        void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey) ctrl = true;
            else if (e.KeyCode == Keys.Alt) alt = true;
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey) shift = true;
            else if (imagefocus) txtHotkeyImage.Text = (ctrl ? "Ctrl + " : "") + (alt ? "Alt + " : "") + (shift ? "Shift + " : "") + e.KeyCode.ToString();
            else if (videofocus) txtHotkeyVideo.Text = (ctrl ? "Ctrl + " : "") + (alt ? "Alt + " : "") + (shift ? "Shift + " : "") + e.KeyCode.ToString();
        }

        void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey) ctrl = false;
            else if (e.KeyCode == Keys.Alt) alt = false;
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey) shift = false;
        }

        void HookKeyboard(bool flag)
        {
            if (flag)
            {
                HookManager.KeyDown += KeyDown;
                HookManager.KeyUp += KeyUp;

                this.ControlBox = false;
            }
            else
            {
                HookManager.KeyDown -= KeyDown;
                HookManager.KeyUp -= KeyUp;

                this.ControlBox = true;
            }
        }

        bool imagefocus = false;
        private void btnHotkeyImage_Click(object sender, EventArgs e)
        {
            btnHotkeyVideo.Enabled = imagefocus;

            imagefocus = !imagefocus;

            HookKeyboard(imagefocus);
        }

        bool videofocus = false;
        private void btnHotkeyVideo_Click(object sender, EventArgs e)
        {
            btnHotkeyImage.Enabled = videofocus;

            videofocus = !videofocus;

            HookKeyboard(videofocus);
        }
    }
}
