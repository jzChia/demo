using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Resee.DataManager
{
    public partial class FormLayerProperty : DevExpress.XtraEditors.XtraForm
    {
        private ILayer layer = null;
        private LayerType layerType;
        private ISymbol simpleRendererSymbol = null;
        private List<UniqueValueRecordClass> uniqueValueRecordClassList = null;
        private ImageList RandomColorRampImageList = null;
        private IEnumColors CurrentEnumColors = null;
        private esriSymbologyStyleClass CurrentSymbologyStyleClass;
        private IStyleGallery pStyleGallery=null;


        public FormLayerProperty(ILayer layer)
        {
            InitializeComponent();

            this.layer = layer;

        }

        private void FormLayerProperty_Load(object sender, EventArgs e)
        {
            //初始化图层类型
            if (layer is IGeoFeatureLayer)
                layerType = LayerType.FeatureLayer;
            else if (layer is IRasterLayer)
                layerType = LayerType.RasterLayer;
            //初始化渲染方式
            InitTreeList();
            //初始化配色方案
            InitColorScheme();
            //初始化渲染信息
            InitRendererInfo();
        }
        /// <summary>
        /// 初始化渲染方式树
        /// </summary>
        private void InitTreeList()
        {
            //设置模板树属性
            treeListRenderer.OptionsBehavior.Editable = false;//不可编辑
            treeListRenderer.OptionsSelection.InvertSelection = true;//改变选中的样式
            //添加列
            treeListRenderer.Columns.Add();
            treeListRenderer.Columns[0].Caption = "渲染方式";
            treeListRenderer.Columns[0].Visible = true;
            //设置渲染样式
            TreeListNode treeListNode = null;
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    treeListNode = treeListRenderer.AppendNode(new object[] { "矢量图层" }, null);
                    treeListRenderer.AppendNode(new object[] { "简单" }, treeListNode);
                    treeListRenderer.AppendNode(new object[] { "唯一值" }, treeListNode);
                    break;
                case LayerType.RasterLayer:
                    treeListNode = treeListRenderer.AppendNode(new object[] { "栅格图层" }, null);
                    treeListRenderer.AppendNode(new object[] { "唯一值" }, treeListNode);
                    break;
            }
            //展开所有节点
            treeListRenderer.ExpandAll();
            //选中当前渲染样式
            FocusRendererType();
        }

        private void FocusRendererType()
        {
            TreeListNode subNode = null;

            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    IGeoFeatureLayer geoFeatureLayer = layer as IGeoFeatureLayer;
                    IFeatureRenderer featureRenderer = geoFeatureLayer.Renderer;

                    if (featureRenderer is ISimpleRenderer)
                    {
                        subNode = GetRendererTypeNode("简单");
                        if (subNode != null)
                            treeListRenderer.SetFocusedNode(subNode);
                    }
                    else if (featureRenderer is IUniqueValueRenderer)
                    {
                        subNode = GetRendererTypeNode("唯一值");
                        if (subNode != null)
                            treeListRenderer.SetFocusedNode(subNode);
                    }
                    break;
                case LayerType.RasterLayer:
                    IRasterLayer rasterLayer = layer as IRasterLayer;
                    IRasterRenderer rasterRenderer = rasterLayer.Renderer;

                    subNode = GetRendererTypeNode("唯一值");
                    if (subNode != null)
                        treeListRenderer.SetFocusedNode(subNode);
                    break;
            }

        }

        private TreeListNode GetRendererTypeNode(string rendererType)
        {
            TreeListNode treeListNode = treeListRenderer.Nodes[0];//根结点

            for (int i = 0; i < treeListNode.Nodes.Count; i++)
            {
                TreeListNode subNode = treeListNode.Nodes[i];
                string subNodeRendererType = subNode.GetDisplayText("渲染方式");
                if (subNodeRendererType == rendererType)
                {
                    treeListRenderer.SetFocusedNode(subNode);
                    return subNode;
                }
            }
            return null;
        }

        private void treeListRenderer_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            string rendererType = e.Node.GetDisplayText("渲染方式");

            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    if (rendererType == "简单")
                        xtraTabControl.SelectedTabPage = pageVecetorSimple;
                    else if (rendererType == "唯一值")
                        xtraTabControl.SelectedTabPage = pageVectorUniqueValue;
                    break;
                case LayerType.RasterLayer:
                    if (rendererType == "唯一值")
                        xtraTabControl.SelectedTabPage = pageRasterUniqueValue;
                    break;
            }
        }
        /// <summary>
        /// 初始化渲染信息
        /// </summary>
        private void InitRendererInfo()
        {
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    {
                        IGeoFeatureLayer geoFeatureLayer = layer as IGeoFeatureLayer;
                        IFeatureRenderer featureRenderer = geoFeatureLayer.Renderer;

                        switch (geoFeatureLayer.FeatureClass.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                                CurrentSymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
                                break;
                            case esriGeometryType.esriGeometryPolyline:
                                CurrentSymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassLineSymbols;
                                break;
                            default:
                                CurrentSymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassFillSymbols;
                                break;
                        }

                        //遍历图层字段集,将有效字段加入“渲染字段”中
                        cmbUniqueValueField.Properties.Items.Clear();
                        ITable pTable = geoFeatureLayer as ITable;
                        IFields pFields = pTable.Fields;
                        for (int i = 0; i < pFields.FieldCount; i++)
                        {
                            IField pField = pFields.get_Field(i);
                            if (pField.Type == esriFieldType.esriFieldTypeOID || pField.Type == esriFieldType.esriFieldTypeGeometry)
                                continue;
                            cmbUniqueValueField.Properties.Items.Add(pField.Name);
                        }
                        //初始化渲染页面
                        if (featureRenderer is IUniqueValueRenderer)//唯一值渲染
                        {
                            #region 简单

                            if (simpleRendererSymbol == null)
                            {
                                IRgbColor pRgbColor = new RgbColorClass();
                                Random mRandom = new Random();
                                pRgbColor.Red = mRandom.Next(0, 255);
                                pRgbColor.Green = mRandom.Next(0, 255);
                                pRgbColor.Blue = mRandom.Next(0, 255);
                                switch (CurrentSymbologyStyleClass)
                                {
                                    case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                                        ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                                        pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                                        pSimpleMarkerSymbol.Color = pRgbColor as IColor;
                                        pSimpleMarkerSymbol.Size = 4;
                                        simpleRendererSymbol = pSimpleMarkerSymbol as ISymbol;
                                        break;
                                    case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                                        ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                                        pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                                        pSimpleLineSymbol.Color = pRgbColor as IColor;
                                        simpleRendererSymbol = pSimpleLineSymbol as ISymbol;
                                        break;
                                    case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                                        ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                                        pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                                        pSimpleFillSymbol.Color = pRgbColor as IColor;
                                        simpleRendererSymbol = pSimpleFillSymbol as ISymbol;
                                        break;
                                }
                                Bitmap myBitmap = Common.PreviewItem(simpleRendererSymbol, labelSimplePreview.Width, labelSimplePreview.Height);
                                labelSimplePreview.Appearance.Image = myBitmap;
                            }

                            #endregion

                            #region 唯一值

                            IUniqueValueRenderer uniqueValueRenderer = featureRenderer as IUniqueValueRenderer;
                            //设置渲染字段
                            string field = uniqueValueRenderer.get_Field(0);
                            cmbUniqueValueField.Text = field;
                            //设置颜色带
                            string colorScheme = uniqueValueRenderer.ColorScheme;
                            SelectColorScheme(cmbUniqueValueColorRamp, colorScheme);
                            //设置表格
                            uniqueValueRecordClassList = InitRendererData(uniqueValueRenderer);
                            InitGridControl(gridControlUniqueValue, gridViewUniqueValue, repositoryItemPictureEdit, uniqueValueRecordClassList);

                            #endregion
                        }
                        else//非唯一值渲染
                        {
                            #region 简单

                            ISimpleRenderer simpleRenderer = featureRenderer as ISimpleRenderer;
                            simpleRendererSymbol = simpleRenderer.Symbol;
                            Bitmap myBitmap = Common.PreviewItem(simpleRendererSymbol, labelSimplePreview.Width, labelSimplePreview.Height);
                            labelSimplePreview.Appearance.Image = myBitmap;
                            txtSimpleDescription.Text = simpleRenderer.Label;

                            #endregion

                            #region 唯一值

                            //设置渲染字段
                            cmbUniqueValueField.SelectedIndex = 0;
                            //设置颜色带
                            SelectColorScheme(cmbUniqueValueColorRamp, "");
                            //设置表格
                            uniqueValueRecordClassList = new List<UniqueValueRecordClass>();
                            InitGridControl(gridControlUniqueValue, gridViewUniqueValue, repositoryItemPictureEdit, uniqueValueRecordClassList);

                            #endregion
                        }
                    }
                    break;
                case LayerType.RasterLayer:
                    {
                        CurrentSymbologyStyleClass = esriSymbologyStyleClass.esriStyleClassFillSymbols;

                        IRasterLayer rasterLayer = layer as IRasterLayer;
                        IRasterRenderer rasterRenderer = rasterLayer.Renderer;
                        IAttributeTable attributeTable = rasterLayer as IAttributeTable;
                        ITable table = attributeTable.AttributeTable;
                        IFields fields = table.Fields;

                        for (int i = 0; i < fields.FieldCount; i++)
                        {
                            IField field = fields.get_Field(i);
                            if (field.Type == esriFieldType.esriFieldTypeOID || field.Type == esriFieldType.esriFieldTypeGeometry)
                                continue;
                            if (field.Name.ToUpper() == "COUNT")
                                continue;

                            cmbRasterUniqueValueField.Properties.Items.Add(field.Name);
                        }

                        if (rasterRenderer is IRasterUniqueValueRenderer)
                        {
                            #region 唯一值

                            IRasterUniqueValueRenderer rasterUniqueValueRenderer = rasterRenderer as IRasterUniqueValueRenderer;
                            //设置渲染字段
                            string field = rasterUniqueValueRenderer.Field;
                            cmbRasterUniqueValueField.Text = field;
                            //设置颜色带
                            string colorScheme = rasterUniqueValueRenderer.ColorScheme;
                            SelectColorScheme(cmbRasterUniqueValueColorRamp, colorScheme);
                            //设置表格
                            uniqueValueRecordClassList = InitRasterRendererData(rasterUniqueValueRenderer);
                            InitGridControl(gridControlRasterUniqueValue, gridViewRasterUniqueValue, repositoryItemPictureEditRaster, uniqueValueRecordClassList);

                            #endregion

                        }
                        else
                        {
                            #region 唯一值

                            //设置渲染字段
                            cmbRasterUniqueValueField.SelectedIndex = 0;
                            //设置颜色带
                            SelectColorScheme(cmbRasterUniqueValueColorRamp, "");
                            //设置表格
                            uniqueValueRecordClassList = new List<UniqueValueRecordClass>();
                            InitGridControl(gridControlRasterUniqueValue, gridViewRasterUniqueValue, repositoryItemPictureEditRaster, uniqueValueRecordClassList);

                            #endregion

                        }
                    }
                    break;
            }
        }

        private void btnSimpleChange_Click(object sender, EventArgs e)
        {
            ChangeSymbol(ref simpleRendererSymbol, labelSimplePreview, CurrentSymbologyStyleClass);
        }
        /// <summary>
        /// 选择颜色带
        /// </summary>
        /// <param name="colorScheme"></param>
        private void SelectColorScheme(ImageComboBoxEdit cmbColorRamp, string colorScheme)
        {
            for (int i = 0; i < cmbColorRamp.Properties.Items.Count; i++)
            {
                string colorSchemeValue = cmbColorRamp.Properties.Items[i].Value.ToString();
                if (colorScheme == colorSchemeValue)
                {
                    cmbColorRamp.SelectedIndex = i;
                    return;
                }
            }
            cmbColorRamp.SelectedIndex = 0;
        }
        /// <summary>
        /// 初始化表格控件
        /// </summary>
        private void InitGridControl(GridControl gridControl,GridView gridView,RepositoryItemPictureEdit repositoryItemPictureEdit, List<UniqueValueRecordClass> uniqueValueRecordClassList)
        {
            gridControl.DataSource = uniqueValueRecordClassList;
            gridView.PopulateColumns();
            gridView.Columns[0].ColumnEdit = repositoryItemPictureEdit;
            gridView.Columns[0].Caption = "符号";
            gridView.Columns[0].Width = 10;
            gridView.Columns[1].Caption = "值";
            gridView.Columns[1].Width = 45;
            gridView.Columns[1].OptionsColumn.AllowEdit = false;
            gridView.Columns[2].ColumnEdit = (RepositoryItemTextEdit)gridControlUniqueValue.RepositoryItems["repositoryItemTextEdit"];
            gridView.Columns[2].Caption = "标注";
            gridView.Columns[2].Width = 45;
            gridView.Columns[3].Visible = false;
            gridView.OptionsCustomization.AllowColumnMoving = false;
            gridView.OptionsCustomization.AllowColumnResizing = false;
            gridView.OptionsCustomization.AllowSort = false;
            gridView.OptionsCustomization.AllowFilter = false;
        }
        /// <summary>
        /// 初始化唯一值渲染的数据
        /// </summary>
        /// <returns></returns>
        private List<UniqueValueRecordClass> InitRendererData(IUniqueValueRenderer uniqueValueRenderer)
        {
            List<UniqueValueRecordClass> uniqueValueRecordClassList = new List<UniqueValueRecordClass>();

            for (int i = 0; i < uniqueValueRenderer.ValueCount; i++)
            {
                string value = uniqueValueRenderer.get_Value(i);
                ISymbol symbol = uniqueValueRenderer.get_Symbol(value);
                string label = uniqueValueRenderer.get_Label(value);

                UniqueValueRecordClass uniqueValueRecordClass = new UniqueValueRecordClass(symbol, value, label);
                uniqueValueRecordClassList.Add(uniqueValueRecordClass);
            }

            return uniqueValueRecordClassList;
        }
        /// <summary>
        /// 初始化栅格值一值渲染的数据
        /// </summary>
        /// <returns></returns>
        private List<UniqueValueRecordClass> InitRasterRendererData(IRasterUniqueValueRenderer rasterUniqueValueRenderer)
        {
            List<UniqueValueRecordClass> uniqueValueRecordClassList = new List<UniqueValueRecordClass>();

            int classCount = rasterUniqueValueRenderer.get_ClassCount(0);
            for (int i = 0; i < classCount; i++)
            {
                string value = rasterUniqueValueRenderer.get_Value(0, i, 0).ToString();
                ISymbol symbol = rasterUniqueValueRenderer.get_Symbol(0, i);
                string label = rasterUniqueValueRenderer.get_Label(0, i);

                UniqueValueRecordClass uniqueValueRecordClass = new UniqueValueRecordClass(symbol, value, label);
                uniqueValueRecordClassList.Add(uniqueValueRecordClass);
            }

            return uniqueValueRecordClassList;
        }
        private void repositoryItemPictureEdit_DoubleClick(object sender, EventArgs e)
        {
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    {
                        ISymbol symbol = uniqueValueRecordClassList[gridViewUniqueValue.FocusedRowHandle].Symbol;
                        ChangeSymbol(ref symbol, esriSymbologyStyleClass.esriStyleClassFillSymbols);
                        uniqueValueRecordClassList[gridViewUniqueValue.FocusedRowHandle].Symbol = symbol;
                        gridViewUniqueValue.UpdateGroupSummary();
                    }
                    break;
                case LayerType.RasterLayer:
                    {
                        ISymbol symbol = uniqueValueRecordClassList[gridViewRasterUniqueValue.FocusedRowHandle].Symbol;
                        ChangeSymbol(ref symbol, esriSymbologyStyleClass.esriStyleClassFillSymbols);
                        uniqueValueRecordClassList[gridViewRasterUniqueValue.FocusedRowHandle].Symbol = symbol;
                        gridViewRasterUniqueValue.UpdateGroupSummary();
                    }
                    break;
            }

        }
        /// <summary>
        /// 更改符号,仅返回更改后的符号
        /// </summary>
        /// <param name="Symbol">符号对象</param>
        /// <param name="SymbologyStyleClass">符号样式类型</param>
        private void ChangeSymbol(ref ISymbol Symbol, esriSymbologyStyleClass SymbologyStyleClass)
        {
            FormSymbolSelector mySymbolSelectorForm = new FormSymbolSelector(Symbol, SymbologyStyleClass);
            DialogResult myDialogResult = mySymbolSelectorForm.ShowDialog();
            if (myDialogResult == DialogResult.Cancel)
                return;
            Symbol = mySymbolSelectorForm.ExportSymbol;
        }
        /// <summary>
        /// 更改符号,并将更改后符号绘制到Picture控件
        /// </summary>
        /// <param name="Symbol">符号对象</param>
        /// <param name="label">标注对象</param>
        /// <param name="SymbologyStyleClass">符号样式类型</param>
        private void ChangeSymbol(ref ISymbol Symbol, LabelControl label, esriSymbologyStyleClass SymbologyStyleClass)
        {
            ChangeSymbol(ref Symbol, SymbologyStyleClass);
            label.Appearance.Image = Common.PreviewItem(Symbol, label.Width, label.Height);
        }

        private void btnAddAllValues_Click(object sender, EventArgs e)
        {
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    {
                        if (cmbUniqueValueColorRamp.SelectedIndex == -1)
                        {
                            XtraMessageBox.Show("请先选择配色方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        uniqueValueRecordClassList.Clear();
                        IQueryFilter pQueryFilter = new QueryFilterClass();
                        string ValueField = cmbUniqueValueField.EditValue.ToString();
                        pQueryFilter.AddField(ValueField);
                        IGeoFeatureLayer geoFeatureLayer = layer as IGeoFeatureLayer;
                        ITable pTable = geoFeatureLayer as ITable;
                        ICursor pCursor = pTable.Search(pQueryFilter, true);
                        IRow pRow = pCursor.NextRow();
                        string Value;
                        int FieldIndex = pTable.FindField(ValueField);
                        IList<string> myList = new List<string>();
                        while (pRow != null)
                        {
                            Value = pRow.get_Value(FieldIndex).ToString();
                            if (Value == "")
                                Value = "<Null>";
                            if (myList.Contains(Value))
                            {
                                pRow = pCursor.NextRow();
                                continue;
                            }
                            myList.Add(Value);
                            UniqueValueRecordClass myUniqueValueRecordClass = new UniqueValueRecordClass();
                            myUniqueValueRecordClass.Value = Value;
                            myUniqueValueRecordClass.Label = Value;
                            uniqueValueRecordClassList.Add(myUniqueValueRecordClass);
                            pRow = pCursor.NextRow();
                        }
                        if (gridViewUniqueValue.SelectedRowsCount == 0)
                            gridViewUniqueValue.FocusedRowHandle = 0;
                        cmbColorRamp_SelectedIndexChanged(null, null);
                        btnUniqueRemoveValues.Enabled = true;
                        btnUniqueRemoveAllValues.Enabled = true;
                    }
                    break;
                case LayerType.RasterLayer:
                    {
                        if (cmbRasterUniqueValueColorRamp.SelectedIndex == -1)
                        {
                            XtraMessageBox.Show("请先选择配色方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        uniqueValueRecordClassList.Clear();
                        IQueryFilter pQueryFilter = new QueryFilterClass();
                        string ValueField = cmbRasterUniqueValueField.EditValue.ToString();
                        pQueryFilter.AddField(ValueField);

                        IRasterLayer rasterLayer = layer as IRasterLayer;
                        IRasterRenderer rasterRenderer = rasterLayer.Renderer;
                        IAttributeTable attributeTable = rasterLayer as IAttributeTable;
                        ITable pTable = attributeTable.AttributeTable;
                        ICursor pCursor = pTable.Search(pQueryFilter, true);
                        IRow pRow = pCursor.NextRow();
                        string Value;
                        int FieldIndex = pTable.FindField(ValueField);
                        IList<string> myList = new List<string>();
                        while (pRow != null)
                        {
                            Value = pRow.get_Value(FieldIndex).ToString();
                            if (Value == "")
                                Value = "<Null>";
                            if (myList.Contains(Value))
                            {
                                pRow = pCursor.NextRow();
                                continue;
                            }
                            myList.Add(Value);
                            UniqueValueRecordClass myUniqueValueRecordClass = new UniqueValueRecordClass();
                            myUniqueValueRecordClass.Value = Value;
                            myUniqueValueRecordClass.Label = Value;
                            uniqueValueRecordClassList.Add(myUniqueValueRecordClass);
                            pRow = pCursor.NextRow();
                        }
                        if (gridViewRasterUniqueValue.SelectedRowsCount == 0)
                            gridViewRasterUniqueValue.FocusedRowHandle = 0;
                        cmbColorRamp_SelectedIndexChanged(null, null);
                        btnRasterUniqueRemoveValues.Enabled = true;
                        btnRasterUniqueRemoveAllValues.Enabled = true;
                        break;
                    }
            }
        }

        private void btnAddValues_Click(object sender, EventArgs e)
        {
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    {
                        if (cmbUniqueValueColorRamp.SelectedIndex == -1)
                        {
                            XtraMessageBox.Show("请先选择配色方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        UniqueValueAddValueForm myUniqueValueAddValueForm = new UniqueValueAddValueForm(ValueType.String);
                        DialogResult myDialogResult = myUniqueValueAddValueForm.ShowDialog();
                        if (myDialogResult == DialogResult.Cancel)
                            return;
                        btnUniqueRemoveValues.Enabled = true;
                        btnUniqueRemoveAllValues.Enabled = true;
                        string Value = myUniqueValueAddValueForm.Value;
                        myUniqueValueAddValueForm.Dispose();
                        for (int i = 0; i < gridViewUniqueValue.RowCount; i++)
                        {
                            if (gridViewUniqueValue.GetRowCellDisplayText(i, "Value") == Value)
                                return;
                        }
                        string Label = Value;
                        ISymbol pSymbol = null;
                        InitializeRandomColorSymbol(ref pSymbol, RandomColorStatus.ColorRamp, CurrentSymbologyStyleClass);
                        UniqueValueRecordClass myUniqueValueRecordClass = new UniqueValueRecordClass(pSymbol, Value, Label);
                        uniqueValueRecordClassList.Add(myUniqueValueRecordClass);
                        gridViewUniqueValue.FocusedRowHandle = uniqueValueRecordClassList.Count - 1;
                        gridViewUniqueValue.UpdateGroupSummary();
                    }
                    break;
                case LayerType.RasterLayer:
                    {
                        if (cmbRasterUniqueValueColorRamp.SelectedIndex == -1)
                        {
                            XtraMessageBox.Show("请先选择配色方案！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        UniqueValueAddValueForm myUniqueValueAddValueForm = new UniqueValueAddValueForm(ValueType.Int);
                        DialogResult myDialogResult = myUniqueValueAddValueForm.ShowDialog();
                        if (myDialogResult == DialogResult.Cancel)
                            return;
                        btnRasterUniqueRemoveValues.Enabled = true;
                        btnRasterUniqueRemoveAllValues.Enabled = true;
                        string Value = myUniqueValueAddValueForm.Value;
                        myUniqueValueAddValueForm.Dispose();
                        for (int i = 0; i < gridViewUniqueValue.RowCount; i++)
                        {
                            if (gridViewUniqueValue.GetRowCellDisplayText(i, "Value") == Value)
                                return;
                        }
                        string Label = Value;
                        ISymbol pSymbol = null;
                        InitializeRandomColorSymbol(ref pSymbol, RandomColorStatus.ColorRamp, CurrentSymbologyStyleClass);
                        UniqueValueRecordClass myUniqueValueRecordClass = new UniqueValueRecordClass(pSymbol, Value, Label);
                        uniqueValueRecordClassList.Add(myUniqueValueRecordClass);
                        gridViewRasterUniqueValue.FocusedRowHandle = uniqueValueRecordClassList.Count - 1;
                        gridViewRasterUniqueValue.UpdateGroupSummary();
                    }
                    break;
            }
        }

        private void btnRemoveValues_Click(object sender, EventArgs e)
        {
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    {
                        uniqueValueRecordClassList.RemoveAt(gridViewUniqueValue.FocusedRowHandle);
                        gridViewUniqueValue.UpdateGroupSummary();
                        if (gridViewUniqueValue.RowCount < 1)
                        {
                            btnUniqueRemoveValues.Enabled = false;
                            btnUniqueRemoveAllValues.Enabled = false;
                        }
                        if (gridViewUniqueValue.FocusedRowHandle == gridViewUniqueValue.RowCount)
                        {
                            gridViewUniqueValue.FocusedRowHandle = uniqueValueRecordClassList.Count - 1;
                            return;
                        }
                    }
                    break;
                case LayerType.RasterLayer:
                    {
                        uniqueValueRecordClassList.RemoveAt(gridViewRasterUniqueValue.FocusedRowHandle);
                        gridViewRasterUniqueValue.UpdateGroupSummary();
                        if (gridViewRasterUniqueValue.RowCount < 1)
                        {
                            btnRasterUniqueRemoveValues.Enabled = false;
                            btnRasterUniqueRemoveAllValues.Enabled = false;
                        }
                        if (gridViewRasterUniqueValue.FocusedRowHandle == gridViewRasterUniqueValue.RowCount)
                        {
                            gridViewRasterUniqueValue.FocusedRowHandle = uniqueValueRecordClassList.Count - 1;
                            return;
                        }
                    }
                    break;
            }
        }

        private void btnRemoveAllValues_Click(object sender, EventArgs e)
        {
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    {
                        uniqueValueRecordClassList.Clear();
                        gridViewUniqueValue.UpdateGroupSummary();
                        btnUniqueRemoveValues.Enabled = false;
                        btnUniqueRemoveAllValues.Enabled = false;
                    }
                    break;
                case LayerType.RasterLayer:
                    {
                        uniqueValueRecordClassList.Clear();
                        gridViewRasterUniqueValue.UpdateGroupSummary();
                        btnRasterUniqueRemoveValues.Enabled = false;
                        btnRasterUniqueRemoveAllValues.Enabled = false;
                    }
                    break;
            }
        }
        /// <summary>
        /// 初始化颜色带
        /// </summary>
        private void InitColorScheme()
        {
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    InitializeColorScheme(ref RandomColorRampImageList, 
                        ColorRampsPriority.RandomColorRamps, cmbUniqueValueColorRamp);
                    break;
                case LayerType.RasterLayer:
                    InitializeColorScheme(ref RandomColorRampImageList,
                        ColorRampsPriority.RandomColorRamps, cmbRasterUniqueValueColorRamp);
                    break;
            }
        }
        /// <summary>
        /// 根据颜色带优先权设置的不同初始化配色方案
        /// </summary>
        /// <param name="myImageList">图像列表</param>
        /// <param name="myColorRampsPriority">颜色带优先权枚举</param>
        /// <param name="myImageComboBoxEdit">图像控件</param>
        private void InitializeColorScheme(ref ImageList myImageList, ColorRampsPriority myColorRampsPriority, ImageComboBoxEdit myImageComboBoxEdit)
        {
            //初始化ColorRampImageList
            if (myImageList == null)
            {
                myImageList = new ImageList();
                myImageList.ColorDepth = ColorDepth.Depth32Bit;
                myImageList.ImageSize = new Size(myImageComboBoxEdit.Size.Width - myImageComboBoxEdit.Size.Height, myImageComboBoxEdit.Size.Height);
                if (myColorRampsPriority == ColorRampsPriority.RandomColorRamps)
                {

                    #region 加载随机颜色带

                    myImageList.Images.Add("Pastels", Properties.Resources.Pastels);
                    myImageList.Images.Add("Muted Pastels", Properties.Resources.Muted_Pastels);
                    myImageList.Images.Add("Enamel", Properties.Resources.Enamel);
                    myImageList.Images.Add("Dark Glazes", Properties.Resources.Dark_Glazes);
                    myImageList.Images.Add("Cool Tones", Properties.Resources.Cool_Tones);
                    myImageList.Images.Add("Warm Tones", Properties.Resources.Warm_Tones);
                    myImageList.Images.Add("Pastels Blue to Red", Properties.Resources.Pastels_Blue_to_Red);
                    myImageList.Images.Add("Verdant Tones", Properties.Resources.Verdant_Tones);
                    myImageList.Images.Add("Terra Tones", Properties.Resources.Terra_Tones);
                    myImageList.Images.Add("Basic Random", Properties.Resources.Basic_Random);
                    myImageList.Images.Add("Pastel Terra Tones", Properties.Resources.Pastel_Terra_Tones);
                    myImageList.Images.Add("Reds", Properties.Resources.Reds);
                    myImageList.Images.Add("Oranges", Properties.Resources.Oranges);
                    myImageList.Images.Add("Yellows", Properties.Resources.Yellows);
                    myImageList.Images.Add("Yellow Greens", Properties.Resources.Yellow_Greens);
                    myImageList.Images.Add("Greens", Properties.Resources.Greens);
                    myImageList.Images.Add("Green Blues", Properties.Resources.Green_Blues);
                    myImageList.Images.Add("Cyans", Properties.Resources.Cyans);
                    myImageList.Images.Add("Blues", Properties.Resources.Blues);
                    myImageList.Images.Add("Purple Blues", Properties.Resources.Purple_Blues);
                    myImageList.Images.Add("Purples", Properties.Resources.Purples);
                    myImageList.Images.Add("Purple Reds", Properties.Resources.Purple_Reds);
                    myImageList.Images.Add("Magentas", Properties.Resources.Magentas);
                    myImageList.Images.Add("Warm Grey", Properties.Resources.Warm_Grey);
                    myImageList.Images.Add("Cool Grey", Properties.Resources.Cool_Grey);
                    myImageList.Images.Add("Black and White", Properties.Resources.Black_and_White);

                    #endregion

                    #region 加载渐变颜色带

                    myImageList.Images.Add("Yellow to Dark Red", Properties.Resources.Yellow_to_Dark_Red);
                    myImageList.Images.Add("Blue Light to Dark", Properties.Resources.Blue_Light_to_Dark);
                    myImageList.Images.Add("Purple-Blue Light to Dark", Properties.Resources.Purple_Blue_Light_to_Dark);
                    myImageList.Images.Add("Blue-Green Light to Dark", Properties.Resources.Blue_Green_Light_to_Dark);
                    myImageList.Images.Add("Green Light to Dark", Properties.Resources.Green_Light_to_Dark);
                    myImageList.Images.Add("Purple-Red Light to Dark", Properties.Resources.Purple_Red_Light_to_Dark);
                    myImageList.Images.Add("Red Light to Dark", Properties.Resources.Red_Light_to_Dark);
                    myImageList.Images.Add("Yellow-Green Light to Dark", Properties.Resources.Yellow_Green_Light_to_Dark);
                    myImageList.Images.Add("Gray Light to Dark", Properties.Resources.Gray_Light_to_Dark);
                    myImageList.Images.Add("Brown Light to Dark", Properties.Resources.Brown_Light_to_Dark);
                    myImageList.Images.Add("Orange Light to Dark", Properties.Resources.Orange_Light_to_Dark);
                    myImageList.Images.Add("Blue Bright", Properties.Resources.Blue_Bright);
                    myImageList.Images.Add("Purple-Blue Bright", Properties.Resources.Purple_Blue_Bright);
                    myImageList.Images.Add("Blue-Green Bright", Properties.Resources.Blue_Green_Bright);
                    myImageList.Images.Add("Green Bright", Properties.Resources.Green_Bright);
                    myImageList.Images.Add("Purple Bright", Properties.Resources.Purple_Bright);
                    myImageList.Images.Add("Purple-Red Bright", Properties.Resources.Purple_Red_Bright);
                    myImageList.Images.Add("Red Bright", Properties.Resources.Red_Bright);
                    myImageList.Images.Add("Yellow-Green Bright", Properties.Resources.Yellow_Green_Bright);
                    myImageList.Images.Add("Orange Bright", Properties.Resources.Orange_Bright);
                    myImageList.Images.Add("White to Black", Properties.Resources.White_to_Black);
                    myImageList.Images.Add("Black to White", Properties.Resources.Black_to_White);
                    myImageList.Images.Add("Spectrum-Full Bright", Properties.Resources.Spectrum_Full_Bright);
                    myImageList.Images.Add("Spectrum-Full Light", Properties.Resources.Spectrum_Full_Light);
                    myImageList.Images.Add("Spectrum-Full Dark", Properties.Resources.Spectrum_Full_Dark);
                    myImageList.Images.Add("Red to Green", Properties.Resources.Red_to_Green);
                    myImageList.Images.Add("Cyan to Purple", Properties.Resources.Cyan_to_Purple);
                    myImageList.Images.Add("Yellow to Red", Properties.Resources.Yellow_to_Red);
                    myImageList.Images.Add("Partial Spectrum", Properties.Resources.Partial_Spectrum);
                    myImageList.Images.Add("Cyan-Light to Blue-Dark", Properties.Resources.Cyan_Light_to_Blue_Dark);
                    myImageList.Images.Add("Green to Blue", Properties.Resources.Green_to_Blue);
                    myImageList.Images.Add("Yellow to Green to Dark Blue", Properties.Resources.Yellow_to_Green_to_Dark_Blue);
                    myImageList.Images.Add("Precipitation", Properties.Resources.Precipitation);
                    myImageList.Images.Add("Temperature", Properties.Resources.Temperature);
                    myImageList.Images.Add("Elevation #1", Properties.Resources.Elevation__1);
                    myImageList.Images.Add("Elevation #2", Properties.Resources.Elevation__2);
                    myImageList.Images.Add("Brown to Blue Green Diverging, Bright", Properties.Resources.Brown_to_Blue_Green_Diverging__Bright);
                    myImageList.Images.Add("Brown to Blue Green Diverging, Dark", Properties.Resources.Brown_to_Blue_Green_Diverging__Dark);
                    myImageList.Images.Add("Red to Blue Diverging, Dark", Properties.Resources.Red_to_Blue_Diverging__Dark);
                    myImageList.Images.Add("Red to Blue Diverging, Bright", Properties.Resources.Red_to_Blue_Diverging__Bright);
                    myImageList.Images.Add("Purple to Green Diverging, Dark", Properties.Resources.Purple_to_Green_Diverging__Dark);
                    myImageList.Images.Add("Purple to Green Diverging, Bright", Properties.Resources.Purple_to_Green_Diverging__Bright);
                    myImageList.Images.Add("Partial Spectrum 1 Diverging", Properties.Resources.Partial_Spectrum_1_Diverging);
                    myImageList.Images.Add("Partial Spectrum 2 Diverging", Properties.Resources.Partial_Spectrum_2_Diverging);
                    myImageList.Images.Add("Pink to YellowGreen Diverging, Dark", Properties.Resources.Pink_to_YellowGreen_Diverging__Dark);
                    myImageList.Images.Add("Pink to YellowGreen Diverging, Bright", Properties.Resources.Pink_to_YellowGreen_Diverging__Bright);
                    myImageList.Images.Add("Red to Green Diverging, Dark", Properties.Resources.Red_to_Green_Diverging__Dark);
                    myImageList.Images.Add("Red to Green Diverging, Bright", Properties.Resources.Red_to_Green_Diverging__Bright);
                    myImageList.Images.Add("Distance", Properties.Resources.Distance);
                    myImageList.Images.Add("Surface", Properties.Resources.Surface);
                    myImageList.Images.Add("Slope", Properties.Resources.Slope);
                    myImageList.Images.Add("Aspect", Properties.Resources.Aspect);
                    myImageList.Images.Add("Cold to Hot Diverging", Properties.Resources.Cold_to_Hot_Diverging);

                    #endregion

                }
                else
                {

                    #region 加载渐变颜色带

                    myImageList.Images.Add("Yellow to Dark Red", Properties.Resources.Yellow_to_Dark_Red);
                    myImageList.Images.Add("Blue Light to Dark", Properties.Resources.Blue_Light_to_Dark);
                    myImageList.Images.Add("Purple-Blue Light to Dark", Properties.Resources.Purple_Blue_Light_to_Dark);
                    myImageList.Images.Add("Blue-Green Light to Dark", Properties.Resources.Blue_Green_Light_to_Dark);
                    myImageList.Images.Add("Green Light to Dark", Properties.Resources.Green_Light_to_Dark);
                    myImageList.Images.Add("Purple-Red Light to Dark", Properties.Resources.Purple_Red_Light_to_Dark);
                    myImageList.Images.Add("Red Light to Dark", Properties.Resources.Red_Light_to_Dark);
                    myImageList.Images.Add("Yellow-Green Light to Dark", Properties.Resources.Yellow_Green_Light_to_Dark);
                    myImageList.Images.Add("Gray Light to Dark", Properties.Resources.Gray_Light_to_Dark);
                    myImageList.Images.Add("Brown Light to Dark", Properties.Resources.Brown_Light_to_Dark);
                    myImageList.Images.Add("Orange Light to Dark", Properties.Resources.Orange_Light_to_Dark);
                    myImageList.Images.Add("Blue Bright", Properties.Resources.Blue_Bright);
                    myImageList.Images.Add("Purple-Blue Bright", Properties.Resources.Purple_Blue_Bright);
                    myImageList.Images.Add("Blue-Green Bright", Properties.Resources.Blue_Green_Bright);
                    myImageList.Images.Add("Green Bright", Properties.Resources.Green_Bright);
                    myImageList.Images.Add("Purple Bright", Properties.Resources.Purple_Bright);
                    myImageList.Images.Add("Purple-Red Bright", Properties.Resources.Purple_Red_Bright);
                    myImageList.Images.Add("Red Bright", Properties.Resources.Red_Bright);
                    myImageList.Images.Add("Yellow-Green Bright", Properties.Resources.Yellow_Green_Bright);
                    myImageList.Images.Add("Orange Bright", Properties.Resources.Orange_Bright);
                    myImageList.Images.Add("White to Black", Properties.Resources.White_to_Black);
                    myImageList.Images.Add("Black to White", Properties.Resources.Black_to_White);
                    myImageList.Images.Add("Spectrum-Full Bright", Properties.Resources.Spectrum_Full_Bright);
                    myImageList.Images.Add("Spectrum-Full Light", Properties.Resources.Spectrum_Full_Light);
                    myImageList.Images.Add("Spectrum-Full Dark", Properties.Resources.Spectrum_Full_Dark);
                    myImageList.Images.Add("Red to Green", Properties.Resources.Red_to_Green);
                    myImageList.Images.Add("Cyan to Purple", Properties.Resources.Cyan_to_Purple);
                    myImageList.Images.Add("Yellow to Red", Properties.Resources.Yellow_to_Red);
                    myImageList.Images.Add("Partial Spectrum", Properties.Resources.Partial_Spectrum);
                    myImageList.Images.Add("Cyan-Light to Blue-Dark", Properties.Resources.Cyan_Light_to_Blue_Dark);
                    myImageList.Images.Add("Green to Blue", Properties.Resources.Green_to_Blue);
                    myImageList.Images.Add("Yellow to Green to Dark Blue", Properties.Resources.Yellow_to_Green_to_Dark_Blue);
                    myImageList.Images.Add("Precipitation", Properties.Resources.Precipitation);
                    myImageList.Images.Add("Temperature", Properties.Resources.Temperature);
                    myImageList.Images.Add("Elevation #1", Properties.Resources.Elevation__1);
                    myImageList.Images.Add("Elevation #2", Properties.Resources.Elevation__2);
                    myImageList.Images.Add("Brown to Blue Green Diverging, Bright", Properties.Resources.Brown_to_Blue_Green_Diverging__Bright);
                    myImageList.Images.Add("Brown to Blue Green Diverging, Dark", Properties.Resources.Brown_to_Blue_Green_Diverging__Dark);
                    myImageList.Images.Add("Red to Blue Diverging, Dark", Properties.Resources.Red_to_Blue_Diverging__Dark);
                    myImageList.Images.Add("Red to Blue Diverging, Bright", Properties.Resources.Red_to_Blue_Diverging__Bright);
                    myImageList.Images.Add("Purple to Green Diverging, Dark", Properties.Resources.Purple_to_Green_Diverging__Dark);
                    myImageList.Images.Add("Purple to Green Diverging, Bright", Properties.Resources.Purple_to_Green_Diverging__Bright);
                    myImageList.Images.Add("Partial Spectrum 1 Diverging", Properties.Resources.Partial_Spectrum_1_Diverging);
                    myImageList.Images.Add("Partial Spectrum 2 Diverging", Properties.Resources.Partial_Spectrum_2_Diverging);
                    myImageList.Images.Add("Pink to YellowGreen Diverging, Dark", Properties.Resources.Pink_to_YellowGreen_Diverging__Dark);
                    myImageList.Images.Add("Pink to YellowGreen Diverging, Bright", Properties.Resources.Pink_to_YellowGreen_Diverging__Bright);
                    myImageList.Images.Add("Red to Green Diverging, Dark", Properties.Resources.Red_to_Green_Diverging__Dark);
                    myImageList.Images.Add("Red to Green Diverging, Bright", Properties.Resources.Red_to_Green_Diverging__Bright);
                    myImageList.Images.Add("Distance", Properties.Resources.Distance);
                    myImageList.Images.Add("Surface", Properties.Resources.Surface);
                    myImageList.Images.Add("Slope", Properties.Resources.Slope);
                    myImageList.Images.Add("Aspect", Properties.Resources.Aspect);
                    myImageList.Images.Add("Cold to Hot Diverging", Properties.Resources.Cold_to_Hot_Diverging);

                    #endregion

                    #region 加载随机颜色带

                    myImageList.Images.Add("Pastels", Properties.Resources.Pastels);
                    myImageList.Images.Add("Muted Pastels", Properties.Resources.Muted_Pastels);
                    myImageList.Images.Add("Enamel", Properties.Resources.Enamel);
                    myImageList.Images.Add("Dark Glazes", Properties.Resources.Dark_Glazes);
                    myImageList.Images.Add("Cool Tones", Properties.Resources.Cool_Tones);
                    myImageList.Images.Add("Warm Tones", Properties.Resources.Warm_Tones);
                    myImageList.Images.Add("Pastels Blue to Red", Properties.Resources.Pastels_Blue_to_Red);
                    myImageList.Images.Add("Verdant Tones", Properties.Resources.Verdant_Tones);
                    myImageList.Images.Add("Terra Tones", Properties.Resources.Terra_Tones);
                    myImageList.Images.Add("Basic Random", Properties.Resources.Basic_Random);
                    myImageList.Images.Add("Pastel Terra Tones", Properties.Resources.Pastel_Terra_Tones);
                    myImageList.Images.Add("Reds", Properties.Resources.Reds);
                    myImageList.Images.Add("Oranges", Properties.Resources.Oranges);
                    myImageList.Images.Add("Yellows", Properties.Resources.Yellows);
                    myImageList.Images.Add("Yellow Greens", Properties.Resources.Yellow_Greens);
                    myImageList.Images.Add("Greens", Properties.Resources.Greens);
                    myImageList.Images.Add("Green Blues", Properties.Resources.Green_Blues);
                    myImageList.Images.Add("Cyans", Properties.Resources.Cyans);
                    myImageList.Images.Add("Blues", Properties.Resources.Blues);
                    myImageList.Images.Add("Purple Blues", Properties.Resources.Purple_Blues);
                    myImageList.Images.Add("Purples", Properties.Resources.Purples);
                    myImageList.Images.Add("Purple Reds", Properties.Resources.Purple_Reds);
                    myImageList.Images.Add("Magentas", Properties.Resources.Magentas);
                    myImageList.Images.Add("Warm Grey", Properties.Resources.Warm_Grey);
                    myImageList.Images.Add("Cool Grey", Properties.Resources.Cool_Grey);
                    myImageList.Images.Add("Black and White", Properties.Resources.Black_and_White);

                    #endregion

                }
            }
            //初始化ColorRamp内容
            myImageComboBoxEdit.Properties.SmallImages = myImageList;
            foreach (string Key in myImageList.Images.Keys)
            {
                ImageComboBoxItem myImageComboBoxItem = new ImageComboBoxItem();
                myImageComboBoxItem.Value = Key;
                myImageComboBoxItem.ImageIndex = myImageList.Images.IndexOfKey(Key);
                myImageComboBoxEdit.Properties.Items.Add(myImageComboBoxItem);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLayerPropertiesOK_Click(object sender, EventArgs e)
        {
            switch (xtraTabControl.SelectedTabPage.Name)
            {
                case "pageVecetorSimple":
                    {
                        IGeoFeatureLayer geoFeatureLayer = layer as IGeoFeatureLayer;
                        IFeatureRenderer featureRenderer = geoFeatureLayer.Renderer;
                        ISimpleRenderer simpleRenderer = null;
                        if (featureRenderer is ISimpleRenderer)
                            simpleRenderer = featureRenderer as ISimpleRenderer;
                        else
                            simpleRenderer = new SimpleRendererClass();

                        simpleRenderer.Symbol = simpleRendererSymbol;
                        simpleRenderer.Label = txtSimpleDescription.Text;
                        geoFeatureLayer.Renderer = simpleRenderer as IFeatureRenderer;
                    }
                    break;
                case "pageVectorUniqueValue":
                    {
                        IGeoFeatureLayer geoFeatureLayer = layer as IGeoFeatureLayer;
                        IFeatureRenderer featureRenderer = geoFeatureLayer.Renderer;
                        IUniqueValueRenderer uniqueValueRenderer = null;
                        if (featureRenderer is IUniqueValueRenderer)
                            uniqueValueRenderer = featureRenderer as IUniqueValueRenderer;
                        else
                            uniqueValueRenderer = new UniqueValueRendererClass();
                        //移除所有值
                        uniqueValueRenderer.RemoveAllValues();
                        //写入唯一值表格中各项内容
                        for (int i = 0; i < uniqueValueRecordClassList.Count; i++)
                        {
                            UniqueValueRecordClass myUniqueValueRecordClass = uniqueValueRecordClassList[i];
                            uniqueValueRenderer.AddValue(myUniqueValueRecordClass.Value, "", myUniqueValueRecordClass.Symbol);
                            uniqueValueRenderer.set_Label(myUniqueValueRecordClass.Value, myUniqueValueRecordClass.Label);
                        }
                        //写入唯一值字段、配色方案
                        uniqueValueRenderer.FieldCount = 1;
                        uniqueValueRenderer.set_Field(0, cmbUniqueValueField.Text);
                        uniqueValueRenderer.ColorScheme = cmbUniqueValueColorRamp.Properties.Items[cmbUniqueValueColorRamp.SelectedIndex].Value.ToString();

                        geoFeatureLayer.Renderer = uniqueValueRenderer as IFeatureRenderer;
                    }
                    break;
                case "pageRasterUniqueValue":
                    {
                        IRasterLayer rasterLayer = layer as IRasterLayer;
                        IRasterRenderer rasterRenderer = rasterLayer.Renderer;
                        IRasterUniqueValueRenderer rasterUniqueValueRenderer = null;
                        if (rasterRenderer is IRasterUniqueValueRenderer)
                            rasterUniqueValueRenderer = rasterRenderer as IRasterUniqueValueRenderer;
                        else
                            rasterUniqueValueRenderer = new RasterUniqueValueRendererClass();
                        //移除所有值
                        for (int i = 0; i < rasterUniqueValueRenderer.HeadingCount; i++)
                        {
                            int classCount = rasterUniqueValueRenderer.get_ClassCount(i);
                            for (int j = 0; j < classCount; j++)
                                rasterUniqueValueRenderer.RemoveValues(i, j);
                        }
                        //写入唯一值表格中各项内容
                        rasterUniqueValueRenderer.HeadingCount = 1;
                        rasterUniqueValueRenderer.set_ClassCount(0, uniqueValueRecordClassList.Count);
                        for (int i = 0; i < uniqueValueRecordClassList.Count; i++)
                        {
                            UniqueValueRecordClass myUniqueValueRecordClass = uniqueValueRecordClassList[i];
                            rasterUniqueValueRenderer.AddValue(0, i, myUniqueValueRecordClass.Value);
                            rasterUniqueValueRenderer.set_Label(0, i, myUniqueValueRecordClass.Label);
                            rasterUniqueValueRenderer.set_Symbol(0, i, myUniqueValueRecordClass.Symbol);
                        }
                        //写入唯一值字段、配色方案
                        rasterUniqueValueRenderer.Field = cmbRasterUniqueValueField.Text;
                        if (cmbRasterUniqueValueColorRamp.SelectedIndex == -1)
                            rasterUniqueValueRenderer.ColorScheme = "";
                        else
                            rasterUniqueValueRenderer.ColorScheme = cmbRasterUniqueValueColorRamp.Properties.Items[cmbRasterUniqueValueColorRamp.SelectedIndex].Value.ToString();

                        rasterLayer.Renderer = rasterUniqueValueRenderer as IRasterRenderer;
                    }
                    break;
            }

            DialogResult = DialogResult.OK;
        }

        private void cmbColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uniqueValueRecordClassList == null || uniqueValueRecordClassList.Count == 0)
                return;

            ImageComboBoxEdit cmbColorRamp = null;
            switch (layerType)
            {
                case LayerType.FeatureLayer:
                    cmbColorRamp = cmbUniqueValueColorRamp;
                    break;
                case LayerType.RasterLayer:
                    cmbColorRamp = cmbRasterUniqueValueColorRamp;
                    break;
            }

            int ColorSize = uniqueValueRecordClassList.Count;
            CreateEnumColorsFromColorRamps(cmbColorRamp, ColorSize);
            CurrentEnumColors.Reset();
            for (int i = 0; i < uniqueValueRecordClassList.Count; i++)
            {
                IColor pColor = CurrentEnumColors.Next();
                switch (CurrentSymbologyStyleClass)
                {
                    case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                        IMarkerSymbol pMarkerSymbol;
                        if (uniqueValueRecordClassList[i].Symbol == null)
                        {
                            pMarkerSymbol = new SimpleMarkerSymbol();
                            pMarkerSymbol.Size = 4;
                        }
                        else
                            pMarkerSymbol = uniqueValueRecordClassList[i].Symbol as IMarkerSymbol;
                        pMarkerSymbol.Color = pColor;
                        uniqueValueRecordClassList[i].Symbol = pMarkerSymbol as ISymbol;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                        ILineSymbol pLineSymbol;
                        if (uniqueValueRecordClassList[i].Symbol == null)
                            pLineSymbol = new SimpleLineSymbol();
                        else
                            pLineSymbol = uniqueValueRecordClassList[i].Symbol as ILineSymbol;
                        pLineSymbol.Color = pColor;
                        uniqueValueRecordClassList[i].Symbol = pLineSymbol as ISymbol;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                        IFillSymbol pFillSymbol;
                        if (uniqueValueRecordClassList[i].Symbol == null)
                            pFillSymbol = new SimpleFillSymbolClass();
                        else
                            pFillSymbol = uniqueValueRecordClassList[i].Symbol as IFillSymbol;
                        pFillSymbol.Color = pColor;
                        uniqueValueRecordClassList[i].Symbol = pFillSymbol as ISymbol;
                        break;
                }
            }
            gridViewUniqueValue.UpdateGroupSummary();
            gridViewRasterUniqueValue.UpdateGroupSummary();

        }
        /// <summary>
        /// 从颜色带产生枚举颜色，用于进行渲染
        /// </summary>
        /// <param name="mImageComboBoxEdit">颜色带控件</param>
        /// <param name="ColorSize">颜色数量</param>
        private void CreateEnumColorsFromColorRamps(ImageComboBoxEdit mImageComboBoxEdit, int ColorSize)
        {
            ImageComboBoxItem myImageComboBoxItem = mImageComboBoxEdit.Properties.Items[mImageComboBoxEdit.SelectedIndex];
            IStyleGalleryItem pStyleGalleryItem = MatchScheme(myImageComboBoxItem);
            IColorRamp pColorRamp = pStyleGalleryItem.Item as IColorRamp;
            //IColorRamp.CreateRamp要求颜色数量必须大于1
            if (ColorSize == 1)
                ColorSize = 2;
            pColorRamp.Size = ColorSize;
            bool OK;
            pColorRamp.CreateRamp(out OK);
            CurrentEnumColors = pColorRamp.Colors;
        }
        /// <summary>
        /// 匹配颜色带，用于通过颜色带的名称返回实际颜色带对象
        /// </summary>
        /// <param name="myImageComboBoxItem">图像组合框子项</param>
        /// <returns>样式条目对象</returns>
        private IStyleGalleryItem MatchScheme(ImageComboBoxItem myImageComboBoxItem)
        {
            string ColorScheme = myImageComboBoxItem.Value.ToString();
            IStyleGalleryStorage pStyleGalleryStorage = new ServerStyleGalleryClass();
            string styleFilename = Application.StartupPath + @"\Styles\ESRI.ServerStyle";
            pStyleGalleryStorage.AddFile(styleFilename);
            pStyleGallery = pStyleGalleryStorage as IStyleGallery;
            IEnumStyleGalleryItem pEnumStyleGalleryItem = pStyleGallery.get_Items("Color Ramps", styleFilename, "");

            pEnumStyleGalleryItem.Reset();
            IStyleGalleryItem pStyleGalleryItem = pEnumStyleGalleryItem.Next();
            while (pStyleGalleryItem != null)
            {
                if (pStyleGalleryItem.Name == ColorScheme)
                    break;
                pStyleGalleryItem = pEnumStyleGalleryItem.Next();
            }
            Marshal.ReleaseComObject(pEnumStyleGalleryItem);
            return pStyleGalleryItem;
        }
        /// <summary>
        /// 根据当前随机颜色状态及符号样式初始化随机颜色符号对象
        /// </summary>
        /// <param name="Symbol">符号对象</param>
        /// <param name="CurrentRandomColorStatus">随机颜色状态</param>
        /// <param name="SymbologyStyleClass">符号样式类型</param>
        private void InitializeRandomColorSymbol(ref ISymbol Symbol, RandomColorStatus CurrentRandomColorStatus, esriSymbologyStyleClass SymbologyStyleClass)
        {
            if (Symbol == null)
            {
                if (CurrentEnumColors == null)
                    cmbColorRamp_SelectedIndexChanged(null, null);

                IColor pColor = null;
                switch (CurrentRandomColorStatus)
                {
                    case RandomColorStatus.HsvColor:
                        IHsvColor pHsvColor = new HsvColorClass();
                        Random mRandom = new Random();
                        pHsvColor.Hue = mRandom.Next(0, 360);
                        pHsvColor.Saturation = 25;
                        pHsvColor.Value = 100;
                        pColor = pHsvColor as IColor;
                        break;
                    case RandomColorStatus.ColorRamp:
                        pColor = CurrentEnumColors.Next();
                        if (pColor == null)
                        {
                            CurrentEnumColors.Reset();
                            pColor = CurrentEnumColors.Next();
                        }
                        break;
                }
                switch (SymbologyStyleClass)
                {
                    case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                        ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                        pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                        pSimpleMarkerSymbol.Color = pColor;
                        pSimpleMarkerSymbol.Size = 4;
                        pSimpleMarkerSymbol.Outline = true;
                        Symbol = pSimpleMarkerSymbol as ISymbol;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                        ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                        pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                        pSimpleLineSymbol.Color = pColor;
                        Symbol = pSimpleLineSymbol as ISymbol;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                        ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                        pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                        pSimpleFillSymbol.Color = pColor;
                        Symbol = pSimpleFillSymbol as ISymbol;
                        break;
                }
            }
        }

        private void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uniqueValueRecordClassList != null)
            {
                btnRemoveAllValues_Click(null, null);
                btnAddAllValues_Click(null, null);
            }
        }
    }
}