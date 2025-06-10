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

        public TreeLoad(List<Employee> list, TreeView orgTV)
        {
            var companies = new Dictionary<string, TreeNode>();

            foreach (var emp in list)
            {
                // 1. 회사 노드가 없으면 생성
                if (!companies.ContainsKey(emp.CompanyName))
                {
                    TreeNode companyNode = new TreeNode(emp.CompanyName);
                    companies[emp.CompanyName] = companyNode;
                    orgTV.Nodes.Add(companyNode);
                }

                TreeNode currentCompanyNode = companies[emp.CompanyName];

                // 2. 부서 노드 찾기 또는 생성
                TreeNode departmentNode = currentCompanyNode.Nodes
                    .Cast<TreeNode>()
                    .FirstOrDefault(n => n.Text == emp.DepartmentName);

                if (departmentNode == null)
                {
                    departmentNode = new TreeNode(emp.DepartmentName);
                    currentCompanyNode.Nodes.Add(departmentNode);
                }

                // 3. 직원 노드 추가
                TreeNode employeeNode = new TreeNode($"{emp.EmployeeName} ({emp.EmployeePosition})");
                departmentNode.Nodes.Add(employeeNode);
            }

            orgTV.ExpandAll();
        }
    }
}
