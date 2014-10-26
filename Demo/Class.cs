//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data.OleDb;
//using Oracle.DataAccess.Client;
//using System.Reflection;
//using System.Windows.Forms;

//namespace Demo
//{
//    public class BaseLayer
//    {
//        public string LAYERID;
//        public string LAYERNAME = "";
//        public string DBNAME = "";
//        public string URI = "";
//        public string MAPNUM = "";
//        public long SCALE;
//        public string PROJECTION = "";
//        public string AUTHOR = "";
//        public string CREATETIME = "";
//        public string DATATYPE = "";
//        public string DESCRIPTION = "";
//        public long LAYERSIZE;
//        public bool ISEXIST;
//    }

//    public class RasterLayer : BaseLayer
//    {
//        public long BANDCOUNT;
//        public float MINX;
//        public float MAXX;
//        public float MINY;
//        public float MAXY;
//        public long HEIGHT;
//        public long WIDTH;
//        public float RESOLUTION;
//        public float NODATAVALUE;
//        public bool ISVISIBLE;


//        public static string errMSG = "";
//        private static RasterLayer InitRasterData(RasterLayer RL,object obj, string fieldName)
//        {
//            try
//            {
//                switch (fieldName)
//                {
//                    case "LAYERID": RL.LAYERID = Convert.ToString(obj); break;
//                    case "AUTHOR": RL.AUTHOR = Convert.ToString(obj); break;
//                    case "BANDCOUNT": RL.BANDCOUNT = Convert.ToInt64(obj);break;
//                    case "CREATETIME": RL.CREATETIME = Convert.ToString(obj);break;
//                    case "DATATYPE": RL.DATATYPE = Convert.ToString(obj);break;
//                    case "DBNAME": RL.DBNAME = Convert.ToString(obj);break;
//                    case "DESCRIPTION": RL.DESCRIPTION = Convert.ToString(obj);break;
//                    case "HEIGHT": RL.HEIGHT = Convert.ToInt64(obj); break;
//                    case "LAYERSIZE": RL.LAYERSIZE = Convert.ToInt64(obj); break;
//                    case "ISEXIST": RL.ISEXIST = Convert.ToBoolean(Convert.ToInt32(obj)); break;
//                    case "ISVISIBLE": RL.ISVISIBLE = Convert.ToBoolean(Convert.ToInt32(obj));break;
//                    case "LAYERNAME": RL.LAYERNAME = Convert.ToString(obj);break;
//                    case "MAPNUM": RL.MAPNUM = Convert.ToString(obj);break;
//                    case "MAXX": RL.MAXX = Convert.ToSingle(obj);break;
//                    case "MAXY": RL.MAXY = Convert.ToSingle(obj);break;
//                    case "MINX": RL.MINX = Convert.ToSingle(obj);break;
//                    case "MINY": RL.MINY = Convert.ToSingle(obj); break;
//                    case "NODATAVALUE": RL.NODATAVALUE = Convert.ToSingle(obj); break;
//                    case "PROJECTION": RL.PROJECTION = Convert.ToString(obj); break;
//                    case "RESOLUTION": RL.RESOLUTION = Convert.ToSingle(obj); break;
//                    case "SCALE": RL.SCALE = Convert.ToInt64(obj); break;
//                    case "URI": RL.URI = Convert.ToString(obj); break;
//                    case "WIDTH": RL.WIDTH = Convert.ToInt64(obj); break;
//                    default: break;
//                }
//            }
//            catch
//            {
//                errMSG = fieldName+ ";";
//            }

//            return RL;
//        }



//        public static int ImportRasterIndexOra(RasterLayer RL)
//        {
//            OracleConnection oraRasterConn;
//            OracleCommand oraCMD;

//            oraRasterConn = DBCon.OraConOpen();;

//            string sql;
//            sql = "INSERT INTO RASTER_LAYERS(LAYERID,LAYERNAME,DBNAME,URI,MAPNUM,SCALE,PROJECTION,AUTHOR,CREATETIME,DATATYPE,DESCRIPTION,BANDCOUNT,MINX,MAXX,MINY,MAXY,HEIGHT,WIDTH,RESOLUTION,NODATAVALUE,ISVISIBLE,LAYERSIZE,ISEXIST) " +
//                " VALUES(sys_guid(),:p1, :p2, :p3, :p4, :p5, :p6, :p7, :p8, :p9, :p10, :p11, :p12, :p13, :p14, :p15, :p16, :p17, :p18, :p19, :p20,:p21,:p22)";
//            oraCMD = new OracleCommand(sql, oraRasterConn);
//            oraCMD.Parameters.Add(":p1", RL.LAYERNAME);
//            oraCMD.Parameters.Add(":p2", RL.DBNAME);
//            oraCMD.Parameters.Add(":p3", RL.URI);
//            oraCMD.Parameters.Add(":p4", RL.MAPNUM);
//            oraCMD.Parameters.Add(":p5", RL.SCALE);
//            oraCMD.Parameters.Add(":p6", RL.PROJECTION);
//            oraCMD.Parameters.Add(":p7", RL.AUTHOR);
//            oraCMD.Parameters.Add(":p8", RL.CREATETIME);
//            oraCMD.Parameters.Add(":p9", RL.DATATYPE);
//            oraCMD.Parameters.Add(":p10", RL.DESCRIPTION);
//            oraCMD.Parameters.Add(":p11", RL.BANDCOUNT);
//            oraCMD.Parameters.Add(":p12", RL.MINX);
//            oraCMD.Parameters.Add(":p13", RL.MAXX);
//            oraCMD.Parameters.Add(":p14", RL.MINY);
//            oraCMD.Parameters.Add(":p15", RL.MAXY);
//            oraCMD.Parameters.Add(":p16", RL.HEIGHT);
//            oraCMD.Parameters.Add(":p17", RL.WIDTH);
//            oraCMD.Parameters.Add(":p18", RL.RESOLUTION);
//            oraCMD.Parameters.Add(":p19", RL.NODATAVALUE);
//            oraCMD.Parameters.Add(":p20", Convert.ToInt64(RL.ISVISIBLE));
//            oraCMD.Parameters.Add(":p21", RL.LAYERSIZE);
//            oraCMD.Parameters.Add(":p22", Convert.ToInt64(RL.ISEXIST));


