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
            
            TreeLoad OrgTree = new TreeLoad(CSVHelper.ReadCSV(), OrgTV);
        }

        private void OrgTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //선택 시 fullPath로 \\로 나온다.
        }
    }
}
