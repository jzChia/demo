using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Demo.Utility;

namespace Demo.Entities
{
    public class FileLayer : BaseLayer
    {
        public string OPENAS = "";
        public bool ISFOLDER;

        public static string errMSG = "";
        private static FileLayer InitFileData(FileLayer FL, object obj, string fieldName)
        {
            try
            {
                switch (fieldName)
                {
                    case "LAYERID": FL.LAYERID = Convert.ToString(obj); break;
                    case "AUTHOR": FL.AUTHOR = Convert.ToString(obj); break;
                    case "CREATETIME": FL.CREATETIME = Convert.ToString(obj); break;
                    case "DATATYPE": FL.DATATYPE = Convert.ToString(obj); break;
                    case "DBNAME": FL.DBNAME = Convert.ToString(obj); break;
                    case "DESCRIPTION": FL.DESCRIPTION = Convert.ToString(obj); break;
                    case "ISFOLDER": FL.ISFOLDER = Convert.ToBoolean(Convert.ToInt32(obj)); break;
                    case "LAYERSIZE": FL.LAYERSIZE = Convert.ToInt64(obj); break;
                    case "ISEXIST": FL.ISEXIST = Convert.ToBoolean(Convert.ToInt32(obj)); break;
                    case "LAYERNAME": FL.LAYERNAME = Convert.ToString(obj); break;
                    case "MAPNUM": FL.MAPNUM = Convert.ToString(obj); break;
                    case "PROJECTION": FL.PROJECTION = Convert.ToString(obj); break;
                    case "SCALE": FL.SCALE = Convert.ToInt64(obj); break;
                    case "URI": FL.URI = Convert.ToString(obj); break;
                    case "OPENAS": FL.OPENAS = Convert.ToString(obj); break;
                    case "QUERYGRADER": FL.QUERYGRADER = Convert.ToInt32(obj); break;
                    case "GETGRADER": FL.GETGRADER = Convert.ToInt32(obj); break;
                    case "REGION": FL.REGION = Convert.ToString(obj); break;
                    case "DATAGETDATE": FL.DATAGETDATE = Convert.ToDateTime(obj); break;
                    case "DATAFORMAT": FL.DATAFORMAT = Convert.ToString(obj); break;
                    case "DATASOURCES": FL.DATASOURCES = Convert.ToString(obj); break;
                    default: break;
                }
            }
            catch
            {
                errMSG = fieldName + ";";
            }

            return FL;
        }

        public static int ImportFileIndex(FileLayer FL)
        {
            return ImportFileIndex(FL,null);
        }