//            int result = oraCMD.ExecuteNonQuery();

//            oraRasterConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static RasterLayer GetRasterIndexOra(string LayerID)
//        {
//            RasterLayer RL = new RasterLayer();

//            OracleConnection oraRasterConn;
//            OracleCommand oraCMD;
//            OracleDataReader oraDataReader;

//            oraRasterConn = DBCon.OraConOpen(); ;

//            string sql;

//            sql = "SELECT * FROM RASTER_LAYERS WHERE LAYERID = :p1";
//            oraCMD = new OracleCommand(sql, oraRasterConn);
//            oraCMD.Parameters.Add(":p1", LayerID);


//            try { oraDataReader = oraCMD.ExecuteReader(); }
//            catch { return null; }

//            if (!oraDataReader.HasRows)
//            {
//                return null;
//            }

//            while (oraDataReader.Read())
//            {
//                int fieldCount = oraDataReader.FieldCount;
               
//                for (int i = 0; i < fieldCount;i++ )
//                {
//                    string fieldName = oraDataReader.GetName(i).ToString();
//                    RL = InitRasterData(RL, oraDataReader[fieldName], fieldName);
    
//                }
               
//            }
//            oraRasterConn.Close();
//            oraCMD.Dispose();

//            return RL;
//        }

//        /// <summary>
//        /// 写入元数据库
//        /// </summary>
//        /// <param name="RL"></param>
//        /// <returns></returns>
//        public static int ImportRasterIndex(RasterLayer RL)
//        {
//            OleDbConnection my_con;
//            OleDbCommand dbcmd;

//            string SQL;
//            my_con = DBCon.DBCon_Open();

//            SQL = "INSERT INTO [RASTER_LAYERS] ([LAYERNAME],[DBNAME],[URI],[MAPNUM],[SCALE],[PROJECTION],[AUTHOR],[CREATETIME],[DATATYPE],[DESCRIPTION],[BANDCOUNT],[MINX],[MAXX],[MINY],[MAXY],[HEIGHT],[WIDTH],[RESOLUTION],[NODATAVALUE],[ISVISIBLE]) " +
//                 "VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20)";
//            dbcmd = new OleDbCommand(SQL, my_con);

//            dbcmd.Parameters.AddWithValue("@p1", RL.LAYERNAME);
//            dbcmd.Parameters.AddWithValue("@p2", RL.DBNAME);
//            dbcmd.Parameters.AddWithValue("@p3", RL.URI);
//            dbcmd.Parameters.AddWithValue("@p4", RL.MAPNUM);
//            dbcmd.Parameters.AddWithValue("@p5", RL.SCALE);
//            dbcmd.Parameters.AddWithValue("@p6", RL.PROJECTION);
//            dbcmd.Parameters.AddWithValue("@p7", RL.AUTHOR);
//            dbcmd.Parameters.AddWithValue("@p8", RL.CREATETIME);
//            dbcmd.Parameters.AddWithValue("@p9", RL.DATATYPE);
//            dbcmd.Parameters.AddWithValue("@p10", RL.DESCRIPTION);
//            dbcmd.Parameters.AddWithValue("@p11", RL.BANDCOUNT);
//            dbcmd.Parameters.AddWithValue("@p12", RL.MINX);
//            dbcmd.Parameters.AddWithValue("@p13", RL.MAXX);
//            dbcmd.Parameters.AddWithValue("@p14", RL.MINY);
//            dbcmd.Parameters.AddWithValue("@p15", RL.MAXY);
//            dbcmd.Parameters.AddWithValue("@p16", RL.HEIGHT);
//            dbcmd.Parameters.AddWithValue("@p17", RL.WIDTH);
//            dbcmd.Parameters.AddWithValue("@p18", RL.RESOLUTION);
//            dbcmd.Parameters.AddWithValue("@p19", RL.NODATAVALUE);
//            dbcmd.Parameters.AddWithValue("@p20", Convert.ToInt64(RL.ISVISIBLE));


//            int r = dbcmd.ExecuteNonQuery();

//            my_con.Close();
//            my_con.Dispose();

//            return r;
//        }
//        public static RasterLayer GetRasterIndex(long LayerID)
//        {
//            RasterLayer RL = new RasterLayer();
            
//            OleDbConnection my_con;
//            OleDbCommand dbcmd;
//            OleDbDataReader dbrd;
//            string SQL;
//            my_con = DBCon.DBCon_Open();

//            SQL = "SELECT * FROM [RASTER_LAYERS] WHERE ID = @p1";
//            OleDbDataAdapter adapter = new OleDbDataAdapter();
//            dbcmd = new OleDbCommand(SQL, my_con);
//            dbcmd.Parameters.AddWithValue("@p1", LayerID);

//            adapter.SelectCommand = dbcmd;

//            try { dbrd = dbcmd.ExecuteReader(); }
//            catch { return null; }

//            if (!dbrd.HasRows)
//            {
//                return null;
//            }

//            string errMSG = "";
//            while (dbrd.Read())
//            {
//                try { RL.AUTHOR = Convert.ToString(dbrd["AUTHOR"]); }
//                catch { errMSG += "AUTHOR;"; }

//                try { RL.BANDCOUNT = Convert.ToInt64(dbrd["BANDCOUNT"]); }
//                catch { errMSG += "BANDCOUNT;"; }

