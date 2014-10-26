using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Demo.Utility;

namespace Demo.Entities
{
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


        public static string errMSG = "";

        private static RasterLayer InitRasterData(RasterLayer RL, object obj, string fieldName)
        {
            try
            {
                switch (fieldName)
                {
                    case "LAYERID": RL.LAYERID = Convert.ToString(obj); break;
                    case "AUTHOR": RL.AUTHOR = Convert.ToString(obj); break;
                    case "BANDCOUNT": RL.BANDCOUNT = Convert.ToInt64(obj); break;
                    case "CREATETIME": RL.CREATETIME = Convert.ToString(obj); break;
                    case "DATATYPE": RL.DATATYPE = Convert.ToString(obj); break;
                    case "DBNAME": RL.DBNAME = Convert.ToString(obj); break;
                    case "DESCRIPTION": RL.DESCRIPTION = Convert.ToString(obj); break;
                    case "HEIGHT": RL.HEIGHT = Convert.ToInt64(obj); break;
                    case "LAYERSIZE": RL.LAYERSIZE = Convert.ToInt64(obj); break;
                    case "ISEXIST": RL.ISEXIST = Convert.ToBoolean(Convert.ToInt32(obj)); break;
                    case "ISVISIBLE": RL.ISVISIBLE = Convert.ToBoolean(Convert.ToInt32(obj)); break;
                    case "LAYERNAME": RL.LAYERNAME = Convert.ToString(obj); break;
                    case "MAPNUM": RL.MAPNUM = Convert.ToString(obj); break;
                    case "MAXX": RL.MAXX = Convert.ToSingle(obj); break;
                    case "MAXY": RL.MAXY = Convert.ToSingle(obj); break;
                    case "MINX": RL.MINX = Convert.ToSingle(obj); break;
                    case "MINY": RL.MINY = Convert.ToSingle(obj); break;
                    case "NODATAVALUE": RL.NODATAVALUE = Convert.ToSingle(obj); break;
                    case "PROJECTION": RL.PROJECTION = Convert.ToString(obj); break;
                    case "RESOLUTION": RL.RESOLUTION = Convert.ToSingle(obj); break;
                    case "SCALE": RL.SCALE = Convert.ToInt64(obj); break;
                    case "URI": RL.URI = Convert.ToString(obj); break;
                    case "WIDTH": RL.WIDTH = Convert.ToInt64(obj); break;
                    case "QUERYGRADER": RL.QUERYGRADER = Convert.ToInt32(obj); break;
                    case "GETGRADER": RL.GETGRADER = Convert.ToInt32(obj); break;
                    case "REGION": RL.REGION = Convert.ToString(obj); break;
                    case "DATAGETDATE": RL.DATAGETDATE = Convert.ToDateTime(obj); break;
                    case "DATAFORMAT": RL.DATAFORMAT = Convert.ToString(obj); break;
                    case "DATASOURCES": RL.DATASOURCES = Convert.ToString(obj); break;
                    default: break;
                }
            }
            catch
            {
                errMSG = fieldName + ";";
            }

            return RL;
        }

        public static int ImportRasterIndex(RasterLayer RL)
        {
            return ImportRasterIndex(RL, null);
        }

