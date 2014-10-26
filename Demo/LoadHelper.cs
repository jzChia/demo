using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using System.Xml;

namespace Demo
{
    public class LoadHelper
    {
        public LoadTarget target = new LoadTarget();
        public DateTime startTime;

        public LoadHelper(LoadTarget target)
        {
            this.target = target;
        }


        /// <summary>
        /// FeatureClass导入GDB
        /// </summary>
        /// <param name="in_features">输入FeatureClass</param>
        /// <param name="gdb_Path">库完整路径</param>
        /// <param name="feat_Name">库内文件名</param>
        /// <param name="result">运行结果</param>
        /// <returns></returns>
        public static bool ImportFeaturesToGDB(IFeatureClass in_features, string gdb_Path, string feat_Name, out string result)
        {
            try
            {
                ESRI.ArcGIS.DataManagementTools.CopyFeatures cfs = new ESRI.ArcGIS.DataManagementTools.CopyFeatures();
                cfs.in_features = in_features;
                cfs.out_feature_class = gdb_Path + "//" + GetNameGDB(gdb_Path, feat_Name);
                Geoprocessor gp = new Geoprocessor();
                gp.OverwriteOutput = true;
                IGeoProcessorResult gpr = gp.Execute(cfs, null) as IGeoProcessorResult;
                if (gpr != null && gpr.Status == esriJobStatus.esriJobSucceeded)
                {
                    result = "Success";
                    return true;
                }
                else
                {
                    result = "Failure";
                    return false;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Raster导入GDB
        /// </summary>
        /// <param name="in_Raster">输入Raster</param>
        /// <param name="gdb_Path">库完整路径</param>
        /// <param name="rst_Name">库内文件名</param>
        /// <param name="result">运行结果</param>
        /// <returns></returns>
        public static bool ImportRasterToGDB(IRaster in_Raster, string gdb_Path, string rst_Name, out string result)
        {
            try
            {
                ESRI.ArcGIS.DataManagementTools.CopyRaster cr = new ESRI.ArcGIS.DataManagementTools.CopyRaster();
                cr.in_raster = in_Raster;
                cr.out_rasterdataset = gdb_Path + "/" + GetNameGDB(gdb_Path, rst_Name);
                cr.background_value = 0;
                Geoprocessor gp = new Geoprocessor();
                gp.OverwriteOutput = true;
                IGeoProcessorResult gpr = gp.Execute(cr, null) as IGeoProcessorResult;
                if (gpr != null && gpr.Status == esriJobStatus.esriJobSucceeded)
                {
                    result = "Success";
                    return true;
                }
                else
                {
                    result = "Failure";
                    return false;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
                return false;
            }
        }

        /// <summary>
        /// GDB导出Shapefile
        /// </summary>
        /// <param name="gdb_Path">服务端库完整路径</param>
        /// <param name="feat_Name">服务端库内文件名</param>
        /// <param name="outputFolderPath">输出路径</param>
        /// <param name="outputName">输出名（无需后缀）</param>
        /// <param name="result">运行结果</param>
        /// <param name="layer">操作图层</param>
        /// <returns></returns>
        public static bool ExportFeaturesFromGDB(string gdb_Path, string feat_Name, string outputFolderPath, string outputName, out string result, out ILayer layer)
        {
            try
            {
                ESRI.ArcGIS.DataManagementTools.CopyFeatures cfs = new ESRI.ArcGIS.DataManagementTools.CopyFeatures();
                cfs.in_features = gdb_Path + "\\" + feat_Name;
                if (System.IO.Path.GetExtension(outputFolderPath).ToUpper().Equals(".GDB"))
                    cfs.out_feature_class = outputFolderPath + "\\" + GetNameGDB(outputFolderPath, outputName);
                else cfs.out_feature_class = outputFolderPath + "\\" + GetNameShapeFile(outputFolderPath, feat_Name);

                Geoprocessor gp = new Geoprocessor();
                gp.OverwriteOutput = true;
                IGeoProcessorResult gpr = gp.Execute(cfs, null) as IGeoProcessorResult;
                layer = null;

                if (gpr != null && gpr.Status == esriJobStatus.esriJobSucceeded)
                {
                    if (MessageBox.Show("是否将图层加入到当前工程？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        IWorkspaceFactory wsf;
                        if (System.IO.Path.GetExtension(outputFolderPath).ToUpper().Equals(".GDB"))
                            wsf = new FileGDBWorkspaceFactoryClass();
                        else wsf = new ShapefileWorkspaceFactoryClass();

                        IFeatureWorkspace featWs = (IFeatureWorkspace)wsf.OpenFromFile(outputFolderPath, 0);
                        IFeatureLayer featLyr = new FeatureLayerClass();
                        featLyr.FeatureClass = featWs.OpenFeatureClass(outputName) as IFeatureClass;
                        layer = featLyr as ILayer;
                        if (System.IO.Path.GetExtension(outputFolderPath).ToUpper().Equals(".GDB"))
                            layer.Name = GetNameGDB(outputFolderPath, outputName);
                        else layer.Name = GetNameShapeFile(outputFolderPath, outputName);
                    }
                    result = "Success";
                    return true;
                }
                else
                {
                    result = "Failure";
                    return false;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
                layer = null;
                return false;
            }
        }

        /// <summary>
        /// GDB导出Raster
        /// </summary>
        /// <param name="gdb_Path">服务端库完整路径</param>
        /// <param name="feat_Name">服务端文件名</param>
        /// <param name="outputFolderPath">输出路径</param>
        /// <param name="outputName">输出文件名（无需后缀）</param>
        /// <param name="result">运行结果</param>
        /// <param name="layer">操作图层</param>
        /// <returns></returns>
        public void ExportRasterFromGDB()
        {
            startTime = DateTime.Now;

            CopyRasterMethod();
        }

        private void CopyRasterMethod()
        {
            try
            {
                double Size = 0;
                string gdb_Path = target.In_Directory;
                string feat_Name = target.In_FileName;
                string outputFolderPath = target.Out_Directory;
                string outputName = target.Out_FileName;

                ESRI.ArcGIS.DataManagementTools.CopyRaster cr = new ESRI.ArcGIS.DataManagementTools.CopyRaster();
                cr.in_raster = gdb_Path + "\\" + feat_Name;
                if (System.IO.Path.GetExtension(outputFolderPath).ToUpper().Equals(".GDB"))
                    cr.out_rasterdataset = outputFolderPath + "\\" + GetNameGDB(outputFolderPath, outputName);
                else cr.out_rasterdataset = outputFolderPath + "\\" + GetNameRaster(outputFolderPath, outputName);

                IWorkspaceFactory2 in_wsf = new FileGDBWorkspaceFactoryClass();
                IRasterWorkspace2 in_rstWs = (IRasterWorkspace2)in_wsf.OpenFromFile(gdb_Path, 0);
                IRasterLayer in_rstLyr = new RasterLayerClass();
                IRasterDataset in_rstDs = in_rstWs.OpenRasterDataset(feat_Name);
                in_rstLyr.CreateFromDataset(in_rstDs);

                IRasterProps in_rasterProps = (IRasterProps)in_rstLyr.Raster;
                int Height = in_rasterProps.Height;
                int Width = in_rasterProps.Width;
                rstPixelType in_rstPT = in_rasterProps.PixelType;
                int BandsCount = in_rstLyr.BandCount;
                Dictionary<rstPixelType, int> DictPT = new Dictionary<rstPixelType, int>();
                DictPT.Clear();
                DictPT.Add(rstPixelType.PT_DOUBLE, 64);
                DictPT.Add(rstPixelType.PT_FLOAT, 32);
                DictPT.Add(rstPixelType.PT_LONG, 32);
                DictPT.Add(rstPixelType.PT_SHORT, 32);
                DictPT.Add(rstPixelType.PT_UCHAR, 8);
                DictPT.Add(rstPixelType.PT_ULONG, 32);
                DictPT.Add(rstPixelType.PT_USHORT, 32);
                DictPT.Add(rstPixelType.PT_CHAR, 8);

                int Depth = 32;
                DictPT.TryGetValue(in_rasterProps.PixelType, out Depth);

                Size = 1.0 * Height * Width * BandsCount * Depth/8.0/1024/1024;
                target.Size = Size;

                Geoprocessor gp = new Geoprocessor();
                gp.OverwriteOutput = true;
                IGeoProcessorResult gpr = gp.Execute(cr, null) as IGeoProcessorResult;

                if (gpr != null && gpr.Status == esriJobStatus.esriJobSucceeded)
                {
                    DateTime tm2 = DateTime.Now;
                    TimeSpan ts1 = new TimeSpan(startTime.Ticks);
                    TimeSpan ts2 = new TimeSpan(tm2.Ticks);
                    TimeSpan ts3 = ts2.Subtract(ts1);

                    string out1, out2;
                    GetConfig("Size", out out1);
                    GetConfig("Time", out out2);
                    double size = double.Parse(out1);
                    double time = double.Parse(out2);
                    SaveConfig("Size", (size + Size).ToString());
                    SaveConfig("Time", (time + ts3.TotalSeconds).ToString());
                    target.IsFinished = true;
                    target.IsBusy = false;
                    OnFinished(new LayerOperaEventArgs(target));
                }
                else
                {

                }
            }
            catch (Exception e)
            {

            }
        }

        public class LayerOperaEventArgs : EventArgs
        {
            public LayerOperaEventArgs(LoadTarget target)
            {
                this.Target = target;
            }

            public LoadTarget Target { get; set; }
        }
        public delegate void OnFinishedHandle(LayerOperaEventArgs e);
        public event OnFinishedHandle OnFinished;

        void tm_Tick(object sender, EventArgs e)
        {
            if (target.Progress >= target.Size)
            {
                Timer tm = sender as Timer;
                tm.Stop();
            }
            DateTime tm2 = DateTime.Now;
            TimeSpan ts1 = new TimeSpan(startTime.Ticks);
            TimeSpan ts2 = new TimeSpan(tm2.Ticks);
            TimeSpan ts3 = ts2.Subtract(ts1);
            this.target.Progress = (int)(this.target.Speed * ts3.TotalSeconds);
        }



        public static void SaveConfig(string strKey, string strValue)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = Application.StartupPath + "\\config.xml";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("cfg");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att.Value == strKey)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["value"];
                    att.Value = strValue;
                    break;
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }

        /// <summary>
        /// 读取设置
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public static void GetConfig(string strKey, out string strValue)
        {
            strValue = "";
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = Application.StartupPath + "\\config.xml";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("cfg");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att.Value == strKey)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["value"];
                    strValue = att.Value;
                    break;
                }
            }
        }


        public static ILayer VectorView(string gdb_Path, string feat_Name)
        {
            try
            {
                IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
                IFeatureWorkspace pRasterWKEx = (IFeatureWorkspace)pWKF.OpenFromFile(gdb_Path, 0);
                IFeatureLayer featLyr = new FeatureLayerClass();
                featLyr.FeatureClass = pRasterWKEx.OpenFeatureClass(feat_Name) as IFeatureClass;
                ILayer lyr = featLyr as ILayer;
                lyr.Name = feat_Name;
                return lyr;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ILayer RasterView(string gdb_Path, string feat_Name)
        {
            try
            {
                IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
                IRasterWorkspace2 pRasterWKEx = (IRasterWorkspace2)pWKF.OpenFromFile(gdb_Path, 0);
                IRasterLayer featLyr = new RasterLayerClass();
                IRasterDataset rstDs = pRasterWKEx.OpenRasterDataset(feat_Name) as IRasterDataset;
                IRaster rst = rstDs.CreateDefaultRaster();
                featLyr.CreateFromRaster(rst);
                ILayer lyr = featLyr as ILayer;
                lyr.Name = feat_Name;
                return lyr;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string GetNameGDB(string gdb_Path, string file_Name)
        {
            IWorkspaceFactory wsf = new FileGDBWorkspaceFactoryClass();
            IWorkspace ws = wsf.OpenFromFile(gdb_Path, 0);
            IEnumDatasetName edn = ws.get_DatasetNames(esriDatasetType.esriDTAny);
            return Check(edn, file_Name, string.Empty);
        }

        public static string GetNameShapeFile(string gdb_Path, string file_Name)
        {
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            IWorkspace ws = wsf.OpenFromFile(gdb_Path, 0);
            IEnumDatasetName edn = ws.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
            return Check(edn, file_Name, string.Empty);
        }

        public static string GetNameRaster(string gdb_Path, string file_Name)
        {
            IWorkspaceFactory wsf = new RasterWorkspaceFactory();
            IWorkspace ws = wsf.OpenFromFile(gdb_Path, 0);
            IEnumDatasetName edn = ws.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
            return Check(edn, file_Name, string.Empty);
        }

        public static string Check(IEnumDatasetName edn, string feat_Name, string index)
        {
            edn.Reset();
            IDatasetName dsName = edn.Next();
            while (dsName != null)
            {
                if (dsName.Name.ToLower().Equals((feat_Name + index).ToLower()))
                {
                    if (index == string.Empty) return Check(edn, feat_Name, "1");
                    else return Check(edn, feat_Name, (int.Parse(index) + 1).ToString());
                }
                dsName = edn.Next();
            }
            return feat_Name + index.ToString();
        }


    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ESRI.ArcGIS.Carto;
//using ESRI.ArcGIS.Geodatabase;
//using ESRI.ArcGIS.Geoprocessor;
//using ESRI.ArcGIS.Geoprocessing;
//using ESRI.ArcGIS.esriSystem;
//using System.Windows.Forms;
//using ESRI.ArcGIS.DataSourcesGDB;
//using ESRI.ArcGIS.DataSourcesFile;
//using ESRI.ArcGIS.DataSourcesRaster;

//namespace Demo
//{
//    public class LoadHelper
//    {
//        /// <summary>
//        /// FeatureClass导入GDB
//        /// </summary>
//        /// <param name="in_features">输入FeatureClass</param>
//        /// <param name="gdb_Path">库完整路径</param>
//        /// <param name="feat_Name">库内文件名</param>
//        /// <param name="result">运行结果</param>
//        /// <returns></returns>
//        public static bool ImportFeaturesToGDB(IFeatureClass in_features, string gdb_Path, string feat_Name, out string result)
//        {
//            try
//            {
//                ESRI.ArcGIS.DataManagementTools.CopyFeatures cfs = new ESRI.ArcGIS.DataManagementTools.CopyFeatures();
//                cfs.in_features = in_features;
//                cfs.out_feature_class = gdb_Path + "//" + GetNameGDB(gdb_Path, feat_Name);
//                Geoprocessor gp = new Geoprocessor();
//                gp.OverwriteOutput = true;
//                IGeoProcessorResult gpr = gp.Execute(cfs, null) as IGeoProcessorResult;
//                if (gpr != null && gpr.Status == esriJobStatus.esriJobSucceeded)
//                {
//                    result = "Success";
//                    return true;
//                }
//                else
//                {
//                    result = "Failure";
//                    return false;
//                }
//            }
//            catch (Exception e)
//            {
//                result = e.Message;
//                return false;
//            }
//        }

//        /// <summary>
//        /// Raster导入GDB
//        /// </summary>
//        /// <param name="in_Raster">输入Raster</param>
//        /// <param name="gdb_Path">库完整路径</param>
//        /// <param name="rst_Name">库内文件名</param>
//        /// <param name="result">运行结果</param>
//        /// <returns></returns>
//        public static bool ImportRasterToGDB(IRaster in_Raster, string gdb_Path, string rst_Name, out string result)
//        {
//            try
//            {
//                ESRI.ArcGIS.DataManagementTools.CopyRaster cr = new ESRI.ArcGIS.DataManagementTools.CopyRaster();
//                cr.in_raster = in_Raster;
//                cr.out_rasterdataset = gdb_Path + "/" + GetNameGDB(gdb_Path, rst_Name);
//                cr.background_value = 0;
//                Geoprocessor gp = new Geoprocessor();
//                gp.OverwriteOutput = true;
//                IGeoProcessorResult gpr = gp.Execute(cr, null) as IGeoProcessorResult;
//                if (gpr != null && gpr.Status == esriJobStatus.esriJobSucceeded)
//                {
//                    result = "Success";
//                    return true;
//                }
//                else
//                {
//                    result = "Failure";
//                    return false;
//                }
//            }
//            catch(Exception e)
//            {
//                result = e.Message;
//                return false;
//            }
//        }

//        /// <summary>
//        /// GDB导出Shapefile
//        /// </summary>
//        /// <param name="gdb_Path">服务端库完整路径</param>
//        /// <param name="feat_Name">服务端库内文件名</param>
//        /// <param name="outputFolderPath">输出路径</param>
//        /// <param name="outputName">输出名（无需后缀）</param>
//        /// <param name="result">运行结果</param>
//        /// <param name="layer">操作图层</param>
//        /// <returns></returns>
//        public static bool ExportFeaturesFromGDB(string gdb_Path, string feat_Name, string outputFolderPath, string outputName, out string result, out ILayer layer) 
//        {
//            try
//            {
//                ESRI.ArcGIS.DataManagementTools.CopyFeatures cfs = new ESRI.ArcGIS.DataManagementTools.CopyFeatures();
//                cfs.in_features = gdb_Path + "\\" + feat_Name;
//                if (System.IO.Path.GetExtension(outputFolderPath).ToUpper().Equals(".GDB"))
//                    cfs.out_feature_class = outputFolderPath + "\\" + GetNameGDB(outputFolderPath, outputName);
//                else cfs.out_feature_class = outputFolderPath + "\\" + GetNameShapeFile(outputFolderPath, feat_Name);

//                Geoprocessor gp = new Geoprocessor();
//                gp.OverwriteOutput = true;
//                IGeoProcessorResult gpr = gp.Execute(cfs, null) as IGeoProcessorResult;
//                layer = null;

//                if (gpr != null && gpr.Status == esriJobStatus.esriJobSucceeded)
//                {
//                    if (MessageBox.Show("是否将图层加入到当前工程？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//                    {
//                        IWorkspaceFactory wsf;
//                        if (System.IO.Path.GetExtension(outputFolderPath).ToUpper().Equals(".GDB"))
//                            wsf = new FileGDBWorkspaceFactoryClass();
//                        else wsf = new ShapefileWorkspaceFactoryClass();
                        
//                        IFeatureWorkspace featWs = (IFeatureWorkspace)wsf.OpenFromFile(outputFolderPath, 0);
//                        IFeatureLayer featLyr = new FeatureLayerClass();
//                        featLyr.FeatureClass = featWs.OpenFeatureClass(outputName) as IFeatureClass;
//                        layer = featLyr as ILayer;
//                        layer.Name = GetNameGDB(outputFolderPath, outputName);
//                    }
//                    result = "Success";
//                    return true;
//                }
//                else
//                {
//                    result = "Failure";
//                    return false;
//                }
//            }
//            catch (Exception e) 
//            {
//                result = e.Message;
//                layer = null;
//                return false;
//            }
//        }

//        /// <summary>
//        /// GDB导出Raster
//        /// </summary>
//        /// <param name="gdb_Path">服务端库完整路径</param>
//        /// <param name="feat_Name">服务端文件名</param>
//        /// <param name="outputFolderPath">输出路径</param>
//        /// <param name="outputName">输出文件名（无需后缀）</param>
//        /// <param name="result">运行结果</param>
//        /// <param name="layer">操作图层</param>
//        /// <returns></returns>
//        public static bool ExportRasterFromGDB(string gdb_Path, string feat_Name, string outputFolderPath, string outputName, out string result, out ILayer layer)
//        {
//            try
//            {
//                ESRI.ArcGIS.DataManagementTools.CopyRaster cr = new ESRI.ArcGIS.DataManagementTools.CopyRaster();
//                cr.in_raster = gdb_Path + "\\" + feat_Name;
//                if (System.IO.Path.GetExtension(outputFolderPath).ToUpper().Equals(".GDB"))
//                    cr.out_rasterdataset = outputFolderPath + "\\" + GetNameGDB(outputFolderPath, outputName);
//                else cr.out_rasterdataset = outputFolderPath + "\\" + GetNameRaster(outputFolderPath, feat_Name);

//                Geoprocessor gp = new Geoprocessor();
//                gp.OverwriteOutput = true;
//                IGeoProcessorResult gpr = gp.Execute(cr, null) as IGeoProcessorResult;
//                layer = null;

//                if (gpr != null && gpr.Status == esriJobStatus.esriJobSucceeded)
//                {
//                    if (MessageBox.Show("是否将图层加入到当前工程？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//                    {
//                        IWorkspaceFactory2 wsf;
//                        if (System.IO.Path.GetExtension(outputFolderPath).ToUpper().Equals(".GDB"))
//                            wsf = new FileGDBWorkspaceFactoryClass();
//                        else wsf = new RasterWorkspaceFactoryClass();
//                        IRasterWorkspace2 rstWs = (IRasterWorkspace2)wsf.OpenFromFile(outputFolderPath, 0);
//                        IRasterLayer rstLyr = new RasterLayerClass();
//                        rstLyr.CreateFromDataset(rstWs.OpenRasterDataset(outputName));
//                        layer = rstLyr as ILayer;
//                        layer.Name = GetNameGDB(outputFolderPath, outputName);
//                    }
//                    result = "Success";
//                    return true;
//                }
//                else
//                {
//                    result = "Failure";
//                    return false;
//                }
//            }
//            catch(Exception e)
//            {
//                result = e.Message;
//                layer = null;
//                return false;
//            }
//        }

//        public static ILayer VectorView(string gdb_Path, string feat_Name) 
//        {
//            try
//            {
//                IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
//                IFeatureWorkspace pRasterWKEx = (IFeatureWorkspace)pWKF.OpenFromFile(gdb_Path, 0);
//                IFeatureLayer featLyr = new FeatureLayerClass();
//                featLyr.FeatureClass = pRasterWKEx.OpenFeatureClass(feat_Name) as IFeatureClass;
//                ILayer lyr = featLyr as ILayer;
//                lyr.Name = feat_Name;
//                return lyr;
//            }
//            catch(Exception e)
//            {
//                return null;
//            }
//        }

//        public static ILayer RasterView(string gdb_Path, string feat_Name) 
//        {
//            try
//            {
//                IWorkspaceFactory2 pWKF = new FileGDBWorkspaceFactoryClass();
//                IRasterWorkspace2 pRasterWKEx = (IRasterWorkspace2)pWKF.OpenFromFile(gdb_Path, 0);
//                IRasterLayer featLyr = new RasterLayerClass();
//                IRasterDataset rstDs = pRasterWKEx.OpenRasterDataset(feat_Name) as IRasterDataset;
//                IRaster rst = rstDs.CreateDefaultRaster();
//                featLyr.CreateFromRaster(rst);
//                ILayer lyr = featLyr as ILayer;
//                lyr.Name = feat_Name;
//                return lyr;
//            }
//            catch(Exception e)
//            {
//                return null;
//            }
//        }

//        public static string GetNameGDB(string gdb_Path, string file_Name) 
//        {
//            IWorkspaceFactory wsf = new FileGDBWorkspaceFactoryClass();
//            IWorkspace ws = wsf.OpenFromFile(gdb_Path, 0);
//            IEnumDatasetName edn = ws.get_DatasetNames(esriDatasetType.esriDTAny);
//            return Check(edn, file_Name, string.Empty);
//        }

//        public static string GetNameShapeFile(string gdb_Path, string file_Name)
//        {
//            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
//            IWorkspace ws = wsf.OpenFromFile(gdb_Path, 0);
//            IEnumDatasetName edn = ws.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
//            return Check(edn, file_Name, string.Empty);
//        }

//        public static string GetNameRaster(string gdb_Path, string file_Name)
//        {
//            IWorkspaceFactory wsf = new RasterWorkspaceFactory();
//            IWorkspace ws = wsf.OpenFromFile(gdb_Path, 0);
//            IEnumDatasetName edn = ws.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
//            return Check(edn, file_Name, string.Empty);
//        }

//        public static string Check(IEnumDatasetName edn, string feat_Name, string index) 
//        {
//            edn.Reset();
//            IDatasetName dsName = edn.Next();
//            while (dsName != null)
//            {
//                if (dsName.Name.ToLower().Equals((feat_Name + index).ToLower()))
//                {
//                    if (index == string.Empty) return Check(edn, feat_Name, "1");
//                    else return Check(edn, feat_Name, (int.Parse(index) + 1).ToString());
//                }
//                dsName = edn.Next();
//            }
//            return feat_Name + index.ToString();
//        }


//    }
//}
