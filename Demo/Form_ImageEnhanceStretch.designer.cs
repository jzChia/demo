namespace Resee.ImageProcessing
{
    partial class Form_ImageEnhanceStretch
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
            this.comboBoxEditInputRaster1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditInputRaster2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.iButtonCancle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "选择影像：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "拉伸方法：";
            // 
            // comboBoxEditInputRaster1
            // 
            this.comboBoxEditInputRaster1.EditValue = "";
            this.comboBoxEditInputRaster1.Location = new System.Drawing.Point(81, 16);
            this.comboBoxEditInputRaster1.Name = "comboBoxEditInputRaster1";
            this.comboBoxEditInputRaster1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditInputRaster1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditInputRaster1.Size = new System.Drawing.Size(258, 21);
            this.comboBoxEditInputRaster1.TabIndex = 3;
            // 
            // comboBoxEditInputRaster2
            // 
            this.comboBoxEditInputRaster2.Location = new System.Drawing.Point(81, 50);
            this.comboBoxEditInputRaster2.Name = "comboBoxEditInputRaster2";
            this.comboBoxEditInputRaster2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditInputRaster2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditInputRaster2.Size = new System.Drawing.Size(258, 21);
            this.comboBoxEditInputRaster2.TabIndex = 4;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Location = new System.Drawing.Point(156, 83);
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
            this.iButtonCancle.Location = new System.Drawing.Point(255, 83);
            this.iButtonCancle.Name = "iButtonCancle";
            this.iButtonCancle.Size = new System.Drawing.Size(82, 23);
            this.iButtonCancle.TabIndex = 11;
            this.iButtonCancle.Text = "取  消";
            this.iButtonCancle.Click += new System.EventHandler(this.iButtonCancle_Click);
            // 
            // Form_ImageEnhanceStretch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 114);
            this.Controls.Add(this.iButtonCancle);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.comboBoxEditInputRaster2);
            this.Controls.Add(this.comboBoxEditInputRaster1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "Form_ImageEnhanceStretch";
            this.Text = "影像增强";
            this.Load += new System.EventHandler(this.Form_ImageEnhanceStretch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditInputRaster2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditInputRaster1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditInputRaster2;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton iButtonCancle;
    }
}