using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class Player_
    {
        //DB VALIDATION
        public string Uid { get; set; }
        public Team ParentTeam { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPosition { get; set; }
        public string PlayerNumber { get; set; }
        public string PlayerFoot { get; set; }
        public string Level { get; set; }
    }
}
