namespace MainGUI
{
    partial class Form_Moasic
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxInputRaster = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxBlend = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPatton = new System.Windows.Forms.ComboBox();
            this.labelPatton = new System.Windows.Forms.Label();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.labelOutput = new System.Windows.Forms.Label();
            this.labelInput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 59);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(407, 124);
            this.listBox1.TabIndex = 50;
            // 
            // buttonDown
            // 
            this.buttonDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDown.Location = new System.Drawing.Point(425, 160);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(23, 23);
            this.buttonDown.TabIndex = 47;
            this.buttonDown.Text = "↓";
            this.buttonDown.UseVisualStyleBackColor = true;
            // 
            // buttonDel
            // 
            this.buttonDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDel.Location = new System.Drawing.Point(425, 109);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(23, 23);
            this.buttonDel.TabIndex = 48;
            this.buttonDel.Text = "×";
            this.buttonDel.UseVisualStyleBackColor = true;
            // 
            // buttonUp
            // 
            this.buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUp.Location = new System.Drawing.Point(425, 59);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(23, 23);
            this.buttonUp.TabIndex = 49;
            this.buttonUp.Text = "↑";
            this.buttonUp.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(13, 296);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(144, 16);
            this.checkBox1.TabIndex = 46;
            this.checkBox1.Text = "将输出结果添加至图层";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBoxInputRaster
            // 
            this.comboBoxInputRaster.FormattingEnabled = true;
            this.comboBoxInputRaster.Location = new System.Drawing.Point(12, 32);
            this.comboBoxInputRaster.Name = "comboBoxInputRaster";
            this.comboBoxInputRaster.Size = new System.Drawing.Size(436, 20);
            this.comboBoxInputRaster.TabIndex = 45;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(373, 292);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 44;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(292, 292);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 43;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // textBoxBlend
            // 
            this.textBoxBlend.Location = new System.Drawing.Point(229, 250);
            this.textBoxBlend.Name = "textBoxBlend";
            this.textBoxBlend.Size = new System.Drawing.Size(219, 21);
            this.textBoxBlend.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "羽化宽度";
            // 
            // comboBoxPatton
            // 
            this.comboBoxPatton.FormattingEnabled = true;
            this.comboBoxPatton.Items.AddRange(new object[] {
            "8_BIT_UNSIGNED",
            "8_BIT_SIGNED",
            "16_BIT_UNSIGNED",
            "16_BIT_SIGNED",
            "32_BIT_UNSIGNED",
            "32_BIT_SIGNED"});
            this.comboBoxPatton.Location = new System.Drawing.Point(12, 250);
            this.comboBoxPatton.Name = "comboBoxPatton";
            this.comboBoxPatton.Size = new System.Drawing.Size(188, 20);
            this.comboBoxPatton.TabIndex = 40;
            // 
            // labelPatton
            // 
            this.labelPatton.AutoSize = true;
            this.labelPatton.Location = new System.Drawing.Point(12, 235);
            this.labelPatton.Name = "labelPatton";
            this.labelPatton.Size = new System.Drawing.Size(53, 12);
            this.labelPatton.TabIndex = 39;
            this.labelPatton.Text = "像素类型";
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(12, 211);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(436, 21);
            this.textBoxOutput.TabIndex = 38;
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(11, 196);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(89, 12);
            this.labelOutput.TabIndex = 37;
            this.labelOutput.Text = "输出数据集名称";
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(12, 17);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(53, 12);
            this.labelInput.TabIndex = 36;
            this.labelInput.Text = "输入栅格";
            // 
            // Form_Moasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 333);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonDown);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBoxInputRaster);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxBlend);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPatton);
            this.Controls.Add(this.labelPatton);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.labelInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form_Moasic";
            this.Text = "镶嵌至新栅格";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Moasic_FormClosing);
            this.Load += new System.EventHandler(this.FormMoasic_Load);
            this.Move += new System.EventHandler(this.Form_Moasic_Move);
            this.Resize += new System.EventHandler(this.Form_Moasic_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBoxInputRaster;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxBlend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPatton;
        private System.Windows.Forms.Label labelPatton;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Label labelInput;

    }
}