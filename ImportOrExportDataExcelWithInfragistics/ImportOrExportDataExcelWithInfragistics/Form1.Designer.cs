namespace ImportOrExportDataExcelWithInfragistics
{
    partial class Form1
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
            this.btn_CreateExcel = new System.Windows.Forms.Button();
            this.btn_ReadFromExcel = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btn_CreateExcel
            // 
            this.btn_CreateExcel.Location = new System.Drawing.Point(73, 35);
            this.btn_CreateExcel.Name = "btn_CreateExcel";
            this.btn_CreateExcel.Size = new System.Drawing.Size(167, 23);
            this.btn_CreateExcel.TabIndex = 0;
            this.btn_CreateExcel.Text = "Excel Oluştur";
            this.btn_CreateExcel.UseVisualStyleBackColor = true;
            this.btn_CreateExcel.Click += new System.EventHandler(this.btn_CreateExcel_Click);
            // 
            // btn_ReadFromExcel
            // 
            this.btn_ReadFromExcel.Location = new System.Drawing.Point(73, 93);
            this.btn_ReadFromExcel.Name = "btn_ReadFromExcel";
            this.btn_ReadFromExcel.Size = new System.Drawing.Size(167, 23);
            this.btn_ReadFromExcel.TabIndex = 1;
            this.btn_ReadFromExcel.Text = "Excelden Oku";
            this.btn_ReadFromExcel.UseVisualStyleBackColor = true;
            this.btn_ReadFromExcel.Click += new System.EventHandler(this.btn_ReadFromExcel_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(73, 122);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(167, 148);
            this.listBox1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 271);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_ReadFromExcel);
            this.Controls.Add(this.btn_CreateExcel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_CreateExcel;
        private System.Windows.Forms.Button btn_ReadFromExcel;
        private System.Windows.Forms.ListBox listBox1;
    }
}

