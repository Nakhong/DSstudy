using OrganizationTreeForm.Model;
using OrganizationTreeForm.Utils;
using OrganizationTreeForm.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganizationTreeForm
{
    public partial class Organization : Form
    {
        DBHelper db;        // DBClass 변수
        DataTable dt;		// 데이터테이블 변수

        public Organization()
        {

            db = new DBHelper(); // 생성자에 매개변수를 넘기면서 객체생성
            dt = new DataTable(); // 객체생성

            //string qry = "SELECT [UID],[CountryName],[CountryAddress],[LeagueName],[TeamName],[PlayerName],[PlayerNumber],[PlayerPosition] " +
             //           "FROM [Soccer$]";
            string qry2 = "SELECT * FROM [Player$]";

            dt = db.Read(qry2); // 데이터테이블 형식으로 받아옴

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["Uid"]}, {row["PlayerName"]}, {row["PlayerPosition"]}");
            }

            InitializeComponent();
            //EXCEL 읽기
            //ExcelHelper excelHelper = new ExcelHelper();
            //Dictionary<string, Country> excelData = excelHelper.ReadExcel();
            ////TREE그리기
            //TreeLoad treeLoad = new TreeLoad();
            //treeLoad.DisplayTree(excelData, OrgTV);
        }

        /// <summary>
        /// TreeNode 선택 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrgTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 각 node에 있는 tag를 바라보고 label의 값을 대입 시켜준다.
            object player = e.Node.Tag;
            
        }
    }
}
