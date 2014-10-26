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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;


namespace Resee.DataManager
{
    public partial class Form_PACAnalyst : DevExpress.XtraEditors.XtraForm
    {
        private IMapControl2 m_pMapCtrl = null;
        private GDBHelper m_pGDBHelper = null;

        public Form_PACAnalyst(IMapControl2 pMapCtrl,GDBHelper pGDBHelper)
        {
            InitializeComponent();

            m_pGDBHelper = pGDBHelper;

            m_pMapCtrl = pMapCtrl;
            InitInputRasterCombox(m_pMapCtrl);
        }

        public void InitInputRasterCombox(IMapControl2 pMapControl)
        {
            int layerCount = pMapControl.LayerCount;
            this.comboBoxEditInputRaster1.Properties.Items.Clear();
            //this.comboBoxEditInputRaster1.Properties.Items.Add("");

            this.comboBoxEditInputRaster2.Properties.Items.Clear();
            //this.comboBoxEditInputRaster2.Properties.Items.Add("");

            for (int i = 0; i < layerCount; i++)
            {
                ILayer pLayer = pMapControl.get_Layer(i);
                //判断是否为RasterLayer
                IRasterLayer pRasterLayer = pLayer as IRasterLayer;
                if (pRasterLayer != null)
                {
                    this.comboBoxEditInputRaster1.Properties.Items.Add(pLayer.Name);
                    this.comboBoxEditInputRaster2.Properties.Items.Add(pLayer.Name);
                }
            }
        }

        private void simpleButtonOutPutRaster_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (this.comboBoxEditInputRaster1.Text.ToString() == "" || this.comboBoxEditInputRaster2.Text.ToString() == "" || this.comboBoxEditOutputRaster.Text.ToString() == "")
            {
                MessageBox.Show("请输入参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string strResultsDBPath = m_pGDBHelper.GetResultsDBPath();

            if (Utilities.GDBUtilites.CheckNameExist(strResultsDBPath, this.comboBoxEditOutputRaster.Text.ToString()) == false)
            {
                //执行PCA分析
                ILayer pInputLayer1 = null;
                ILayer pInputLayer2 = null;

                for (int i = 0; i < m_pMapCtrl.LayerCount; i++)
                {
                    if (m_pMapCtrl.get_Layer(i).Name == this.comboBoxEditInputRaster1.Text.ToString())
                    {
                        pInputLayer1 = m_pMapCtrl.get_Layer(i);
                    }
                    else if (m_pMapCtrl.get_Layer(i).Name == this.comboBoxEditInputRaster2.Text.ToString())
                    {
                        pInputLayer2 = m_pMapCtrl.get_Layer(i);
                    }

                }

                if (pInputLayer1 != null && pInputLayer2 != null)
                {
                    IRasterLayer pRasterLayer1 = pInputLayer1 as IRasterLayer;
                    IRasterLayer pRasterLayer2 = pInputLayer2 as IRasterLayer;

                    string RasterPath1=pRasterLayer1.FilePath;
                    string RasterPath2=pRasterLayer2.FilePath;

                    IGeoProcessor2 pGP = new GeoProcessorClass();
                    pGP.AddToolbox(m_pGDBHelper.GetToolboxPath());

                    IVariantArray gpParameters = new VarArrayClass();
                    //gpParameters.Add(pRasterLayer1.Raster);
                    //gpParameters.Add(pRasterLayer2.Raster);
                    gpParameters.Add(RasterPath1);
                    gpParameters.Add(RasterPath2);
                    gpParameters.Add(String.Format("{0}\\{1}", m_pGDBHelper.GetResultsDBPath(), this.comboBoxEditOutputRaster.Text));

                    string ToolName = "ReseePACBandMath";
                    ToolName = "NewDiffPCA";

                    IGeoProcessorResult pGeoProcessorResult=pGP.Execute(ToolName, gpParameters, null);
                    if (pGeoProcessorResult.Status == esriJobStatus.esriJobSucceeded)
                    {
                        if (this.checkBox1.Checked)
                        {
                            IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
                            IRasterWorkspaceEx pRasterWKEx = (IRasterWorkspaceEx)pWKF.OpenFromFile(m_pGDBHelper.GetResultsDBPath(), 0);
                            IRasterDataset3 pRasterDataset = pRasterWKEx.OpenRasterDataset(this.comboBoxEditOutputRaster.Text.ToString()) as IRasterDataset3;

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



    }
}