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
    /// <summary>
    /// TreeView node 만들어주기
    /// </summary>
    public class TreeLoad
    {
        public Dictionary<string, TreeNode> companyNodes = new Dictionary<string, TreeNode>();
        private Dictionary<string, TreeNode> departmentNodes = new Dictionary<string, TreeNode>();

        public TreeLoad(List<Employee> list, TreeView orgTV) // csv row로 list 추가
        {
            foreach (var emp in list) // csv에서 가공한 데이터 반복
            {
                if (!companyNodes.ContainsKey(emp.CompanyName))
                {
                    TreeNode companyNode = new TreeNode(emp.CompanyName); //회사 노드 생성
                    companyNodes[emp.CompanyName] = companyNode; // 중복 확인 위해서 추가
                    orgTV.Nodes.Add(companyNode); // Treeview 컨트롤에 회사 추가.
                }

                if (!departmentNodes.ContainsKey(emp.DepartmentName)) 
                {
                    TreeNode departmentNode = new TreeNode(emp.DepartmentName); //부서 노드 생성
                    departmentNodes[emp.DepartmentName] = departmentNode; //중복 확인 위해서 추가
                    companyNodes[emp.CompanyName].Nodes.Add(departmentNode); //부서 추가
                }

                TreeNode employeeNode = new TreeNode($"{emp.EmployeeName}({emp.EmployeePosition})"); //직원 노드 생성
                departmentNodes[emp.DepartmentName].Nodes.Add(employeeNode); //직원 추가
            }

            orgTV.ExpandAll(); //tree 전부 펼치기
        }
    }
}
