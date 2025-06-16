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

        public Organization()
        {
            InitializeComponent();

            ExcelHelper excelHelper = new ExcelHelper();
            Dictionary<string,Country> a = excelHelper.ReadExcel();

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
