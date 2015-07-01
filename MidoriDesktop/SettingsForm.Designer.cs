namespace MidoriDesktop
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPostClipboard = new System.Windows.Forms.RadioButton();
            this.rbPostServer = new System.Windows.Forms.RadioButton();
            this.rbPostFile = new System.Windows.Forms.RadioButton();
            this.tabHotkeys = new System.Windows.Forms.TabPage();
            this.txtHotkeyVideo = new System.Windows.Forms.TextBox();
            this.txtHotkeyImage = new System.Windows.Forms.TextBox();
            this.btnHotkeyVideo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHotkeyImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAPIKey = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabHotkeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabGeneral);
            this.tabs.Controls.Add(this.tabHotkeys);
            this.tabs.Location = new System.Drawing.Point(-1, -1);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(441, 230);
            this.tabs.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(433, 204);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtAPIKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rbPostClipboard);
            this.groupBox1.Controls.Add(this.rbPostServer);
            this.groupBox1.Controls.Add(this.rbPostFile);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 118);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Post-capture";
            // 
            // rbPostClipboard
            // 
            this.rbPostClipboard.AutoSize = true;
            this.rbPostClipboard.Location = new System.Drawing.Point(10, 19);
            this.rbPostClipboard.Name = "rbPostClipboard";
            this.rbPostClipboard.Size = new System.Drawing.Size(108, 17);
            this.rbPostClipboard.TabIndex = 1;
            this.rbPostClipboard.Text = "Save to clipboard";
            this.rbPostClipboard.UseVisualStyleBackColor = true;
            // 
            // rbPostServer
            // 
            this.rbPostServer.AutoSize = true;
            this.rbPostServer.Location = new System.Drawing.Point(10, 65);
            this.rbPostServer.Name = "rbPostServer";
            this.rbPostServer.Size = new System.Drawing.Size(103, 17);
            this.rbPostServer.TabIndex = 2;
            this.rbPostServer.Text = "Upload to server";
            this.rbPostServer.UseVisualStyleBackColor = true;
            // 
            // rbPostFile
            // 
            this.rbPostFile.AutoSize = true;
            this.rbPostFile.Location = new System.Drawing.Point(10, 42);
            this.rbPostFile.Name = "rbPostFile";
            this.rbPostFile.Size = new System.Drawing.Size(78, 17);
            this.rbPostFile.TabIndex = 1;
            this.rbPostFile.Text = "Save to file";
            this.rbPostFile.UseVisualStyleBackColor = true;
            // 
            // tabHotkeys
            // 
            this.tabHotkeys.Controls.Add(this.txtHotkeyVideo);
            this.tabHotkeys.Controls.Add(this.txtHotkeyImage);
            this.tabHotkeys.Controls.Add(this.btnHotkeyVideo);
            this.tabHotkeys.Controls.Add(this.label2);
            this.tabHotkeys.Controls.Add(this.btnHotkeyImage);
            this.tabHotkeys.Controls.Add(this.label1);
            this.tabHotkeys.Location = new System.Drawing.Point(4, 22);
            this.tabHotkeys.Name = "tabHotkeys";
            this.tabHotkeys.Padding = new System.Windows.Forms.Padding(3);
            this.tabHotkeys.Size = new System.Drawing.Size(433, 204);
            this.tabHotkeys.TabIndex = 1;
            this.tabHotkeys.Text = "Hotkeys";
            this.tabHotkeys.UseVisualStyleBackColor = true;
            // 
            // txtHotkeyVideo
            // 
            this.txtHotkeyVideo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotkeyVideo.BackColor = System.Drawing.SystemColors.Window;
            this.txtHotkeyVideo.Location = new System.Drawing.Point(93, 37);
            this.txtHotkeyVideo.Name = "txtHotkeyVideo";
            this.txtHotkeyVideo.ReadOnly = true;
            this.txtHotkeyVideo.Size = new System.Drawing.Size(298, 20);
            this.txtHotkeyVideo.TabIndex = 2;
            // 
            // txtHotkeyImage
            // 
            this.txtHotkeyImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotkeyImage.BackColor = System.Drawing.SystemColors.Window;
            this.txtHotkeyImage.Location = new System.Drawing.Point(93, 8);
            this.txtHotkeyImage.Name = "txtHotkeyImage";
            this.txtHotkeyImage.ReadOnly = true;
            this.txtHotkeyImage.Size = new System.Drawing.Size(298, 20);
            this.txtHotkeyImage.TabIndex = 2;
            // 
            // btnHotkeyVideo
            // 
            this.btnHotkeyVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHotkeyVideo.Location = new System.Drawing.Point(397, 35);
            this.btnHotkeyVideo.Name = "btnHotkeyVideo";
            this.btnHotkeyVideo.Size = new System.Drawing.Size(25, 23);
            this.btnHotkeyVideo.TabIndex = 1;
            this.btnHotkeyVideo.Text = "...";
            this.btnHotkeyVideo.UseVisualStyleBackColor = true;
            this.btnHotkeyVideo.Click += new System.EventHandler(this.btnHotkeyVideo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Capture video:";
            // 
            // btnHotkeyImage
            // 
            this.btnHotkeyImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHotkeyImage.Location = new System.Drawing.Point(397, 6);
            this.btnHotkeyImage.Name = "btnHotkeyImage";
            this.btnHotkeyImage.Size = new System.Drawing.Size(25, 23);
            this.btnHotkeyImage.TabIndex = 1;
            this.btnHotkeyImage.Text = "...";
            this.btnHotkeyImage.UseVisualStyleBackColor = true;
            this.btnHotkeyImage.Click += new System.EventHandler(this.btnHotkeyImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Capture image:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "API key:";
            // 
            // txtAPIKey
            // 
            this.txtAPIKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAPIKey.Location = new System.Drawing.Point(60, 88);
            this.txtAPIKey.Name = "txtAPIKey";
            this.txtAPIKey.Size = new System.Drawing.Size(347, 20);
            this.txtAPIKey.TabIndex = 4;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 227);
            this.Controls.Add(this.tabs);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Midori Settings";
            this.tabs.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabHotkeys.ResumeLayout(false);
            this.tabHotkeys.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabHotkeys;
        private System.Windows.Forms.TextBox txtHotkeyImage;
        private System.Windows.Forms.Button btnHotkeyImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHotkeyVideo;
        private System.Windows.Forms.Button btnHotkeyVideo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbPostServer;
        private System.Windows.Forms.RadioButton rbPostFile;
        private System.Windows.Forms.RadioButton rbPostClipboard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAPIKey;
        private System.Windows.Forms.Label label3;
    }
}