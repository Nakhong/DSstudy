using OrganizationTreeForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class Employee : Department
    {
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeEmail { get; set; }
        public Employee() {
            
        }

        public Employee(string companyName,string companyAddress,string companyNumber,string departmentName, string employeeName, string employeeNumber, string employeePosition, string employeeEmail) {
            CompanyName = companyName;
            CompanyAddress = companyAddress;
            CompanyNumber = companyNumber;
            DepartmentName = departmentName;
            EmployeeName = employeeName;
            EmployeeNumber = employeeNumber;
            EmployeeEmail = employeeEmail;
            EmployeePosition = employeePosition;
        }


    }
}
