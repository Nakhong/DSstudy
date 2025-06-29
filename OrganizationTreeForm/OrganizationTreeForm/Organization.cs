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

            var countries = Country.LoadAll();

            foreach (var country in countries)
            {
                OrgTV.Nodes.Add(country.ToTreeNode());
            }

            OrgTV.ExpandAll();

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
            Player player = e.Node.Tag as Player;

            if (player != null)
            {

                playerControl.SetPlayerInfo(
                    player.PlayerName,
                    player.PlayerNumber,
                    player.PlayerPosition,
                    player.PlayerFoot
                );
            }
        }
    }
}
