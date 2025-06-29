using OrganizationTreeForm.Utils;
using System;
using System.Collections.Generic;
using System.Data;
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
        public string ParentUid { get; set; }
        public string Level { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();

        public static List<Team> LoadByParent(string parentUid)
        {
            DBHelper db = new DBHelper();
            var dt = db.Read($"SELECT * FROM [Soccer$] WHERE Level = '3' AND parentUid = '{parentUid}'");
            List<Team> teams = new List<Team>();

            foreach (DataRow row in dt.Rows)
            {
                var team = new Team
                {
                    Uid = row["Uid"].ToString(),
                    TeamName = row["Name"].ToString(),
                    ParentUid = row["parentUid"].ToString(),
                    Level = row["Level"].ToString()
                };

                team.Players = Player.LoadByParent(team.Uid);
                teams.Add(team);
            }
            return teams;
        }

        public TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(TeamName) { Name = Uid };
            foreach (var player in Players)
            {
                node.Nodes.Add(player.ToTreeNode());
            }
            return node;
        }
    }


}