        public static int ImportFileIndex(FileLayer FL, string xmlfile)
        {
            OracleConnection oraFileConn;
            OracleCommand oraCMD;

            oraFileConn = DBCon.OraConOpen(); ;

            StringBuilder sql;
            sql = new StringBuilder("INSERT INTO FILE_LAYERS(LAYERID,LAYERNAME,DBNAME,URI,MAPNUM,SCALE,PROJECTION,AUTHOR," +
                "CREATETIME,DATATYPE,DESCRIPTION,OPENAS,ISFOLDER,LAYERSIZE,ISEXIST,QUERYGRADER,GETGRADER,REGION,DATAGETDATE,DATAFORMAT,DATASOURCES");
            if (String.IsNullOrEmpty(xmlfile))
            {
                sql.Append(",XMLFILE");
            }
            sql.Append(" ) VALUES(sys_guid(),:p1, :p2, :p3, :p4, :p5, :p6, :p7, :p8, :p9, :p10, :p11, :p12, :p13, :p14,:p15,:p16, :p17, :p18,:p19,:p20");
            if (String.IsNullOrEmpty(xmlfile))
            {
                sql.Append(",:p21");
            }
            sql.Append(")");
            oraCMD = new OracleCommand(sql.ToString(), oraFileConn);
            oraCMD.Parameters.Add(":p1", FL.LAYERNAME);
            oraCMD.Parameters.Add(":p2", FL.DBNAME);
            oraCMD.Parameters.Add(":p3", FL.URI);
            oraCMD.Parameters.Add(":p4", FL.MAPNUM);
            oraCMD.Parameters.Add(":p5", FL.SCALE);
            oraCMD.Parameters.Add(":p6", FL.PROJECTION);
            oraCMD.Parameters.Add(":p7", FL.AUTHOR);
            oraCMD.Parameters.Add(":p8", FL.CREATETIME);
            oraCMD.Parameters.Add(":p9", FL.DATATYPE);
            oraCMD.Parameters.Add(":p10", FL.DESCRIPTION);
            oraCMD.Parameters.Add(":p11", FL.OPENAS);
            oraCMD.Parameters.Add(":p12", Convert.ToInt64(FL.ISFOLDER));
            oraCMD.Parameters.Add(":p13", FL.LAYERSIZE);
            oraCMD.Parameters.Add(":p14", Convert.ToInt64(FL.ISEXIST));
            oraCMD.Parameters.Add(":p15", FL.QUERYGRADER);
            oraCMD.Parameters.Add(":p16", FL.GETGRADER);
            oraCMD.Parameters.Add(":p17", FL.REGION);
            oraCMD.Parameters.Add(":p18", FL.DATAGETDATE);
            oraCMD.Parameters.Add(":p19", FL.DATAFORMAT);
            oraCMD.Parameters.Add(":p20", FL.DATASOURCES);

            if (String.IsNullOrEmpty(xmlfile))
            {
                oraCMD.Parameters.Add(":p21", OraBlob.WriteBolb(xmlfile));
            }
            int result = oraCMD.ExecuteNonQuery();

            oraFileConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static int ImportFileIndex(string layerID, string xmlfile)
        {
            if (String.IsNullOrEmpty(layerID) || String.IsNullOrEmpty(xmlfile))
            {
                return 0;
            }
            OracleConnection oraRasterConn;
            OracleCommand oraCMD;

            oraRasterConn = DBCon.OraConOpen(); ;

            StringBuilder sql;

            sql = new StringBuilder("UPDATE FILE_LAYERS SET XMLFILE=:p1 WHERE LAYERID=:p2");


            oraCMD = new OracleCommand(sql.ToString(), oraRasterConn);
            oraCMD.Parameters.Add(":p1", layerID);
            oraCMD.Parameters.Add(":p2", OraBlob.WriteBolb(xmlfile));

            int result = oraCMD.ExecuteNonQuery();

            oraRasterConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static FileLayer GetFileIndex(string LayerID)
        {
            FileLayer FL = new FileLayer();

            OracleConnection oraFileConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraFileConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT * FROM FILE_LAYERS WHERE LAYERID = :p1";
            oraCMD = new OracleCommand(sql, oraFileConn);
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
                            FL.XMLFILE = System.Text.Encoding.UTF8.GetString(data);
                        }
                        catch { FL.XMLFILE = ""; }
                    }
                    else
                    {
                        FL = InitFileData(FL, oraDataReader[fieldName], fieldName);
                    }
               

                }

            }
            oraFileConn.Close();
            oraCMD.Dispose();

            return FL;
        }

        public static List<FileLayer> GetFileLayerBySearchConditions(string dbName, SearchConditions sc)
        {
            if (String.IsNullOrEmpty(dbName) || sc == null)
                return null;

            List<FileLayer> FLs = new List<FileLayer>();

            OracleConnection oraRasterConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraRasterConn = DBCon.OraConOpen(); ;

            StringBuilder sql;

            sql = new StringBuilder("SELECT * FROM FILE_LAYERS WHERE 1=1 AND DBNAME='" + dbName + "' ");

            if (sc.area.Count > 0)
            {
                sql.Append("AND ( ");
                foreach (string s in sc.area)
                {
                    sql.Append(" AND REGION  LIKE '" + s + "' OR ");
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

            FileLayer FL;
            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;
                FL = new FileLayer();
                for (int i = 0; i < fieldCount; i++)
                {
                    string fieldName = oraDataReader.GetName(i).ToString();
                    if (fieldName.Equals("XMLFILE"))
                    {
                        try
                        {
                            byte[] data = (byte[])oraDataReader["XMLFILE"];

                            UTF8Encoding enc = new UTF8Encoding(true, true);//如果blob数据原本为utf8编码
                            FL.XMLFILE = System.Text.Encoding.UTF8.GetString(data);
                        }
                        catch
                        {
                            FL.XMLFILE = "";
                        }

                    }
                    else
                    {
                        FL = InitFileData(FL, oraDataReader[fieldName], fieldName);
                    }
                }

                FLs.Add(FL);

            }
            oraRasterConn.Close();
            oraCMD.Dispose();

            return FLs;
        }
    }

}
