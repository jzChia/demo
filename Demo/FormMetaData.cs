using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;
using Demo.Entities;
using Demo.Utility;

namespace Demo
{
    public partial class FormMetaData : Form
    {
        public string DataSource = "";
        public IFeatureLayer pFeatureLayer;
        public IRasterLayer pRasterLayer;

        public Demo.Entities.RasterLayer rstLayer;
        public VectorLayer vctLayer;
        public FileLayer fleLayer;

        /// <summary>
        /// 0 栅格 1 矢量 2 文件
        /// </summary>
        public int Mode = -1;

        #region 构造函数
        public FormMetaData()
        {
            InitializeComponent();
        }

        public FormMetaData(ILayer pL)
        {
            InitializeComponent();
            pRasterLayer = pL as IRasterLayer;
            pFeatureLayer = pL as IFeatureLayer;

            if (pRasterLayer != null)
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
                Mode = 0;
                GetRasterMeta();
            }
            else if (pFeatureLayer != null)
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
                Mode = 1;
                GetVectorMeta();
            }
            else
            {
                Mode = -1;
            }
        }

        public FormMetaData(string path)
        {
            InitializeComponent();

            if (System.IO.File.Exists(path))
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage3;
                Mode = 2;
                textBoxFFolder.Text = "false";
            }
            else if (System.IO.Directory.Exists(path))
            {
                xtraTabControl1.SelectedTabPage = xtraTabPage3;
                Mode = 2;
                textBoxFFolder.Text = "true";
            }
            else
            {
                Mode = -1;
            }
        }

        public FormMetaData(Demo.Entities.RasterLayer RL)
        {
            InitializeComponent();
            Raster2Control(RL);
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            Mode = 0;
        }

        public FormMetaData(VectorLayer VL)
        {
            InitializeComponent();
            Vector2Control(VL);
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
            Mode = 1;
        }

        public FormMetaData(FileLayer FL)
        {
            InitializeComponent();
            File2Control(FL);
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            Mode = 2;
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Mode == -1)
            {
                MessageBox.Show("导入数据有误，请核实");
                this.Close();
            }
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            string datasourcetext = comboBoxDatasource.Text;
            comboBoxDatasource.DataSource = Demo.Utility.DataSource.getDataSource();
            List<Area> areas = Area.getNationList();

            for (int i = 0; i < areas.Count; i++)
            {
                this.treeComboBoxArea.Nodes.Add(areas[i].areacode, areas[i].areaname);
                List<Area> areap = Area.getProvinceList(areas[i].areacode);
                for (int j = 0; j < areap.Count; j++)
                {
                    this.treeComboBoxArea.Nodes[i].Nodes.Add(areap[j].areacode, areap[j].areaname);
                }
            }
            comboBoxDatasource.Text = datasourcetext;
        }

        /// <summary>
        /// 获取栅格元数据
        /// </summary>
        private void GetRasterMeta()
        {
            textBoxLayerName.Text = pRasterLayer.Name;
            textBoxDataType.Text = "栅格数据";
            textBoxCreateTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh时mm分ss秒");

            IRaster pRaster = pRasterLayer.Raster;
            IRasterDataset pRasterDataset = new RasterDatasetClass();
            IRasterProps pRasterProps = (IRasterProps)pRaster;

            textBoxRHeight.Text = pRasterProps.Height.ToString();
            textBoxProjection.Text = pRasterProps.SpatialReference.Name;
            textBoxRWidth.Text = pRasterProps.Width.ToString();

            //投影到经纬度
            IEnvelope pExtent = pRasterProps.Extent;
            try
            {
                ISpatialReferenceFactory spatialrefFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference pSR = spatialrefFactory.CreateGeographicCoordinateSystem((int)(esriSRGeoCSType.esriSRGeoCS_WGS1984));
                pExtent.Project(pSR);
            }
            catch { MessageBox.Show("输入文件空间参考有误"); }
            textBoxRMinX.Text = pExtent.XMin.ToString();
            textBoxRMinY.Text = pExtent.YMin.ToString();
            textBoxRMaxX.Text = pExtent.XMax.ToString();
            textBoxRMaxY.Text = pExtent.YMax.ToString();

            IPnt pPnt = pRasterProps.MeanCellSize();
            textBoxRResolution.Text = Math.Min(pPnt.X, pPnt.Y).ToString();

            try
            {
                object nodata = pRasterProps.NoDataValue;
                if (nodata.GetType().IsArray)
                {
                    Array a = ((Array)nodata);
                    textBoxRNoDataValue.Text = a.GetValue(0).ToString();
                }
            }
            catch { MessageBox.Show("无效值获取失败"); }
            textBoxRBandCount.Text = pRasterLayer.BandCount.ToString();

        }

        /// <summary>
        /// 获取矢量元数据
        /// </summary>
        private void GetVectorMeta()
        {
            textBoxLayerName.Text = pFeatureLayer.Name;
            textBoxDataType.Text = "矢量数据";
            textBoxCreateTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh时mm分ss秒");

            //投影到经纬度
            IEnvelope pExtent = pFeatureLayer.AreaOfInterest;
            try
            {
                ISpatialReferenceFactory spatialrefFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference pSR = spatialrefFactory.CreateGeographicCoordinateSystem((int)(esriSRGeoCSType.esriSRGeoCS_WGS1984));
                pExtent.Project(pSR);
            }
            catch { MessageBox.Show("输入文件空间参考有误"); }
            textBoxVMinX.Text = pExtent.XMin.ToString();
            textBoxVMinY.Text = pExtent.YMin.ToString();
            textBoxVMaxX.Text = pExtent.XMax.ToString();
            textBoxVMaxY.Text = pExtent.YMax.ToString();

            textBoxVShape.Text = pFeatureLayer.FeatureClass.ShapeType.ToString();
            textBoxProjection.Text = (pFeatureLayer.FeatureClass as IGeoDataset).SpatialReference.Name;
        }

        private void Base2Control(BaseLayer BL)
        {
            if (BL == null) return;
            textBoxAuthor.Text = BL.AUTHOR;
            textBoxScale.Text = BL.SCALE.ToString();
            textBoxCreateTime.Text = BL.CREATETIME;
            textBoxDataType.Text = BL.DATATYPE;
            textBoxDescription.Text = BL.DESCRIPTION;
            textBoxLayerName.Text = BL.LAYERNAME;
            textBoxMapNum.Text = BL.MAPNUM;
            textBoxProjection.Text = BL.PROJECTION;
            comboBoxDatasource.Text = BL.DATASOURCES;
            treeComboBoxArea.areacodetext = BL.REGION;
            dateTimePickerDatagetdate.Value = BL.DATAGETDATE;
            textBoxDataformat.Text = BL.DATAFORMAT;
            treeComboBoxArea.Text = Area.getArea(BL.REGION).areaname;
        }

        private void Control2Base(out Demo.Entities.RasterLayer BL)
        {
            BL = new Demo.Entities.RasterLayer();
            BL.SCALE = Convert.ToInt64(textBoxScale.Text);
            BL.AUTHOR = textBoxAuthor.Text;
            BL.CREATETIME = textBoxCreateTime.Text;
            BL.DATATYPE = textBoxDataType.Text;
            BL.DESCRIPTION = textBoxDescription.Text;
            BL.LAYERNAME = textBoxLayerName.Text;
            BL.MAPNUM = textBoxMapNum.Text;
            BL.PROJECTION = textBoxProjection.Text;
            BL.DATAFORMAT = textBoxDataformat.Text;
            BL.DATAGETDATE = dateTimePickerDatagetdate.Value;
            BL.DATASOURCES = comboBoxDatasource.Text;
            BL.REGION = treeComboBoxArea.areacodetext.PadRight(9, '0');
        }
        private void Control2Base(out VectorLayer BL)
        {
            BL = new VectorLayer();
            BL.SCALE = Convert.ToInt64(textBoxScale.Text);
            BL.AUTHOR = textBoxAuthor.Text;
            BL.CREATETIME = textBoxCreateTime.Text;
            BL.DATATYPE = textBoxDataType.Text;
            BL.DESCRIPTION = textBoxDescription.Text;
            BL.LAYERNAME = textBoxLayerName.Text;
            BL.MAPNUM = textBoxMapNum.Text;
            BL.PROJECTION = textBoxProjection.Text;
            BL.DATAFORMAT = textBoxDataformat.Text;
            BL.DATAGETDATE = dateTimePickerDatagetdate.Value;
            BL.DATASOURCES = comboBoxDatasource.Text;
            BL.REGION = treeComboBoxArea.areacodetext.PadRight(9, '0');
        }
        private void Control2Base(out FileLayer BL)
        {
            BL = new FileLayer();
            BL.SCALE = Convert.ToInt64(textBoxScale.Text);
            BL.AUTHOR = textBoxAuthor.Text;
            BL.CREATETIME = textBoxCreateTime.Text;
            BL.DATATYPE = textBoxDataType.Text;
            BL.DESCRIPTION = textBoxDescription.Text;
            BL.LAYERNAME = textBoxLayerName.Text;
            BL.MAPNUM = textBoxMapNum.Text;
            BL.PROJECTION = textBoxProjection.Text;
            BL.DATAFORMAT = textBoxDataformat.Text;
            BL.DATAGETDATE = dateTimePickerDatagetdate.Value;
            BL.DATASOURCES = comboBoxDatasource.Text;
            BL.REGION = treeComboBoxArea.areacodetext.PadRight(9, '0');
        }

        private void Raster2Control(Demo.Entities.RasterLayer RL)
        {
            Base2Control((BaseLayer)RL);

            textBoxRBandCount.Text = RL.BANDCOUNT.ToString();
            textBoxRHeight.Text = RL.HEIGHT.ToString();
            textBoxRVisible.Text = RL.ISVISIBLE.ToString();
            textBoxRMaxX.Text = RL.MAXX.ToString();
            textBoxRMaxY.Text = RL.MAXY.ToString();
            textBoxRMinX.Text = RL.MINX.ToString();
            textBoxRMinY.Text = RL.MINY.ToString();
            textBoxRNoDataValue.Text = RL.NODATAVALUE.ToString();
            textBoxRWidth.Text = RL.WIDTH.ToString();
            textBoxRResolution.Text = RL.RESOLUTION.ToString();

        }

        private void Control2Raster(out Demo.Entities.RasterLayer RL)
        {
            RL = new Demo.Entities.RasterLayer();
            Control2Base(out RL);
            if (RL == null) return;

            RL.BANDCOUNT = Convert.ToInt64(textBoxRBandCount.Text);
            RL.HEIGHT = Convert.ToInt64(textBoxRHeight.Text);
            RL.ISVISIBLE = Convert.ToBoolean(textBoxRVisible.Text);
            RL.MAXX = Convert.ToSingle(textBoxRMaxX.Text);
            RL.MAXY = Convert.ToSingle(textBoxRMaxY.Text);
            RL.MINX = Convert.ToSingle(textBoxRMinX.Text);
            RL.MINY = Convert.ToSingle(textBoxRMinY.Text);
            RL.NODATAVALUE = Convert.ToSingle(textBoxRNoDataValue.Text);
            RL.WIDTH = Convert.ToInt64(textBoxRWidth.Text);
            RL.RESOLUTION = Convert.ToSingle(textBoxRResolution.Text);
        }

        private void Vector2Control(VectorLayer VL)
        {
            Base2Control((BaseLayer)VL);

            textBoxVVisible.Text = VL.ISVISIBLE.ToString();
            textBoxVMaxX.Text = VL.MAXX.ToString();
            textBoxVMaxY.Text = VL.MAXY.ToString();
            textBoxVMinX.Text = VL.MINX.ToString();
            textBoxVMinY.Text = VL.MINY.ToString();
            textBoxVShape.Text = VL.SHAPE;
        }

        private void Control2Vector(out VectorLayer VL)
        {
            VL = new VectorLayer();
            Control2Base(out VL);
            if (VL == null) return;

            VL.ISVISIBLE = Convert.ToBoolean(textBoxVVisible.Text);
            VL.MAXX = Convert.ToSingle(textBoxVMaxX.Text);
            VL.MAXY = Convert.ToSingle(textBoxVMaxY.Text);
            VL.MINX = Convert.ToSingle(textBoxVMinX.Text);
            VL.MINY = Convert.ToSingle(textBoxVMinY.Text);
            VL.SHAPE = textBoxVShape.Text;
        }

        private void File2Control(FileLayer FL)
        {
            Base2Control((BaseLayer)FL);

            textBoxFFolder.Text = FL.ISFOLDER.ToString();
            textBoxFOpenAs.Text = FL.OPENAS;
        }

        private void Control2File(out FileLayer FL)
        {
            FL = new FileLayer();
            Control2Base(out FL);
            if (FL == null) return;

            FL.ISFOLDER = Convert.ToBoolean(textBoxFFolder.Text);
            FL.OPENAS = textBoxFOpenAs.Text;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "xml文件|*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fname = ofd.FileName;
                if (Mode == 0)
                {
                    Demo.Entities.RasterLayer rl = xmlSerialize.DeSerialize<Demo.Entities.RasterLayer>(fname);
                    Raster2Control(rl);
                }
                else if (Mode == 1)
                {
                    VectorLayer vl = xmlSerialize.DeSerialize<VectorLayer>(fname);
                    Vector2Control(vl);
                }
                else if (Mode == 2)
                {
                    FileLayer fl = xmlSerialize.DeSerialize<FileLayer>(fname);
                    File2Control(fl);
                }
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            string errMSG = "";
            if (DataVerify(out errMSG) || MessageBox.Show(errMSG + "\n是否忽略？", "元数据完整性校验", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Mode == 0)
                {
                    Control2Raster(out rstLayer);
                }
                else if (Mode == 1)
                {
                    Control2Vector(out vctLayer);
                }
                else if (Mode == 2)
                {
                    Control2File(out fleLayer);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {

            }
        }

        private bool DataVerify(out string errMSG)
        {
            errMSG = "";
            bool verified = true;
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is TextBox && ((TextBox)control).Text == "")
                {
                    errMSG += ((TextBox)control).Tag.ToString() + "为空；\n";
                    verified = false;
                }
            }
            return verified;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "xml 文件|*.xml";

            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filename = sfd.FileName;
                if (!filename.EndsWith(".xml"))
                {
                    filename += ".xml";
                }
                if (Mode == 0)
                {
                    Demo.Entities.RasterLayer RL = new Demo.Entities.RasterLayer();
                    Control2Raster(out RL);

                    xmlSerialize.Serialize<Demo.Entities.RasterLayer>(RL, filename);
                }
                else if (Mode == 1)
                {
                    VectorLayer vl = new VectorLayer();
                    Control2Vector(out vl);
                    xmlSerialize.Serialize<VectorLayer>(vl, filename);
                }
                else if (Mode == 2)
                {
                    FileLayer fl = new FileLayer();
                    Control2File(out fl);
                    xmlSerialize.Serialize<FileLayer>(fl, filename);
                }
                else
                {
                    MessageBox.Show("未知文件类型,请重新导出！");
                }
            }

        }

    }
}
