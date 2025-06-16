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
        DBClass db;        // DBClass 변수
        DataTable dt;		// 데이터테이블 변수

        public Organization()
        {
            InitializeComponent();

            ExcelHelper excelHelper = new ExcelHelper();
            excelHelper.ReadExcel();

            //db = new DBClass(); // 생성자에 매개변수를 넘기면서 객체생성
            //dt = new DataTable(); // 객체생성

            //string qry = "SELECT [UID],[CountryName],[CountryAddress],[LeagueName],[TeamName],[PlayerName],[PlayerNumber],[PlayerPosition] " +
            //            "FROM [Soccer$]";
            //string qry2 = "SELECT * FROM [Sheet1$]";

            //dt = db.Read(qry2); // 데이터테이블 형식으로 받아옴

        }

        /// <summary>
        /// TreeNode 선택 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrgTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //선택 시 fullPath로 \\로 나온다. "이삭엔지니어링\\DS사업본부\\홍화낙 (전임)"
        }
    }
}
