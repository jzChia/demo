using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Demo.Utility;

namespace Demo.Entities
{
    public class VectorLayer : BaseLayer
    {
        public string SHAPE = "";
        public float MINX;
        public float MAXX;
        public float MINY;
        public float MAXY;
        public bool ISVISIBLE;

        public static string errMSG = "";
        private static VectorLayer InitVectorData(VectorLayer VL, object obj, string fieldName)
        {
            try
            {
                switch (fieldName)
                {
                    case "LAYERID": VL.LAYERID = Convert.ToString(obj); break;
                    case "AUTHOR": VL.AUTHOR = Convert.ToString(obj); break;
                    case "CREATETIME": VL.CREATETIME = Convert.ToString(obj); break;
                    case "DATATYPE": VL.DATATYPE = Convert.ToString(obj); break;
                    case "DBNAME": VL.DBNAME = Convert.ToString(obj); break;
                    case "DESCRIPTION": VL.DESCRIPTION = Convert.ToString(obj); break;
                    case "ISVISIBLE": VL.ISVISIBLE = Convert.ToBoolean(Convert.ToInt32(obj)); break;
                    case "LAYERSIZE": VL.LAYERSIZE = Convert.ToInt64(obj); break;
                    case "ISEXIST": VL.ISEXIST = Convert.ToBoolean(Convert.ToInt32(obj)); break;
                    case "LAYERNAME": VL.LAYERNAME = Convert.ToString(obj); break;
                    case "MAPNUM": VL.MAPNUM = Convert.ToString(obj); break;
                    case "MAXX": VL.MAXX = Convert.ToSingle(obj); break;
                    case "MAXY": VL.MAXY = Convert.ToSingle(obj); break;
                    case "MINX": VL.MINX = Convert.ToSingle(obj); break;
                    case "MINY": VL.MINY = Convert.ToSingle(obj); break;
                    case "PROJECTION": VL.PROJECTION = Convert.ToString(obj); break;
                    case "SCALE": VL.SCALE = Convert.ToInt64(obj); break;
                    case "URI": VL.URI = Convert.ToString(obj); break;
                    case "SHAPE": VL.SHAPE = Convert.ToString(obj); break;
                    case "QUERYGRADER": VL.QUERYGRADER = Convert.ToInt32(obj); break;
                    case "GETGRADER": VL.GETGRADER = Convert.ToInt32(obj); break;
                    case "REGION": VL.REGION = Convert.ToString(obj); break;
                    case "DATAGETDATE": VL.DATAGETDATE = Convert.ToDateTime(obj); break;
                    case "DATAFORMAT": VL.DATAFORMAT = Convert.ToString(obj); break;
                    case "DATASOURCES": VL.DATASOURCES = Convert.ToString(obj); break;
                    default: break;
                }
            }
            catch
            {
                errMSG = fieldName + ";";
            }

            return VL;
        }

        public static int ImportVectorIndex(VectorLayer VL)
        {
            return ImportVectorIndex(VL, null);
        }

