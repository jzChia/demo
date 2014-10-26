namespace MainGUI
{
    partial class Form_Clip
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
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxGeo = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxExtent = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxOutputRaster = new System.Windows.Forms.TextBox();
            this.labelOutputRaster = new System.Windows.Forms.Label();
            this.labelOutput = new System.Windows.Forms.Label();
            this.comboBoxInput = new System.Windows.Forms.ComboBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(175, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "执行函数";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // checkBoxGeo
            // 
            this.checkBoxGeo.AutoSize = true;
            this.checkBoxGeo.Location = new System.Drawing.Point(12, 96);
            this.checkBoxGeo.Name = "checkBoxGeo";
            this.checkBoxGeo.Size = new System.Drawing.Size(120, 16);
            this.checkBoxGeo.TabIndex = 36;
            this.checkBoxGeo.Text = "按照几何形状裁剪";
            this.checkBoxGeo.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(13, 168);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(168, 16);
            this.checkBox1.TabIndex = 37;
            this.checkBox1.Text = "分析结果添加到地图窗口中";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBoxExtent
            // 
            this.comboBoxExtent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxExtent.FormattingEnabled = true;
            this.comboBoxExtent.Location = new System.Drawing.Point(11, 67);
            this.comboBoxExtent.Name = "comboBoxExtent";
            this.comboBoxExtent.Size = new System.Drawing.Size(401, 20);
            this.comboBoxExtent.TabIndex = 35;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(335, 186);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 34;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(254, 186);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 33;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // textBoxOutputRaster
            // 
            this.textBoxOutputRaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputRaster.Location = new System.Drawing.Point(11, 139);
            this.textBoxOutputRaster.Name = "textBoxOutputRaster";
            this.textBoxOutputRaster.Size = new System.Drawing.Size(401, 21);
            this.textBoxOutputRaster.TabIndex = 32;
            // 
            // labelOutputRaster
            // 
            this.labelOutputRaster.AutoSize = true;
            this.labelOutputRaster.Location = new System.Drawing.Point(11, 124);
            this.labelOutputRaster.Name = "labelOutputRaster";
            this.labelOutputRaster.Size = new System.Drawing.Size(89, 12);
            this.labelOutputRaster.TabIndex = 31;
            this.labelOutputRaster.Text = "输出栅格数据集";
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(11, 52);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(53, 12);
            this.labelOutput.TabIndex = 30;
            this.labelOutput.Text = "裁剪范围";
            // 
            // comboBoxInput
            // 
            this.comboBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxInput.FormattingEnabled = true;
            this.comboBoxInput.Location = new System.Drawing.Point(11, 29);
            this.comboBoxInput.Name = "comboBoxInput";
            this.comboBoxInput.Size = new System.Drawing.Size(401, 20);
            this.comboBoxInput.TabIndex = 29;
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(11, 14);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(53, 12);
            this.labelInput.TabIndex = 28;
            this.labelInput.Text = "输入栅格";
            // 
            // Form_Clip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 222);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxGeo);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBoxExtent);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxOutputRaster);
            this.Controls.Add(this.labelOutputRaster);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.comboBoxInput);
            this.Controls.Add(this.labelInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Clip";
            this.Text = " 裁剪";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Clip_FormClosing);
            this.Load += new System.EventHandler(this.Form_Clip_Load);
            this.Move += new System.EventHandler(this.Form_Clip_Move);
            this.Resize += new System.EventHandler(this.Form_Clip_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxGeo;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBoxExtent;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxOutputRaster;
        private System.Windows.Forms.Label labelOutputRaster;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.ComboBox comboBoxInput;
        private System.Windows.Forms.Label labelInput;

    }
}