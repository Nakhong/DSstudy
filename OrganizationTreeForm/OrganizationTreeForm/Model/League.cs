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
        public List<Team> Teams { get; set; } = new List<Team>();
        public Country ParentCountry { get; set; }

    }
}
