using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Demo.Entities
{
    public class BaseLayer
    {
        public string LAYERID;
        public string LAYERNAME = "";
        public string DBNAME = "";
        public string URI = "";
        public string MAPNUM = "";
        public long SCALE;
        public string PROJECTION = "";
        public string AUTHOR = "";
        public string CREATETIME = "";
        public string DATATYPE = "";
        public string DESCRIPTION = "";
        public long LAYERSIZE;
        public bool ISEXIST;
        public int QUERYGRADER;
        public int GETGRADER;
        public string REGION="";
        public DateTime DATAGETDATE ;
        public string DATAFORMAT="";
        public string DATASOURCES= "";
        [XmlIgnore] 
        public string XMLFILE = "";
    }
}
