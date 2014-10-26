using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Resee.DataManager
{
    public class GDBHelper
    {
        private string m_strWKPath = "";//"D:\\temp\\程序\\Resee_Workspace";
        private string m_strRastersDBName = "Rasters.gdb";
        private string m_strVectorsDBName = "Vectors.gdb";
        private string m_strTablesDBName = "Vectors.gdb";
        private string m_strResultsDBName = "Results.gdb";
        private string m_strTempDBName = "Temp.gdb";
        private string m_strToolboxName = "ReseeToolbox.tbx";
        private string m_strCachePath = "Cache";

        /// <summary>
        /// 获取应用程序路径
        /// </summary>
        /// <returns></returns>
        string GetAppPath()
        {
            string FileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            FileName = FileName.Substring(0, FileName.LastIndexOf("//"));
            return FileName;
        } 

        /// <summary>
        /// 获取工作空间路径
        /// </summary>
        /// <returns></returns>
        public string GetWorkspacePath()
        {
            string strAppPath = Application.StartupPath;
            m_strWKPath = strAppPath.Substring(0, strAppPath.LastIndexOf("\\")) + "\\" + "Resee_Workspace";

            return m_strWKPath;
        }

        /// <summary>
        /// 获取影像数据库路径
        /// </summary>
        /// <returns></returns>
        public string GetRastersDBPath()
        {
            if (m_strWKPath.Length == 0)
            {
                m_strWKPath = GetWorkspacePath();
            }

            return m_strWKPath + "\\" + m_strRastersDBName;
        }

        /// <summary>
        /// 获取矢量数据库路径
        /// </summary>
        /// <returns></returns>
        public string GetVectorsDBPath()
        {
            if (m_strWKPath.Length == 0)
            {
                m_strWKPath = GetWorkspacePath();
            }

            return m_strWKPath + "\\" + m_strVectorsDBName;
        }

        /// <summary>
        /// 获取表格数据库路径
        /// </summary>
        /// <returns></returns>
        public string GetTablesDBPath()
        {
            if (m_strWKPath.Length == 0)
            {
                m_strWKPath = GetWorkspacePath();
            }

            return m_strWKPath + "\\" + m_strTablesDBName;
        }

        /// <summary>
        /// 获取成果数据库路径
        /// </summary>
        /// <returns></returns>
        public string GetResultsDBPath()
        {
            if (m_strWKPath.Length == 0)
            {
                m_strWKPath = GetWorkspacePath();
            }

            return m_strWKPath + "\\" + m_strResultsDBName;
        }

        /// <summary>
        /// 获取临时数据库路径
        /// </summary>
        /// <returns></returns>
        public string GetTempDBPath()
        {
            if (m_strWKPath.Length == 0)
            {
                m_strWKPath = GetWorkspacePath();
            }

            return m_strWKPath + "\\" + m_strTempDBName;
        }

        /// <summary>
        /// 获取工具箱路径
        /// </summary>
        /// <returns></returns>
        public string GetToolboxPath()
        {
            if (m_strWKPath.Length == 0)
            {
                m_strWKPath = GetWorkspacePath();
            }

            return m_strWKPath + "\\" + m_strToolboxName;
        }

        /// <summary>
        /// 获取临时文件夹路径
        /// </summary>
        /// <returns></returns>
        public string GetCachePath()
        {
            if (m_strWKPath.Length == 0)
            {
                m_strWKPath = GetWorkspacePath();
            }

            return m_strWKPath + "\\" + m_strCachePath;
        }

        //初始化工作空间内容列表
        public void InitGDBCatalog(TreeView catalogTreeView)
        {
            if (catalogTreeView == null)
            {
                return;
            }

            try
            {
                catalogTreeView.BeginUpdate();
                catalogTreeView.LabelEdit = true;
                catalogTreeView.Nodes.Clear();

                //根节点
                TreeNode pRastersDBNode = catalogTreeView.Nodes.Add("nodeRasters","影像数据库库",0,5);
                TreeNode pVectorsDBNode = catalogTreeView.Nodes.Add("nodeVectors","矢量数据库",2,5);
                TreeNode pTablesDBNode = catalogTreeView.Nodes.Add("nodeTables","表格数据库",4,5);
                TreeNode pResultsDBNode = catalogTreeView.Nodes.Add("nodeResults","成果数据库",1,5);
              

                catalogTreeView.LabelEdit = false;
                catalogTreeView.EndUpdate();


                RefreshRastersCatalog(catalogTreeView);

                RefreshVectorsCatalog(catalogTreeView);

                RefreshTablesCatalog(catalogTreeView);

                RefreshResultsCatalog(catalogTreeView);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "初始化空间数据库失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


        }

        //刷新影像数据库内容列表
        public void RefreshRastersCatalog(TreeView catalogTreeView)
        {
            if (catalogTreeView == null)
            {
                return;
            }

            try
            {
                catalogTreeView.BeginUpdate();
                catalogTreeView.LabelEdit = true;

                TreeNode pRastersDBNode = catalogTreeView.Nodes[catalogTreeView.Nodes.IndexOfKey("nodeRasters")];
                pRastersDBNode.Nodes.Clear();


                //初始化影像数据库
                IWorkspaceFactory2 pWKFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKFactory.OpenFromFile(GetRastersDBPath(), 0);

                IEnumDataset iEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTMosaicDataset) as IEnumDataset;
                iEnumDataset.Reset();
                IDataset pDataset = iEnumDataset.Next();

                while (pDataset != null)
                {
                    TreeNode pTreeNode = pRastersDBNode.Nodes.Add(pDataset.Name);

                    pDataset = iEnumDataset.Next();
                }

                catalogTreeView.LabelEdit = false;
                catalogTreeView.EndUpdate();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "刷新影像数据库内容列表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        //刷新矢量数据库内容列表
        public void RefreshVectorsCatalog(TreeView catalogTreeView)
        {
            if (catalogTreeView == null)
            {
                return;
            }

            try
            {
                catalogTreeView.BeginUpdate();
                catalogTreeView.LabelEdit = true;

                TreeNode pVectorsDBNode = catalogTreeView.Nodes[catalogTreeView.Nodes.IndexOfKey("nodeVectors")];
                pVectorsDBNode.Nodes.Clear();


                //初始化影像数据库
                IWorkspaceFactory2 pWKFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKFactory.OpenFromFile(GetVectorsDBPath(), 0);

                IEnumDataset iEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureDataset) as IEnumDataset;
                iEnumDataset.Reset();
                IDataset pDataset = iEnumDataset.Next();
          
                while (pDataset != null)
                {

                    TreeNode pFDTreeNode = pVectorsDBNode.Nodes.Add(pDataset.Name);

                    IFeatureClassContainer pFCContainer = pDataset as IFeatureClassContainer;
                    IEnumFeatureClass pEnumFC = pFCContainer.Classes;
                    IFeatureClass pFC = pEnumFC.Next();
                    IDataset pFCDataset = null;

                    while (pFC != null)
                    {
                        pFCDataset = pFC as IDataset;
                        pFDTreeNode.Nodes.Add(pFCDataset.Name);

                        pFC = pEnumFC.Next();
                    }

                    pDataset = iEnumDataset.Next();
                }

                iEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass) as IEnumDataset;
                iEnumDataset.Reset();
                pDataset = iEnumDataset.Next() as IDataset;

                while (pDataset != null)
                {
                    TreeNode pTreeNode = pVectorsDBNode.Nodes.Add(pDataset.Name);

                    pDataset = iEnumDataset.Next();
                }

                catalogTreeView.LabelEdit = false;
                catalogTreeView.EndUpdate();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "刷新矢量数据库内容列表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        //刷新表格数据库内容列表
        public void RefreshTablesCatalog(TreeView catalogTreeView)
        {
            if (catalogTreeView == null)
            {
                return;
            }

            try
            {
                catalogTreeView.BeginUpdate();
                catalogTreeView.LabelEdit = true;

                TreeNode pTablesDBNode = catalogTreeView.Nodes[catalogTreeView.Nodes.IndexOfKey("nodeTables")];
                pTablesDBNode.Nodes.Clear();


                //初始化表格数据库
                IWorkspaceFactory2 pWKFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKFactory.OpenFromFile(GetTablesDBPath(), 0);

                IEnumDataset iEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTTable) as IEnumDataset;
                iEnumDataset.Reset();
                IDataset pDataset = iEnumDataset.Next();

                while (pDataset != null)
                {
                    TreeNode pTreeNode = pTablesDBNode.Nodes.Add(pDataset.Name);

                    pDataset = iEnumDataset.Next();
                }

                catalogTreeView.LabelEdit = false;
                catalogTreeView.EndUpdate();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "刷新表格数据库内容列表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        //刷新成果数据库内容列表
        public void RefreshResultsCatalog(TreeView catalogTreeView)
        {
            if (catalogTreeView == null)
            {
                return;
            }

            try
            {
                catalogTreeView.BeginUpdate();
                catalogTreeView.LabelEdit = true;

                TreeNode pResultsDBNode = catalogTreeView.Nodes[catalogTreeView.Nodes.IndexOfKey("nodeResults")];
                pResultsDBNode.Nodes.Clear();

                //初始化成果数据库
                IWorkspaceFactory2 pWKFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspace pWorkspace = pWKFactory.OpenFromFile(GetResultsDBPath(), 0);
                //成果-影像
                TreeNode pRSRastersTreeNode = pResultsDBNode.Nodes.Add("影像");
                IEnumDataset iEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTMosaicDataset) as IEnumDataset;
                iEnumDataset.Reset();
                IDataset pDataset = iEnumDataset.Next() as IDataset;

                while (pDataset != null)
                {
                    TreeNode pTreeNode = pRSRastersTreeNode.Nodes.Add(pDataset.Name);

                    pDataset = iEnumDataset.Next();
                }

                //成果-矢量
                TreeNode pRSVestorsTreeNode = pResultsDBNode.Nodes.Add("矢量");
                iEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureDataset) as IEnumDataset;
                iEnumDataset.Reset();
                pDataset = iEnumDataset.Next() as IDataset;

                while (pDataset != null)
                {
                    TreeNode pFDTreeNode = pRSVestorsTreeNode.Nodes.Add(pDataset.Name);

                    IFeatureClassContainer pFCContainer = pDataset as IFeatureClassContainer;
                    IEnumFeatureClass pEnumFC = pFCContainer.Classes;
                    IFeatureClass pFC = pEnumFC.Next();
                    IDataset pFCDataset = null;

                    while (pFCDataset != null)
                    {
                        pFCDataset = pFC as IDataset;
                        pFDTreeNode.Nodes.Add(pFCDataset.Name);

                        pFC = pEnumFC.Next();
                    }

                    pDataset = iEnumDataset.Next();
                }

                iEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTFeatureClass) as IEnumDataset;
                iEnumDataset.Reset();
                pDataset = iEnumDataset.Next() as IDataset;

                while (pDataset != null)
                {
                    //IFeatureClass pFC = pDataset as IFeatureClass;
                    TreeNode pTreeNode = pRSVestorsTreeNode.Nodes.Add(pDataset.Name);

                    pDataset = iEnumDataset.Next();
                }

                //成果—表格
                TreeNode pRSTablesTreeNode = pResultsDBNode.Nodes.Add("表格");
                iEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTTable) as IEnumDataset;
                iEnumDataset.Reset();
                pDataset = iEnumDataset.Next() as IDataset;

                while (pDataset != null)
                {
                    TreeNode pTreeNode = pRSTablesTreeNode.Nodes.Add(pDataset.Name);

                    pDataset = iEnumDataset.Next();
                }


                catalogTreeView.LabelEdit = false;
                catalogTreeView.EndUpdate();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "刷新成果数据库内容列表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        //创建新的镶嵌数据集
        //public bool CreateMosaicDataset()
        //{
        //    IVariantArray parameters = new VarArrayClass();
        //    parameters.Add(GetRastersDBPath());
        //    parameters.Add(null);
        //    parameters.Add(null);

            
        //    //弹出窗体
        //    Form formCreateMosaic = new Form_CreateMosaicDataset(parameters);
            
        //    if (formCreateMosaic.ShowDialog() == DialogResult.OK)
        //    {
        //        IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
        //        IWorkspace pWK = pWKF.OpenFromFile(GetRastersDBPath(),0);

        //        IGeoProcessor2 pGP = new GeoProcessorClass();
        //        pGP.AddToolbox(GetToolboxPath());

        //        IVariantArray gpParameters = new VarArrayClass();
        //        gpParameters.Add(pWK);
        //        string str = parameters.get_Element(1).ToString();
        //        gpParameters.Add(str);
        //        gpParameters.Add(null);

        //        pGP.Execute("ReseeCreateMosaicDataset", gpParameters, null);

        //        return true;
        //    }

           
        //    return false;
        //}
        
        //导入矢量数据
        //public bool ImportRasterData()
        //{
        //    IVariantArray parameters = new VarArrayClass();
        //    parameters.Add(GetRastersDBPath());
        //    parameters.Add(null);
        //    parameters.Add(null);
        //    parameters.Add(null);


        //    //弹出窗体
        //    Form formImportRaster = new Form_ImportRasterData(parameters);

        //    if (formImportRaster.ShowDialog() == DialogResult.OK)
        //    {
        //        string strMosaicDatasetName =  parameters.get_Element(1).ToString();
        //        string strRasterTypeName = parameters.get_Element(2).ToString();
        //        string strDataSourcePath =  parameters.get_Element(3).ToString();
        //        bool bTrue = LoadRasters2MosaicDataset(GetRastersDBPath(), strMosaicDatasetName, strRasterTypeName, strDataSourcePath);
        //        if (bTrue)
        //        {
        //            MessageBox.Show("导入成功！自动融合匹配过程耗时较长，将转入后台运行，请稍候……");
        //           // return true;
        //        }
        //        else
        //        {
        //            MessageBox.Show("警告：导入数据失败！");
        //            return false;
        //        }

        //        bTrue = RebuildMosaicDataset(strMosaicDatasetName);
        //        if (bTrue)
        //        {
        //            MessageBox.Show("自动融合匹配、金字塔及统计信息创建成功！");
        //            return true;
        //        }
        //        else
        //        {
        //            MessageBox.Show("提示：自动融合匹配、金字塔及统计信息创建失败，请重试！");
        //            return true;
        //        }
        //    }

        //    return false;
        //}


        //MosaicDataset操作
        public IWorkspace CreateFileGDB(string filegdbpath)
        {
            string fgdbParentFolder = System.IO.Path.GetDirectoryName(filegdbpath);
            string fgdbName = System.IO.Path.GetFileName(filegdbpath);
            return CreateFileGDB(fgdbParentFolder, fgdbName);
        }
        public IWorkspace CreateFileGDB(string fgdbParentFolder, string fgdbName)
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
            IWorkspaceFactory FgdbFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            IWorkspaceName wkname = FgdbFactory.Create(fgdbParentFolder, fgdbName, null, 0);
            IName name = wkname as IName;
            IWorkspace wk = name.Open() as IWorkspace;
            return wk;
        }
        public IWorkspace OpenFileGDB(string fgdbpath)
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            IWorkspace fgdbWorkspace = workspaceFactory.OpenFromFile(fgdbpath, 0);
            return fgdbWorkspace;

        }
        public IMosaicDataset CreateMosaicDataset(string fgdbParentFolder, string fgdbName, string mosaicDatasetName)
        {
            IWorkspace wk = CreateFileGDB(fgdbParentFolder, fgdbName);
            return CreateMosaicDataset(ref wk, mosaicDatasetName, null, 0, rstPixelType.PT_DOUBLE, "");
        }
        public IMosaicDataset CreateMosaicDataset(ref IWorkspace fgdbWorkspace, string mosaicDatasetName,
            ISpatialReference mosaicSrs, int mosaicDatasetBands, rstPixelType mosaicDatasetBits, string configKeyword)
        {
            IMosaicDataset theMosaicDataset = null;
            try
            {
                Console.WriteLine("Create Mosaic Dataset: " + mosaicDatasetName);

                if (mosaicSrs == null)
                {

                    ISpatialReferenceFactory spatialrefFactory = new SpatialReferenceEnvironmentClass();
                    //ISpatialReference mosaicSrs = spatialrefFactory.CreateProjectedCoordinateSystem(
                    //    (int)(esriSRProjCSType.esriSRProjCS_World_Mercator));
                    mosaicSrs = spatialrefFactory.CreateGeographicCoordinateSystem(
                    (int)(esriSRGeoCSType.esriSRGeoCS_WGS1984));
                }

                // 创建镶嵌数据集创建参数对象
                ICreateMosaicDatasetParameters creationPars = new CreateMosaicDatasetParametersClass();

                // 设置镶嵌数据集band数量.
                // 0 为默认设置
                if (mosaicDatasetBands != 0)
                    creationPars.BandCount = mosaicDatasetBands;
                // 设置象元类型.
                //  默认为 unknown
                if (mosaicDatasetBits != rstPixelType.PT_UNKNOWN)
                    creationPars.PixelType = mosaicDatasetBits;

                //创建栅格数据库扩展帮助类
                IMosaicWorkspaceExtensionHelper mosaicExtHelper = new MosaicWorkspaceExtensionHelperClass();
                // 查找扩展
                IMosaicWorkspaceExtension mosaicExt = mosaicExtHelper.FindExtension(fgdbWorkspace);
                // 使用扩展创建新的镶嵌数据集, 需要提供空间参考和创建参数。
                theMosaicDataset = mosaicExt.CreateMosaicDataset(mosaicDatasetName,
                    mosaicSrs, creationPars, configKeyword);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception Caught while creating Mosaic Dataset: " + exc.Message);
                Console.WriteLine("Shutting down.");
                Console.WriteLine("Press any key...");
                Console.ReadKey();

            }
            return theMosaicDataset;
        }


        public IMosaicDataset OpenMosaicDataset(ref IWorkspace fgdbWorkspace, string mosaicDatasetName)
        {

            IMosaicWorkspaceExtensionHelper mosaicExtHelper = new MosaicWorkspaceExtensionHelperClass();
            IMosaicWorkspaceExtension mosaicExt = mosaicExtHelper.FindExtension(fgdbWorkspace);

            Console.WriteLine("Opening Mosaic Dataset");

            IMosaicDataset theMosaicDataset = mosaicExt.OpenMosaicDataset(mosaicDatasetName);

            return theMosaicDataset;

        }

        public IRasterType PreparingRasterType(string rasterTypeName, string rasterTypeProductFilter, string rasterTypeProductName)
        {

            Console.WriteLine("Preparing Raster Type");
            // Create a Raster Type Name object.
            IRasterTypeName theRasterTypeName = new RasterTypeNameClass();
            // Assign the name of the Raster Type to the name object.
            // The Name field accepts a path to an .art file as well 
            // the name for a built in Raster Type.
            theRasterTypeName.Name = rasterTypeName;
            // Use the Open function from the IName interface to get the Raster Type object.
            IRasterType theRasterType = (IRasterType)(((IName)theRasterTypeName).Open());
            if (theRasterType == null)
                Console.WriteLine("Raster Type not found " + rasterTypeName);

            // Set the URI Filter on the loaded raster type.
            if (rasterTypeProductFilter != "")
            {
                // Get the supported URI filters from the raster type object using the 
                // raster type properties interface.
                IArray mySuppFilters = ((IRasterTypeProperties)theRasterType).SupportedURIFilters;
                IItemURIFilter productFilter = null;
                for (int i = 0; i < mySuppFilters.Count; ++i)
                {
                    // Set the desired filter from the supported filters.
                    productFilter = (IItemURIFilter)mySuppFilters.get_Element(i);
                    if (productFilter.Name == rasterTypeProductFilter)
                        theRasterType.URIFilter = productFilter;
                }
            }
            // Enable the correct templates in the raster type.
            string[] rasterProductNames = rasterTypeProductName.Split(';');
            bool enableTemplate = false;
            if (rasterProductNames.Length >= 1 && (rasterProductNames[0] != ""))
            {
                // Get the supported item templates from the raster type.
                IItemTemplateArray templateArray = theRasterType.ItemTemplates;
                for (int i = 0; i < templateArray.Count; ++i)
                {
                    // Go through the supported item templates and enable the ones needed.
                    IItemTemplate template = templateArray.get_Element(i);
                    enableTemplate = false;
                    for (int j = 0; j < rasterProductNames.Length; ++j)
                        if (template.Name == rasterProductNames[j])
                            enableTemplate = true;
                    if (enableTemplate)
                        template.Enabled = true;
                    else
                        template.Enabled = false;
                }
            }

            return theRasterType;
        }

        public IDataSourceCrawler PreparingDataSourceCrawler(ref IRasterType theRasterType, string dataSourceFilter, string dataSource)
        {
            Console.WriteLine("Preparing Data Source Crawler");
            // Create a new property set to specify crawler properties.
            IPropertySet crawlerProps = new PropertySetClass();
            // Specify a file filter
            crawlerProps.SetProperty("Filter", dataSourceFilter);
            // Specify whether to search subdirectories.
            crawlerProps.SetProperty("Recurse", true);
            // Specify the source path.
            crawlerProps.SetProperty("Source", dataSource);
            // Get the recommended crawler from the raster type based on the specified 
            // properties using the IRasterBuilder interface.
            IDataSourceCrawler theCrawler = ((IRasterBuilder)theRasterType).GetRecommendedCrawler(crawlerProps);

            return theCrawler;
        }

        public void AddRasters(ref IMosaicDataset theMosaicDataset, ref IRasterType theRasterType, ref IDataSourceCrawler theCrawler)
        {
            IMosaicDatasetOperation theMosaicDatasetOperation = (IMosaicDatasetOperation)(theMosaicDataset);
            Console.WriteLine("Adding Rasters");
            // Create a AddRaster parameters object.
            IAddRastersParameters AddRastersArgs = new AddRastersParametersClass();
            // Specify the data crawler to be used to crawl the data.
            AddRastersArgs.Crawler = theCrawler;
            // Specify the raster type to be used to add the data.
            AddRastersArgs.RasterType = theRasterType;
            // Use the mosaic dataset operation interface to add 
            // rasters to the mosaic dataset.
            theMosaicDatasetOperation.AddRasters(AddRastersArgs, null);
        }

        public void ComputePixelSizeRanges(ref IMosaicDataset theMosaicDataset)
        {
            IMosaicDatasetOperation theMosaicDatasetOperation = (IMosaicDatasetOperation)(theMosaicDataset);
            Console.WriteLine("Computing Pixel Size Ranges");
            // Create a calculate cellsize ranges parameters object.
            ICalculateCellSizeRangesParameters computeArgs = new CalculateCellSizeRangesParametersClass();
            // Use the mosaic dataset operation interface to calculate cellsize ranges.
            theMosaicDatasetOperation.CalculateCellSizeRanges(computeArgs, null);
        }

        public void BuildingBoundary(ref IMosaicDataset theMosaicDataset)
        {
            IMosaicDatasetOperation theMosaicDatasetOperation = (IMosaicDatasetOperation)(theMosaicDataset);
            Console.WriteLine("Building Boundary");
            // Create a build boundary parameters object.
            IBuildBoundaryParameters boundaryArgs = new BuildBoundaryParametersClass();
            // Set flags that control boundary generation.
            boundaryArgs.AppendToExistingBoundary = true;
            // Use the mosaic dataset operation interface to build boundary.
            theMosaicDatasetOperation.BuildBoundary(boundaryArgs, null);
        }

        public void BuildOverview(ref IMosaicDataset theMosaicDataset, bool buildOverviews)
        {
            IMosaicDatasetOperation theMosaicDatasetOperation = (IMosaicDatasetOperation)(theMosaicDataset);


            if (buildOverviews)
            {
                #region Defining Overviews
                Console.WriteLine("Defining overviews");
                // Create a define overview parameters object.
                IDefineOverviewsParameters defineOvArgs = new DefineOverviewsParametersClass();
                // Use the overview tile parameters interface to specify the overview factor
                // used to generate overviews.
                ((IOverviewTileParameters)defineOvArgs).OverviewFactor = 3;
                // Use the mosaic dataset operation interface to define overviews.
                theMosaicDatasetOperation.DefineOverviews(defineOvArgs, null);
                #endregion

                #region Compute Pixel Size Ranges
                Console.WriteLine("Computing Pixel Size Ranges");
                // Calculate cell size ranges to update the Min/Max pixel sizes.
                ICalculateCellSizeRangesParameters computeArgs = new CalculateCellSizeRangesParametersClass();
                theMosaicDatasetOperation.CalculateCellSizeRanges(computeArgs, null);
                #endregion

                #region Generating Overviews
                Console.WriteLine("Generating Overviews");
                // Create a generate overviews parameters object.
                IGenerateOverviewsParameters genPars = new GenerateOverviewsParametersClass();
                // Set properties to control overview generation.
                IQueryFilter genQuery = new QueryFilterClass();
                ((ISelectionParameters)genPars).QueryFilter = genQuery;
                genPars.GenerateMissingImages = true;
                genPars.GenerateStaleImages = true;
                // Use the mosaic dataset operation interface to generate overviews.
                theMosaicDatasetOperation.GenerateOverviews(genPars, null);
                #endregion
            }
        }

        /// <summary>
        /// 判断数据库和镶嵌数据集是否存在的,存在就读取，不存在，就创建
        /// </summary>
        /// <param name="filegdb"></param>
        /// <param name="mosaicDatasetName"></param>
        /// <returns></returns>
        public IMosaicDataset CreateMD(string filegdb, string mosaicDatasetName)
        {
            ISpatialReference mosaicSrs = null;
            int mosaicDatasetBands = 0;
            rstPixelType mosaicDatasetBits = rstPixelType.PT_UNKNOWN;
            string configKeyword = "";
            IWorkspace wk = null;
            if (System.IO.Directory.Exists(filegdb))
            {
                wk = OpenFileGDB(filegdb);
            }
            else
            {
                wk = CreateFileGDB(filegdb);
            }

            IMosaicDataset theMosaicDataset = null;
            IWorkspace2 wk2 = wk as IWorkspace2;

            if (wk2.get_NameExists(esriDatasetType.esriDTMosaicDataset, mosaicDatasetName))
            {
                theMosaicDataset = OpenMosaicDataset(ref wk, mosaicDatasetName);
            }
            else
            {
                theMosaicDataset = CreateMosaicDataset(ref wk, mosaicDatasetName, mosaicSrs, mosaicDatasetBands, mosaicDatasetBits, configKeyword);
            }

            return theMosaicDataset;


        }

        /// <summary>
        /// 每次都创建新的数据库和新的镶嵌数据集的例子
        /// </summary>
        /// <param name="filegdb"></param>
        /// <param name="mosaicDatasetName"></param>
        /// <returns></returns>
        public IMosaicDataset OpenMosaicDataset(string filegdb, string mosaicDatasetName)
        {
            IWorkspace wk = OpenFileGDB(filegdb);

            IMosaicDataset theMosaicDataset = OpenMosaicDataset(ref wk, mosaicDatasetName);

            return theMosaicDataset;
        }

        public bool LoadRasters2MosaicDataset(string fgdbfile, string mosaicDatasetName, string rasterTypeName,string dataSource)
        {
            try
            {
                string fgdbName = System.IO.Path.GetFileName(fgdbfile);
                string fgdbParentFolder = System.IO.Path.GetDirectoryName(fgdbfile);


                //string rasterTypeName = "Raster Dataset";
                string rasterTypeProductFilter = "";
                string rasterTypeProductName = "Pansharpen;Multispectral";
                string dataSourceFilter = "";
                bool buildOverviews = true;
                string filegdbname = fgdbParentFolder + "\\" + fgdbName;
                IMosaicDataset theMosaicDataset = CreateMD(filegdbname, mosaicDatasetName);

                IRasterType theRasterType = PreparingRasterType(rasterTypeName, rasterTypeProductFilter, rasterTypeProductName);
                IDataSourceCrawler theCrawler = PreparingDataSourceCrawler(ref theRasterType, dataSourceFilter, dataSource);

                AddRasters(ref theMosaicDataset, ref theRasterType, ref theCrawler);
                ComputePixelSizeRanges(ref theMosaicDataset);
                BuildingBoundary(ref theMosaicDataset);
                //BuildOverview(ref theMosaicDataset, buildOverviews);

                return true;
            }
            catch (System.Exception exc)
            {
                return false;
            }
        }

        public bool RebuildMosaicDataset(string mosaicDatasetName)
        {
            IMosaicDataset pMosaicDataset = OpenMosaicDataset(GetRastersDBPath(), mosaicDatasetName);

            IGeoProcessor2 pGP = new GeoProcessorClass();
            pGP.AddToolbox(GetToolboxPath());

            IVariantArray gpParameters = new VarArrayClass();
            gpParameters.Add(pMosaicDataset);

            IGeoProcessorResult pGeoProcessorResult = pGP.Execute("ReseeBuildMosaicDataset", gpParameters, null);
            if (pGeoProcessorResult.Status == esriJobStatus.esriJobSucceeded)
            {
                return true;
            }


            return false;
        }

        public bool ExcutePACModel()
        {
            
            return false;
        }


    }
}
