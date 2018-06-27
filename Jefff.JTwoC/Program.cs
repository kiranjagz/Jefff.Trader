using CsvHelper;
using Jefff.JTwoC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.JTwoC
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Person>();
            list.Add(new Person { Age = 108, Name = "Jefff", Surname = "Jaguar" });
            list.Add(new Person { Age = 123, Name = "Mac", Surname = "Pac" });

            var json = JsonConvert.SerializeObject(list, Formatting.Indented);

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            var csv = ToCSV(dt);

            File.WriteAllText("C:\\test.csv", csv);
            Console.Read();
        }

        private static string ToCSV(DataTable dt)
        {
            var csvString = new StringWriter();
            using (var csv = new CsvWriter(csvString))
            {
                csv.Configuration.Delimiter = ",";

                foreach (DataColumn column in dt.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                foreach (DataRow row in dt.Rows)
                {
                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }

            }
            return csvString.ToString();
        }
    }
}
