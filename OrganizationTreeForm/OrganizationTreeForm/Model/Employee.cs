using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    class Employee : Department
    {
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeePosition { get; set; }
        public int EmployeeAge { get; set; }
        public string EmployeeEmail { get; set; }
    }
}
