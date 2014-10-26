namespace Resee.DataManager
{
    partial class UniqueValueAddValueForm
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
            this.ValueTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.AddValue = new DevExpress.XtraEditors.SimpleButton();
            this.CancelAddValue = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ValueTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ValueTextEdit
            // 
            this.ValueTextEdit.Location = new System.Drawing.Point(12, 12);
            this.ValueTextEdit.Name = "ValueTextEdit";
            this.ValueTextEdit.Size = new System.Drawing.Size(170, 21);
            this.ValueTextEdit.TabIndex = 0;
            // 
            // AddValue
            // 
            this.AddValue.Location = new System.Drawing.Point(50, 39);
            this.AddValue.Name = "AddValue";
            this.AddValue.Size = new System.Drawing.Size(63, 23);
            this.AddValue.TabIndex = 1;
            this.AddValue.Text = "添加";
            this.AddValue.Click += new System.EventHandler(this.AddValue_Click);
            // 
            // CancelAddValue
            // 
            this.CancelAddValue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelAddValue.Location = new System.Drawing.Point(119, 39);
            this.CancelAddValue.Name = "CancelAddValue";
            this.CancelAddValue.Size = new System.Drawing.Size(63, 23);
            this.CancelAddValue.TabIndex = 2;
            this.CancelAddValue.Text = "取消";
            // 
            // UniqueValueAddValueForm
            // 
            this.AcceptButton = this.AddValue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelAddValue;
            this.ClientSize = new System.Drawing.Size(195, 67);
            this.Controls.Add(this.CancelAddValue);
            this.Controls.Add(this.AddValue);
            this.Controls.Add(this.ValueTextEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UniqueValueAddValueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加值";
            ((System.ComponentModel.ISupportInitialize)(this.ValueTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit ValueTextEdit;
        private DevExpress.XtraEditors.SimpleButton AddValue;
        private DevExpress.XtraEditors.SimpleButton CancelAddValue;
    }
}