using OrganizationTreeForm.Model;
using OrganizationTreeForm.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganizationTreeForm.View
{
    public class TreeLoad
    {
        public Dictionary<string, TreeNode> companyNodes = new Dictionary<string, TreeNode>();
        private Dictionary<string, TreeNode> departmentNodes = new Dictionary<string, TreeNode>();

        public TreeLoad(List<Employee> list, TreeView orgTV)
        {
            foreach (var emp in list)
            {
                if (!companyNodes.ContainsKey(emp.CompanyName))
                {
                    TreeNode companyNode = new TreeNode(emp.CompanyName);
                    companyNodes[emp.CompanyName] = companyNode;
                    orgTV.Nodes.Add(companyNode);
                }

                var companyNodeRef = companyNodes[emp.CompanyName];

                if (!departmentNodes.ContainsKey(emp.DepartmentName))
                {
                    TreeNode departmentNode = new TreeNode(emp.DepartmentName);
                    departmentNodes[emp.DepartmentName] = departmentNode;
                    companyNodes[emp.CompanyName].Nodes.Add(departmentNode);
                }

                TreeNode employeeNode = new TreeNode($"{emp.EmployeeName} ({emp.EmployeePosition})");
                departmentNodes[emp.DepartmentName].Nodes.Add(employeeNode);
            }

            orgTV.ExpandAll();
        }
    }
}
