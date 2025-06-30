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
    public class League
    {
        public string Uid { get; set; }
        public string LeagueName { get; set; }
        public string ParentUid { get; set; }
        public string Level { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();

        public static List<League> LoadByParent(string parentUid)
        {
            DBHelper db = new DBHelper();
            var dt = db.Read($"SELECT * FROM [Soccer$] WHERE Level = '2' AND parentUid = '{parentUid}'");
            List<League> leagues = new List<League>();

            foreach (DataRow row in dt.Rows)
            {
                var league = new League
                {
                    Uid = row["Uid"].ToString(),
                    LeagueName = row["Name"].ToString(),
                    ParentUid = row["parentUid"].ToString(),
                    Level = row["Level"].ToString()
                };

                league.Teams = Team.LoadByParent(league.Uid);
                leagues.Add(league);
            }
            return leagues;
        }

        public TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(LeagueName) { Name = Uid };
            foreach (var team in Teams)
            {
                node.Nodes.Add(team.ToTreeNode());
            }
            return node;
        }
    }

}
