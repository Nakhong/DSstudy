using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class Team
    {
        public string Uid { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public League ParentLeague { get; set; }
        public string TeamName { get; set; }

    }
}
