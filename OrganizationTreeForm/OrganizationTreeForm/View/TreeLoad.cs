using OrganizationTreeForm.Model;
using OrganizationTreeForm.Utils;
using System;
using System.Collections.Generic;
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

        public void DisplayTree(Dictionary<string, Country> countries, TreeView treeView)
        {
            treeView.Nodes.Clear();

            foreach (var country in countries.Values)
            {
                TreeNode countryNode = new TreeNode(country.CountryName);
                AddLeagueNodes(country.Leagues, countryNode);
                treeView.Nodes.Add(countryNode);
            }

            treeView.ExpandAll();
        }

        private void AddLeagueNodes(List<League> leagues, TreeNode parentNode)
        {
            foreach (var league in leagues)
            {
                TreeNode leagueNode = new TreeNode(league.LeagueName);
                AddTeamNodes(league.Teams, leagueNode);
                parentNode.Nodes.Add(leagueNode);
            }
        }

        private void AddTeamNodes(List<Team> teams, TreeNode parentNode)
        {
            foreach (var team in teams)
            {
                TreeNode teamNode = new TreeNode(team.TeamName);
                AddPlayerNodes(team.Players, teamNode);
                parentNode.Nodes.Add(teamNode);
            }
        }

        private void AddPlayerNodes(List<Player> players, TreeNode parentNode)
        {
            foreach (var player in players)
            {
                TreeNode playerNode = new TreeNode($"{player.PlayerName} ({player.PlayerPosition})");
                parentNode.Nodes.Add(playerNode);
            }
        }

    }
}
