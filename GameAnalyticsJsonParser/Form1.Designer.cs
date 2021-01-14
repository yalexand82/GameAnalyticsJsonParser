namespace GameAnalyticsJsonParser
{
    partial class frmMain
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
            this.btnSourceFolder = new System.Windows.Forms.Button();
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.rBtnCsv = new System.Windows.Forms.RadioButton();
            this.rBtnExcel = new System.Windows.Forms.RadioButton();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnBuildOutput = new System.Windows.Forms.Button();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnSourceFolder
            // 
            this.btnSourceFolder.Location = new System.Drawing.Point(10, 11);
            this.btnSourceFolder.Name = "btnSourceFolder";
            this.btnSourceFolder.Size = new System.Drawing.Size(118, 23);
            this.btnSourceFolder.TabIndex = 0;
            this.btnSourceFolder.Text = "Select Source Folder";
            this.btnSourceFolder.UseVisualStyleBackColor = true;
            this.btnSourceFolder.Click += new System.EventHandler(this.btnSourceFolder_Click);
            // 
            // txtSourceFolder
            // 
            this.txtSourceFolder.Location = new System.Drawing.Point(134, 13);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.Size = new System.Drawing.Size(716, 20);
            this.txtSourceFolder.TabIndex = 1;
            this.txtSourceFolder.TextChanged += new System.EventHandler(this.txtSourceFolder_TextChanged);
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(12, 40);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(116, 23);
            this.btnOutputFolder.TabIndex = 2;
            this.btnOutputFolder.Text = "Select Output Folder";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(134, 43);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(716, 20);
            this.txtOutputFolder.TabIndex = 3;
            this.txtOutputFolder.TextChanged += new System.EventHandler(this.txtOutputFolder_TextChanged);
            // 
            // rBtnCsv
            // 
            this.rBtnCsv.AutoSize = true;
            this.rBtnCsv.Checked = true;
            this.rBtnCsv.Location = new System.Drawing.Point(42, 105);
            this.rBtnCsv.Name = "rBtnCsv";
            this.rBtnCsv.Size = new System.Drawing.Size(42, 17);
            this.rBtnCsv.TabIndex = 4;
            this.rBtnCsv.TabStop = true;
            this.rBtnCsv.Text = "csv";
            this.rBtnCsv.UseVisualStyleBackColor = true;
            // 
            // rBtnExcel
            // 
            this.rBtnExcel.AutoSize = true;
            this.rBtnExcel.Location = new System.Drawing.Point(42, 128);
            this.rBtnExcel.Name = "rBtnExcel";
            this.rBtnExcel.Size = new System.Drawing.Size(74, 17);
            this.rBtnExcel.TabIndex = 5;
            this.rBtnExcel.Text = "Excel .xlsx";
            this.rBtnExcel.UseVisualStyleBackColor = true;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(18, 89);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(110, 13);
            this.lblOutput.TabIndex = 6;
            this.lblOutput.Text = "Select Output Format:";
            // 
            // btnBuildOutput
            // 
            this.btnBuildOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuildOutput.Location = new System.Drawing.Point(363, 89);
            this.btnBuildOutput.Name = "btnBuildOutput";
            this.btnBuildOutput.Size = new System.Drawing.Size(133, 33);
            this.btnBuildOutput.TabIndex = 7;
            this.btnBuildOutput.Text = "Build Output";
            this.btnBuildOutput.UseVisualStyleBackColor = true;
            this.btnBuildOutput.Click += new System.EventHandler(this.btnBuildOutput_Click);
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.HorizontalScrollbar = true;
            this.lstStatus.Location = new System.Drawing.Point(6, 154);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.ScrollAlwaysVisible = true;
            this.lstStatus.Size = new System.Drawing.Size(843, 342);
            this.lstStatus.TabIndex = 8;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 513);
            this.Controls.Add(this.btnBuildOutput);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.rBtnExcel);
            this.Controls.Add(this.rBtnCsv);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.txtSourceFolder);
            this.Controls.Add(this.btnSourceFolder);
            this.Name = "frmMain";
            this.Text = "Game Analytics Json Parser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSourceFolder;
        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.RadioButton rBtnCsv;
        private System.Windows.Forms.RadioButton rBtnExcel;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.Button btnBuildOutput;
    }
}

