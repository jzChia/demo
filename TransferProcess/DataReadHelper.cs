using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesRaster;

namespace TransferProcess
{
    public class FilesReadHelper
    {
        public string Directory { get; set; }
        public string NameWithoutExtension { get; set; }
        public string Extension { get; set; }

        public FilesReadHelper(string path)
        {
            Directory = System.IO.Path.GetDirectoryName(path);
            NameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path);
            Extension = System.IO.Path.GetExtension(path);
        }

        public string AccumulativeName
        {
            get
            {
                string accumulateStr = string.Empty;
                while (System.IO.File.Exists(Directory + "\\" + NameWithoutExtension + accumulateStr + Extension))
                {
                    if (accumulateStr.Equals(string.Empty)) accumulateStr = "1";
                    else accumulateStr = (int.Parse(accumulateStr) + 1).ToString();
                }
                return NameWithoutExtension + accumulateStr + Extension;
            }
        }
    }

    public class FeatureReadHelper
    {
        public string Directory { get; set; }
        public string FeatDsName { get; set; }
        public string NameWithoutExtension { get; set; }
        public string Extension { get; set; }
        public IWorkspaceFactory Wsf { get; set; }
        public IWorkspace Ws { get; set; }
        public IFeatureWorkspace FeatWs { get; set; }
        public IFeatureClass FeatCls { get; set; }
        public IEnumDataset EnumDs { get; set; }

        public FeatureReadHelper(string path)
        {
            path = path.Trim();
            if (path.ToLower().Contains(".gdb"))
            {
                ReadFileGDBWorkspaceFactory(path);
            }
            else
            {
                ReadShapefileWorksapceFactory(path);
            }
        }

        private void ReadFileGDBWorkspaceFactory(string path)
        {
            Wsf = new FileGDBWorkspaceFactoryClass();
            Directory = path.Substring(0, path.IndexOf(".gdb") + 4);
            FeatDsName = System.IO.Path.GetDirectoryName(path.Substring(path.IndexOf(".gdb") + 5, path.Length - path.IndexOf(".gdb") - 5));
            NameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path.Substring(path.IndexOf(".gdb") + 5, path.Length - path.IndexOf(".gdb") - 5));
            Extension = System.IO.Path.GetExtension(path.Substring(path.IndexOf(".gdb") + 5, path.Length - path.IndexOf(".gdb") - 5));
            Ws = Wsf.OpenFromFile(Directory, 0);
            FeatWs = Ws as IFeatureWorkspace;
            if (FeatDsName.Equals(string.Empty))
            {
                IFeatureClassContainer featClsCtn = FeatWs.OpenFeatureDataset(FeatDsName) as IFeatureClassContainer;
                FeatCls = featClsCtn.get_ClassByName(NameWithoutExtension);
                EnumDs = Ws.get_Datasets(esriDatasetType.esriDTFeatureClass);
            }
            else
            {
                FeatCls = FeatWs.OpenFeatureClass(NameWithoutExtension);
                IFeatureDataset FeatDs = FeatWs.OpenFeatureDataset(FeatDsName);
                EnumDs = FeatDs.Subsets;
            }
        }

        private void ReadShapefileWorksapceFactory(string path)
        {
            Wsf = new ShapefileWorkspaceFactoryClass();
            Directory = System.IO.Path.GetDirectoryName(path);
            NameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path);
            Extension = System.IO.Path.GetExtension(path);
            Ws = Wsf.OpenFromFile(Directory, 0);
            FeatWs = Ws as IFeatureWorkspace;
            FeatCls = FeatWs.OpenFeatureClass(NameWithoutExtension);
            EnumDs = Ws.get_Datasets(esriDatasetType.esriDTFeatureClass);
        }

        private void ReadSDEWorkspaceFactory(string path, string server, string instance, string user, string password, string version)
        {
            IPropertySet set = new PropertySetClass();
            set.SetProperty("Server", server);
            set.SetProperty("Instance", instance);
            set.SetProperty("User", user);
            set.SetProperty("password", password);
            set.SetProperty("version", version);
            Wsf = new SdeWorkspaceFactoryClass();
            Wsf.Open(set, 0);
            FeatWs = Ws as IFeatureWorkspace;
        }

        public string AccumulativeName
        {
            get
            {
                return Check(EnumDs, string.Empty);
            }
        }

        private string Check(IEnumDataset eds, string index)
        {
            eds.Reset();
            IDataset ds = eds.Next();
            while (ds != null)
            {
                if (ds.Name.ToLower().Equals((NameWithoutExtension + index).ToLower()))
                {
                    if (index == string.Empty) return Check(eds, "1");
                    else return Check(eds, (int.Parse(index) + 1).ToString());
                }
                ds = eds.Next();
            }
            return NameWithoutExtension + index.ToString() + Extension;
        }

        /// <summary>
        /// 在该文件目录下删除指定名称的文件
        /// </summary>
        /// <param name="path"></param>
        public void Delete(string fileName)
        {
            IDataset ds = EnumDs.Next();
            while (ds != null)
            {
                if (ds.Name.ToLower().Equals(fileName))
                {
                    ds.Delete();
                }
                ds = EnumDs.Next();
            }
        }
    }

    public class RasterReadHelper
    {
        public string Directory { get; set; }
        public string NameWithoutExtension { get; set; }
        public string Extension { get; set; }
        public IWorkspaceFactory Wsf { get; set; }
        public IWorkspace Ws { get; set; }
        public IRasterWorkspace RstWs { get; set; }
        public IRasterWorkspaceEx RstWsEx { get; set; }
        public IRasterDataset2 RstDs { get; set; }
        public IRaster Rst { get; set; }
        public IEnumDataset EnumDs { get; set; }
        public IRasterProps RstProps { get; set; }
        public IRawBlocks RawBlocks { get; set; }
        public IRasterInfo RstInfo { get; set; }

        public RasterReadHelper(string path)
        {
            path = path.Trim();
            Directory = string.Empty;
            NameWithoutExtension = string.Empty;
            Extension = string.Empty;

            if (path.ToLower().Contains(".gdb"))
            {
                ReadFileGDBWorkspaceFactory(path);
            }
            else
            {
                ReadRasterWorkspaceFactory(path);
            }
        }

        private void ReadFileGDBWorkspaceFactory(string path)
        {
            Wsf = new FileGDBWorkspaceFactoryClass();
            Directory = path.Substring(0, path.IndexOf(".gdb") + 4);
            NameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path.Substring(path.IndexOf(".gdb") + 5, path.Length - path.IndexOf(".gdb") - 5));
            Extension = System.IO.Path.GetExtension(path.Substring(path.IndexOf(".gdb") + 5, path.Length - path.IndexOf(".gdb") - 5));
            Ws = Wsf.OpenFromFile(Directory, 0);
            RstWsEx = Ws as IRasterWorkspaceEx;
            RstDs = RstWsEx.OpenRasterDataset(NameWithoutExtension) as IRasterDataset2;

            Rst = RstDs.CreateDefaultRaster();
            RstProps = Rst as IRasterProps;
            RawBlocks = (IRawBlocks)RstDs;
            RstInfo = RawBlocks.RasterInfo;

            EnumDs = Ws.get_Datasets(esriDatasetType.esriDTRasterDataset);
        }

        private void ReadRasterWorkspaceFactory(string path)
        {
            Wsf = new RasterWorkspaceFactoryClass();
            Directory = System.IO.Path.GetDirectoryName(path);
            NameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(path);
            Extension = System.IO.Path.GetExtension(path);
            Ws = Wsf.OpenFromFile(Directory, 0);
            RstWs = Ws as IRasterWorkspace;
            RstDs = RstWs.OpenRasterDataset(NameWithoutExtension + Extension) as IRasterDataset2;

            Rst = RstDs.CreateDefaultRaster();
            RstProps = Rst as IRasterProps;
            RawBlocks = (IRawBlocks)RstDs;
            RstInfo = RawBlocks.RasterInfo;

            EnumDs = Ws.get_Datasets(esriDatasetType.esriDTRasterDataset);
        }

        public string AccumulativeName
        {
            get
            {
                return Check(EnumDs, string.Empty);
            }
        }

        public string Check(IEnumDataset eds, string index)
        {
            eds.Reset();
            IDataset ds = eds.Next();
            while (ds != null)
            {
                if (ds.Name.ToLower().Equals((NameWithoutExtension + index + Extension).ToLower()))
                {
                    if (index == string.Empty) return Check(eds, "1");
                    else return Check(eds, (int.Parse(index) + 1).ToString());
                }
                ds = eds.Next();
            }
            return NameWithoutExtension + index.ToString() + Extension;
        }

        /// <summary>
        /// 在该文件目录下删除指定名称的文件
        /// </summary>
        /// <param name="path"></param>
        public void Delete(string fileName)
        {
            IDataset ds = EnumDs.Next();
            while (ds != null)
            {
                if (ds.Name.ToLower().Equals(fileName.ToLower()))
                {
                    ds.Delete();
                }
                ds = EnumDs.Next();
            }
        }

    }

}