//                try { RL.CREATETIME = Convert.ToString(dbrd["CREATETIME"]); }
//                catch { errMSG += "CREATETIME;"; }

//                try { RL.DATATYPE = Convert.ToString(dbrd["DATATYPE"]); }
//                catch { errMSG += "DATATYPE;"; }

//                try { RL.DBNAME = Convert.ToString(dbrd["DBNAME"]); }
//                catch { errMSG += "DBNAME;"; }

//                try { RL.DESCRIPTION = Convert.ToString(dbrd["DESCRIPTION"]); }
//                catch { errMSG += "DESCRIPTION;"; }

//                try { RL.HEIGHT = Convert.ToInt64(dbrd["HEIGHT"]); }
//                catch { errMSG += "HEIGHT;"; }

//                try { RL.ISVISIBLE = Convert.ToBoolean(dbrd["ISVISIBLE"]); }
//                catch { errMSG += "ISVISIBLE;"; }

//                try { RL.LAYERNAME = Convert.ToString(dbrd["LAYERNAME"]); }
//                catch { errMSG += "LAYERNAME;"; }

//                try { RL.MAPNUM = Convert.ToString(dbrd["MAPNUM"]); }
//                catch { errMSG += "MAPNUM;"; }

//                try { RL.MAXX = Convert.ToSingle(dbrd["MAXX"]); }
//                catch { errMSG += "MAXX;"; }

//                try { RL.MAXY = Convert.ToSingle(dbrd["MAXY"]); }
//                catch { errMSG += "MAXY;"; }

//                try { RL.MINX = Convert.ToSingle(dbrd["MINX"]); }
//                catch { errMSG += "MINX;"; }

//                try { RL.MINY = Convert.ToSingle(dbrd["MINY"]); }
//                catch { errMSG += "MINY;"; }

//                try { RL.NODATAVALUE = Convert.ToSingle(dbrd["NODATAVALUE"]); }
//                catch { errMSG += "NODATAVALUE;"; }

//                try { RL.PROJECTION = Convert.ToString(dbrd["PROJECTION"]); }
//                catch { errMSG += "PROJECTION;"; }

//                try { RL.RESOLUTION = Convert.ToSingle(dbrd["RESOLUTION"]); }
//                catch { errMSG += "RESOLUTION;"; }

//                try { RL.SCALE = Convert.ToInt64(dbrd["SCALE"]); }
//                catch { errMSG += "SCALE;"; }

//                try { RL.URI = Convert.ToString(dbrd["URI"]); }
//                catch { errMSG += "URI;"; }

//                try { RL.WIDTH = Convert.ToInt64(dbrd["WIDTH"]); }
//                catch { errMSG += "WIDTH;"; }
//            }
//            my_con.Close();
//            my_con.Dispose();

//            return RL;
//        }
//    }

//    public class VectorLayer : BaseLayer
//    {
//        public string SHAPE="";
//        public float MINX;
//        public float MAXX;
//        public float MINY;
//        public float MAXY;
//        public bool ISVISIBLE;

//        public static string errMSG = "";
//        private static VectorLayer InitVectorData(VectorLayer VL, object obj, string fieldName)
//        {
//            try
//            {
//                switch (fieldName)
//                {
//                    case "LAYERID": VL.LAYERID = Convert.ToString(obj); break;
//                    case "AUTHOR": VL.AUTHOR = Convert.ToString(obj); break;
//                    case "CREATETIME": VL.CREATETIME = Convert.ToString(obj); break;
//                    case "DATATYPE": VL.DATATYPE = Convert.ToString(obj); break;
//                    case "DBNAME": VL.DBNAME = Convert.ToString(obj); break;
//                    case "DESCRIPTION": VL.DESCRIPTION = Convert.ToString(obj); break;
//                    case "ISVISIBLE": VL.ISVISIBLE = Convert.ToBoolean(Convert.ToInt32(obj)); break;
//                    case "LAYERSIZE": VL.LAYERSIZE = Convert.ToInt64(obj); break;
//                    case "ISEXIST": VL.ISEXIST = Convert.ToBoolean(Convert.ToInt32(obj)); break;
//                    case "LAYERNAME": VL.LAYERNAME = Convert.ToString(obj); break;
//                    case "MAPNUM": VL.MAPNUM = Convert.ToString(obj); break;
//                    case "MAXX": VL.MAXX = Convert.ToSingle(obj); break;
//                    case "MAXY": VL.MAXY = Convert.ToSingle(obj); break;
//                    case "MINX": VL.MINX = Convert.ToSingle(obj); break;
//                    case "MINY": VL.MINY = Convert.ToSingle(obj); break;
//                    case "PROJECTION": VL.PROJECTION = Convert.ToString(obj); break;
//                    case "SCALE": VL.SCALE = Convert.ToInt64(obj); break;
//                    case "URI": VL.URI = Convert.ToString(obj); break;
//                    case "SHAPE": VL.SHAPE = Convert.ToString(obj); break;
//                    default: break;
//                }
//            }
//            catch
//            {
//                errMSG = fieldName + ";";
//            }

//            return VL;
//        }
//        public static int ImportVectorIndexOra(VectorLayer VL)
//        {
//            OracleConnection oraVectorConn;
//            OracleCommand oraCMD;

//            oraVectorConn = DBCon.OraConOpen(); ;

