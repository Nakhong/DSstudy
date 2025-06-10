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
        private static string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Data\\Data.csv";
        public static List<Employee> ReadCSV()
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);

            var list = new List<Employee>();

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 8) continue;

                var emp = new Employee(parts[0], parts[1], parts[2], parts[3], parts[4], parts[6], parts[5], parts[7]);
                list.Add(emp);
            }

            return list;
        }
    }
}
