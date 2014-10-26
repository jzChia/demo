using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
    public partial class Form_ImageMath : DevExpress.XtraEditors.XtraForm
    {
        private IMapControl2 m_pMapCtrl = null;
        private GDBHelper m_pGDBHelper = null;

        public Form_ImageMath(IMapControl2 pMapCtrl, GDBHelper pGDBHelper)
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

            this.comboBoxContrastRaster.Properties.Items.Clear();
            //this.comboBoxEditInputRaster2.Properties.Items.Add("");

            for (int i = 0; i < layerCount; i++)
            {
                ILayer pLayer = pMapControl.get_Layer(i);
                //判断是否为RasterLayer
                IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                if (pRasterLayer != null)
                {
                    this.comboBoxInputRaster.Properties.Items.Add(pLayer.Name);
                    this.comboBoxContrastRaster.Properties.Items.Add(pLayer.Name);
                }
            }
        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (this.comboBoxInputRaster.Text.ToString() == "" || this.comboBoxContrastRaster.Text.ToString() == "" || this.comboBoxOutputRaster.Text.ToString() == "")
            {
                MessageBox.Show("请输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }//有任意一个空为空则弹出对话框提示：请输入参数。
            string strResultsDBPath = m_pGDBHelper.GetResultsDBPath();//声明变量获取数据库结果路径
            string ToolName = "minusmethod";

            if (comboBoxMethod.Text == "差值法")
            {
                ToolName = "minusmethod";
            }
            if (comboBoxMethod.Text == "比值法")
            {
                ToolName = "ratiomethod";
            }




            if (Utilities.GDBUtilites.CheckNameExist(strResultsDBPath, this.comboBoxOutputRaster.Text.ToString()) == false)//如果没有同名文件
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
                    gpParameters.Add(strResultsDBPath + "\\" + this.comboBoxOutputRaster.Text);

                    IGeoProcessorResult pGeoProcessorResult = null;


                    pGeoProcessorResult = pGP.Execute(ToolName, gpParameters, null);


                    if (pGeoProcessorResult.Status == esriJobStatus.esriJobSucceeded)
                    {
                        if (this.checkBoxAdd.Checked)
                        {
                            IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
                            IRasterWorkspaceEx pRasterWKEx = (IRasterWorkspaceEx)pWKF.OpenFromFile(m_pGDBHelper.GetResultsDBPath(), 0);
                            IRasterDataset3 pRasterDataset = pRasterWKEx.OpenRasterDataset(this.comboBoxOutputRaster.Text.ToString()) as IRasterDataset3;

                            Utilities.MapUtilites.AddRasterLayer(m_pMapCtrl.ActiveView, pRasterDataset, null);
                        }
                        MessageBox.Show("影像代数分析完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void simpleButtonOutPutRaster_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "(*.所有文件|*.*)";
            if (open.ShowDialog() == DialogResult.OK)
            { simpleButtonOutPutRaster.Text = open.FileName; }
        }

        private void simpleButtonInputRaster1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(*.所有文件|*.*)";
            if (open.ShowDialog() == DialogResult.OK)
            { comboBoxInputRaster.Text = open.FileName; }
        }

        private void simpleButtonInputRaster2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(*.所有文件|*.*)";
            if (open.ShowDialog() == DialogResult.OK)
            { comboBoxContrastRaster.Text = open.FileName; }
        }

        
    }
}