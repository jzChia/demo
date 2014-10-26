namespace Resee.DataManager
{
    partial class Form_PACAnalyst
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditInputRaster1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditInputRaster2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButtonInputRaster1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonInputRaster2 = new DevExpress.XtraEditors.SimpleButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.iButtonCancle = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOutPutRaster = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEditOutputRaster = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditOutputRaster.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "输入影像：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "对比影像：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "输出路径：";
            // 
            // comboBoxEditInputRaster1
            // 
            this.comboBoxEditInputRaster1.EditValue = "";
            this.comboBoxEditInputRaster1.Location = new System.Drawing.Point(81, 16);
            this.comboBoxEditInputRaster1.Name = "comboBoxEditInputRaster1";
            this.comboBoxEditInputRaster1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditInputRaster1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditInputRaster1.Size = new System.Drawing.Size(276, 21);
            this.comboBoxEditInputRaster1.TabIndex = 3;
            // 
            // comboBoxEditInputRaster2
            // 
            this.comboBoxEditInputRaster2.Location = new System.Drawing.Point(81, 50);
            this.comboBoxEditInputRaster2.Name = "comboBoxEditInputRaster2";
            this.comboBoxEditInputRaster2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditInputRaster2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditInputRaster2.Size = new System.Drawing.Size(276, 21);
            this.comboBoxEditInputRaster2.TabIndex = 4;
            // 
            // simpleButtonInputRaster1
            // 
            this.simpleButtonInputRaster1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonInputRaster1.Appearance.Options.UseFont = true;
            this.simpleButtonInputRaster1.Location = new System.Drawing.Point(364, 16);
            this.simpleButtonInputRaster1.Name = "simpleButtonInputRaster1";
            this.simpleButtonInputRaster1.Size = new System.Drawing.Size(30, 23);
            this.simpleButtonInputRaster1.TabIndex = 6;
            this.simpleButtonInputRaster1.Text = "....";
            // 
            // simpleButtonInputRaster2
            // 
            this.simpleButtonInputRaster2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonInputRaster2.Appearance.Options.UseFont = true;
            this.simpleButtonInputRaster2.Location = new System.Drawing.Point(364, 48);
            this.simpleButtonInputRaster2.Name = "simpleButtonInputRaster2";
            this.simpleButtonInputRaster2.Size = new System.Drawing.Size(30, 23);
            this.simpleButtonInputRaster2.TabIndex = 7;
            this.simpleButtonInputRaster2.Text = "....";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(17, 118);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(170, 18);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "分析结果添加到地图窗口中";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Location = new System.Drawing.Point(214, 142);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(82, 23);
            this.simpleButton4.TabIndex = 10;
            this.simpleButton4.Text = "确  定";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // iButtonCancle
            // 
            this.iButtonCancle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iButtonCancle.Appearance.Options.UseFont = true;
            this.iButtonCancle.Location = new System.Drawing.Point(313, 142);
            this.iButtonCancle.Name = "iButtonCancle";
            this.iButtonCancle.Size = new System.Drawing.Size(82, 23);
            this.iButtonCancle.TabIndex = 11;
            this.iButtonCancle.Text = "取  消";
            this.iButtonCancle.Click += new System.EventHandler(this.iButtonCancle_Click);
            // 
            // simpleButtonOutPutRaster
            // 
            this.simpleButtonOutPutRaster.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonOutPutRaster.Appearance.Options.UseFont = true;
            this.simpleButtonOutPutRaster.Enabled = false;
            this.simpleButtonOutPutRaster.Location = new System.Drawing.Point(364, 82);
            this.simpleButtonOutPutRaster.Name = "simpleButtonOutPutRaster";
            this.simpleButtonOutPutRaster.Size = new System.Drawing.Size(30, 23);
            this.simpleButtonOutPutRaster.TabIndex = 8;
            this.simpleButtonOutPutRaster.Text = "....";
            this.simpleButtonOutPutRaster.Click += new System.EventHandler(this.simpleButtonOutPutRaster_Click);
            // 
            // comboBoxEditOutputRaster
            // 
            this.comboBoxEditOutputRaster.Location = new System.Drawing.Point(81, 84);
            this.comboBoxEditOutputRaster.Name = "comboBoxEditOutputRaster";
            this.comboBoxEditOutputRaster.Size = new System.Drawing.Size(276, 21);
            this.comboBoxEditOutputRaster.TabIndex = 5;
            // 
            // Form_PACAnalyst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 177);
            this.Controls.Add(this.iButtonCancle);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.simpleButtonOutPutRaster);
            this.Controls.Add(this.simpleButtonInputRaster2);
            this.Controls.Add(this.simpleButtonInputRaster1);
            this.Controls.Add(this.comboBoxEditInputRaster2);
            this.Controls.Add(this.comboBoxEditInputRaster1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.comboBoxEditOutputRaster);
            this.Name = "Form_PACAnalyst";
            this.Text = "主成分分析模型";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditOutputRaster.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditInputRaster1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditInputRaster2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonInputRaster1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonInputRaster2;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton iButtonCancle;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOutPutRaster;
        private DevExpress.XtraEditors.TextEdit comboBoxEditOutputRaster;
    }
}