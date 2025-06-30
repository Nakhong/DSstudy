using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrganizationTreeForm.Model;

namespace OrganizationTreeForm.View
{
    public partial class SearchControl : UserControl
    {
        public TreeView TreeView { get; set; }
        public PlayerControl TargetPlayerControl { get; set; }

        public SearchControl()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (TreeView == null)
            {
                MessageBox.Show("트리를 먼저 설정해주세요.");
                return;
            }

            string keyword = searchTB.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("검색어를 입력하세요.");
                return;
            }

            TreeNode found = FindNodeByText(TreeView.Nodes, keyword);

            if (found != null)
            {
                TreeView.SelectedNode = found;
                found.EnsureVisible();
                TreeView.Focus();

                // Player가 검색 된다면 PlayerControl에 전달
                if (found.Tag is Player player && TargetPlayerControl != null)
                {
                    TargetPlayerControl.SetPlayerInfo(
                        player.PlayerName,
                        player.PlayerNumber,
                        player.PlayerPosition,
                        player.PlayerFoot,
                        player.ImagePath
                    );
                }

            }
            else
            {
                MessageBox.Show("일치하는 노드를 찾을 수 없습니다.");
            }
        }

        private TreeNode FindNodeByText(TreeNodeCollection nodes, string keyword)
        {
            foreach (TreeNode node in nodes)
            {
                // 텍스트로 비교
                if (node.Text.ToLower().Contains(keyword.ToLower()))
                {
                    return node;
                }

                // Tag가 Player라면 이름도 검사
                if (node.Tag is Player player)
                {
                    if (!string.IsNullOrEmpty(player.PlayerName) &&
                        player.PlayerName.ToLower().Contains(keyword.ToLower()))
                    {
                        return node;
                    }
                }

                // 하위 노드 검색 (재귀 호출)
                TreeNode found = FindNodeByText(node.Nodes, keyword);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

    }
}