        public static int ImportVectorIndex(VectorLayer VL, string xmlfile)
        {
            OracleConnection oraVectorConn;
            OracleCommand oraCMD;

            oraVectorConn = DBCon.OraConOpen();

            StringBuilder sql;
            sql = new StringBuilder("INSERT INTO VECTOR_LAYERS(LAYERID,LAYERNAME,DBNAME,URI,MAPNUM,SCALE,PROJECTION,AUTHOR," +
                "CREATETIME,DATATYPE,DESCRIPTION,SHAPE,MINX,MAXX,MINY,MAXY,ISVISIBLE,LAYERSIZE,ISEXIST," +
                "QUERYGRADER,GETGRADER ,REGION,DATAGETDATE,DATAFORMAT,DATASOURCES");
            if (!String.IsNullOrEmpty(xmlfile))
            {
                sql.Append(",XMLFILE ");
            }

            sql.Append(") VALUES(sys_guid(),:p1, :p2, :p3, :p4, :p5, :p6, :p7, :p8, :p9, :p10, :p11, :p12, :p13, :p14, :p15, :p16, :p17, :p18,:p19,:p20, :p21, :p22,:p23,:p24");

            if (!String.IsNullOrEmpty(xmlfile))
            {
                sql.Append(",:p25 ");
            }
            sql.Append(")");
            oraCMD = new OracleCommand(sql.ToString(), oraVectorConn);
            oraCMD.Parameters.Add(":p1", VL.LAYERNAME);
            oraCMD.Parameters.Add(":p2", VL.DBNAME);
            oraCMD.Parameters.Add(":p3", VL.URI);
            oraCMD.Parameters.Add(":p4", VL.MAPNUM);
            oraCMD.Parameters.Add(":p5", VL.SCALE);
            oraCMD.Parameters.Add(":p6", VL.PROJECTION);
            oraCMD.Parameters.Add(":p7", VL.AUTHOR);
            oraCMD.Parameters.Add(":p8", VL.CREATETIME);
            oraCMD.Parameters.Add(":p9", VL.DATATYPE);
            oraCMD.Parameters.Add(":p10", VL.DESCRIPTION);
            oraCMD.Parameters.Add(":p11", VL.SHAPE);
            oraCMD.Parameters.Add(":p12", VL.MINX);
            oraCMD.Parameters.Add(":p13", VL.MAXX);
            oraCMD.Parameters.Add(":p14", VL.MINY);
            oraCMD.Parameters.Add(":p15", VL.MAXY);
            oraCMD.Parameters.Add(":p16", Convert.ToInt64(VL.ISVISIBLE));
            oraCMD.Parameters.Add(":p17", VL.LAYERSIZE);
            oraCMD.Parameters.Add(":p18", Convert.ToInt64(VL.ISEXIST));
            oraCMD.Parameters.Add(":p19", VL.QUERYGRADER);
            oraCMD.Parameters.Add(":p20", VL.GETGRADER);
            oraCMD.Parameters.Add(":p21", VL.REGION);
            oraCMD.Parameters.Add(":p22", VL.DATAGETDATE);
            oraCMD.Parameters.Add(":p23", VL.DATAFORMAT);
            oraCMD.Parameters.Add(":p24", VL.DATASOURCES);

            if (!String.IsNullOrEmpty(xmlfile))
            {
                oraCMD.Parameters.Add(":p25", OraBlob.WriteBolb(xmlfile));
            }

            int result = oraCMD.ExecuteNonQuery();

            oraVectorConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static int ImportVectorIndex(string layerID, string xmlfile)
        {
            if (String.IsNullOrEmpty(layerID) || String.IsNullOrEmpty(xmlfile))
            {
                return 0;
            }
            OracleConnection oraRasterConn;
            OracleCommand oraCMD;

            oraRasterConn = DBCon.OraConOpen(); ;

            StringBuilder sql;

            sql = new StringBuilder("UPDATE VECTOR_LAYERS SET XMLFILE=:p1 WHERE LAYERID=:p2");


            oraCMD = new OracleCommand(sql.ToString(), oraRasterConn);
            oraCMD.Parameters.Add(":p1", layerID);
            oraCMD.Parameters.Add(":p2", OraBlob.WriteBolb(xmlfile));

            int result = oraCMD.ExecuteNonQuery();

            oraRasterConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static VectorLayer GetVectorIndexById(string LayerID)
        {
            VectorLayer VL = new VectorLayer();

            OracleConnection oraVectorConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraVectorConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT * FROM VECTOR_LAYERS WHERE LAYERID = :p1";
            oraCMD = new OracleCommand(sql, oraVectorConn);
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
                        VL.XMLFILE = System.Text.Encoding.UTF8.GetString(data);
                        }
                        catch
                        {
                            VL.XMLFILE = "";
                        }
                    }
                    else
                    {
                        VL = InitVectorData(VL, oraDataReader[fieldName], fieldName);
                    }
                }

            }
            oraVectorConn.Close();
            oraCMD.Dispose();

            return VL;
        }

        public static List<VectorLayer> GetVectorLayerBySearchConditions(string dbName, SearchConditions sc)
        {
            if (String.IsNullOrEmpty(dbName) || sc == null)
                return null;

            List<VectorLayer> VLs = new List<VectorLayer>();

            OracleConnection oraRasterConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraRasterConn = DBCon.OraConOpen(); ;

            StringBuilder sql;

            sql = new StringBuilder("SELECT * FROM VECTOR_LAYERS WHERE 1=1 AND DBNAME='" + dbName + "' ");

            if (sc.area.Count > 0)
            {
                sql.Append("AND ( ");
                foreach (string s in sc.area)
                {
                    sql.Append(" AND REGION LIKE '" + s + "' OR ");
                }
                sql.Append("1<>1 )");
            }

            if (sc.datasource.Count > 0)
            {
                sql.Append("AND ( ");
                foreach (string s in sc.datasource)
                {
                    sql.Append(" AND DATASOURCES='" + s + "' OR ");
                }
                sql.Append("1<>1 )");
            }

            if (sc.scale.Count > 0 && sc.scale.Count == 2)
            {
                sql.Append(" AND SCALE BETWEEN " + sc.scale[0] + " AND " + sc.scale[1]);
            }

            if (!String.IsNullOrEmpty(sc.keywords))
            {
                sql.Append(" AND (LAYERNAME LIKE '" + sc.keywords + "%' OR DESCRIPTION LIKE '%" + sc.keywords + "%')");
            }

            oraCMD = new OracleCommand(sql.ToString(), oraRasterConn);


            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }

            VectorLayer VL;
            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;
                VL = new VectorLayer();
                for (int i = 0; i < fieldCount; i++)
                {
                    string fieldName = oraDataReader.GetName(i).ToString();
                    if (fieldName.Equals("XMLFILE"))
                    {
                        try
                        {
                            byte[] data = (byte[])oraDataReader["XMLFILE"];

                            UTF8Encoding enc = new UTF8Encoding(true, true);//如果blob数据原本为utf8编码
                            VL.XMLFILE = System.Text.Encoding.UTF8.GetString(data);
                        }
                        catch
                        {
                            VL.XMLFILE = "";
                        }

                    }
                    else
                    {
                        VL = InitVectorData(VL, oraDataReader[fieldName], fieldName);
                    }
                }

                VLs.Add(VL);

            }
            oraRasterConn.Close();
            oraCMD.Dispose();

            return VLs;
        }
    }

}
