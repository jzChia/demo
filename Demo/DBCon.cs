using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using Oracle.DataAccess.Client;

namespace Demo
{
    public class DBCon
    {
        //public static string mdbPath = System.Environment.CurrentDirectory + @"\DB.accdb";//mdb路径
        public static OleDbConnection My_con, db_con, sys_con, thumb_con; //定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static string M_str_OleDbcon = "";//使用本地数据库

        private static string host = "Server-PC";

        //oracle 连接字符串
        private static string ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + host + ")(PORT=1521))" +
"(CONNECT_DATA=(SID=ms)));User Id=jzuser;Password=aA123456;";
            //"Data Source= (DESCRIPTION =(ADDRESS_LIST =" +
             // " (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.35.2)(PORT = 1521)) )(CONNECT_DATA =" +
             //  "(SID = ms)(SERVER = DEDICATED)));user=jzuser;password=aA123456;";
        public static OracleConnection myOraCon;
        
        //static string indexDB = @"\\Server-PC\d\Data\INDEX\INDEX.accdb";
        /// <summary>
        /// 主数据库连接
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection DBCon_Open()
        {
            try
            {
                M_str_OleDbcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Environment.serverROOT+Environment.indexDB;//使用本地数据库
                db_con = new OleDbConnection(M_str_OleDbcon);
                db_con.Open();
            }
            catch
            {
                throw new Exception("元数据库连接失败");
            }
            return db_con;
        }

        //
        /// <summary>
        /// oracle数据库连接
        /// </summary>
        /// <returns>OracleConnection</returns>
        public static OracleConnection OraConOpen()
        {
            try
            {
                myOraCon = new OracleConnection(ConnectionString);//创建一个新连接i
                myOraCon.Open();
            }
            catch 
            {
                throw new Exception("数据库连接失败");
            }
            return myOraCon;
        }

    }
}
