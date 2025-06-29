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
    public class Player
    {
        public string Uid { get; set; }
        public string ParentUid { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public string PlayerNumber { get; set; }
        public string PlayerFoot { get; set; }
        public string Level { get; set; }

        public static List<Player> LoadByParent(string parentUid)
        {
            DBHelper db = new DBHelper();
            var dt = db.Read($"SELECT * FROM [Soccer$] WHERE Level = '4' AND parentUid = '{parentUid}'");
            List<Player> players = new List<Player>();

            foreach (DataRow row in dt.Rows)
            {
                players.Add(new Player
                {
                    Uid = row["Uid"].ToString(),
                    ParentUid = row["parentUid"].ToString(),
                    PlayerName = row["Name"].ToString(),
                    PlayerPosition = row["Position"].ToString(),
                    PlayerNumber = row["PlayerNumber"].ToString(),
                    PlayerFoot = row["Foot"].ToString(),
                    Level = row["Level"].ToString()
                });
            }
            return players;
        }

        public TreeNode ToTreeNode()
        {
            return new TreeNode(PlayerName)
            {
                Name = Uid,
                Tag = this
            };
        }
    }

}
