using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganizationTreeForm.Model
{
    public class Team
    {
        public string Uid { get; set; }
        public string TeamName { get; set; }
        public League ParentLeague { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public string Level { get; set; }

        public TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(TeamName) { Name = Uid };
            foreach (var player in Players)
            {
                TreeNode playerNode = new TreeNode(player.PlayerName)
                {
                    Name = player.Uid,
                    Tag = player
                };
                node.Nodes.Add(playerNode);
            }
            return node;
        }
    }

}
