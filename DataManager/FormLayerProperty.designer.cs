namespace Resee.DataManager
{
    partial class FormLayerProperty
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
            this.cmbUniqueValueField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlUniqueValue = new DevExpress.XtraGrid.GridControl();
            this.gridViewUniqueValue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.btnUniqueRemoveAllValues = new DevExpress.XtraEditors.SimpleButton();
            this.btnUniqueRemoveValues = new DevExpress.XtraEditors.SimpleButton();
            this.btnUniqueAddValues = new DevExpress.XtraEditors.SimpleButton();
            this.btnUniqueAddAllValues = new DevExpress.XtraEditors.SimpleButton();
            this.btnLayerPropertiesOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnLayerPropertiesCancel = new DevExpress.XtraEditors.SimpleButton();
            this.cmbUniqueValueColorRamp = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.pageVecetorSimple = new DevExpress.XtraTab.XtraTabPage();
            this.txtSimpleDescription = new DevExpress.XtraEditors.TextEdit();
            this.labelSimplePreview = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnSimpleChange = new DevExpress.XtraEditors.SimpleButton();
            this.pageVectorUniqueValue = new DevExpress.XtraTab.XtraTabPage();
            this.pageRasterUniqueValue = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbRasterUniqueValueColorRamp = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbRasterUniqueValueField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlRasterUniqueValue = new DevExpress.XtraGrid.GridControl();
            this.gridViewRasterUniqueValue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemPictureEditRaster = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.btnRasterUniqueAddAllValues = new DevExpress.XtraEditors.SimpleButton();
            this.btnRasterUniqueAddValues = new DevExpress.XtraEditors.SimpleButton();
            this.btnRasterUniqueRemoveAllValues = new DevExpress.XtraEditors.SimpleButton();
            this.btnRasterUniqueRemoveValues = new DevExpress.XtraEditors.SimpleButton();
            this.treeListRenderer = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUniqueValueField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUniqueValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUniqueValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUniqueValueColorRamp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.pageVecetorSimple.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSimpleDescription.Properties)).BeginInit();
            this.pageVectorUniqueValue.SuspendLayout();
            this.pageRasterUniqueValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRasterUniqueValueColorRamp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRasterUniqueValueField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRasterUniqueValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRasterUniqueValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEditRaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListRenderer)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbUniqueValueField
            // 
            this.cmbUniqueValueField.Location = new System.Drawing.Point(3, 23);
            this.cmbUniqueValueField.Name = "cmbUniqueValueField";
            this.cmbUniqueValueField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUniqueValueField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbUniqueValueField.Size = new System.Drawing.Size(156, 21);
            this.cmbUniqueValueField.TabIndex = 9;
            this.cmbUniqueValueField.SelectedIndexChanged += new System.EventHandler(this.cmbField_SelectedIndexChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(3, 3);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(52, 14);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "渲染字段:";
            // 
            // gridControlUniqueValue
            // 
            this.gridControlUniqueValue.Location = new System.Drawing.Point(3, 50);
            this.gridControlUniqueValue.MainView = this.gridViewUniqueValue;
            this.gridControlUniqueValue.Name = "gridControlUniqueValue";
            this.gridControlUniqueValue.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
            this.gridControlUniqueValue.Size = new System.Drawing.Size(411, 156);
            this.gridControlUniqueValue.TabIndex = 11;
            this.gridControlUniqueValue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUniqueValue});
            // 
            // gridViewUniqueValue
            // 
            this.gridViewUniqueValue.GridControl = this.gridControlUniqueValue;
            this.gridViewUniqueValue.Name = "gridViewUniqueValue";
            this.gridViewUniqueValue.OptionsView.ShowGroupPanel = false;
            this.gridViewUniqueValue.OptionsView.ShowIndicator = false;
            // 
            // repositoryItemPictureEdit
            // 
            this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
            this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.repositoryItemPictureEdit.DoubleClick += new System.EventHandler(this.repositoryItemPictureEdit_DoubleClick);
            // 
            // btnUniqueRemoveAllValues
            // 
            this.btnUniqueRemoveAllValues.Location = new System.Drawing.Point(246, 212);
            this.btnUniqueRemoveAllValues.Name = "btnUniqueRemoveAllValues";
            this.btnUniqueRemoveAllValues.Size = new System.Drawing.Size(75, 23);
            this.btnUniqueRemoveAllValues.TabIndex = 15;
            this.btnUniqueRemoveAllValues.Text = "移除所有值";
            this.btnUniqueRemoveAllValues.Click += new System.EventHandler(this.btnRemoveAllValues_Click);
            // 
            // btnUniqueRemoveValues
            // 
            this.btnUniqueRemoveValues.Location = new System.Drawing.Point(165, 212);
            this.btnUniqueRemoveValues.Name = "btnUniqueRemoveValues";
            this.btnUniqueRemoveValues.Size = new System.Drawing.Size(75, 23);
            this.btnUniqueRemoveValues.TabIndex = 14;
            this.btnUniqueRemoveValues.Text = "移除值";
            this.btnUniqueRemoveValues.Click += new System.EventHandler(this.btnRemoveValues_Click);
            // 
            // btnUniqueAddValues
            // 
            this.btnUniqueAddValues.Location = new System.Drawing.Point(84, 212);
            this.btnUniqueAddValues.Name = "btnUniqueAddValues";
            this.btnUniqueAddValues.Size = new System.Drawing.Size(75, 23);
            this.btnUniqueAddValues.TabIndex = 13;
            this.btnUniqueAddValues.Text = "添加值";
            this.btnUniqueAddValues.Click += new System.EventHandler(this.btnAddValues_Click);
            // 
            // btnUniqueAddAllValues
            // 
            this.btnUniqueAddAllValues.Location = new System.Drawing.Point(3, 212);
            this.btnUniqueAddAllValues.Name = "btnUniqueAddAllValues";
            this.btnUniqueAddAllValues.Size = new System.Drawing.Size(75, 23);
            this.btnUniqueAddAllValues.TabIndex = 12;
            this.btnUniqueAddAllValues.Text = "添加所有值";
            this.btnUniqueAddAllValues.Click += new System.EventHandler(this.btnAddAllValues_Click);
            // 
            // btnLayerPropertiesOK
            // 
            this.btnLayerPropertiesOK.Location = new System.Drawing.Point(421, 289);
            this.btnLayerPropertiesOK.Name = "btnLayerPropertiesOK";
            this.btnLayerPropertiesOK.Size = new System.Drawing.Size(75, 23);
            this.btnLayerPropertiesOK.TabIndex = 16;
            this.btnLayerPropertiesOK.Text = "确定";
            this.btnLayerPropertiesOK.Click += new System.EventHandler(this.btnLayerPropertiesOK_Click);
            // 
            // btnLayerPropertiesCancel
            // 
            this.btnLayerPropertiesCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLayerPropertiesCancel.Location = new System.Drawing.Point(507, 289);
            this.btnLayerPropertiesCancel.Name = "btnLayerPropertiesCancel";
            this.btnLayerPropertiesCancel.Size = new System.Drawing.Size(75, 23);
            this.btnLayerPropertiesCancel.TabIndex = 17;
            this.btnLayerPropertiesCancel.Text = "取消";
            // 
            // cmbUniqueValueColorRamp
            // 
            this.cmbUniqueValueColorRamp.Location = new System.Drawing.Point(203, 23);
            this.cmbUniqueValueColorRamp.Name = "cmbUniqueValueColorRamp";
            this.cmbUniqueValueColorRamp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUniqueValueColorRamp.Size = new System.Drawing.Size(211, 21);
            this.cmbUniqueValueColorRamp.TabIndex = 19;
            this.cmbUniqueValueColorRamp.SelectedIndexChanged += new System.EventHandler(this.cmbColorRamp_SelectedIndexChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(203, 3);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(52, 14);
            this.labelControl8.TabIndex = 18;
            this.labelControl8.Text = "配色方案:";
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Location = new System.Drawing.Point(164, 12);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.pageVecetorSimple;
            this.xtraTabControl.Size = new System.Drawing.Size(423, 271);
            this.xtraTabControl.TabIndex = 20;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageVecetorSimple,
            this.pageVectorUniqueValue,
            this.pageRasterUniqueValue});
            // 
            // pageVecetorSimple
            // 
            this.pageVecetorSimple.Controls.Add(this.txtSimpleDescription);
            this.pageVecetorSimple.Controls.Add(this.labelSimplePreview);
            this.pageVecetorSimple.Controls.Add(this.labelControl6);
            this.pageVecetorSimple.Controls.Add(this.btnSimpleChange);
            this.pageVecetorSimple.Name = "pageVecetorSimple";
            this.pageVecetorSimple.Size = new System.Drawing.Size(417, 243);
            this.pageVecetorSimple.Text = "矢量简单";
            // 
            // txtSimpleDescription
            // 
            this.txtSimpleDescription.Location = new System.Drawing.Point(8, 116);
            this.txtSimpleDescription.Name = "txtSimpleDescription";
            this.txtSimpleDescription.Size = new System.Drawing.Size(203, 21);
            this.txtSimpleDescription.TabIndex = 1;
            // 
            // labelSimplePreview
            // 
            this.labelSimplePreview.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelSimplePreview.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelSimplePreview.Location = new System.Drawing.Point(8, 13);
            this.labelSimplePreview.Name = "labelSimplePreview";
            this.labelSimplePreview.Size = new System.Drawing.Size(111, 63);
            this.labelSimplePreview.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(8, 96);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(76, 14);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "图例符号注释:";
            // 
            // btnSimpleChange
            // 
            this.btnSimpleChange.Location = new System.Drawing.Point(125, 30);
            this.btnSimpleChange.Name = "btnSimpleChange";
            this.btnSimpleChange.Size = new System.Drawing.Size(75, 23);
            this.btnSimpleChange.TabIndex = 1;
            this.btnSimpleChange.Text = "更改符号";
            this.btnSimpleChange.Click += new System.EventHandler(this.btnSimpleChange_Click);
            // 
            // pageVectorUniqueValue
            // 
            this.pageVectorUniqueValue.Controls.Add(this.labelControl7);
            this.pageVectorUniqueValue.Controls.Add(this.cmbUniqueValueColorRamp);
            this.pageVectorUniqueValue.Controls.Add(this.cmbUniqueValueField);
            this.pageVectorUniqueValue.Controls.Add(this.labelControl8);
            this.pageVectorUniqueValue.Controls.Add(this.gridControlUniqueValue);
            this.pageVectorUniqueValue.Controls.Add(this.btnUniqueAddAllValues);
            this.pageVectorUniqueValue.Controls.Add(this.btnUniqueAddValues);
            this.pageVectorUniqueValue.Controls.Add(this.btnUniqueRemoveAllValues);
            this.pageVectorUniqueValue.Controls.Add(this.btnUniqueRemoveValues);
            this.pageVectorUniqueValue.Name = "pageVectorUniqueValue";
            this.pageVectorUniqueValue.Size = new System.Drawing.Size(417, 243);
            this.pageVectorUniqueValue.Text = "矢量唯一值";
            // 
            // pageRasterUniqueValue
            // 
            this.pageRasterUniqueValue.Controls.Add(this.labelControl1);
            this.pageRasterUniqueValue.Controls.Add(this.cmbRasterUniqueValueColorRamp);
            this.pageRasterUniqueValue.Controls.Add(this.cmbRasterUniqueValueField);
            this.pageRasterUniqueValue.Controls.Add(this.labelControl2);
            this.pageRasterUniqueValue.Controls.Add(this.gridControlRasterUniqueValue);
            this.pageRasterUniqueValue.Controls.Add(this.btnRasterUniqueAddAllValues);
            this.pageRasterUniqueValue.Controls.Add(this.btnRasterUniqueAddValues);
            this.pageRasterUniqueValue.Controls.Add(this.btnRasterUniqueRemoveAllValues);
            this.pageRasterUniqueValue.Controls.Add(this.btnRasterUniqueRemoveValues);
            this.pageRasterUniqueValue.Name = "pageRasterUniqueValue";
            this.pageRasterUniqueValue.Size = new System.Drawing.Size(417, 243);
            this.pageRasterUniqueValue.Text = "栅格唯一值";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "渲染字段:";
            // 
            // cmbRasterUniqueValueColorRamp
            // 
            this.cmbRasterUniqueValueColorRamp.Location = new System.Drawing.Point(203, 23);
            this.cmbRasterUniqueValueColorRamp.Name = "cmbRasterUniqueValueColorRamp";
            this.cmbRasterUniqueValueColorRamp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbRasterUniqueValueColorRamp.Size = new System.Drawing.Size(211, 21);
            this.cmbRasterUniqueValueColorRamp.TabIndex = 28;
            this.cmbRasterUniqueValueColorRamp.SelectedIndexChanged += new System.EventHandler(this.cmbColorRamp_SelectedIndexChanged);
            // 
            // cmbRasterUniqueValueField
            // 
            this.cmbRasterUniqueValueField.Location = new System.Drawing.Point(3, 23);
            this.cmbRasterUniqueValueField.Name = "cmbRasterUniqueValueField";
            this.cmbRasterUniqueValueField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbRasterUniqueValueField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbRasterUniqueValueField.Size = new System.Drawing.Size(156, 21);
            this.cmbRasterUniqueValueField.TabIndex = 21;
            this.cmbRasterUniqueValueField.SelectedIndexChanged += new System.EventHandler(this.cmbField_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(203, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 27;
            this.labelControl2.Text = "配色方案:";
            // 
            // gridControlRasterUniqueValue
            // 
            this.gridControlRasterUniqueValue.Location = new System.Drawing.Point(3, 50);
            this.gridControlRasterUniqueValue.MainView = this.gridViewRasterUniqueValue;
            this.gridControlRasterUniqueValue.Name = "gridControlRasterUniqueValue";
            this.gridControlRasterUniqueValue.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEditRaster});
            this.gridControlRasterUniqueValue.Size = new System.Drawing.Size(411, 156);
            this.gridControlRasterUniqueValue.TabIndex = 22;
            this.gridControlRasterUniqueValue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRasterUniqueValue});
            // 
            // gridViewRasterUniqueValue
            // 
            this.gridViewRasterUniqueValue.GridControl = this.gridControlRasterUniqueValue;
            this.gridViewRasterUniqueValue.Name = "gridViewRasterUniqueValue";
            this.gridViewRasterUniqueValue.OptionsView.ShowGroupPanel = false;
            this.gridViewRasterUniqueValue.OptionsView.ShowIndicator = false;
            // 
            // repositoryItemPictureEditRaster
            // 
            this.repositoryItemPictureEditRaster.Name = "repositoryItemPictureEditRaster";
            this.repositoryItemPictureEditRaster.DoubleClick += new System.EventHandler(this.repositoryItemPictureEdit_DoubleClick);
            // 
            // btnRasterUniqueAddAllValues
            // 
            this.btnRasterUniqueAddAllValues.Location = new System.Drawing.Point(3, 212);
            this.btnRasterUniqueAddAllValues.Name = "btnRasterUniqueAddAllValues";
            this.btnRasterUniqueAddAllValues.Size = new System.Drawing.Size(75, 23);
            this.btnRasterUniqueAddAllValues.TabIndex = 23;
            this.btnRasterUniqueAddAllValues.Text = "添加所有值";
            this.btnRasterUniqueAddAllValues.Click += new System.EventHandler(this.btnAddAllValues_Click);
            // 
            // btnRasterUniqueAddValues
            // 
            this.btnRasterUniqueAddValues.Location = new System.Drawing.Point(84, 212);
            this.btnRasterUniqueAddValues.Name = "btnRasterUniqueAddValues";
            this.btnRasterUniqueAddValues.Size = new System.Drawing.Size(75, 23);
            this.btnRasterUniqueAddValues.TabIndex = 24;
            this.btnRasterUniqueAddValues.Text = "添加值";
            this.btnRasterUniqueAddValues.Click += new System.EventHandler(this.btnAddValues_Click);
            // 
            // btnRasterUniqueRemoveAllValues
            // 
            this.btnRasterUniqueRemoveAllValues.Location = new System.Drawing.Point(246, 212);
            this.btnRasterUniqueRemoveAllValues.Name = "btnRasterUniqueRemoveAllValues";
            this.btnRasterUniqueRemoveAllValues.Size = new System.Drawing.Size(75, 23);
            this.btnRasterUniqueRemoveAllValues.TabIndex = 26;
            this.btnRasterUniqueRemoveAllValues.Text = "移除所有值";
            this.btnRasterUniqueRemoveAllValues.Click += new System.EventHandler(this.btnRemoveAllValues_Click);
            // 
            // btnRasterUniqueRemoveValues
            // 
            this.btnRasterUniqueRemoveValues.Location = new System.Drawing.Point(165, 212);
            this.btnRasterUniqueRemoveValues.Name = "btnRasterUniqueRemoveValues";
            this.btnRasterUniqueRemoveValues.Size = new System.Drawing.Size(75, 23);
            this.btnRasterUniqueRemoveValues.TabIndex = 25;
            this.btnRasterUniqueRemoveValues.Text = "移除值";
            this.btnRasterUniqueRemoveValues.Click += new System.EventHandler(this.btnRemoveValues_Click);
            // 
            // treeListRenderer
            // 
            this.treeListRenderer.Location = new System.Drawing.Point(12, 12);
            this.treeListRenderer.Name = "treeListRenderer";
            this.treeListRenderer.Size = new System.Drawing.Size(146, 266);
            this.treeListRenderer.TabIndex = 3;
            this.treeListRenderer.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListRenderer_FocusedNodeChanged);
            // 
            // FormLayerProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 319);
            this.Controls.Add(this.treeListRenderer);
            this.Controls.Add(this.xtraTabControl);
            this.Controls.Add(this.btnLayerPropertiesCancel);
            this.Controls.Add(this.btnLayerPropertiesOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLayerProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层渲染";
            this.Load += new System.EventHandler(this.FormLayerProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbUniqueValueField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUniqueValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUniqueValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUniqueValueColorRamp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.pageVecetorSimple.ResumeLayout(false);
            this.pageVecetorSimple.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSimpleDescription.Properties)).EndInit();
            this.pageVectorUniqueValue.ResumeLayout(false);
            this.pageVectorUniqueValue.PerformLayout();
            this.pageRasterUniqueValue.ResumeLayout(false);
            this.pageRasterUniqueValue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRasterUniqueValueColorRamp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRasterUniqueValueField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlRasterUniqueValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRasterUniqueValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEditRaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListRenderer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cmbUniqueValueField;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraGrid.GridControl gridControlUniqueValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUniqueValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
        private DevExpress.XtraEditors.SimpleButton btnUniqueRemoveAllValues;
        private DevExpress.XtraEditors.SimpleButton btnUniqueRemoveValues;
        private DevExpress.XtraEditors.SimpleButton btnUniqueAddValues;
        private DevExpress.XtraEditors.SimpleButton btnUniqueAddAllValues;
        private DevExpress.XtraEditors.SimpleButton btnLayerPropertiesOK;
        private DevExpress.XtraEditors.SimpleButton btnLayerPropertiesCancel;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbUniqueValueColorRamp;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage pageVectorUniqueValue;
        private DevExpress.XtraTab.XtraTabPage pageVecetorSimple;
        private DevExpress.XtraTab.XtraTabPage pageRasterUniqueValue;
        private DevExpress.XtraTreeList.TreeList treeListRenderer;
        private DevExpress.XtraEditors.TextEdit txtSimpleDescription;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelSimplePreview;
        private DevExpress.XtraEditors.SimpleButton btnSimpleChange;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbRasterUniqueValueColorRamp;
        private DevExpress.XtraEditors.ComboBoxEdit cmbRasterUniqueValueField;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridControlRasterUniqueValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRasterUniqueValue;
        private DevExpress.XtraEditors.SimpleButton btnRasterUniqueAddAllValues;
        private DevExpress.XtraEditors.SimpleButton btnRasterUniqueAddValues;
        private DevExpress.XtraEditors.SimpleButton btnRasterUniqueRemoveAllValues;
        private DevExpress.XtraEditors.SimpleButton btnRasterUniqueRemoveValues;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEditRaster;

    }
}