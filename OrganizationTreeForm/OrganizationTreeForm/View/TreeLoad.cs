using OrganizationTreeForm.Model;
using OrganizationTreeForm.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganizationTreeForm.View
{
    /// <summary>
    /// TreeView node 만들어주기
    /// </summary>
    public class TreeLoad
    {

        public static TreeNode BuildFromDataTable(DataTable dt)
        {
            Dictionary<string, Country> countries = new Dictionary<string, Country>();
            Dictionary<string, League> leagues = new Dictionary<string, League>();
            Dictionary<string, Team> teams = new Dictionary<string, Team>();

            foreach (DataRow row in dt.Rows)
            {
                string level = row["Level"].ToString();
                string uid = row["Uid"].ToString();
                string name = row["Name"].ToString();
                string address = row["Address"].ToString();
                string parentUid = row["parentUid"].ToString();

                switch (level)
                {
                    case "1":
                        countries[uid] = new Country
                        {
                            Uid = uid,
                            CountryName = name,
                            CountryAddress = address,
                            Level = level
                        };
                        break;

                    case "2":
                        var league = new League
                        {
                            Uid = uid,
                            LeagueName = name,
                            Level = level
                        };
                        if (countries.ContainsKey(parentUid))
                        {
                            league.ParentCountry = countries[parentUid];
                            countries[parentUid].Leagues.Add(league);
                        }
                        leagues[uid] = league;
                        break;

                    case "3":
                        var team = new Team
                        {
                            Uid = uid,
                            TeamName = name,
                            Level = level
                        };
                        if (leagues.ContainsKey(parentUid))
                        {
                            team.ParentLeague = leagues[parentUid];
                            leagues[parentUid].Teams.Add(team);
                        }
                        teams[uid] = team;
                        break;

                    case "4":
                        var player = new Player
                        {
                            Uid = uid,
                            PlayerName = name,
                            PlayerNumber = row["PlayerNumber"].ToString(),
                            PlayerFoot = row["Foot"].ToString(),
                            PlayerPosition = row["Position"].ToString(),
                            Level = level,
                            ParentUid = parentUid
                        };
                        if (teams.ContainsKey(parentUid))
                        {
                            teams[parentUid].Players.Add(player);
                        }
                        break;
                }
            }

            // 최상위 Country들을 트리로 변환
            TreeNode rootNode = new TreeNode("Soccer Tree");
            foreach (var country in countries.Values)
            {
                rootNode.Nodes.Add(country.ToTreeNode());
            }

            return rootNode;
        }
    }
}
