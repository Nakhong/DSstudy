using OrganizationTreeForm.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Utils
{
    public static class CSVHelper
    {
        public static List<Employee> ReadCSV()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Data\\Data.csv";

            StreamReader sr = new StreamReader(path, Encoding.UTF8);

            var list = new List<Employee>();

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 8) continue;

                var emp = new Employee(
                    parts[0], parts[2], parts[1], // 회사명, 주소, 전화
                    parts[3],                     // 부서명
                    parts[4], parts[6],           // 이름, 번호
                    parts[5],                     // 직급
                    parts[7]                      // 이메일
                );
                list.Add(emp);
            }

            return list;
        }
    }
}