//            string sql;
//            sql = "INSERT INTO VECTOR_LAYERS(LAYERID,LAYERNAME,DBNAME,URI,MAPNUM,SCALE,PROJECTION,AUTHOR,CREATETIME,DATATYPE,DESCRIPTION,SHAPE,MINX,MAXX,MINY,MAXY,ISVISIBLE,LAYERSIZE,ISEXIST) " +
//                " VALUES(sys_guid(),:p1, :p2, :p3, :p4, :p5, :p6, :p7, :p8, :p9, :p10, :p11, :p12, :p13, :p14, :p15, :p16, :p17, :p18)";
//            oraCMD = new OracleCommand(sql, oraVectorConn);
//            oraCMD.Parameters.Add(":p1", VL.LAYERNAME);
//            oraCMD.Parameters.Add(":p2", VL.DBNAME);
//            oraCMD.Parameters.Add(":p3", VL.URI);
//            oraCMD.Parameters.Add(":p4", VL.MAPNUM);
//            oraCMD.Parameters.Add(":p5", VL.SCALE);
//            oraCMD.Parameters.Add(":p6", VL.PROJECTION);
//            oraCMD.Parameters.Add(":p7", VL.AUTHOR);
//            oraCMD.Parameters.Add(":p8", VL.CREATETIME);
//            oraCMD.Parameters.Add(":p9", VL.DATATYPE);
//            oraCMD.Parameters.Add(":p10", VL.DESCRIPTION);
//            oraCMD.Parameters.Add(":p11", VL.SHAPE);
//            oraCMD.Parameters.Add(":p12", VL.MINX);
//            oraCMD.Parameters.Add(":p13", VL.MAXX);
//            oraCMD.Parameters.Add(":p14", VL.MINY);
//            oraCMD.Parameters.Add(":p15", VL.MAXY);
//            oraCMD.Parameters.Add(":p16", Convert.ToInt64(VL.ISVISIBLE));
//            oraCMD.Parameters.Add(":p17", VL.LAYERSIZE);
//            oraCMD.Parameters.Add(":p18", Convert.ToInt64(VL.ISEXIST));


//            int result = oraCMD.ExecuteNonQuery();

//            oraVectorConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static VectorLayer GetVectorIndexOra(string LayerID)
//        {
//            VectorLayer VL = new VectorLayer();

//            OracleConnection oraVectorConn;
//            OracleCommand oraCMD;
//            OracleDataReader oraDataReader;

//            oraVectorConn = DBCon.OraConOpen(); ;

//            string sql;

//            sql = "SELECT * FROM VECTOR_LAYERS WHERE LAYERID = :p1";
//            oraCMD = new OracleCommand(sql, oraVectorConn);
//            oraCMD.Parameters.Add(":p1", LayerID);


//            try { oraDataReader = oraCMD.ExecuteReader(); }
//            catch { return null; }

//            if (!oraDataReader.HasRows)
//            {
//                return null;
//            }

//            while (oraDataReader.Read())
//            {
//                int fieldCount = oraDataReader.FieldCount;

//                for (int i = 0; i < fieldCount; i++)
//                {
//                    string fieldName = oraDataReader.GetName(i).ToString();
//                    VL = InitVectorData(VL, oraDataReader[fieldName], fieldName);

//                }

//            }
//            oraVectorConn.Close();
//            oraCMD.Dispose();

//            return VL;
//        }

//        public static int ImportVectorIndex(VectorLayer VL)
//        {
//            OleDbConnection my_con;
//            OleDbCommand dbcmd;

//            string SQL;
//            my_con = DBCon.DBCon_Open();

//            SQL = "INSERT INTO [VECTOR_LAYERS] ([LAYERNAME],[DBNAME],[URI],[MAPNUM],[SCALE],[PROJECTION],[AUTHOR],[CREATETIME],[DATATYPE],[DESCRIPTION],[SHAPE],[MINX],[MAXX],[MINY],[MAXY],[ISVISIBLE]) " +
//                 "VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16)";
//            dbcmd = new OleDbCommand(SQL, my_con);

//            dbcmd.Parameters.AddWithValue("@p1", VL.LAYERNAME);
//            dbcmd.Parameters.AddWithValue("@p2", VL.DBNAME);
//            dbcmd.Parameters.AddWithValue("@p3", VL.URI);
//            dbcmd.Parameters.AddWithValue("@p4", VL.MAPNUM);
//            dbcmd.Parameters.AddWithValue("@p5", VL.SCALE);
//            dbcmd.Parameters.AddWithValue("@p6", VL.PROJECTION);
//            dbcmd.Parameters.AddWithValue("@p7", VL.AUTHOR);
//            dbcmd.Parameters.AddWithValue("@p8", VL.CREATETIME);
//            dbcmd.Parameters.AddWithValue("@p9", VL.DATATYPE);
//            dbcmd.Parameters.AddWithValue("@p10", VL.DESCRIPTION);
//            dbcmd.Parameters.AddWithValue("@p11", VL.SHAPE);
//            dbcmd.Parameters.AddWithValue("@p12", VL.MINX);
//            dbcmd.Parameters.AddWithValue("@p13", VL.MAXX);
//            dbcmd.Parameters.AddWithValue("@p14", VL.MINY);
//            dbcmd.Parameters.AddWithValue("@p15", VL.MAXY);
//            dbcmd.Parameters.AddWithValue("@p16", Convert.ToInt64(VL.ISVISIBLE));


//            int r = dbcmd.ExecuteNonQuery();

//            my_con.Close();
//            my_con.Dispose();

//            return r;
//        }
//        public static VectorLayer GetVectorIndex(long LayerID)
//        {
//            VectorLayer VL = new VectorLayer();

//            OleDbConnection my_con;
//            OleDbCommand dbcmd;
//            OleDbDataReader dbrd;
//            string SQL;
//            my_con = DBCon.DBCon_Open();

//            SQL = "SELECT * FROM [VECTOR_LAYERS] WHERE ID = @p1";
//            OleDbDataAdapter adapter = new OleDbDataAdapter();
//            dbcmd = new OleDbCommand(SQL, my_con);
//            dbcmd.Parameters.AddWithValue("@p1", LayerID);

//            adapter.SelectCommand = dbcmd;

//            try { dbrd = dbcmd.ExecuteReader(); }
//            catch { return null; }

//            if (!dbrd.HasRows)
//            {
//                return null;
//            }

