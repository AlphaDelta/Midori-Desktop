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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            txtHotkeyImage.Text = (Settings.HotkeyImageCtrl ? "Ctrl + " : "") + (Settings.HotkeyImageAlt ? "Alt + " : "") + (Settings.HotkeyImageShift ? "Shift + " : "") + ((Keys)Settings.HotkeyImage).ToString();
            txtHotkeyVideo.Text = (Settings.HotkeyVideoCtrl ? "Ctrl + " : "") + (Settings.HotkeyVideoAlt ? "Alt + " : "") + (Settings.HotkeyVideoShift ? "Shift + " : "") + ((Keys)Settings.HotkeyVideo).ToString();
            
            if(Settings.PostCapture == 1) rbPostFile.Checked = true;
            else if (Settings.PostCapture == 2) rbPostServer.Checked = true;
            else rbPostClipboard.Checked = true;

            EventHandler PostCapture = delegate
            {
                Settings.PostCapture = (rbPostServer.Checked ? 2 : (rbPostFile.Checked ? 1 : 0));

                Settings.Save();
            };

            rbPostClipboard.CheckedChanged += PostCapture;
            rbPostFile.CheckedChanged += PostCapture;
            rbPostServer.CheckedChanged += PostCapture;
        }

        bool setimg = false, setvid = false, ctrl = false, alt = false, shift = false,
        tmpimgctrl = false, tmpimgalt = false, tmpimgshift = false,
        tmpvidctrl = false, tmpvidalt = false, tmpvidshift = false;
        int tmpimg, tmpvid;
        void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey) ctrl = true;
            else if (e.KeyCode == Keys.Alt) alt = true;
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey) shift = true;
            else if (imagefocus)
            {
                setimg = true;
                txtHotkeyImage.Text = (ctrl ? "Ctrl + " : "") + (alt ? "Alt + " : "") + (shift ? "Shift + " : "") + e.KeyCode.ToString();
                tmpimgctrl = ctrl;
                tmpimgalt = alt;
                tmpimgshift = shift;
                tmpimg = e.KeyValue;
            }
            else if (videofocus)
            {
                setvid = true;
                txtHotkeyVideo.Text = (ctrl ? "Ctrl + " : "") + (alt ? "Alt + " : "") + (shift ? "Shift + " : "") + e.KeyCode.ToString();
                tmpvidctrl = ctrl;
                tmpvidalt = alt;
                tmpvidshift = shift;
                tmpvid = e.KeyValue;
            }
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
                setimg = false;
                setvid = false;
                HookManager.KeyDown += KeyDown;
                HookManager.KeyUp += KeyUp;

                this.ControlBox = false;
            }
            else
            {
                HookManager.KeyDown -= KeyDown;
                HookManager.KeyUp -= KeyUp;

                this.ControlBox = true;

                if (setimg)
                {
                    Settings.HotkeyImage = tmpimg;
                    Settings.HotkeyImageCtrl = tmpimgctrl;
                    Settings.HotkeyImageAlt = tmpimgalt;
                    Settings.HotkeyImageShift = tmpimgshift;
                }

                if (setvid)
                {
                    Settings.HotkeyVideo = tmpvid;
                    Settings.HotkeyVideoCtrl = tmpvidctrl;
                    Settings.HotkeyVideoAlt = tmpvidalt;
                    Settings.HotkeyVideoShift = tmpvidshift;
                }

                if(setimg || setvid) Settings.Save();
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
