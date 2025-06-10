using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTreeForm.Model
{
    public class Company
    {
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string CompanyAddress { get; set; }

        public Company()
        {
            ReadCSV();
        }

        private void ReadCSV()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Data\\Data.csv";

            StreamReader sr = new StreamReader(path, Encoding.UTF8);

            List<string> lines = new List<string>();

            // 스트림의 끝까지 읽기

            while (!sr.EndOfStream)
            {

                string line = sr.ReadLine();

                string[] data = line.Split(';');

            }
        }
    }
}