//            while (dbrd.Read())
//            {
//                try { VL.AUTHOR = Convert.ToString(dbrd["AUTHOR"]); }
//                catch { errMSG += "AUTHOR;"; }

//                try { VL.CREATETIME = Convert.ToString(dbrd["CREATETIME"]); }
//                catch { errMSG += "CREATETIME;"; }

//                try { VL.DATATYPE = Convert.ToString(dbrd["DATATYPE"]); }
//                catch { errMSG += "DATATYPE;"; }

//                try { VL.DBNAME = Convert.ToString(dbrd["DBNAME"]); }
//                catch { errMSG += "DBNAME;"; }

//                try { VL.DESCRIPTION = Convert.ToString(dbrd["DESCRIPTION"]); }
//                catch { errMSG += "DESCRIPTION;"; }

//                try { VL.ISVISIBLE = Convert.ToBoolean(dbrd["ISVISIBLE"]); }
//                catch { errMSG += "ISVISIBLE;"; }

//                try { VL.LAYERNAME = Convert.ToString(dbrd["LAYERNAME"]); }
//                catch { errMSG += "LAYERNAME;"; }

//                try { VL.MAPNUM = Convert.ToString(dbrd["MAPNUM"]); }
//                catch { errMSG += "MAPNUM;"; }

//                try { VL.MAXX = Convert.ToSingle(dbrd["MAXX"]); }
//                catch { errMSG += "MAXX;"; }

//                try { VL.MAXY = Convert.ToSingle(dbrd["MAXY"]); }
//                catch { errMSG += "MAXY;"; }

//                try { VL.MINX = Convert.ToSingle(dbrd["MINX"]); }
//                catch { errMSG += "MINX;"; }

//                try { VL.MINY = Convert.ToSingle(dbrd["MINY"]); }
//                catch { errMSG += "MINY;"; }

//                try { VL.PROJECTION = Convert.ToString(dbrd["PROJECTION"]); }
//                catch { errMSG += "PROJECTION;"; }

//                try { VL.SCALE = Convert.ToInt64(dbrd["SCALE"]); }
//                catch { errMSG += "SCALE;"; }

//                try { VL.URI = Convert.ToString(dbrd["URI"]); }
//                catch { errMSG += "URI;"; }

//                try { VL.SHAPE = Convert.ToString(dbrd["SHAPE"]); }
//                catch { errMSG += "SHAPE;"; }
//            }
//            my_con.Close();
//            my_con.Dispose();

//            return VL;
//        }
//    }

//    public class FileLayer : BaseLayer
//    {
//        public string OPENAS="";
//        public bool ISFOLDER;

//        public static string errMSG = "";
//        private static FileLayer InitFileData(FileLayer FL, object obj, string fieldName)
//        {
//            try
//            {
//                switch (fieldName)
//                {
//                    case "LAYERID": FL.LAYERID = Convert.ToString(obj); break;
//                    case "AUTHOR": FL.AUTHOR = Convert.ToString(obj); break;
//                    case "CREATETIME": FL.CREATETIME = Convert.ToString(obj); break;
//                    case "DATATYPE": FL.DATATYPE = Convert.ToString(obj); break;
//                    case "DBNAME": FL.DBNAME = Convert.ToString(obj); break;
//                    case "DESCRIPTION": FL.DESCRIPTION = Convert.ToString(obj); break;
//                    case "ISFOLDER": FL.ISFOLDER = Convert.ToBoolean(Convert.ToInt32(obj)); break;
//                    case "LAYERSIZE": FL.LAYERSIZE = Convert.ToInt64(obj); break;
//                    case "ISEXIST": FL.ISEXIST = Convert.ToBoolean(Convert.ToInt32(obj)); break;
//                    case "LAYERNAME": FL.LAYERNAME = Convert.ToString(obj); break;
//                    case "MAPNUM": FL.MAPNUM = Convert.ToString(obj); break;
//                    case "PROJECTION": FL.PROJECTION = Convert.ToString(obj); break;
//                    case "SCALE": FL.SCALE = Convert.ToInt64(obj); break;
//                    case "URI": FL.URI = Convert.ToString(obj); break;
//                    case "OPENAS": FL.OPENAS = Convert.ToString(obj); break;
//                    default: break;
//                }
//            }
//            catch
//            {
//                errMSG = fieldName + ";";
//            }

//            return FL;
//        }
//        public static int ImportFileIndexOra(FileLayer FL)
//        {
//            OracleConnection oraFileConn;
//            OracleCommand oraCMD;

//            oraFileConn = DBCon.OraConOpen(); ;

//            string sql;
//            sql = "INSERT INTO FILE_LAYERS(LAYERID,LAYERNAME,DBNAME,URI,MAPNUM,SCALE,PROJECTION,AUTHOR,CREATETIME,DATATYPE,DESCRIPTION,OPENAS,ISFOLDER,LAYERSIZE,ISEXIST) " +
//                " VALUES(sys_guid(),:p1, :p2, :p3, :p4, :p5, :p6, :p7, :p8, :p9, :p10, :p11, :p12, :p13, :p14)";
//            oraCMD = new OracleCommand(sql, oraFileConn);
//            oraCMD.Parameters.Add(":p1", FL.LAYERNAME);
//            oraCMD.Parameters.Add(":p2", FL.DBNAME);
//            oraCMD.Parameters.Add(":p3", FL.URI);
//            oraCMD.Parameters.Add(":p4", FL.MAPNUM);
//            oraCMD.Parameters.Add(":p5", FL.SCALE);
//            oraCMD.Parameters.Add(":p6", FL.PROJECTION);
//            oraCMD.Parameters.Add(":p7", FL.AUTHOR);
//            oraCMD.Parameters.Add(":p8", FL.CREATETIME);
//            oraCMD.Parameters.Add(":p9", FL.DATATYPE);
//            oraCMD.Parameters.Add(":p10", FL.DESCRIPTION);
//            oraCMD.Parameters.Add(":p11", FL.OPENAS);
//            oraCMD.Parameters.Add(":p12", Convert.ToInt64(FL.ISFOLDER));
//            oraCMD.Parameters.Add(":p13", FL.LAYERSIZE);
//            oraCMD.Parameters.Add(":p14", Convert.ToInt64(FL.ISEXIST));


