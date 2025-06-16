using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class Player
    {
        public string Uid { get; set; }
        public Team ParentTeam { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public int PlayerNumber { get; set; }

    }
}