        public static int ImportRasterIndex(RasterLayer RL, string xmlfile)
        {
            OracleConnection oraRasterConn;
            OracleCommand oraCMD;

            oraRasterConn = DBCon.OraConOpen();

            //string sql;

            StringBuilder sql;
            sql = new StringBuilder("INSERT INTO RASTER_LAYERS(LAYERID,LAYERNAME,DBNAME,URI,MAPNUM,SCALE,PROJECTION,AUTHOR," +
                "CREATETIME,DATATYPE,DESCRIPTION,BANDCOUNT,MINX,MAXX,MINY,MAXY,HEIGHT,WIDTH,RESOLUTION," +
                "NODATAVALUE,ISVISIBLE,LAYERSIZE,ISEXIST,QUERYGRADER,GETGRADER,REGION,DATAGETDATE,DATAFORMAT,DATASOURCES");
            if (!String.IsNullOrEmpty(xmlfile))
            {
                sql.Append(",XMLFILE ");
            }
            sql.Append(")  VALUES(sys_guid(),:p1, :p2, :p3, :p4, :p5, :p6, :p7, :p8, :p9, :p10, :p11, :p12, :p13," +
                " :p14, :p15, :p16, :p17, :p18, :p19, :p20,:p21,:p22,:p23,:p24,:p25,:p26,:p27,:p28");
            if (!String.IsNullOrEmpty(xmlfile))
            {
                sql.Append(",:p29");
            }
            sql.Append(")");
            oraCMD = new OracleCommand(sql.ToString(), oraRasterConn);
            oraCMD.Parameters.Add(":p1", RL.LAYERNAME);
            oraCMD.Parameters.Add(":p2", RL.DBNAME);
            oraCMD.Parameters.Add(":p3", RL.URI);
            oraCMD.Parameters.Add(":p4", RL.MAPNUM);
            oraCMD.Parameters.Add(":p5", RL.SCALE);
            oraCMD.Parameters.Add(":p6", RL.PROJECTION);
            oraCMD.Parameters.Add(":p7", RL.AUTHOR);
            oraCMD.Parameters.Add(":p8", RL.CREATETIME);
            oraCMD.Parameters.Add(":p9", RL.DATATYPE);
            oraCMD.Parameters.Add(":p10", RL.DESCRIPTION);
            oraCMD.Parameters.Add(":p11", RL.BANDCOUNT);
            oraCMD.Parameters.Add(":p12", RL.MINX);
            oraCMD.Parameters.Add(":p13", RL.MAXX);
            oraCMD.Parameters.Add(":p14", RL.MINY);
            oraCMD.Parameters.Add(":p15", RL.MAXY);
            oraCMD.Parameters.Add(":p16", RL.HEIGHT);
            oraCMD.Parameters.Add(":p17", RL.WIDTH);
            oraCMD.Parameters.Add(":p18", RL.RESOLUTION);
            oraCMD.Parameters.Add(":p19", RL.NODATAVALUE);
            oraCMD.Parameters.Add(":p20", Convert.ToInt64(RL.ISVISIBLE));
            oraCMD.Parameters.Add(":p21", RL.LAYERSIZE);
            oraCMD.Parameters.Add(":p22", Convert.ToInt64(RL.ISEXIST));
            oraCMD.Parameters.Add(":p23", RL.QUERYGRADER);
            oraCMD.Parameters.Add(":p24", RL.GETGRADER);
            oraCMD.Parameters.Add(":p25", RL.REGION);
            oraCMD.Parameters.Add(":p26", RL.DATAGETDATE);
            oraCMD.Parameters.Add(":p27", RL.DATAFORMAT);
            oraCMD.Parameters.Add(":p28", RL.DATASOURCES);

            if (!String.IsNullOrEmpty(xmlfile))
            {
                oraCMD.Parameters.Add(":p29", OraBlob.WriteBolb(xmlfile));
            }


            int result = oraCMD.ExecuteNonQuery();

            oraRasterConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static int ImportRasterIndex(string layerID, string xmlfile)
        {
            if (String.IsNullOrEmpty(layerID) || String.IsNullOrEmpty(xmlfile))
            {
                return 0;
            }
            OracleConnection oraRasterConn;
            OracleCommand oraCMD;

            oraRasterConn = DBCon.OraConOpen(); ;

            StringBuilder sql;

            sql = new StringBuilder("UPDATE RASTER_LAYERS SET XMLFILE=:p1 WHERE LAYERID=:p2");

            
            oraCMD = new OracleCommand(sql.ToString(), oraRasterConn);
            oraCMD.Parameters.Add(":p1",layerID);
            oraCMD.Parameters.Add(":p2", OraBlob.WriteBolb(xmlfile));

            int result = oraCMD.ExecuteNonQuery();

            oraRasterConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static RasterLayer GetRasterIndexById(string LayerID)
        {
            RasterLayer RL = new RasterLayer();

            OracleConnection oraRasterConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraRasterConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT * FROM RASTER_LAYERS WHERE LAYERID = :p1";
            oraCMD = new OracleCommand(sql, oraRasterConn);
            oraCMD.Parameters.Add(":p1", LayerID);


            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }

            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;

                for (int i = 0; i < fieldCount; i++)
                {
                    string fieldName = oraDataReader.GetName(i).ToString();
                    if (fieldName.Equals("XMLFILE"))
                    {
                        try
                        {
                            byte[] data = (byte[])oraDataReader["XMLFILE"];

                            UTF8Encoding enc = new UTF8Encoding(true, true);//如果blob数据原本为utf8编码
                            RL.XMLFILE = System.Text.Encoding.UTF8.GetString(data);
                        }
                        catch
                        {
                            RL.XMLFILE = "";
                        }
                    }
                    else
                    {
                        RL = InitRasterData(RL, oraDataReader[fieldName], fieldName);
                    }
                }

            }
            oraRasterConn.Close();
            oraCMD.Dispose();

            return RL;
        }

