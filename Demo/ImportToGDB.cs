using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;
using Demo.Entities;

namespace Demo
{
    public enum enumDataType
    { 
        raster=0,
        dem=1,
        vector=2,
        threeD=3,
        test=4,
        other=5
    };
    public partial class ImportToGDB : Form
    {
        public int nowDTnum;
        public enumDataType nowDT;

        public Demo.Entities.RasterLayer rstLayer;
        public VectorLayer vctLayer;
        public FileLayer fleLayer;

        public ImportToGDB()
        {
            InitializeComponent();
        }

        public ImportToGDB(int dataType)
        {
            if (dataType > 5) return;

            nowDTnum = dataType;
            nowDT = (enumDataType)dataType;

            InitializeComponent();

            comboBox1.SelectedIndex = dataType;
            comboBox2.SelectedIndex = 1;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            switch (nowDT)
            { 
                case enumDataType.raster:
                    ofd.Filter = "栅格数据|*.tif;*.img;*.pix;*.bmp;*.jpg";
                    break;
                case enumDataType.dem:
                    ofd.Filter = "地形数据|*.tif;*.img;*.pix;*.grd;*.dem";
                    break;
                case enumDataType.vector:
                    ofd.Filter = "矢量数据|*.shp;*.e00;*.dwg;*.bmp;*.mif";
                    break;
                default:
                    ofd.Filter="数据|*.*";
                    break;
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FormMetaData frmMD;

                textBoxImport.Text = ofd.FileName;
                ILayer pLayer = GetILayerFromPath(ofd.FileName);
                if (pLayer != null)
                {
                    frmMD = new FormMetaData(pLayer);
                }
                else
                {
                    frmMD = new FormMetaData(ofd.FileName);
                }

                if (frmMD.ShowDialog() == DialogResult.OK)
                {
                    this.rstLayer = frmMD.rstLayer;
                    this.vctLayer = frmMD.vctLayer;
                    this.fleLayer = frmMD.fleLayer;
                }
            }
        }

        public string InputPath 
        {
            get 
            {
                return this.textBoxImport.Text;
            }
        }

        public string OutputPath 
        {
            get 
            {
                string outputPath = Environment.serverIP;
                string gdb_NickName;
                Environment.GDB_Nickname.TryGetValue(comboBox1.Text, out gdb_NickName);
                string gdb_RealName;
                Environment.GDB_Dict.TryGetValue(gdb_NickName, out gdb_RealName);
                outputPath += gdb_RealName;
                outputPath += '\\' + System.IO.Path.GetFileNameWithoutExtension(textBoxImport.Text) + System.IO.Path.GetExtension(this.textBoxImport.Text);
                return outputPath.Replace("\\", "/");
            }
        }

        public TransferManager.RenameMode RenameMode
        {
            get 
            {
                if (comboBox2.SelectedIndex == 0) return TransferManager.RenameMode.Overwrite;
                else return TransferManager.RenameMode.Accumulate;
            }
        }

