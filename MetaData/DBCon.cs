using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;

namespace MetaData
{
    public class DBCon
    {
        //public static string mdbPath = System.Environment.CurrentDirectory + @"\DB.accdb";//mdb路径
        public static OleDbConnection My_con, db_con, sys_con, thumb_con; //定义一个SqlConnection类型的公共变量My_con，用于判断数据库是否连接成功
        public static string M_str_OleDbcon = "";//使用本地数据库
        static string indexDB = @"\\Server-PC\d\Data\INDEX\INDEX.accdb";
        /// <summary>
        /// 主数据库连接
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection DBCon_Open()
        {
            try
            {
                M_str_OleDbcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + indexDB;//使用本地数据库
                db_con = new OleDbConnection(M_str_OleDbcon);
                db_con.Open();
            }
            catch
            {
                throw new Exception("元数据库连接失败");
            }
            return db_con;
        }

    }
}
