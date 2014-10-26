using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;

namespace Demo
{
    public class MyCopyFeatures
    {
        public void CopyFeatures(LoadTarget target)
        {
            IWorkspaceFactory sourceWsf;
            if (System.IO.Path.GetExtension(target.In_Directory).ToUpper().Equals(".GDB"))
                sourceWsf = new FileGDBWorkspaceFactoryClass();
            else sourceWsf = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace sourceFeatWs = sourceWsf.OpenFromFile(target.In_Directory, 0) as IFeatureWorkspace;
            IFeatureClass sourceFeatureClass = sourceFeatWs.OpenFeatureClass(target.In_FileName);

            IWorkspaceFactory targetWsf;
            if (System.IO.Path.GetExtension(target.Out_Directory).ToUpper().Equals(".GDB"))
                targetWsf = new FileGDBWorkspaceFactoryClass();
            else targetWsf = new ShapefileWorkspaceFactoryClass();

            IWorkspace targetWs = targetWsf.OpenFromFile(target.Out_Directory, 0) as IWorkspace;

            string targetFileName;
            if (System.IO.Path.GetExtension(target.Out_Directory).ToUpper().Equals(".GDB"))
                targetFileName = LoadHelper.GetNameGDB(target.Out_Directory, target.Out_FileName);
            else targetFileName = LoadHelper.GetNameShapeFile(target.Out_Directory, target.Out_FileName);

            IFeatureWorkspace targetFeatWs = targetWs as IFeatureWorkspace;
            IFeatureClassDescription featureClassDescription = new FeatureClassDescriptionClass();
            IObjectClassDescription objectClassDescription = featureClassDescription as IObjectClassDescription;

            IFields pFields = sourceFeatureClass.Fields;
            IFieldChecker pFieldChecker = new FieldCheckerClass();
            IEnumFieldError pEnumFieldError = null;
            IFields vFields = null;
            pFieldChecker.ValidateWorkspace = targetWs as IWorkspace;
            pFieldChecker.Validate(pFields, out pEnumFieldError, out vFields);

            IFeatureClass sdeFeatureClass = null;
            if (sdeFeatureClass == null)
            {
                sdeFeatureClass = targetFeatWs.CreateFeatureClass(targetFileName, vFields,
                    objectClassDescription.InstanceCLSID, objectClassDescription.ClassExtensionCLSID,
                    sourceFeatureClass.FeatureType, sourceFeatureClass.ShapeFieldName, "");
                IFeatureCursor featureCursor = sourceFeatureClass.Search(null, true);
                IFeature feature = featureCursor.NextFeature();
                IFeatureCursor sdeFeatureCursor = sdeFeatureClass.Insert(true);
                IFeatureBuffer sdeFeatureBuffer;
                IQueryFilter qf = new QueryFilterClass();
                target.Size = sourceFeatureClass.FeatureCount(qf);
                while (feature != null)
                {
                    sdeFeatureBuffer = sdeFeatureClass.CreateFeatureBuffer();
                    IField shpField = new FieldClass();
                    IFields shpFields = feature.Fields;
                    for (int i = 0; i < shpFields.FieldCount; i++)
                    {
                        shpField = shpFields.get_Field(i);
                        if (shpField.Name.ToLower().Contains("area") || shpField.Name.ToLower().Contains("leng") || shpField.Name.ToLower().Contains("fid") || shpField.Name.ToLower().Contains("objectid")) continue;
                        int index = sdeFeatureBuffer.Fields.FindField(shpField.Name);
                        if (index != -1)
                        {
                            sdeFeatureBuffer.set_Value(index, feature.get_Value(i));
                        }
                    }
                    sdeFeatureCursor.InsertFeature(sdeFeatureBuffer);
                    sdeFeatureCursor.Flush();
                    feature = featureCursor.NextFeature();
                    target.Progress++;
                }
                featureCursor.Flush();
                target.IsFinished = true;
                target.IsBusy = false;
            }
        }

        

        public static void CopyRaster(IRaster sourceRaster, string sourceFileName, IWorkspace targetWs, string targetFileName, LoadTarget target)
        {
            //IRasterWorkspace2 targetRstWs = targetWs as IRasterWorkspace;
            //IRasterLayer rstlyr;

            //IRasterCursor rstCsr=sourceRaster.CreateCursor();
            //rstCsr.nn

            //IRaster sdeRaster = null;
            //if (sdeRaster == null)
            //{
            //    sdeRaster = targetRstWs.CreateRasterDataset(targetFileName,sdeRaster.
            //    IFeatureCursor featureCursor = sourceFeatureClass.Search(null, true);
            //    IFeature feature = featureCursor.NextFeature();
            //    IFeatureCursor sdeFeatureCursor = sdeRaster.Insert(true);
            //    IFeatureBuffer sdeFeatureBuffer;
            //    IQueryFilter qf = new QueryFilterClass();
            //    target.Size = sourceFeatureClass.FeatureCount(qf);
            //    while (feature != null)
            //    {
            //        sdeFeatureBuffer = sdeRaster.CreateFeatureBuffer();
            //        IField shpField = new FieldClass();
            //        IFields shpFields = feature.Fields;
            //        for (int i = 0; i < shpFields.FieldCount; i++)
            //        {
            //            shpField = shpFields.get_Field(i);
            //            if (shpField.Name.ToLower().Contains("area") || shpField.Name.ToLower().Contains("leng") || shpField.Name.ToLower().Contains("fid") || shpField.Name.ToLower().Contains("objectid")) continue;
            //            int index = sdeFeatureBuffer.Fields.FindField(shpField.Name);
            //            if (index != -1)
            //            {
            //                sdeFeatureBuffer.set_Value(index, feature.get_Value(i));
            //            }
            //        }
            //        sdeFeatureCursor.InsertFeature(sdeFeatureBuffer);
            //        sdeFeatureCursor.Flush();
            //        feature = featureCursor.NextFeature();
            //        target.Progress++;
            //    }
            //    featureCursor.Flush();
            //}
        }
    }
}
