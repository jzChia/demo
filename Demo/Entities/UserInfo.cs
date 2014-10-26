using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Demo.Entities
{
    public class UserInfo
    {
        public string USERID;
        public string LOGINID = "";
        public string PASSWORD = "";
        public string ROLEID = "";
        public int ISLOGIN;
        public int STATE;

        public static string errMSG = "";
        private static UserInfo InitUserInfoData(UserInfo RL, object obj, string fieldName)
        {
            try
            {
                switch (fieldName)
                {
                    case "USERID": RL.USERID = Convert.ToString(obj); break;
                    case "LOGINID": RL.LOGINID = Convert.ToString(obj); break;
                    case "PASSWORD": RL.PASSWORD = Convert.ToString(obj); break;
                    case "ROLEID": RL.ROLEID = Convert.ToString(obj); break;
                    case "ISLOGIN": RL.ISLOGIN = Convert.ToInt32(obj); break;
                    case "STATE": RL.STATE = Convert.ToInt32(obj); break;
                    default: break;
                }
            }
            catch
            {
                errMSG = fieldName + ";";
            }

            return RL;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int ImportUserInfo(UserInfo userInfo)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;
            sql = "INSERT INTO USERINFO(USERID,LOGINID,PASSWORD,ROLEID,ISLOGIN,STATE) " +
                " VALUES(sys_guid(),:p1, :p2, :p3,:p4,:p5)";
            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", userInfo.LOGINID);
            oraCMD.Parameters.Add(":p2", userInfo.PASSWORD);
            oraCMD.Parameters.Add(":p3", userInfo.ROLEID);
            oraCMD.Parameters.Add(":p4", userInfo.ISLOGIN);
            oraCMD.Parameters.Add(":p5", userInfo.STATE);


            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int UpdateUserInfo(UserInfo user)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "UPDATE USERINFO SET LOGINID=:p1,PASSWORD=:p2,ROLEID=:p3,ISLOGIN=:p4,STATE=:p5 " +
                " WHERE USERID =:p6)";
            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", user.LOGINID);
            oraCMD.Parameters.Add(":p2", user.PASSWORD);
            oraCMD.Parameters.Add(":p3", user.ROLEID);
            oraCMD.Parameters.Add(":p4", user.ISLOGIN);
            oraCMD.Parameters.Add(":p5", user.STATE);
            oraCMD.Parameters.Add(":p6", user.USERID);


            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        /// <summary>
        /// 修改用户登录状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isLogin"></param>
        /// <returns></returns>
        public static int ChangeUserInfoIslogin(string userId, int isLogin)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "UPDATE USERINFO SET ,ISLOGIN=:p1 WHERE USERID =:p2)";
            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", isLogin);
            oraCMD.Parameters.Add(":p2", userId);

            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static int ChangeUserInfoState(string userId, int state)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "UPDATE USERINFO SET STATE=:p1 WHERE USERID =:p2)";
            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", state);
            oraCMD.Parameters.Add(":p2", userId);

            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static int AuthorizationUserInfo(string userId, string roleId)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "UPDATE USERINFO SET ROLEID=:p1 WHERE USERID =:p2)";
            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", roleId);
            oraCMD.Parameters.Add(":p2", userId);


            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int DeleteUserInfo(UserInfo user)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;

            oraConn = DBCon.OraConOpen(); ;

            string sql;
            sql = "DELETE FROM USERINFO  WHERE USERID=:p1";
            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", user.USERID);


            int result = oraCMD.ExecuteNonQuery();

            oraConn.Close();
            oraCMD.Dispose();

            return result;
        }

        /// <summary>
        /// 用户登录信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="pwd"></param>
        /// <returns>用户信息</returns>
        public static UserInfo GetUserInfoByLoginInfo(string loginId, string pwd)
        {
            UserInfo userinfo = new UserInfo();

            OracleConnection oraRasterConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraRasterConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT * FROM USERINFO WHERE LOGINID = :p1 AND PASSWORD = :p2 AND ISLOGIN=0 AND STATE = 0";
            oraCMD = new OracleCommand(sql, oraRasterConn);
            oraCMD.Parameters.Add(":p1", loginId);
            oraCMD.Parameters.Add(":p2", pwd);


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
                    userinfo = InitUserInfoData(userinfo, oraDataReader[fieldName], fieldName);

                }

            }
            oraRasterConn.Close();
            oraCMD.Dispose();

            return userinfo;
        }

        /// <summary>
        /// 用户登陆状况 
        /// return 0 :登陆成功，1：登录失败，2：用户停用，3:用户已登录，4：未授权用户 
        /// </summary>
        /// <param name="loginId">uuuu</param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int UserInfoLogin(string loginId, string pwd, out UserInfo userinfo)
        {
            userinfo = new UserInfo();
            int result = -1;
            OracleConnection oraRasterConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraRasterConn = DBCon.OraConOpen(); ;

            string sql;

            sql = "SELECT * FROM USERINFO WHERE LOGINID = :p1 AND PASSWORD = :p2 ";
            oraCMD = new OracleCommand(sql, oraRasterConn);
            oraCMD.Parameters.Add(":p1", loginId);
            oraCMD.Parameters.Add(":p2", pwd);


            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return result; }

            if (!oraDataReader.HasRows)
            {
                result = 1;
                return result;
            }

            while (oraDataReader.Read())
            {
                int fieldCount = oraDataReader.FieldCount;

                for (int i = 0; i < fieldCount; i++)
                {
                    string fieldName = oraDataReader.GetName(i).ToString();
                    userinfo = InitUserInfoData(userinfo, oraDataReader[fieldName], fieldName);

                }

            }
            oraRasterConn.Close();
            oraCMD.Dispose();

            if (userinfo == null)
            {
                result = 1;
            }
            else if (userinfo.STATE == 1)
            {
                result = 2;
            }
            else if (userinfo.ISLOGIN == 1)
            {
                result = 3;
            }
            else if (userinfo.ROLEID == null)
            {
                result = 4;
            }
            else
            {
                result = 1;
            }

            return result;
        }
    }

}
