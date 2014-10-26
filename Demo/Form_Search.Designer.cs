namespace Demo
{
    partial class Form_Search
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
            this.DB_Raster = new DevExpress.XtraEditors.CheckEdit();
            this.DB_DEM = new DevExpress.XtraEditors.CheckEdit();
            this.DB_Vector = new DevExpress.XtraEditors.CheckEdit();
            this.DB_3D = new DevExpress.XtraEditors.CheckEdit();
            this.DB_Test = new DevExpress.XtraEditors.CheckEdit();
            this.DB_Others = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.Search = new DevExpress.XtraEditors.SimpleButton();
            this.Keywords = new System.Windows.Forms.TextBox();
            this.Region_Province = new System.Windows.Forms.ComboBox();
            this.Region_Country = new System.Windows.Forms.ComboBox();
            this.DataSource = new System.Windows.Forms.ComboBox();
            this.Scale_From = new System.Windows.Forms.ComboBox();
            this.Scale_To = new System.Windows.Forms.ComboBox();
            this.Resolution_From = new System.Windows.Forms.ComboBox();
            this.Resolution_To = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DB_Raster.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_DEM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_Vector.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_3D.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_Test.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_Others.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl1.Location = new System.Drawing.Point(37, 23);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "查询数据库：";
            // 
            // DB_Raster
            // 
            this.DB_Raster.EditValue = true;
            this.DB_Raster.Location = new System.Drawing.Point(121, 20);
            this.DB_Raster.Name = "DB_Raster";
            this.DB_Raster.Properties.AllowFocused = false;
            this.DB_Raster.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB_Raster.Properties.Appearance.Options.UseFont = true;
            this.DB_Raster.Properties.Caption = "影像库";
            this.DB_Raster.Properties.RadioGroupIndex = 1;
            this.DB_Raster.Size = new System.Drawing.Size(75, 24);
            this.DB_Raster.TabIndex = 1;
            this.DB_Raster.Tag = "0";
            // 
            // DB_DEM
            // 
            this.DB_DEM.Location = new System.Drawing.Point(202, 20);
            this.DB_DEM.Name = "DB_DEM";
            this.DB_DEM.Properties.AllowFocused = false;
            this.DB_DEM.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB_DEM.Properties.Appearance.Options.UseFont = true;
            this.DB_DEM.Properties.Caption = "DEM库";
            this.DB_DEM.Properties.RadioGroupIndex = 1;
            this.DB_DEM.Size = new System.Drawing.Size(75, 24);
            this.DB_DEM.TabIndex = 2;
            this.DB_DEM.Tag = "1";
            // 
            // DB_Vector
            // 
            this.DB_Vector.Location = new System.Drawing.Point(283, 20);
            this.DB_Vector.Name = "DB_Vector";
            this.DB_Vector.Properties.AllowFocused = false;
            this.DB_Vector.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB_Vector.Properties.Appearance.Options.UseFont = true;
            this.DB_Vector.Properties.Caption = "矢量库";
            this.DB_Vector.Properties.RadioGroupIndex = 1;
            this.DB_Vector.Size = new System.Drawing.Size(75, 24);
            this.DB_Vector.TabIndex = 3;
            this.DB_Vector.Tag = "2";
            // 
            // DB_3D
            // 
            this.DB_3D.Location = new System.Drawing.Point(364, 20);
            this.DB_3D.Name = "DB_3D";
            this.DB_3D.Properties.AllowFocused = false;
            this.DB_3D.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB_3D.Properties.Appearance.Options.UseFont = true;
            this.DB_3D.Properties.Caption = "三维库";
            this.DB_3D.Properties.RadioGroupIndex = 1;
            this.DB_3D.Size = new System.Drawing.Size(75, 24);
            this.DB_3D.TabIndex = 4;
            this.DB_3D.Tag = "3";
            // 
            // DB_Test
            // 
            this.DB_Test.Location = new System.Drawing.Point(445, 20);
            this.DB_Test.Name = "DB_Test";
            this.DB_Test.Properties.AllowFocused = false;
            this.DB_Test.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB_Test.Properties.Appearance.Options.UseFont = true;
            this.DB_Test.Properties.Caption = "试验库";
            this.DB_Test.Properties.RadioGroupIndex = 1;
            this.DB_Test.Size = new System.Drawing.Size(75, 24);
            this.DB_Test.TabIndex = 5;
            this.DB_Test.Tag = "4";
            // 
            // DB_Others
            // 
            this.DB_Others.Location = new System.Drawing.Point(526, 20);
            this.DB_Others.Name = "DB_Others";
            this.DB_Others.Properties.AllowFocused = false;
            this.DB_Others.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB_Others.Properties.Appearance.Options.UseFont = true;
            this.DB_Others.Properties.Caption = "其他库";
            this.DB_Others.Properties.RadioGroupIndex = 1;
            this.DB_Others.Size = new System.Drawing.Size(75, 24);
            this.DB_Others.TabIndex = 6;
            this.DB_Others.Tag = "5";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl2.Location = new System.Drawing.Point(37, 50);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 19);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "所属区域：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Location = new System.Drawing.Point(123, 50);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 19);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "国家：";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(285, 50);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 19);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "地区：";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl5.Location = new System.Drawing.Point(37, 77);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 19);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "查询数据源：";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl6.Location = new System.Drawing.Point(285, 104);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(26, 19);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "至：";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl7.Location = new System.Drawing.Point(123, 104);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(26, 19);
            this.labelControl7.TabIndex = 16;
            this.labelControl7.Text = "从：";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl8.Location = new System.Drawing.Point(37, 104);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(52, 19);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "比例尺：";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Location = new System.Drawing.Point(285, 130);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(26, 19);
            this.labelControl9.TabIndex = 23;
            this.labelControl9.Text = "至：";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl10.Location = new System.Drawing.Point(123, 130);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(26, 19);
            this.labelControl10.TabIndex = 22;
            this.labelControl10.Text = "从：";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl11.Location = new System.Drawing.Point(37, 130);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(52, 19);
            this.labelControl11.TabIndex = 21;
            this.labelControl11.Text = "分辨率：";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl12.Location = new System.Drawing.Point(37, 157);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(52, 19);
            this.labelControl12.TabIndex = 26;
            this.labelControl12.Text = "关键字：";
            // 
            // Search
            // 
            this.Search.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Search.Location = new System.Drawing.Point(526, 155);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(75, 23);
            this.Search.TabIndex = 28;
            this.Search.Text = "查询";
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // Keywords
            // 
            this.Keywords.Font = new System.Drawing.Font("宋体", 9F);
            this.Keywords.Location = new System.Drawing.Point(121, 156);
            this.Keywords.Name = "Keywords";
            this.Keywords.Size = new System.Drawing.Size(399, 21);
            this.Keywords.TabIndex = 30;
            // 
            // Region_Province
            // 
            this.Region_Province.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Region_Province.Font = new System.Drawing.Font("宋体", 9F);
            this.Region_Province.FormattingEnabled = true;
            this.Region_Province.Location = new System.Drawing.Point(330, 49);
            this.Region_Province.Name = "Region_Province";
            this.Region_Province.Size = new System.Drawing.Size(100, 20);
            this.Region_Province.TabIndex = 31;
            // 
            // Region_Country
            // 
            this.Region_Country.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Region_Country.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Region_Country.FormattingEnabled = true;
            this.Region_Country.Location = new System.Drawing.Point(168, 49);
            this.Region_Country.Name = "Region_Country";
            this.Region_Country.Size = new System.Drawing.Size(100, 20);
            this.Region_Country.TabIndex = 32;
            this.Region_Country.SelectedIndexChanged += new System.EventHandler(this.Region_Country_SelectedIndexChanged);
            // 
            // DataSource
            // 
            this.DataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DataSource.Font = new System.Drawing.Font("宋体", 9F);
            this.DataSource.FormattingEnabled = true;
            this.DataSource.Location = new System.Drawing.Point(123, 76);
            this.DataSource.Name = "DataSource";
            this.DataSource.Size = new System.Drawing.Size(145, 20);
            this.DataSource.TabIndex = 33;
            // 
            // Scale_From
            // 
            this.Scale_From.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Scale_From.FormattingEnabled = true;
            this.Scale_From.Location = new System.Drawing.Point(168, 103);
            this.Scale_From.Name = "Scale_From";
            this.Scale_From.Size = new System.Drawing.Size(100, 20);
            this.Scale_From.TabIndex = 34;
            // 
            // Scale_To
            // 
            this.Scale_To.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Scale_To.FormattingEnabled = true;
            this.Scale_To.Location = new System.Drawing.Point(330, 103);
            this.Scale_To.Name = "Scale_To";
            this.Scale_To.Size = new System.Drawing.Size(100, 20);
            this.Scale_To.TabIndex = 35;
            // 
            // Resolution_From
            // 
            this.Resolution_From.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resolution_From.FormattingEnabled = true;
            this.Resolution_From.Location = new System.Drawing.Point(168, 129);
            this.Resolution_From.Name = "Resolution_From";
            this.Resolution_From.Size = new System.Drawing.Size(100, 20);
            this.Resolution_From.TabIndex = 36;
            // 
            // Resolution_To
            // 
            this.Resolution_To.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resolution_To.FormattingEnabled = true;
            this.Resolution_To.Location = new System.Drawing.Point(330, 129);
            this.Resolution_To.Name = "Resolution_To";
            this.Resolution_To.Size = new System.Drawing.Size(100, 20);
            this.Resolution_To.TabIndex = 37;
            // 
            // Form_Search
            // 
            this.AcceptButton = this.Search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 199);
            this.Controls.Add(this.Resolution_To);
            this.Controls.Add(this.Resolution_From);
            this.Controls.Add(this.Scale_To);
            this.Controls.Add(this.Scale_From);
            this.Controls.Add(this.DataSource);
            this.Controls.Add(this.Region_Country);
            this.Controls.Add(this.Region_Province);
            this.Controls.Add(this.Keywords);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.DB_Others);
            this.Controls.Add(this.DB_Test);
            this.Controls.Add(this.DB_3D);
            this.Controls.Add(this.DB_Vector);
            this.Controls.Add(this.DB_DEM);
            this.Controls.Add(this.DB_Raster);
            this.Controls.Add(this.labelControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_Search";
            this.Text = "Form_Search";
            ((System.ComponentModel.ISupportInitialize)(this.DB_Raster.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_DEM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_Vector.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_3D.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_Test.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DB_Others.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit DB_Raster;
        private DevExpress.XtraEditors.CheckEdit DB_DEM;
        private DevExpress.XtraEditors.CheckEdit DB_Vector;
        private DevExpress.XtraEditors.CheckEdit DB_3D;
        private DevExpress.XtraEditors.CheckEdit DB_Test;
        private DevExpress.XtraEditors.CheckEdit DB_Others;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.SimpleButton Search;
        private System.Windows.Forms.TextBox Keywords;
        private System.Windows.Forms.ComboBox Region_Province;
        private System.Windows.Forms.ComboBox Region_Country;
        private System.Windows.Forms.ComboBox DataSource;
        private System.Windows.Forms.ComboBox Scale_From;
        private System.Windows.Forms.ComboBox Scale_To;
        private System.Windows.Forms.ComboBox Resolution_From;
        private System.Windows.Forms.ComboBox Resolution_To;
    }
}