//            int result = oraCMD.ExecuteNonQuery();

//            oraFileConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static FileLayer GetFileIndexOra(string LayerID)
//        {
//            FileLayer FL = new FileLayer();

//            OracleConnection oraFileConn;
//            OracleCommand oraCMD;
//            OracleDataReader oraDataReader;

//            oraFileConn = DBCon.OraConOpen(); ;

//            string sql;

//            sql = "SELECT * FROM FILE_LAYERS WHERE LAYERID = :p1";
//            oraCMD = new OracleCommand(sql, oraFileConn);
//            oraCMD.Parameters.Add(":p1", LayerID);


//            try { oraDataReader = oraCMD.ExecuteReader(); }
//            catch { return null; }

//            if (!oraDataReader.HasRows)
//            {
//                return null;
//            }

//            while (oraDataReader.Read())
//            {
//                int fieldCount = oraDataReader.FieldCount;

//                for (int i = 0; i < fieldCount; i++)
//                {
//                    string fieldName = oraDataReader.GetName(i).ToString();
//                    FL = InitFileData(FL, oraDataReader[fieldName], fieldName);

//                }

//            }
//            oraFileConn.Close();
//            oraCMD.Dispose();

//            return FL;
//        }


//        public static int ImportFileIndex(FileLayer FL)
//        {
//            OleDbConnection my_con;
//            OleDbCommand dbcmd;

//            string SQL;
//            my_con = DBCon.DBCon_Open();

//            SQL = "INSERT INTO [FILE_LAYERS] ([LAYERNAME],[DBNAME],[URI],[MAPNUM],[SCALE],[PROJECTION],[AUTHOR],[CREATETIME],[DATATYPE],[DESCRIPTION],[OPENAS],[ISFOLDER]) " +
//                 "VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)";
//            dbcmd = new OleDbCommand(SQL, my_con);

//            dbcmd.Parameters.AddWithValue("@p1", FL.LAYERNAME);
//            dbcmd.Parameters.AddWithValue("@p2", FL.DBNAME);
//            dbcmd.Parameters.AddWithValue("@p3", FL.URI);
//            dbcmd.Parameters.AddWithValue("@p4", FL.MAPNUM);
//            dbcmd.Parameters.AddWithValue("@p5", FL.SCALE);
//            dbcmd.Parameters.AddWithValue("@p6", FL.PROJECTION);
//            dbcmd.Parameters.AddWithValue("@p7", FL.AUTHOR);
//            dbcmd.Parameters.AddWithValue("@p8", FL.CREATETIME);
//            dbcmd.Parameters.AddWithValue("@p9", FL.DATATYPE);
//            dbcmd.Parameters.AddWithValue("@p10", FL.DESCRIPTION);
//            dbcmd.Parameters.AddWithValue("@p11", FL.OPENAS);
//            dbcmd.Parameters.AddWithValue("@p12", Convert.ToInt64(FL.ISFOLDER));

//            int r = dbcmd.ExecuteNonQuery();

//            my_con.Close();
//            my_con.Dispose();

//            return r;
//        }
//        public static FileLayer GetFileIndex(long LayerID)
//        {
//            FileLayer FL = new FileLayer();

//            OleDbConnection my_con;
//            OleDbCommand dbcmd;
//            OleDbDataReader dbrd;
//            string SQL;
//            my_con = DBCon.DBCon_Open();

//            SQL = "SELECT * FROM [FILE_LAYERS] WHERE ID = @p1";
//            OleDbDataAdapter adapter = new OleDbDataAdapter();
//            dbcmd = new OleDbCommand(SQL, my_con);
//            dbcmd.Parameters.AddWithValue("@p1", LayerID);

//            adapter.SelectCommand = dbcmd;

//            try { dbrd = dbcmd.ExecuteReader(); }
//            catch { return null; }

//            if (!dbrd.HasRows)
//            {
//                return null;
//            }

//            string errMSG = "";
//            while (dbrd.Read())
//            {
//                try { FL.AUTHOR = Convert.ToString(dbrd["AUTHOR"]); }
//                catch { errMSG += "AUTHOR;"; }

//                try { FL.CREATETIME = Convert.ToString(dbrd["CREATETIME"]); }
//                catch { errMSG += "CREATETIME;"; }

//                try { FL.DATATYPE = Convert.ToString(dbrd["DATATYPE"]); }
//                catch { errMSG += "DATATYPE;"; }

//                try { FL.DBNAME = Convert.ToString(dbrd["DBNAME"]); }
//                catch { errMSG += "DBNAME;"; }

//                try { FL.DESCRIPTION = Convert.ToString(dbrd["DESCRIPTION"]); }
//                catch { errMSG += "DESCRIPTION;"; }

//                try { FL.ISFOLDER = Convert.ToBoolean(dbrd["ISFOLDER"]); }
//                catch { errMSG += "ISFOLDER;"; }

//                try { FL.LAYERNAME = Convert.ToString(dbrd["LAYERNAME"]); }
//                catch { errMSG += "LAYERNAME;"; }

//                try { FL.MAPNUM = Convert.ToString(dbrd["MAPNUM"]); }
//                catch { errMSG += "MAPNUM;"; }

//                try { FL.PROJECTION = Convert.ToString(dbrd["PROJECTION"]); }
//                catch { errMSG += "PROJECTION;"; }

