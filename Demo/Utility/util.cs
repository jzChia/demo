using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Demo.Utility
{
    public class Area
    {
        public string areacode;
        public string areaname;

        public Area()
        { }

        public Area(string areacode,string areaname)
        {
            this.areacode = areacode;
            this.areaname = areaname;
        
        }

        public override string ToString()
        {
            return areaname;
        }

        public static List<Area> getNationList()
        {
            List<Area> area = new List<Area>();

            OracleConnection oraVectorConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraVectorConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT SUBSTR(AREACODE,0,3) as AREACODE,AREANAME FROM AREA WHERE AREACODE LIKE '___000000' GROUP BY SUBSTR(AREACODE,0,3),AREANAME";
            oraCMD = new OracleCommand(sql, oraVectorConn);

            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }

            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;

                area.Add(new Area(Convert.ToString(oraDataReader["AREACODE"]), Convert.ToString(oraDataReader["AREANAME"])));             

            }
            oraVectorConn.Close();
            oraCMD.Dispose();

            return area;
        }


        public static List<Area> getProvinceList(string nationCode)
        {
            List<Area> area = new List<Area>();

            OracleConnection oraVectorConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraVectorConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT SUBSTR(AREACODE,4,2)AS AREACODE,AREANAME FROM AREA WHERE AREACODE <> '"
                +nationCode+"000000' and AREACODE LIKE '"+nationCode+"%' GROUP BY SUBSTR(AREACODE,4,2),AREANAME";
            oraCMD = new OracleCommand(sql, oraVectorConn);

            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }

            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;

                area.Add(new Area(Convert.ToString(oraDataReader["AREACODE"]), Convert.ToString(oraDataReader["AREANAME"])));

            }
            oraVectorConn.Close();
            oraCMD.Dispose();

            return area;
        }

        public static Area getArea(string areacode)
        {
            Area area = new Area();
            OracleConnection oraVectorConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraVectorConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT AREACODE,AREANAME FROM AREA WHERE AREACODE = '" + areacode + "'";
            oraCMD = new OracleCommand(sql, oraVectorConn);

            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }

            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;

                area = new Area(Convert.ToString(oraDataReader["AREACODE"]), Convert.ToString(oraDataReader["AREANAME"]));

            }
            oraVectorConn.Close();
            oraCMD.Dispose();

            return area;
        }

    
    }

    public class DataSource
    {
        public static List<string> getDataSource()
        {
            List<string> datasource = new List<string>();

            OracleConnection oraVectorConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraVectorConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT NAME FROM DATASOURCE";
            oraCMD = new OracleCommand(sql, oraVectorConn);

            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }

            while (oraDataReader.Read())
            {
                //int fieldCount = oraDataReader.FieldCount;
                datasource.Add(Convert.ToString(oraDataReader["NAME"]));

            }
            oraVectorConn.Close();
            oraCMD.Dispose();

            return datasource;
        }
    }

}
