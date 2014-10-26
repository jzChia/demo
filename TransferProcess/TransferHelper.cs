using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;

namespace TransferProcess
{
    public class TransferHelper
    {
        /// <summary>
        /// 文件传输
        /// </summary>
        /// <param name="task"></param>
        public static void CopyFiles(TransferTask task)
        {
            if (task.RenameMode == RenameMode.Overwrite)
            {
                System.IO.File.Copy(task.SourceFileName, task.DestFileName, true);
            }
            else if (task.RenameMode == RenameMode.Accumulate)
            {
                FilesReadHelper inputReadHelper = new FilesReadHelper(task.SourceFileName);
                FilesReadHelper outputReadHelper = new FilesReadHelper(task.DestFileName);
                System.IO.File.Copy(task.SourceFileName, outputReadHelper.Directory + "\\" + inputReadHelper.AccumulativeName);
            }
        }

        /// <summary>
        /// 栅格传输
        /// </summary>
        /// <param name="task"></param>
        public static void CopyRaster(TransferTask task) 
        {
            RasterReadHelper inputReadHelper = new RasterReadHelper(task.SourceFileName);
            RasterReadHelper outputReadHelper = new RasterReadHelper(task.DestFileName);
            string outputNameWithoutExtention = string.Empty;

            if (task.RenameMode.Equals(RenameMode.Overwrite))
            {
                outputReadHelper.Delete(outputReadHelper.NameWithoutExtension + outputReadHelper.Extension);
                outputNameWithoutExtention = outputReadHelper.NameWithoutExtension;
            }
            else 
            {
                outputNameWithoutExtention = inputReadHelper.AccumulativeName;
            }

            ESRI.ArcGIS.DataManagementTools.CopyRaster CpyRst = new ESRI.ArcGIS.DataManagementTools.CopyRaster(task.SourceFileName, outputReadHelper.Directory + "\\" + outputNameWithoutExtention);
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
        public static void CopyFeatures(TransferTask task) 
        {
            FeatureReadHelper inputReadHelper = new FeatureReadHelper(task.SourceFileName);
            FeatureReadHelper outputReadHelper = new FeatureReadHelper(task.DestFileName);
            string outputNameWithoutExtention = string.Empty;

            if (task.RenameMode.Equals(RenameMode.Overwrite))
            {
                outputReadHelper.Delete(outputReadHelper.NameWithoutExtension + outputReadHelper.Extension);
                outputNameWithoutExtention = outputReadHelper.NameWithoutExtension;
            }
            else
            {
                outputNameWithoutExtention = inputReadHelper.AccumulativeName;
            }

            ESRI.ArcGIS.DataManagementTools.CopyFeatures CpyFeat = new ESRI.ArcGIS.DataManagementTools.CopyFeatures(task.SourceFileName, outputReadHelper.Directory + "\\" + outputNameWithoutExtention);
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