        private void buttonMetaData_Click(object sender, EventArgs e)
        {
            Demo.FormMetaData fmd;
            switch (nowDT)
            {
                case enumDataType.raster:
                    fmd = new Demo.FormMetaData(this.rstLayer);
                    break;
                case enumDataType.dem:
                    fmd = new Demo.FormMetaData(this.rstLayer);
                    break;
                case enumDataType.vector:
                    fmd = new Demo.FormMetaData(this.vctLayer);
                    break;
                default:
                    fmd = new Demo.FormMetaData(this.fleLayer);
                    break;
            }
            
            if (fmd.ShowDialog() == DialogResult.OK)
            {
                this.rstLayer = fmd.rstLayer;
                this.vctLayer = fmd.vctLayer;
                this.fleLayer = fmd.fleLayer;
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            Environment.InitGDB_Dict();
            int r=1;
            switch (nowDT)
            {
                case enumDataType.raster:
                    rstLayer.DBNAME = comboBox1.Text;
                    rstLayer.URI = string.Format("{0}#{1}", Environment.GDB_Nickname[rstLayer.DBNAME], rstLayer.LAYERNAME);
                    //r=RasterLayer.ImportRasterIndex(rstLayer);
                    //r = RasterLayer.ImportRasterIndexOra(rstLayer
                    //    );
                    break;
                case enumDataType.dem:
                    rstLayer.DBNAME = comboBox1.Text;
                    rstLayer.URI = string.Format("{0}#{1}", Environment.GDB_Nickname[rstLayer.DBNAME], rstLayer.LAYERNAME);
                    //r=RasterLayer.ImportRasterIndex(rstLayer);
                    break;
                case enumDataType.vector:
                    vctLayer.DBNAME = comboBox1.Text;
                    vctLayer.URI = string.Format("{0}#{1}", Environment.GDB_Nickname[vctLayer.DBNAME], vctLayer.LAYERNAME);
                    //r = VectorLayer.ImportVectorIndex(vctLayer);
                    break;
                default:
                    fleLayer.DBNAME = comboBox1.Text;
                    break;
            }
            if (r != 1) MessageBox.Show("导入错误");
            //
        }

        private ILayer GetILayerFromPath(string path)
        {
            ILayer pLayer = null;

            string[] vectorExtension = { ".SHP", ".E00", ".DWG" };
            string[] rasterExtension = { ".TIF", ".IMG", ".PIX", ".JPG", ".BMP" };
            //if(System.IO.Path.GetExtension

            if (System.IO.Path.GetDirectoryName(path).ToUpper().EndsWith(".GDB"))
            {
                IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
                IRasterWorkspaceEx pRasterWKEx = (IRasterWorkspaceEx)pWKF.OpenFromFile(System.IO.Path.GetDirectoryName(path), 0);
                IRasterDataset3 pRasterDataset = pRasterWKEx.OpenRasterDataset(System.IO.Path.GetFileName(path)) as IRasterDataset3;
            }
            else
            {
                if (rasterExtension.Contains(System.IO.Path.GetExtension(path).ToUpper()))
                {
                    IWorkspaceFactory pWorkspaceFactory;
                    IRasterWorkspace pRasterWorkspace;

                    // 获取当前路径和文件名 
                    if (path == "") return null;
                    int Index = path.LastIndexOf("//");
                    if (Index == -1) Index = path.LastIndexOf("\\");

                    string fileName = path.Substring(Index + 1);
                    string filePath = path.Substring(0, Index);

                    pWorkspaceFactory = new RasterWorkspaceFactoryClass();
                    pRasterWorkspace = (IRasterWorkspace)pWorkspaceFactory.OpenFromFile(filePath, 0);
                    IRasterDataset pRasterDataset = (IRasterDataset)pRasterWorkspace.OpenRasterDataset(fileName);
                    IRasterLayer pRasterLayer = new RasterLayerClass();
                    pRasterLayer.CreateFromDataset(pRasterDataset);

                    return (ILayer)pRasterLayer;
                }
                else if (vectorExtension.Contains(System.IO.Path.GetExtension(path).ToUpper()))
                {
                    IWorkspaceFactory pWorkspaceFactory;
                    IFeatureWorkspace pFeatureWorkspace;
                    IFeatureLayer pFeatureLayer;

                    if (path == "") return null;

                    int Index = path.LastIndexOf("\\");
                    string filePath = path.Substring(0, Index);
                    string fileName = path.Substring(Index + 1);

                    // 打开工作空间并添加shp文件 
                    pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                    pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(filePath, 0);
                    pFeatureLayer = new FeatureLayerClass();
                    pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(fileName);
                    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;

                    return (ILayer)pFeatureLayer;
                }
            }

            return pLayer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Demo.Entities.RasterLayer rl=Demo.Entities.RasterLayer.GetRasterIndexById("");
            if (rl != null)
            {
                FormMetaData fmd = new FormMetaData(rl);
                fmd.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VectorLayer vl = VectorLayer.GetVectorIndexById("");
            if (vl != null)
            {
                FormMetaData fmd = new FormMetaData(vl);
                fmd.ShowDialog();
            }
        }
    }
}
