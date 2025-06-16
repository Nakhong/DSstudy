using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class Country
    {
        public string Uid { get; set; }
        public string CountryName { get; set; }
        public string CountryAddress { get; set; }
        public List<League> Leagues { get; set; } = new List<League>();
        public string Level { get; set; }
    }
}