//                try { FL.SCALE = Convert.ToInt64(dbrd["SCALE"]); }
//                catch { errMSG += "SCALE;"; }

//                try { FL.URI = Convert.ToString(dbrd["URI"]); }
//                catch { errMSG += "URI;"; }

//                try { FL.OPENAS = Convert.ToString(dbrd["OPENAS"]); }
//                catch { errMSG += "OPENAS;"; }
//            }
//            my_con.Close();
//            my_con.Dispose();

//            return FL;
//        }
//    }

//    public class URI
//    {
//        public string URIstring;
//        public string GDB_Name;
//        public string GDB_NickName;
//        public string GDB_Path;
//        public string LayerName;

//        public string GetURI(URI uri)
//        {
//            return "";
//        }

//        public string GetTruePath(string uriStr)
//        {
//            Environment.InitGDB_Dict();

//            if (!uriStr.Contains("#")) return "";

//            string[] arr = uriStr.Split('#');
//            GDB_Name = arr[0];
//            LayerName = arr[1];

//            if (Environment.GDB_Dict.ContainsKey(GDB_Name))
//                GDB_Path = Environment.serverROOT + Environment.GDB_Dict[GDB_Name];
//            else
//                return "";

//            return GDB_Path + "\\" + LayerName;
//        }

//        public string GetServerTruePath(string uriStr)
//        {
//            Environment.InitGDB_Dict();

//            if (!uriStr.Contains("#")) return "";

//            string[] arr = uriStr.Split('#');
//            GDB_Name = arr[0];
//            LayerName = arr[1];

//            if (Environment.GDB_Dict.ContainsKey(GDB_Name))
//                GDB_Path = Environment.serverIP + Environment.GDB_Dict[GDB_Name];
//            else
//                return "";

//            return (GDB_Path + "\\" + LayerName).Replace("\\", "/");
//        }

//    }

//    public class TreeNodeOra
//    {
//        public static TreeNode GetTreeNode(string DB, string DBName)
//        {
//            OracleConnection oraConn;
//            OracleCommand oraCMD;
//            OracleDataReader oraDataReader;

//            oraConn = DBCon.OraConOpen(); ;

//            string sql;
//            sql = string.Format("SELECT * FROM {0} WHERE DBNAME = :p1", DB);

//            oraCMD = new OracleCommand(sql, oraConn);
//            oraCMD.Parameters.Add(":p1", DBName);

//            //adapter.SelectCommand = dbcmd;

//            try { oraDataReader = oraCMD.ExecuteReader(); }
//            catch { return null; }

//            if (!oraDataReader.HasRows)
//            {
//                return null;
//            }
//            TreeNode nowNode = new TreeNode(DBName);
//            nowNode.Tag = DB;

//            string errMSG = "";
//            while (oraDataReader.Read())
//            {
//                TreeNode childNode = new TreeNode();
//                try { childNode.Tag = oraDataReader["LAYERID"]; }
//                catch { errMSG += "LAYERID;"; }
//                try { childNode.Text = Convert.ToString(oraDataReader["LAYERNAME"]); }
//                catch { errMSG += "LAYERNAME;"; }
//                nowNode.Nodes.Add(childNode);
//            }
//            oraConn.Close();
//            oraCMD.Dispose();

//            return nowNode;
//        }
//    }

//    public class UserInfo
//    {
//        public string USERID;
//        public string LOGINID="";
//        public string PASSWORD="";
//        public string ROLEID="";

//        public static string errMSG = "";
//        private static UserInfo InitUserInfoData(UserInfo RL, object obj, string fieldName)
//        {
//            try
//            {
//                switch (fieldName)
//                {
//                    case "USERID": RL.USERID = Convert.ToString(obj); break;
//                    case "LOGINID": RL.LOGINID = Convert.ToString(obj); break;
//                    case "PASSWORD": RL.PASSWORD = Convert.ToString(obj); break;
//                    case "ROLEID": RL.ROLEID = Convert.ToString(obj); break;
//                    default: break;
//                }
//            }
//            catch
//            {
//                errMSG = fieldName + ";";
//            }

//            return RL;
//        }

//        public static int ImportUserInfoOra(UserInfo userInfo)
//        {
//            OracleConnection oraConn;
//            OracleCommand oraCMD;

//            oraConn = DBCon.OraConOpen(); ;

//            string sql;
//            sql = "INSERT INTO USERINFO(USERID,LOGINID,PASSWORD,ROLEID) " +
//                " VALUES(sys_guid(),:p1, :p2, :p3)";
//            oraCMD = new OracleCommand(sql, oraConn);
//            oraCMD.Parameters.Add(":p1", userInfo.LOGINID);
//            oraCMD.Parameters.Add(":p2", userInfo.PASSWORD);
//            oraCMD.Parameters.Add(":p3", userInfo.ROLEID);


//            int result = oraCMD.ExecuteNonQuery();

//            oraConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static int UpdateUserInfoOra(UserInfo user)
//        {
//            OracleConnection oraConn;
//            OracleCommand oraCMD;

//            oraConn = DBCon.OraConOpen(); ;

//            string sql;

//            sql = "UPDATE USERINFO SET LOGINID=:p1,PASSWORD=:p2,ROLEID=:p3 " +
//                " WHERE USERID =:p4)";
//            oraCMD = new OracleCommand(sql, oraConn);
//            oraCMD.Parameters.Add(":p1", user.LOGINID);
//            oraCMD.Parameters.Add(":p2", user.PASSWORD);
//            oraCMD.Parameters.Add(":p3", user.ROLEID);
//            oraCMD.Parameters.Add(":p4", user.USERID);


//            int result = oraCMD.ExecuteNonQuery();

//            oraConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static int DeleteUserInfoOra(UserInfo user)
//        {
//            OracleConnection oraConn;
//            OracleCommand oraCMD;

