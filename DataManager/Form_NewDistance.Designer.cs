namespace Resee.DataManager
{
    partial class Form_NewDistance
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBoxAdd = new System.Windows.Forms.CheckBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelContrast = new System.Windows.Forms.Label();
            this.labelOutput = new System.Windows.Forms.Label();
            this.comboBoxInputRaster = new System.Windows.Forms.ComboBox();
            this.comboBoxContrastRaster = new System.Windows.Forms.ComboBox();
            this.comboBoxOutputRaster = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonInputRaster = new System.Windows.Forms.Button();
            this.buttonContrast = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Resee.DataManager.Properties.Resources.QQ截图20130309124218;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(419, 66);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // checkBoxAdd
            // 
            this.checkBoxAdd.AutoSize = true;
            this.checkBoxAdd.Checked = true;
            this.checkBoxAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAdd.Location = new System.Drawing.Point(21, 192);
            this.checkBoxAdd.Name = "checkBoxAdd";
            this.checkBoxAdd.Size = new System.Drawing.Size(170, 18);
            this.checkBoxAdd.TabIndex = 21;
            this.checkBoxAdd.Text = "分析结果添加到地图窗口中";
            this.checkBoxAdd.UseVisualStyleBackColor = true;
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(18, 82);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(67, 14);
            this.labelInput.TabIndex = 24;
            this.labelInput.Text = "输入影像：";
            // 
            // labelContrast
            // 
            this.labelContrast.AutoSize = true;
            this.labelContrast.Location = new System.Drawing.Point(18, 116);
            this.labelContrast.Name = "labelContrast";
            this.labelContrast.Size = new System.Drawing.Size(67, 14);
            this.labelContrast.TabIndex = 25;
            this.labelContrast.Text = "对比影像：";
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(18, 150);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(67, 14);
            this.labelOutput.TabIndex = 26;
            this.labelOutput.Text = "输出路径：";
            // 
            // comboBoxInputRaster
            // 
            this.comboBoxInputRaster.FormattingEnabled = true;
            this.comboBoxInputRaster.Location = new System.Drawing.Point(78, 80);
            this.comboBoxInputRaster.Name = "comboBoxInputRaster";
            this.comboBoxInputRaster.Size = new System.Drawing.Size(271, 22);
            this.comboBoxInputRaster.TabIndex = 27;
            // 
            // comboBoxContrastRaster
            // 
            this.comboBoxContrastRaster.FormattingEnabled = true;
            this.comboBoxContrastRaster.Location = new System.Drawing.Point(78, 114);
            this.comboBoxContrastRaster.Name = "comboBoxContrastRaster";
            this.comboBoxContrastRaster.Size = new System.Drawing.Size(271, 22);
            this.comboBoxContrastRaster.TabIndex = 28;
            // 
            // comboBoxOutputRaster
            // 
            this.comboBoxOutputRaster.FormattingEnabled = true;
            this.comboBoxOutputRaster.Location = new System.Drawing.Point(78, 148);
            this.comboBoxOutputRaster.Name = "comboBoxOutputRaster";
            this.comboBoxOutputRaster.Size = new System.Drawing.Size(271, 22);
            this.comboBoxOutputRaster.TabIndex = 29;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(216, 225);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(82, 23);
            this.buttonOK.TabIndex = 30;
            this.buttonOK.Text = "确 定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(313, 225);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(82, 23);
            this.buttonCancel.TabIndex = 31;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.iButtonCancle_Click);
            // 
            // buttonInputRaster
            // 
            this.buttonInputRaster.Location = new System.Drawing.Point(366, 81);
            this.buttonInputRaster.Name = "buttonInputRaster";
            this.buttonInputRaster.Size = new System.Drawing.Size(29, 21);
            this.buttonInputRaster.TabIndex = 32;
            this.buttonInputRaster.Text = "......";
            this.buttonInputRaster.UseVisualStyleBackColor = true;
            this.buttonInputRaster.Click += new System.EventHandler(this.buttonInputRaster_Click);
            // 
            // buttonContrast
            // 
            this.buttonContrast.Location = new System.Drawing.Point(366, 115);
            this.buttonContrast.Name = "buttonContrast";
            this.buttonContrast.Size = new System.Drawing.Size(29, 21);
            this.buttonContrast.TabIndex = 33;
            this.buttonContrast.Text = "......";
            this.buttonContrast.UseVisualStyleBackColor = true;
            this.buttonContrast.Click += new System.EventHandler(this.buttonContrast_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(366, 149);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(29, 21);
            this.buttonOutput.TabIndex = 34;
            this.buttonOutput.Text = "......";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // Form_NewDistance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 260);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonContrast);
            this.Controls.Add(this.buttonInputRaster);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxOutputRaster);
            this.Controls.Add(this.comboBoxContrastRaster);
            this.Controls.Add(this.comboBoxInputRaster);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.labelContrast);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.checkBoxAdd);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_NewDistance";
            this.Text = "欧式距离";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBoxAdd;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Label labelContrast;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.ComboBox comboBoxInputRaster;
        private System.Windows.Forms.ComboBox comboBoxContrastRaster;
        private System.Windows.Forms.ComboBox comboBoxOutputRaster;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonInputRaster;
        private System.Windows.Forms.Button buttonContrast;
        private System.Windows.Forms.Button buttonOutput;
    }
}