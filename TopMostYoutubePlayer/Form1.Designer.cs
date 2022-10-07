
namespace TopMostYoutubePlayer
{
    partial class frmPlayer
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
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.wbPlayerBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUrl.Location = new System.Drawing.Point(12, 30);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(117, 20);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "YoutubeLink:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(135, 27);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(511, 22);
            this.txtUrl.TabIndex = 1;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(660, 26);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.Text = "Başlat";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // wbPlayerBrowser
            // 
            this.wbPlayerBrowser.Location = new System.Drawing.Point(12, 55);
            this.wbPlayerBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbPlayerBrowser.Name = "wbPlayerBrowser";
            this.wbPlayerBrowser.Size = new System.Drawing.Size(720, 365);
            this.wbPlayerBrowser.TabIndex = 3;
            // 
            // frmPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 429);
            this.Controls.Add(this.wbPlayerBrowser);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lblUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPlayer";
            this.Text = "TopMostYoutubePlayer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.WebBrowser wbPlayerBrowser;
    }
}

