using OrganizationTreeForm.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// CSV 관련 메소드 추가
/// CRUD
/// </summary>
namespace OrganizationTreeForm.Utils
{
    public static class CSVHelper
    {
        private static string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Data\\Data.csv";
        public static List<Employee> ReadCSV()
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);

            List<Employee> list = new List<Employee>(); // employee list 컨테이너 생성

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line)) continue; // csv에 빈 cell 체크

                var parts = line.Split(';'); // ;를 기준으로 나누기
                if (parts.Length < 8) continue;

                Employee emp = new Employee(parts[0], parts[1], parts[2], parts[3], parts[4], parts[6], parts[5], parts[7]); // employee 생성
                list.Add(emp);
            }

            return list;
        }
    }
}
