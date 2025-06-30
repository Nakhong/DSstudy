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
    public class Country
    {

        public string Uid { get; set; }
        public string CountryName { get; set; }
        public string CountryAddress { get; set; }
        public List<League> Leagues { get; set; } = new List<League>();
        public string Level { get; set; }

        public static List<Country> LoadAll()
        {
            DBHelper db = new DBHelper();
            var dt = db.Read("SELECT * FROM [Soccer$] WHERE Level = '1'");
            List<Country> countries = new List<Country>();
            
            foreach (DataRow row in dt.Rows)
            {
                var country = new Country
                {
                    Uid = row["Uid"].ToString(),
                    CountryName = row["Name"].ToString(),
                    CountryAddress = row["Address"].ToString(),
                    Level = row["Level"].ToString()
                };

                country.Leagues = League.LoadByParent(country.Uid);
                countries.Add(country);
            }
            return countries;
        }

        public TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(CountryName) { Name = Uid, Tag = this };
            foreach (var league in Leagues)
            {
                node.Nodes.Add(league.ToTreeNode());
            }
            return node;
        }
    }

}
