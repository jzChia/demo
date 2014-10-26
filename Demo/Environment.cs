using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;

namespace Demo
{
    public static class Environment
    {
        public static Dictionary<string, string> GDB_Dict = new Dictionary<string, string>();
        public static Dictionary<string, string> GDB_Nickname = new Dictionary<string, string>();
        public static string indexDB=@"\INDEX\INDEX.accdb";
        public static string serverROOT = @"D:\app\Data";
        public static string serverIP = @"\\Server-PC\d\Data";

        public static void InitGDB_Dict()
        {
            GDB_Dict.Clear();
            GDB_Dict.Add("GDB_RASTER", @"\GDB\Raster.gdb");
            GDB_Dict.Add("GDB_VECTOR", @"\GDB\Vector.gdb");
            GDB_Dict.Add("GDB_DEM", @"\GDB\DEM.gdb");
            GDB_Dict.Add("FILE_RASTER", @"\FILE\Raster");
            GDB_Dict.Add("FILE_VECTOR", @"\FILE\Vector");
            GDB_Dict.Add("FILE_DEM", @"\FILE\DEM");
            GDB_Dict.Add("FILE_3D", @"\FILE\3D");
            GDB_Dict.Add("FILE_Test", @"\FILE\Test");
            GDB_Dict.Add("FILE_Other", @"\FILE\Other");

            GDB_Nickname.Clear();
            GDB_Nickname.Add("影像库", "GDB_RASTER");
            GDB_Nickname.Add("DEM库","GDB_DEM");
            GDB_Nickname.Add("矢量库","GDB_VECTOR");
            GDB_Nickname.Add("三维库","FILE_3D");
            GDB_Nickname.Add("试验库","FILE_Test");
            GDB_Nickname.Add("其他库","FILE_Other");
        }
    }

    public class SDEPara
    {
        public string SERVER="server";
        public string INSTANCE="DB";
        public string DATABASE = "DB";
        public string USER="user";
        public string PASSWORD="pwd";
        public string VERSION="SDE.DEFAULT";
        public string AUTHENTICATION_MODE="DBMS";

        /// <summary>
        /// 创建sde连接参数
        /// </summary>
        /// <param name="serverIP">服务器IP(直连可以为空)</param>
        /// <param name="instance">数据库实例(基于服务的连接为5151或者esri_sde,如果是直连则为sde:postgresql:localhost)</param>
        /// <param name="user">sde用户名</param>
        /// <param name="password">sde用户密码</param>
        /// <param name="version">sde版本</param>
        /// <param name="database">数据库名称,一般为sde</param>
        /// <param name="isDirectConnection">是否直连</param>
        /// <returns></returns>
        public IPropertySet CreatePropertySet(string serverIP, string instance, string user, string password, string version, string database, bool isDirectConnection)
        {
            IPropertySet pPropertySet = new PropertySetClass();
            pPropertySet.SetProperty("USER", user);
            pPropertySet.SetProperty("PASSWORD", password);
            pPropertySet.SetProperty("VERSION", version);
            if (isDirectConnection)
            {
                if (instance.Contains(":"))
                {
                    pPropertySet.SetProperty("INSTANCE", instance);
                    pPropertySet.SetProperty("DATABASE", database);
                }
                else
                {
                    throw new Exception("直连字符串参数'instance'不对,例如 sde:postgresql:localhost");
                }
            }
            else
            {
                pPropertySet.SetProperty("SERVER", serverIP);
                pPropertySet.SetProperty("INSTANCE", instance);
            }
            return pPropertySet;
        }


        //基于服务的连接
        private void baseOnSDE()
        {
            IWorkspaceFactory pWorkspaceFactory = new SdeWorkspaceFactoryClass();
            IWorkspace pWorkspace = pWorkspaceFactory.Open(CreatePropertySet("192.168.0.40", "5151", "sde", "hy@123456", "sde.DEFAULT", "sde", false), 0);
            DebugInfo(pWorkspace);
        }
        //直连
        private void directConnect()
        {
            IWorkspaceFactory pWorkspaceFactory = new SdeWorkspaceFactoryClass();
            IWorkspace pWorkspace = pWorkspaceFactory.Open(CreatePropertySet("", "sde:postgresql:192.168.0.40", "sde", "hy@123456", "sde.DEFAULT", "sde", true), 0);
            DebugInfo(pWorkspace);
        }

        private void DebugInfo(IWorkspace pWorkspace)
        {
            object names;
            object values;
            pWorkspace.ConnectionProperties.GetAllProperties(out names, out values);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] nameArray = (string[])names;
            object[] valueArray = (object[])values;
            for (int i = 0; i < nameArray.Length; i++)
            {
                sb.AppendFormat("{0}    {1}\r\n", nameArray[i], valueArray[i]);
            }
            //MessageBox.Show(sb.ToString());

            IEnumDatasetName pEnumDatasetName = pWorkspace.get_DatasetNames(esriDatasetType.esriDTAny);
            IDatasetName tName;
            string s = "";
            while ((tName = pEnumDatasetName.Next()) != null)
            {
                s += tName.Name + "\n";
            }
            //MessageBox.Show(s);
        }

    }


}