        public static List<RasterLayer> GetRasterLayerBySearchConditions(string dbName, SearchConditions sc)
        {
            if (String.IsNullOrEmpty(dbName) || sc == null)
                return null;

            List<RasterLayer> RLs = new List<RasterLayer>();

            OracleConnection oraRasterConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraRasterConn = DBCon.OraConOpen(); ;

            StringBuilder sql;

            sql = new StringBuilder( "SELECT * FROM RASTER_LAYERS WHERE 1=1 AND DBNAME='"+dbName+"' ") ;

            if (sc.area.Count > 0)
            {
                sql.Append("AND ( ");
                foreach (string s in sc.area)
                {
                    sql.Append("  REGION LIKE '"+s+"'  OR ");
                }
                sql.Append("1<>1 )");
            }

            if (sc.datasource.Count > 0)
            {
                sql.Append("AND ( ");
                foreach (string s in sc.datasource)
                {
                    sql.Append(" AND DATASOURCES='"+s+"'  OR ");
                }
                sql.Append("1<>1 )");
            }


            if (sc.scale.Count > 0 && sc.scale.Count==2)
            {
                sql.Append(" AND SCALE BETWEEN " + sc.scale[0] + " AND " + sc.scale[1]);
            }

            if (sc.resolution.Count > 0 && sc.resolution.Count == 2)
            {
                sql.Append(" AND RESOLUTION BETWEEN " + sc.resolution[0] + " AND " + sc.resolution[1]);
            }

            if (!String.IsNullOrEmpty(sc.keywords))
            {
                sql.Append(" AND (LAYERNAME LIKE '" + sc.keywords + "%' OR DESCRIPTION LIKE '%"+sc.keywords+"%')");
            }

            oraCMD = new OracleCommand(sql.ToString(), oraRasterConn);


            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }

            RasterLayer RL;
            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;
                RL = new RasterLayer();
                for (int i = 0; i < fieldCount; i++)
                {
                    string fieldName = oraDataReader.GetName(i).ToString();
                    if (fieldName.Equals("XMLFILE"))
                    {
                        try
                        {
                            byte[] data = (byte[])oraDataReader["XMLFILE"];

                            UTF8Encoding enc = new UTF8Encoding(true, true);//如果blob数据原本为utf8编码
                            RL.XMLFILE = System.Text.Encoding.UTF8.GetString(data);
                        }
                        catch 
                        {
                            RL.XMLFILE = "";
                        }

                    }
                    else
                    {
                        RL = InitRasterData(RL, oraDataReader[fieldName], fieldName);
                    }
                }

                RLs.Add(RL);

            }
            oraRasterConn.Close();
            oraCMD.Dispose();

            return RLs;
        }
    }
}
