using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class Player
    {
        public List<Country> Country { get; set; }
        public List<League> League { get; set; }
        public List<Team> Team { get; set; }
        public Player()
        {
            League = new List<League>();
            Team = new List<Team>();
        }
    }
}
