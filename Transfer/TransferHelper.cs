using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesRaster;

namespace Transfer
{
    public class TransferHelper
    {
        /// <summary>
        /// 文件传输
        /// </summary>
        /// <param name="task"></param>
        public static void CopyFiles(string sourceFileName, string destFileName, RenameMode renameMode)
        {
            if (renameMode == RenameMode.Overwrite)
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
            }
            else if (renameMode == RenameMode.Accumulate)
            {
                FilesReadHelper inputReadHelper = new FilesReadHelper(sourceFileName);
                FilesReadHelper outputReadHelper = new FilesReadHelper(System.IO.Path.GetDirectoryName(destFileName) + "\\" + System.IO.Path.GetFileName(sourceFileName));
                System.IO.File.Copy(sourceFileName, outputReadHelper.Directory + "\\" + outputReadHelper.AccumulativeName);
            }
        }

        /// <summary>
        /// 栅格传输
        /// </summary>
        /// <param name="task"></param>
        public static void CopyRaster(string sourceFileName, string destFileName, RenameMode renameMode)
        {
            RasterReadHelper inputReadHelper = new RasterReadHelper(sourceFileName);
            RasterReadHelper outputReadHelper = new RasterReadHelper(destFileName);
            string outputNameWithoutExtention = string.Empty;

            if (renameMode.Equals(RenameMode.Overwrite))
            {
                outputReadHelper.Delete(outputReadHelper.NameWithoutExtension + outputReadHelper.Extension);
                outputNameWithoutExtention = outputReadHelper.NameWithoutExtension;
            }
            else
            {
                outputNameWithoutExtention = outputReadHelper.AccumulativeName;
            }

            ESRI.ArcGIS.DataManagementTools.CopyRaster CpyRst = new ESRI.ArcGIS.DataManagementTools.CopyRaster(sourceFileName, outputReadHelper.Directory + "\\" + outputNameWithoutExtention);
            Geoprocessor gp = new Geoprocessor();
            gp.SetEnvironmentValue("Compression", inputReadHelper.RstDs.CompressionType);
            gp.OverwriteOutput = true;
            IGeoProcessorResult gpResult = gp.Execute(CpyRst, null) as IGeoProcessorResult;
            if (gpResult == null || gpResult.Status == esriJobStatus.esriJobFailed)
            {
                for (int i = 0; i < gpResult.MessageCount; i++)
                {
                    MessageBox.Show(gpResult.GetMessage(i));
                }
            }
        }

        /// <summary>
        /// 传输矢量
        /// </summary>
        /// <param name="task"></param>
        public static void CopyFeatures(string sourceFileName, string destFileName, RenameMode renameMode)
        {
            FeatureReadHelper inputReadHelper = new FeatureReadHelper(sourceFileName);
            FeatureReadHelper outputReadHelper = new FeatureReadHelper(destFileName);
            string outputNameWithoutExtention = string.Empty;

            if (renameMode.Equals(RenameMode.Overwrite))
            {
                outputReadHelper.Delete(outputReadHelper.NameWithoutExtension + outputReadHelper.Extension);
                outputNameWithoutExtention = outputReadHelper.NameWithoutExtension;
            }
            else
            {
                outputNameWithoutExtention = inputReadHelper.AccumulativeName;
            }

            ESRI.ArcGIS.DataManagementTools.CopyFeatures CpyFeat = new ESRI.ArcGIS.DataManagementTools.CopyFeatures(sourceFileName, outputReadHelper.Directory + "\\" + outputNameWithoutExtention);
            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            IGeoProcessorResult gpResult = gp.Execute(CpyFeat, null) as IGeoProcessorResult;
            if (gpResult == null || gpResult.Status == esriJobStatus.esriJobFailed)
            {
                for (int i = 0; i < gpResult.MessageCount; i++)
                {
                    MessageBox.Show(gpResult.GetMessage(i));
                }
            }
        }
    }
}
