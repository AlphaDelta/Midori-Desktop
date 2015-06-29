namespace MidoriDesktop
{
    partial class Error
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbSubtext = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(9, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(82, 25);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Oh no!";
            // 
            // lbSubtext
            // 
            this.lbSubtext.AutoSize = true;
            this.lbSubtext.Location = new System.Drawing.Point(12, 34);
            this.lbSubtext.Name = "lbSubtext";
            this.lbSubtext.Size = new System.Drawing.Size(130, 13);
            this.lbSubtext.TabIndex = 1;
            this.lbSubtext.Text = "Something evil happened!";
            // 
            // txtError
            // 
            this.txtError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtError.BackColor = System.Drawing.SystemColors.Window;
            this.txtError.Location = new System.Drawing.Point(12, 83);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(427, 88);
            this.txtError.TabIndex = 0;
            // 
            // Error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(451, 183);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.lbSubtext);
            this.Controls.Add(this.lbTitle);
            this.Name = "Error";
            this.ShowIcon = false;
            this.Text = "Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbTitle;
        public System.Windows.Forms.Label lbSubtext;
        public System.Windows.Forms.TextBox txtError;

    }
}