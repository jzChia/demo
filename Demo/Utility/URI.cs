using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Utility
{
    public class URI
    {
        public string URIstring;
        public string GDB_Name;
        public string GDB_NickName;
        public string GDB_Path;
        public string LayerName;

        public string GetURI(URI uri)
        {
            return "";
        }

        public string GetTruePath(string uriStr)
        {
            Environment.InitGDB_Dict();

            if (!uriStr.Contains("#")) return "";

            string[] arr = uriStr.Split('#');
            GDB_Name = arr[0];
            LayerName = arr[1];

            if (Environment.GDB_Dict.ContainsKey(GDB_Name))
                GDB_Path = Environment.serverROOT + Environment.GDB_Dict[GDB_Name];
            else
                return "";

            return GDB_Path + LayerName;
        }

        public string GetServerTruePath(string uriStr)
        {
            Environment.InitGDB_Dict();

            if (!uriStr.Contains("#")) return "";

            string[] arr = uriStr.Split('#');
            GDB_Name = arr[0];
            LayerName = arr[1];

            if (Environment.GDB_Dict.ContainsKey(GDB_Name))
                GDB_Path = Environment.serverIP + Environment.GDB_Dict[GDB_Name];
            else
                return "";

            return (GDB_Path + "\\" + LayerName).Replace("\\", "/");
        }

    }

}
