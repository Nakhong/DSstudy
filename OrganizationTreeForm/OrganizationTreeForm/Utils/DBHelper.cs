using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Utils
{
    public class DBHelper
    {
        OleDbConnection oleCon;
        private static string path = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Soccer.xlsx";

        // 연결 함수
        public void Conn()
        {

            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
            this.oleCon = new OleDbConnection(connectionString);
            oleCon.Open();
        }

        public DataTable Read(string query)
        {
            try
            {
                // db 연결
                Conn();

                OleDbDataAdapter oleOda = new OleDbDataAdapter(query, oleCon);

                DataTable exDataTable = new DataTable();
                oleOda.Fill(exDataTable);
                oleCon.Close();

                return exDataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}