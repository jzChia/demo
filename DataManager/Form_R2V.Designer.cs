namespace Resee.DataManager
{
    partial class Form_R2V
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
            this.iButtonCancle = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxEditInputRaster2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditOutputRaster = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditOutputRaster.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // iButtonCancle
            // 
            this.iButtonCancle.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iButtonCancle.Appearance.Options.UseFont = true;
            this.iButtonCancle.Location = new System.Drawing.Point(307, 105);
            this.iButtonCancle.Name = "iButtonCancle";
            this.iButtonCancle.Size = new System.Drawing.Size(82, 23);
            this.iButtonCancle.TabIndex = 20;
            this.iButtonCancle.Text = "取  消";
            this.iButtonCancle.Click += new System.EventHandler(this.iButtonCancle_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Location = new System.Drawing.Point(208, 105);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(82, 23);
            this.simpleButton4.TabIndex = 19;
            this.simpleButton4.Text = "确  定";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(11, 81);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(170, 18);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "分析结果添加到地图窗口中";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBoxEditInputRaster2
            // 
            this.comboBoxEditInputRaster2.Location = new System.Drawing.Point(75, 13);
            this.comboBoxEditInputRaster2.Name = "comboBoxEditInputRaster2";
            this.comboBoxEditInputRaster2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditInputRaster2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditInputRaster2.Size = new System.Drawing.Size(314, 21);
            this.comboBoxEditInputRaster2.TabIndex = 14;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 50);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "输出矢量：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "栅格数据：";
            // 
            // comboBoxEditOutputRaster
            // 
            this.comboBoxEditOutputRaster.Location = new System.Drawing.Point(75, 47);
            this.comboBoxEditOutputRaster.Name = "comboBoxEditOutputRaster";
            this.comboBoxEditOutputRaster.Size = new System.Drawing.Size(314, 21);
            this.comboBoxEditOutputRaster.TabIndex = 15;
            // 
            // Form_R2V
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 142);
            this.Controls.Add(this.iButtonCancle);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBoxEditInputRaster2);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.comboBoxEditOutputRaster);
            this.Name = "Form_R2V";
            this.Text = "栅格转矢量";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditOutputRaster.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton iButtonCancle;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditInputRaster2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit comboBoxEditOutputRaster;
    }
}