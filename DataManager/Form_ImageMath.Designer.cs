namespace Resee.DataManager
{
    partial class Form_ImageMath
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
            this.comboBoxMethod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonCancle = new DevExpress.XtraEditors.SimpleButton();
            this.ButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.checkBoxAdd = new System.Windows.Forms.CheckBox();
            this.simpleButtonOutPutRaster = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonInputRaster2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonInputRaster1 = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxContrastRaster = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxInputRaster = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxOutputRaster = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxContrastRaster.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxInputRaster.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxOutputRaster.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Resee.DataManager.Properties.Resources.QQ截图20130309124218;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(413, 66);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // comboBoxMethod
            // 
            this.comboBoxMethod.FormattingEnabled = true;
            this.comboBoxMethod.Items.AddRange(new object[] {
            "差值法",
            "比值法"});
            this.comboBoxMethod.Location = new System.Drawing.Point(81, 74);
            this.comboBoxMethod.Name = "comboBoxMethod";
            this.comboBoxMethod.Size = new System.Drawing.Size(276, 22);
            this.comboBoxMethod.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 27;
            this.label1.Text = "方法选择：";
            // 
            // ButtonCancle
            // 
            this.ButtonCancle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancle.Appearance.Options.UseFont = true;
            this.ButtonCancle.Location = new System.Drawing.Point(312, 233);
            this.ButtonCancle.Name = "ButtonCancle";
            this.ButtonCancle.Size = new System.Drawing.Size(82, 23);
            this.ButtonCancle.TabIndex = 26;
            this.ButtonCancle.Text = "取  消";
            this.ButtonCancle.Click += new System.EventHandler(this.iButtonCancle_Click);
            // 
            // ButtonOK
            // 
            this.ButtonOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOK.Appearance.Options.UseFont = true;
            this.ButtonOK.Location = new System.Drawing.Point(216, 233);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(82, 23);
            this.ButtonOK.TabIndex = 25;
            this.ButtonOK.Text = "确  定";
            this.ButtonOK.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // checkBoxAdd
            // 
            this.checkBoxAdd.AutoSize = true;
            this.checkBoxAdd.Checked = true;
            this.checkBoxAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAdd.Location = new System.Drawing.Point(17, 211);
            this.checkBoxAdd.Name = "checkBoxAdd";
            this.checkBoxAdd.Size = new System.Drawing.Size(170, 18);
            this.checkBoxAdd.TabIndex = 24;
            this.checkBoxAdd.Text = "分析结果添加到地图窗口中";
            this.checkBoxAdd.UseVisualStyleBackColor = true;
            // 
            // simpleButtonOutPutRaster
            // 
            this.simpleButtonOutPutRaster.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonOutPutRaster.Appearance.Options.UseFont = true;
            this.simpleButtonOutPutRaster.Enabled = false;
            this.simpleButtonOutPutRaster.Location = new System.Drawing.Point(364, 175);
            this.simpleButtonOutPutRaster.Name = "simpleButtonOutPutRaster";
            this.simpleButtonOutPutRaster.Size = new System.Drawing.Size(30, 23);
            this.simpleButtonOutPutRaster.TabIndex = 23;
            this.simpleButtonOutPutRaster.Text = "....";
            this.simpleButtonOutPutRaster.Click += new System.EventHandler(this.simpleButtonOutPutRaster_Click);
            // 
            // simpleButtonInputRaster2
            // 
            this.simpleButtonInputRaster2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonInputRaster2.Appearance.Options.UseFont = true;
            this.simpleButtonInputRaster2.Location = new System.Drawing.Point(364, 141);
            this.simpleButtonInputRaster2.Name = "simpleButtonInputRaster2";
            this.simpleButtonInputRaster2.Size = new System.Drawing.Size(30, 23);
            this.simpleButtonInputRaster2.TabIndex = 22;
            this.simpleButtonInputRaster2.Text = "....";
            this.simpleButtonInputRaster2.Click += new System.EventHandler(this.simpleButtonInputRaster2_Click);
            // 
            // simpleButtonInputRaster1
            // 
            this.simpleButtonInputRaster1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonInputRaster1.Appearance.Options.UseFont = true;
            this.simpleButtonInputRaster1.Location = new System.Drawing.Point(364, 109);
            this.simpleButtonInputRaster1.Name = "simpleButtonInputRaster1";
            this.simpleButtonInputRaster1.Size = new System.Drawing.Size(30, 23);
            this.simpleButtonInputRaster1.TabIndex = 21;
            this.simpleButtonInputRaster1.Text = "....";
            this.simpleButtonInputRaster1.Click += new System.EventHandler(this.simpleButtonInputRaster1_Click);
            // 
            // comboBoxContrastRaster
            // 
            this.comboBoxContrastRaster.Location = new System.Drawing.Point(81, 143);
            this.comboBoxContrastRaster.Name = "comboBoxContrastRaster";
            this.comboBoxContrastRaster.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxContrastRaster.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxContrastRaster.Size = new System.Drawing.Size(276, 21);
            this.comboBoxContrastRaster.TabIndex = 19;
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
            this.comboBoxInputRaster.TabIndex = 18;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 180);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "输出路径：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 146);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "对比影像：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 112);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "输入影像：";
            // 
            // comboBoxOutputRaster
            // 
            this.comboBoxOutputRaster.Location = new System.Drawing.Point(81, 177);
            this.comboBoxOutputRaster.Name = "comboBoxOutputRaster";
            this.comboBoxOutputRaster.Size = new System.Drawing.Size(276, 21);
            this.comboBoxOutputRaster.TabIndex = 20;
            // 
            // Form_ImageMath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 269);
            this.Controls.Add(this.comboBoxMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonCancle);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.checkBoxAdd);
            this.Controls.Add(this.simpleButtonOutPutRaster);
            this.Controls.Add(this.simpleButtonInputRaster2);
            this.Controls.Add(this.simpleButtonInputRaster1);
            this.Controls.Add(this.comboBoxContrastRaster);
            this.Controls.Add(this.comboBoxInputRaster);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.comboBoxOutputRaster);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form_ImageMath";
            this.Text = "影像代数";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxContrastRaster.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxInputRaster.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxOutputRaster.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxMethod;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton ButtonCancle;
        private DevExpress.XtraEditors.SimpleButton ButtonOK;
        private System.Windows.Forms.CheckBox checkBoxAdd;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOutPutRaster;
        private DevExpress.XtraEditors.SimpleButton simpleButtonInputRaster2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonInputRaster1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxContrastRaster;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxInputRaster;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit comboBoxOutputRaster;
    }
}