//            oraConn = DBCon.OraConOpen(); ;

//            string sql;
//            sql = "DELETE FROM USERINFO  WHERE USERID=:p1";
//            oraCMD = new OracleCommand(sql, oraConn);
//            oraCMD.Parameters.Add(":p1", user.USERID);


//            int result = oraCMD.ExecuteNonQuery();

//            oraConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static UserInfo GetUserInfoOra(string loginId, string pwd)
//        {
//            UserInfo userinfo = new UserInfo();

//            OracleConnection oraRasterConn;
//            OracleCommand oraCMD;
//            OracleDataReader oraDataReader;

//            oraRasterConn = DBCon.OraConOpen(); ;

//            string sql;

//            sql = "SELECT * FROM USERINFO WHERE LOGINID = :p1 AND PASSWORD = :p2";
//            oraCMD = new OracleCommand(sql, oraRasterConn);
//            oraCMD.Parameters.Add(":p1", loginId);
//            oraCMD.Parameters.Add(":p2", pwd);


//            try { oraDataReader = oraCMD.ExecuteReader(); }
//            catch { return null; }

//            if (!oraDataReader.HasRows)
//            {
//                return null;
//            }

//            while (oraDataReader.Read())
//            {
//                int fieldCount = oraDataReader.FieldCount;

//                for (int i = 0; i < fieldCount; i++)
//                {
//                    string fieldName = oraDataReader.GetName(i).ToString();
//                    userinfo = InitUserInfoData(userinfo, oraDataReader[fieldName], fieldName);

//                }

//            }
//            oraRasterConn.Close();
//            oraCMD.Dispose();

//            return userinfo;
//        }
//    }

//    public class Role
//    {
//        public string ROLEID;
//        public string ROLENAME;
//        public int QUERYGRADER;
//        public int GETGRADER;

//        public static string errMSG = "";
//        private static Role InitRoleData(Role RL, object obj, string fieldName)
//        {
//            try
//            {
//                switch (fieldName)
//                {
//                    case "ROLEID": RL.ROLEID = Convert.ToString(obj); break;
//                    case "ROLENAME": RL.ROLENAME = Convert.ToString(obj); break;
//                    case "QUERYGRADER": RL.QUERYGRADER = Convert.ToInt32(obj); break;
//                    case "GETGRADER": RL.GETGRADER = Convert.ToInt32(obj); break;
//                    default: break;
//                }
//            }
//            catch
//            {
//                errMSG = fieldName + ";";
//            }

//            return RL;
//        }


//        public static int ImportRoleOra(Role role)
//        {
//            OracleConnection oraConn;
//            OracleCommand oraCMD;

//            oraConn = DBCon.OraConOpen(); ;

//            string sql;
//            sql = "INSERT INTO ROLE(ROLEID,ROLENAME,QUERYGRADER,GETGRADER) " +
//                " VALUES(sys_guid(),:p1, :p2, :p3)";
//            oraCMD = new OracleCommand(sql, oraConn);
//            oraCMD.Parameters.Add(":p1", role.ROLENAME);
//            oraCMD.Parameters.Add(":p2", role.QUERYGRADER);
//            oraCMD.Parameters.Add(":p3", role.GETGRADER);


//            int result = oraCMD.ExecuteNonQuery();

//            oraConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static int UpdateRoleOra(Role role)
//        {
//            OracleConnection oraConn;
//            OracleCommand oraCMD;

//            oraConn = DBCon.OraConOpen(); ;

//            string sql;
//            sql = "UPDATE ROLE SET ROLENAME=:p1,QUERYGRADER=:p2,GETGRADER=:p3 " +
//                " WHERE ROLEID=:p4";
//            oraCMD = new OracleCommand(sql, oraConn);
//            oraCMD.Parameters.Add(":p1", role.ROLENAME);
//            oraCMD.Parameters.Add(":p2", role.QUERYGRADER);
//            oraCMD.Parameters.Add(":p3", role.GETGRADER);
//            oraCMD.Parameters.Add(":p4", role.ROLEID);


//            int result = oraCMD.ExecuteNonQuery();

//            oraConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static int DeleteRoleOra(Role role)
//        {
//            OracleConnection oraConn;
//            OracleCommand oraCMD;

//            oraConn = DBCon.OraConOpen(); ;

//            string sql;
//            sql = "DELETE FROM ROLE  WHERE ROLEID=:p1";
//            oraCMD = new OracleCommand(sql, oraConn);
//           // oraCMD.Parameters.Add();
//            oraCMD.Parameters.Add(":p1", role.ROLEID);


//            int result = oraCMD.ExecuteNonQuery();

//            oraConn.Close();
//            oraCMD.Dispose();

//            return result;
//        }

//        public static Role GetRoleOra(string roleId)
//        {
//            Role role = new Role();

//            OracleConnection oraRasterConn;
//            OracleCommand oraCMD;
//            OracleDataReader oraDataReader;

//            oraRasterConn = DBCon.OraConOpen(); ;

//            string sql;

//            sql = "SELECT * FROM ROLE WHERE ROLEID = :p1";
//            oraCMD = new OracleCommand(sql, oraRasterConn);
//            oraCMD.Parameters.Add(":p1", roleId);


//            try { oraDataReader = oraCMD.ExecuteReader(); }
//            catch { return null; }

//            if (!oraDataReader.HasRows)
//            {
//                return null;
//            }

//            while (oraDataReader.Read())
//            {
//                int fieldCount = oraDataReader.FieldCount;

//                for (int i = 0; i < fieldCount; i++)
//                {
//                    string fieldName = oraDataReader.GetName(i).ToString();
//                    role = InitRoleData(role, oraDataReader[fieldName], fieldName);

//                }

//            }
//            oraRasterConn.Close();
//            oraCMD.Dispose();

//            return role;
//        }
//    }
//}
