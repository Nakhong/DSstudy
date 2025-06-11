using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class League
    {
        public string LeagueName { get; set; }
        public List<Country> Country { get; set; }
        public List<Team> Team { get; set; }
        public League()
        {
            Country = new List<Country>();
            Team = new List<Team>();
        }
    }
}
