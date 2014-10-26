using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace Demo.Utility
{
    public class TreeNodeOra
    {
        public static TreeNode GetTreeNode(string DB, string DBName)
        {
            OracleConnection oraConn;
            OracleCommand oraCMD;
            OracleDataReader oraDataReader;

            oraConn = DBCon.OraConOpen(); ;

            string sql;
            sql = string.Format("SELECT * FROM {0} WHERE DBNAME = :p1", DB);

            oraCMD = new OracleCommand(sql, oraConn);
            oraCMD.Parameters.Add(":p1", DBName);

            try { oraDataReader = oraCMD.ExecuteReader(); }
            catch { return null; }

            if (!oraDataReader.HasRows)
            {
                return null;
            }
            TreeNode nowNode = new TreeNode(DBName);
            nowNode.Tag = DB;

            string errMSG = "";
            while (oraDataReader.Read())
            {
                TreeNode childNode = new TreeNode();
                try { childNode.Tag = oraDataReader["LAYERID"]; }
                catch { errMSG += "LAYERID;"; }
                try { childNode.Text = Convert.ToString(oraDataReader["LAYERNAME"]); }
                catch { errMSG += "LAYERNAME;"; }
                nowNode.Nodes.Add(childNode);
            }
            oraConn.Close();
            oraCMD.Dispose();

            return nowNode;
        }
    }

}
