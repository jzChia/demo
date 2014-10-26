using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Demo.Entities
{
    public enum HandleLogTypeEnum
    {
        login=0,
        logout=1,
        data_delete=2,
        data_update=3,
        data_query=4,
        data_import=5,
        data_export=6,
        data_browse=7,
        user_insert =8,
        user_update =9,
        user_delete=10,
        role_insert=11,
        role_update=12,
        role_delete=13
    }

    public class HandleLog
    {
        public string LOGID;
        public string USERID;
        public HandleLogTypeEnum TYPE;
        public DateTime BEGINDATE;
        public DateTime ENDDATE;
        public string DESCRIPTION;
        public long LAYERSIZE;
        public string RESULT;
        public string TARGETDB;
        public string TARGETITEM;
        public string BATCHID;
       // id ， user， type， begindate，enddate， description， size， result， targetdb， targetitem， batchid，


        public static string errMSG = "";
        private static HandleLog InitHandleLogData(HandleLog HL, object obj, string fieldName)
        {
            try
            {
                switch (fieldName)
                {
                    case "LOGID": HL.LOGID = Convert.ToString(obj); break;
                    case "USERID": HL.USERID = Convert.ToString(obj); break;
                    case "TYPE": HL.TYPE = (HandleLogTypeEnum)Enum.Parse(typeof(HandleLogTypeEnum),Convert.ToString(obj)); break;
                    case "BEGINDATE": HL.BEGINDATE = Convert.ToDateTime(obj); break;
                    case "ENDDATE": HL.ENDDATE = Convert.ToDateTime(obj); break;
                    case "DESCRIPTION": HL.DESCRIPTION = Convert.ToString(obj); break;
                    case "LAYERSIZE": HL.LAYERSIZE = Convert.ToInt64(obj); break;
                    case "RESULT": HL.RESULT = Convert.ToString(obj); break;
                    case "TARGETDB": HL.TARGETDB = Convert.ToString(obj); break;
                    case "TARGETITEM": HL.TARGETITEM = Convert.ToString(obj); break;
                    case "BATCHID": HL.BATCHID = Convert.ToString(obj); break;
                    default: break;
                }
            }
            catch
            {
                errMSG = fieldName + ";";
            }

            return HL;
        }

        public static int ImportHandleLog(HandleLog HL)
        {
            OracleConnection oraFileConn;
            OracleCommand oraCMD;

            oraFileConn = DBCon.OraConOpen(); ;

            StringBuilder sql;
            sql = new StringBuilder("INSERT INTO HANDLELOG(LOGID,USERID, TYPE,BEGINDATE,ENDDATE,DESCRIPTION, LAYERSIZE,RESULT,TARGETDB, TARGETITEM,BATCHID)" );
            
            sql.Append(" VALUES(sys_guid(),:p1, :p2, :p3, :p4, :p5, :p6, :p7, :p8, :p9, :p10)");
           
            oraCMD = new OracleCommand(sql.ToString(), oraFileConn);
            oraCMD.Parameters.Add(":p1", HL.USERID);
            oraCMD.Parameters.Add(":p2", HL.TYPE.ToString());
            oraCMD.Parameters.Add(":p3", HL.BEGINDATE);
            oraCMD.Parameters.Add(":p4", HL.ENDDATE);
            oraCMD.Parameters.Add(":p5", HL.DESCRIPTION);
            oraCMD.Parameters.Add(":p6", HL.LAYERSIZE);
            oraCMD.Parameters.Add(":p7", HL.RESULT);
            oraCMD.Parameters.Add(":p8", HL.TARGETDB);
            oraCMD.Parameters.Add(":p9", HL.TARGETITEM);
            oraCMD.Parameters.Add(":p10", HL.BATCHID);

            int result = oraCMD.ExecuteNonQuery();

            oraFileConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static HandleLog GetHandleLogById(string LogID)
        {
            HandleLog HL = new HandleLog();

            OracleConnection oraFileConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraFileConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT * FROM HANDLELOG WHERE LOGID = :p1";
            oraCMD = new OracleCommand(sql, oraFileConn);
            oraCMD.Parameters.Add(":p1", LogID);


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
                    HL = InitHandleLogData(HL, oraDataReader[fieldName], fieldName);

                }

            }
            oraFileConn.Close();
            oraCMD.Dispose();

            return HL;
        }

        public static List<HandleLog> GetHandleLogByTargetDBAndTargetItem(string targetDB,string targetItem)
        {
            if (String.IsNullOrEmpty(targetDB) || String.IsNullOrEmpty(targetItem))
                return null;
            return GetHandleLogByTargetAndType(targetDB,targetItem,null);
        }

        public static List<HandleLog> GetHandleLogByTargetAndType(string targetDB, string targetItem,string type)
        {
            List<HandleLog> HLs = new List<HandleLog>();

            OracleConnection oraFileConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraFileConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT * FROM HANDLELOG WHERE TARGETDB = :p1 AND TARGETITEM=:p2 ";

            if (!String.IsNullOrEmpty(type))
            {
                sql += " AND TYPE=:p3"; ;
            }
            oraCMD = new OracleCommand(sql, oraFileConn);
            oraCMD.Parameters.Add(":p1", targetDB);
            oraCMD.Parameters.Add(":p2", targetItem);
            if (!String.IsNullOrEmpty(type))
            {
                oraCMD.Parameters.Add(":p3", type);
            }


            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }

            HandleLog HL;
            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;
                HL = new HandleLog();
                for (int i = 0; i < fieldCount; i++)
                {
                    string fieldName = oraDataReader.GetName(i).ToString();
                    HL = InitHandleLogData(HL, oraDataReader[fieldName], fieldName);

                }
                HLs.Add(HL);

            }
            oraFileConn.Close();
            oraCMD.Dispose();

            return HLs;
        }
    }


}
