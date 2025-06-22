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
        DBHelper db = new DBHelper();

        public string Uid { get; set; }
        public string CountryName { get; set; }
        public string CountryAddress { get; set; }
        public List<League> Leagues { get; set; } = new List<League>();
        public string Level { get; set; }

        public TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(CountryName) { Name = Uid };
            foreach (var league in Leagues)
            {
                node.Nodes.Add(league.ToTreeNode());
            }
            return node;
        }

        public List<Country> LoadCountries()
        {
            DBHelper db = new DBHelper();
            DataTable dt = db.Read("SELECT [Uid], [Name], [Address], [Level] FROM [Soccer$]");
            List<Country> countries = new List<Country>();

            foreach (DataRow row in dt.Rows)
            {
                Country country = new Country
                {
                    Uid = row["Uid"].ToString(),
                    CountryName = row["Name"].ToString(),
                    CountryAddress = row["Address"].ToString(),
                    Level = row["Level"].ToString()
                };
                countries.Add(country);
            }
            return countries;
        }
    }
}
