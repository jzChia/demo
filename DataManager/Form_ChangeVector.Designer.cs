namespace Resee.DataManager
{
    partial class Form_ChangeVector
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelContrast = new System.Windows.Forms.Label();
            this.labelOutout = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxInputRaster = new System.Windows.Forms.ComboBox();
            this.comboBoxContrastRaster = new System.Windows.Forms.ComboBox();
            this.comboBoxOutputRaster = new System.Windows.Forms.ComboBox();
            this.buttonInputRaster = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(18, 190);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(170, 18);
            this.checkBox1.TabIndex = 33;
            this.checkBox1.Text = "分析结果添加到地图窗口中";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 80);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(0, 14);
            this.labelControl1.TabIndex = 24;
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(15, 79);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(67, 14);
            this.labelInput.TabIndex = 36;
            this.labelInput.Text = "输入影像：";
            // 
            // labelContrast
            // 
            this.labelContrast.AutoSize = true;
            this.labelContrast.Location = new System.Drawing.Point(15, 114);
            this.labelContrast.Name = "labelContrast";
            this.labelContrast.Size = new System.Drawing.Size(67, 14);
            this.labelContrast.TabIndex = 37;
            this.labelContrast.Text = "对比影像：";
            this.labelContrast.Click += new System.EventHandler(this.label2_Click);
            // 
            // labelOutout
            // 
            this.labelOutout.AutoSize = true;
            this.labelOutout.Location = new System.Drawing.Point(15, 148);
            this.labelOutout.Name = "labelOutout";
            this.labelOutout.Size = new System.Drawing.Size(67, 14);
            this.labelOutout.TabIndex = 38;
            this.labelOutout.Text = "输出路径：";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(220, 223);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(82, 23);
            this.buttonOK.TabIndex = 39;
            this.buttonOK.Text = "确 定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(313, 223);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(82, 23);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.iButtonCancle_Click);
            // 
            // comboBoxInputRaster
            // 
            this.comboBoxInputRaster.FormattingEnabled = true;
            this.comboBoxInputRaster.Location = new System.Drawing.Point(80, 78);
            this.comboBoxInputRaster.Name = "comboBoxInputRaster";
            this.comboBoxInputRaster.Size = new System.Drawing.Size(276, 22);
            this.comboBoxInputRaster.TabIndex = 41;
            // 
            // comboBoxContrastRaster
            // 
            this.comboBoxContrastRaster.FormattingEnabled = true;
            this.comboBoxContrastRaster.Location = new System.Drawing.Point(80, 112);
            this.comboBoxContrastRaster.Name = "comboBoxContrastRaster";
            this.comboBoxContrastRaster.Size = new System.Drawing.Size(276, 22);
            this.comboBoxContrastRaster.TabIndex = 42;
            // 
            // comboBoxOutputRaster
            // 
            this.comboBoxOutputRaster.FormattingEnabled = true;
            this.comboBoxOutputRaster.Location = new System.Drawing.Point(80, 145);
            this.comboBoxOutputRaster.Name = "comboBoxOutputRaster";
            this.comboBoxOutputRaster.Size = new System.Drawing.Size(276, 22);
            this.comboBoxOutputRaster.TabIndex = 43;
            // 
            // buttonInputRaster
            // 
            this.buttonInputRaster.Location = new System.Drawing.Point(362, 80);
            this.buttonInputRaster.Name = "buttonInputRaster";
            this.buttonInputRaster.Size = new System.Drawing.Size(33, 21);
            this.buttonInputRaster.TabIndex = 44;
            this.buttonInputRaster.Text = "......";
            this.buttonInputRaster.UseVisualStyleBackColor = true;
            this.buttonInputRaster.Click += new System.EventHandler(this.buttonInputRaster_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 21);
            this.button1.TabIndex = 45;
            this.button1.Text = "......";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(362, 147);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 21);
            this.button2.TabIndex = 46;
            this.button2.Text = "......";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form_ChangeVector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 254);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonInputRaster);
            this.Controls.Add(this.comboBoxOutputRaster);
            this.Controls.Add(this.comboBoxContrastRaster);
            this.Controls.Add(this.comboBoxInputRaster);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelOutout);
            this.Controls.Add(this.labelContrast);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_ChangeVector";
            this.Text = "变化向量";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Label labelContrast;
        private System.Windows.Forms.Label labelOutout;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxInputRaster;
        private System.Windows.Forms.ComboBox comboBoxContrastRaster;
        private System.Windows.Forms.ComboBox comboBoxOutputRaster;
        private System.Windows.Forms.Button buttonInputRaster;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}