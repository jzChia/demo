using System;                                                        /*引入命名空间*/
using System.Collections.Generic;//.net中的泛型合集
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;//文本程序集
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;

using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;


namespace Resee.DataManager
{
    public partial class Form_PCAAnalyst : DevExpress.XtraEditors.XtraForm
    {
        private IMapControl2 m_pMapCtrl = null;
        private GDBHelper m_pGDBHelper = null;

        public Form_PCAAnalyst(IMapControl2 pMapCtrl,GDBHelper pGDBHelper)
        {
            InitializeComponent();

            m_pGDBHelper = pGDBHelper;

            m_pMapCtrl = pMapCtrl;
            InitInputRasterCombox(m_pMapCtrl);
        }

        public void InitInputRasterCombox(IMapControl2 pMapControl)
        {
            int layerCount = pMapControl.LayerCount;
            this.comboBoxInputRaster.Properties.Items.Clear();
            //this.comboBoxEditInputRaster1.Properties.Items.Add("");

            this.comboBoxContrastRaster.Items.Clear();
            //this.comboBoxEditInputRaster2.Properties.Items.Add("");

            //遍历Mapcontrol中的每个Layer
            for (int i = 0; i < layerCount; i++)
            {
                ILayer pLayer = pMapControl.get_Layer(i);
                //判断是否为RasterLayer
                IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                //IFeatureLayer pFe = pLayer as IFeatureLayer;
                if (pRasterLayer != null)
                {
                    this.comboBoxInputRaster.Properties.Items.Add(pLayer.Name);
                    this.comboBoxContrastRaster.Items.Add(pLayer.Name);
                }
            }
        }

        private void simpleButtonOutPutRaster_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (this.comboBoxInputRaster.Text.ToString() == "" || this.comboBoxContrastRaster.Text.ToString() == "" || this.textBoxOutputRaster.Text.ToString() == "")
            {
                MessageBox.Show("请输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }//有任意一个空为空则弹出对话框提示：请输入参数。
            string strResultsDBPath = m_pGDBHelper.GetResultsDBPath();//声明变量获取数据库结果路径
            string ToolName = "ToolName";

            if (comboBoxMethod.Text == "主成分差异法")
            {
                ToolName = "NewPCADiff";
            }
            if (comboBoxMethod.Text == "多波段主成分变换法")
            {
                ToolName = "NewBandPCA";
            }
            if (comboBoxMethod.Text == "差异主成分法")
            {
                ToolName = "NewDiffPCA";
            }
            




            if (Utilities.GDBUtilites.CheckNameExist(strResultsDBPath, this.textBoxOutputRaster.Text.ToString()) == false)//如果没有同名文件
            {
                //执行PCA分析
                ILayer pInputLayer1 = null;
                ILayer pInputLayer2 = null;

                for (int i = 0; i < m_pMapCtrl.LayerCount; i++)
                {
                    if (m_pMapCtrl.get_Layer(i).Name == this.comboBoxInputRaster.Text.ToString())
                    {
                        pInputLayer1 = m_pMapCtrl.get_Layer(i);
                    }
                    else if (m_pMapCtrl.get_Layer(i).Name == this.comboBoxContrastRaster.Text.ToString())
                    {
                        pInputLayer2 = m_pMapCtrl.get_Layer(i);
                    }

                }

                if (pInputLayer1 != null && pInputLayer2 != null)
                {
                    IRasterLayer pRasterLayer1 = pInputLayer1 as IRasterLayer;
                    IRasterLayer pRasterLayer2 = pInputLayer2 as IRasterLayer;
                    string rasterpath1 = pRasterLayer1.FilePath;
                    string rasterpath2 = pRasterLayer2.FilePath;

                    IGeoProcessor2 pGP = new GeoProcessorClass();
                    string GPPath = m_pGDBHelper.GetToolboxPath();

                    pGP.AddToolbox(GPPath);



                    IVariantArray gpParameters = new VarArrayClass();
                    //gpParameters.Add(pRasterLayer1.Raster);
                    //gpParameters.Add(pRasterLayer2.Raster);
                    gpParameters.Add(rasterpath1);
                    gpParameters.Add(rasterpath2);
                    gpParameters.Add(strResultsDBPath + "\\" + this.textBoxOutputRaster.Text);
                    
                    IGeoProcessorResult pGeoProcessorResult=null;
                    try
                    {
                        pGeoProcessorResult = pGP.Execute(ToolName, gpParameters, null);
                    }
                    catch (Exception ex)
                    {
                        if (ex is System.Runtime.InteropServices.COMException)
                        {
                            int errorCode = (ex as System.Runtime.InteropServices.COMException).ErrorCode;
                            MessageBox.Show(errorCode.ToString());
                        }
                    }

                    if (pGeoProcessorResult.Status == esriJobStatus.esriJobSucceeded)
                    {
                        if (this.checkBoxAdd.Checked)
                        {
                            IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
                            IRasterWorkspaceEx pRasterWKEx = (IRasterWorkspaceEx)pWKF.OpenFromFile(m_pGDBHelper.GetResultsDBPath(), 0);
                            IRasterDataset3 pRasterDataset = pRasterWKEx.OpenRasterDataset(this.textBoxOutputRaster.Text) as IRasterDataset3;

                            Utilities.MapUtilites.AddRasterLayer(m_pMapCtrl.ActiveView, pRasterDataset, null);
                        }
                        MessageBox.Show("PCA分析完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
 
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("已存在同名数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void iButtonCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBoxEditInputRaster2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButtonInputRaster1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "(*.所有文件|*.*)";
            if (open.ShowDialog() == DialogResult.OK)
            { textBoxOutputRaster.Text = open.FileName; }
        }

        private void comboBoxInputRaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form_PCAAnalyst_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxOutputRaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonInputRaster_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(*.所有文件|*.*)";
            if (open.ShowDialog() == DialogResult.OK)
            { comboBoxInputRaster.Text = open.FileName; }
        }

        private void buttonInputcontrastRaster_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(*.所有文件|*.*)";
            if (open.ShowDialog() == DialogResult.OK)
            { comboBoxContrastRaster.Text = open.FileName; }
        }


       



    }
}