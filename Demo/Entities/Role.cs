using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
namespace Demo.Entities
{
    public class Role
    {
        public string ROLEID;
        public string ROLENAME;
        public int QUERYGRADER;
        public int GETGRADER;

        public static string errMSG = "";
        private static Role InitRoleData(Role RL, object obj, string fieldName)
        {
            try
            {
                switch (fieldName)
                {
                    case "ROLEID": RL.ROLEID = Convert.ToString(obj); break;
                    case "ROLENAME": RL.ROLENAME = Convert.ToString(obj); break;
                    case "QUERYGRADER": RL.QUERYGRADER = Convert.ToInt32(obj); break;
                    case "GETGRADER": RL.GETGRADER = Convert.ToInt32(obj); break;
                    default: break;
                }
            }
            catch
            {
                errMSG = fieldName + ";";
            }

            return RL;
        }


        public static int ImportRole(Role role)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;
            sql = "INSERT INTO ROLE(ROLEID,ROLENAME,QUERYGRADER,GETGRADER) " +
                " VALUES(sys_guid(),:p1, :p2, :p3)";
            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", role.ROLENAME);
            oraCMD.Parameters.Add(":p2", role.QUERYGRADER);
            oraCMD.Parameters.Add(":p3", role.GETGRADER);


            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static int UpdateRole(Role role)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;
            sql = "UPDATE ROLE SET ROLENAME=:p1,QUERYGRADER=:p2,GETGRADER=:p3 " +
                " WHERE ROLEID=:p4";
            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", role.ROLENAME);
            oraCMD.Parameters.Add(":p2", role.QUERYGRADER);
            oraCMD.Parameters.Add(":p3", role.GETGRADER);
            oraCMD.Parameters.Add(":p4", role.ROLEID);


            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static int DeleteRole(Role role)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;
            sql = "DELETE FROM ROLE  WHERE ROLEID=:p1";
            oraCMD = new OracleCommand(sql, oraConn);
            // oraCMD.Parameters.Add();
            oraCMD.Parameters.Add(":p1", role.ROLEID);


            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        public static Role GetRoleById(string roleId)
        {
            Role role = new Role();

            OracleConnection oraRasterConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraRasterConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT * FROM ROLE WHERE ROLEID = :p1";
            oraCMD = new OracleCommand(sql, oraRasterConn);
            oraCMD.Parameters.Add(":p1", roleId);


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
                    role = InitRoleData(role, oraDataReader[fieldName], fieldName);

                }

            }
            oraRasterConn.Close();
            oraCMD.Dispose();

            return role;
        }
    }
}
