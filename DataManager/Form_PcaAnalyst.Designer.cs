namespace Resee.DataManager
{
    partial class Form_PCAAnalyst
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
            this.comboBoxInputRaster = new DevExpress.XtraEditors.ComboBoxEdit();
            this.checkBoxAdd = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMethod = new System.Windows.Forms.ComboBox();
            this.comboBoxContrastRaster = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttoncancel = new System.Windows.Forms.Button();
            this.buttonInputRaster = new System.Windows.Forms.Button();
            this.buttonInputcontrastRaster = new System.Windows.Forms.Button();
            this.buttonOutputRaster = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxOutputRaster = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxInputRaster.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxInputRaster
            // 
            this.comboBoxInputRaster.EditValue = "";
            this.comboBoxInputRaster.Location = new System.Drawing.Point(81, 109);
            this.comboBoxInputRaster.Name = "comboBoxInputRaster";
            this.comboBoxInputRaster.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxInputRaster.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxInputRaster.Size = new System.Drawing.Size(276, 21);
            this.comboBoxInputRaster.TabIndex = 3;
            this.comboBoxInputRaster.SelectedIndexChanged += new System.EventHandler(this.comboBoxInputRaster_SelectedIndexChanged);
            // 
            // checkBoxAdd
            // 
            this.checkBoxAdd.AutoSize = true;
            this.checkBoxAdd.Checked = true;
            this.checkBoxAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAdd.Location = new System.Drawing.Point(17, 211);
            this.checkBoxAdd.Name = "checkBoxAdd";
            this.checkBoxAdd.Size = new System.Drawing.Size(170, 18);
            this.checkBoxAdd.TabIndex = 9;
            this.checkBoxAdd.Text = "分析结果添加到地图窗口中";
            this.checkBoxAdd.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Resee.DataManager.Properties.Resources.QQ截图20130309124218;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(419, 66);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "方法选择：";
            // 
            // comboBoxMethod
            // 
            this.comboBoxMethod.FormattingEnabled = true;
            this.comboBoxMethod.Items.AddRange(new object[] {
            "主成分差异法",
            "多波段主成分变换法",
            "差异主成分法"});
            this.comboBoxMethod.Location = new System.Drawing.Point(81, 74);
            this.comboBoxMethod.Name = "comboBoxMethod";
            this.comboBoxMethod.Size = new System.Drawing.Size(276, 22);
            this.comboBoxMethod.TabIndex = 14;
            // 
            // comboBoxContrastRaster
            // 
            this.comboBoxContrastRaster.FormattingEnabled = true;
            this.comboBoxContrastRaster.Location = new System.Drawing.Point(81, 146);
            this.comboBoxContrastRaster.Name = "comboBoxContrastRaster";
            this.comboBoxContrastRaster.Size = new System.Drawing.Size(276, 22);
            this.comboBoxContrastRaster.TabIndex = 15;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(221, 240);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(82, 23);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "确 定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // buttoncancel
            // 
            this.buttoncancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttoncancel.Location = new System.Drawing.Point(309, 240);
            this.buttoncancel.Name = "buttoncancel";
            this.buttoncancel.Size = new System.Drawing.Size(82, 23);
            this.buttoncancel.TabIndex = 18;
            this.buttoncancel.Text = "取 消";
            this.buttoncancel.UseVisualStyleBackColor = true;
            // 
            // buttonInputRaster
            // 
            this.buttonInputRaster.Location = new System.Drawing.Point(363, 109);
            this.buttonInputRaster.Name = "buttonInputRaster";
            this.buttonInputRaster.Size = new System.Drawing.Size(28, 21);
            this.buttonInputRaster.TabIndex = 19;
            this.buttonInputRaster.Text = "......";
            this.buttonInputRaster.UseVisualStyleBackColor = true;
            this.buttonInputRaster.Click += new System.EventHandler(this.buttonInputRaster_Click);
            // 
            // buttonInputcontrastRaster
            // 
            this.buttonInputcontrastRaster.Location = new System.Drawing.Point(363, 146);
            this.buttonInputcontrastRaster.Name = "buttonInputcontrastRaster";
            this.buttonInputcontrastRaster.Size = new System.Drawing.Size(28, 21);
            this.buttonInputcontrastRaster.TabIndex = 20;
            this.buttonInputcontrastRaster.Text = "......";
            this.buttonInputcontrastRaster.UseVisualStyleBackColor = true;
            this.buttonInputcontrastRaster.Click += new System.EventHandler(this.buttonInputcontrastRaster_Click);
            // 
            // buttonOutputRaster
            // 
            this.buttonOutputRaster.Location = new System.Drawing.Point(363, 180);
            this.buttonOutputRaster.Name = "buttonOutputRaster";
            this.buttonOutputRaster.Size = new System.Drawing.Size(28, 21);
            this.buttonOutputRaster.TabIndex = 21;
            this.buttonOutputRaster.Text = "......";
            this.buttonOutputRaster.UseVisualStyleBackColor = true;
            this.buttonOutputRaster.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 22;
            this.label2.Text = "输入影像：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 23;
            this.label3.Text = "对比影像：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 24;
            this.label4.Text = "输出路径：";
            // 
            // textBoxOutputRaster
            // 
            this.textBoxOutputRaster.Location = new System.Drawing.Point(81, 180);
            this.textBoxOutputRaster.Name = "textBoxOutputRaster";
            this.textBoxOutputRaster.Size = new System.Drawing.Size(276, 22);
            this.textBoxOutputRaster.TabIndex = 25;
            // 
            // Form_PCAAnalyst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 275);
            this.Controls.Add(this.textBoxOutputRaster);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonOutputRaster);
            this.Controls.Add(this.buttonInputcontrastRaster);
            this.Controls.Add(this.buttonInputRaster);
            this.Controls.Add(this.buttoncancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxContrastRaster);
            this.Controls.Add(this.comboBoxMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBoxAdd);
            this.Controls.Add(this.comboBoxInputRaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_PCAAnalyst";
            this.Text = "主成分分析模型";
            this.Load += new System.EventHandler(this.Form_PCAAnalyst_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxInputRaster.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit comboBoxInputRaster;
        private System.Windows.Forms.CheckBox checkBoxAdd;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMethod;
        private System.Windows.Forms.ComboBox comboBoxContrastRaster;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttoncancel;
        private System.Windows.Forms.Button buttonInputRaster;
        private System.Windows.Forms.Button buttonInputcontrastRaster;
        private System.Windows.Forms.Button buttonOutputRaster;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxOutputRaster;
    }
}