using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class Team
    {
        public List<Country> Country { get; set; }
        public List<League> League { get; set; }
        public List<Player> Player { get; set; }
        public Team()
        {
            Country = new List<Country>();
            Player = new List<Player>();
        }
    }
}
