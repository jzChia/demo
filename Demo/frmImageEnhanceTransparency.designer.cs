namespace Resee.ImageProcessing
{
    partial class frmImageEnhanceTransparency
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
            this.simpleButtonApply = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancle = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textEditLight = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditTarget = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.trackBarControlLight = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditTarget.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControlLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControlLight.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonApply
            // 
            this.simpleButtonApply.Location = new System.Drawing.Point(303, 86);
            this.simpleButtonApply.Name = "simpleButtonApply";
            this.simpleButtonApply.Size = new System.Drawing.Size(64, 23);
            this.simpleButtonApply.TabIndex = 14;
            this.simpleButtonApply.Text = "应  用";
            this.simpleButtonApply.Click += new System.EventHandler(this.simpleButtonApply_Click);
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Location = new System.Drawing.Point(163, 86);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(64, 23);
            this.simpleButtonOK.TabIndex = 15;
            this.simpleButtonOK.Text = "确  定";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancle
            // 
            this.simpleButtonCancle.Location = new System.Drawing.Point(233, 86);
            this.simpleButtonCancle.Name = "simpleButtonCancle";
            this.simpleButtonCancle.Size = new System.Drawing.Size(64, 23);
            this.simpleButtonCancle.TabIndex = 16;
            this.simpleButtonCancle.Text = "取  消";
            this.simpleButtonCancle.Click += new System.EventHandler(this.simpleButtonCancle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textEditLight);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.comboBoxEditTarget);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.trackBarControlLight);
            this.groupBox1.Location = new System.Drawing.Point(7, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 80);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // textEditLight
            // 
            this.textEditLight.EditValue = "0";
            this.textEditLight.Location = new System.Drawing.Point(325, 46);
            this.textEditLight.Name = "textEditLight";
            this.textEditLight.Size = new System.Drawing.Size(31, 21);
            this.textEditLight.TabIndex = 24;
            this.textEditLight.EditValueChanged += new System.EventHandler(this.textEditLight_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 48);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 14);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "亮  度：";
            // 
            // comboBoxEditTarget
            // 
            this.comboBoxEditTarget.Location = new System.Drawing.Point(76, 16);
            this.comboBoxEditTarget.Name = "comboBoxEditTarget";
            this.comboBoxEditTarget.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditTarget.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditTarget.Size = new System.Drawing.Size(279, 21);
            this.comboBoxEditTarget.TabIndex = 15;
            this.comboBoxEditTarget.SelectedValueChanged += new System.EventHandler(this.comboBoxEditTarget_SelectedValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "目  标：";
            // 
            // trackBarControlLight
            // 
            this.trackBarControlLight.EditValue = null;
            this.trackBarControlLight.Location = new System.Drawing.Point(76, 45);
            this.trackBarControlLight.Name = "trackBarControlLight";
            this.trackBarControlLight.Properties.Maximum = 100;
            this.trackBarControlLight.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.trackBarControlLight.Properties.ShowValueToolTip = true;
            this.trackBarControlLight.Size = new System.Drawing.Size(234, 23);
            this.trackBarControlLight.TabIndex = 16;
            this.trackBarControlLight.ValueChanged += new System.EventHandler(this.trackBarControlLight_ValueChanged);
            // 
            // frmImageEnhanceBright
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 114);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleButtonCancle);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.simpleButtonApply);
            this.Name = "frmImageEnhanceBright";
            this.Text = "信息增强";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditTarget.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControlLight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControlLight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonApply;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancle;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit textEditLight;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditTarget;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ZoomTrackBarControl trackBarControlLight;
    }
}