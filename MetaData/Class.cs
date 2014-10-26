using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaData
{
    
    public class BaseLayer
    {
        public long LAYERID;
        public string LAYERNAME;
        public string URI;
        public string MAPNUM;
        public long SCALE;
        public string PROJECTION;
        public string AUTHOR;
        public string CREATETIME;
        public string DATATYPE;
        public string DESCRIPTION;
    }

    public class RasterLayer : BaseLayer
    {
        public long BANDCOUNT;
        public float MINX;
        public float MAXX;
        public float MINY;
        public float MAXY;
        public long HEIGHT;
        public long WIDTH;
        public float RESOLUTION;
        public float NODATAVALUE;
        public bool ISVISIBLE;
    }

    public class VectorLayer : BaseLayer
    {
        public string SHAPE;
        public float MINX;
        public float MAXX;
        public float MINY;
        public float MAXY;
        public bool ISVISIBLE;
    }

    public class FileLayer : BaseLayer
    {
        public string OPENAS;
        public bool ISFOLDER;
    }

    public class URI
    {
        public string URIstring;
        public string GDBTYPE;
        public string GDBNAME;
        public string GDBPATH;
        public string LAYERNAME;
    }
}
