using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Demo.Utility
{
    public class OraBlob
    {
        public static byte[] WriteBolb(string s)
        {
            try
            {
                XmlDocument _Document = new XmlDocument();
                _Document.LoadXml(s); //xmlString is str type 字符串

                return System.Text.Encoding.UTF8.GetBytes(s);
            }
            catch 
            {
                if (File.Exists(s))
                {
                    FileInfo fi = new FileInfo(s);
                    FileStream fs = fi.OpenRead();
                    byte[] MyData = new byte[fs.Length];
                    fs.Read(MyData, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                    return MyData;
                }
                else { return null; }
            }


        }

        public void ReadBolb()
        {
            OracleConnection connn = DBCon.OraConOpen();
            OracleCommand cmddd = connn.CreateCommand();
            cmddd.CommandText = "select * from TEST where PID='21C67F3362EB421ABAE4844C8303C8FA'";
            OracleDataReader rs = cmddd.ExecuteReader();
            while (rs.Read())//读取数据，如果odr.Read()返回为false的话，就说明到记录集的尾部了
            {
                byte[] data = (byte[])rs["TE"];

                UTF8Encoding enc = new UTF8Encoding(true, true);//如果blob数据原本为utf8编码
                string s = System.Text.Encoding.UTF8.GetString(data);
                MessageBox.Show(s);


            }
            rs.Close();
        }
    }
}
