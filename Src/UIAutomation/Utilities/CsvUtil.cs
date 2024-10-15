using System.Data;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace UIAutomation.Utilities
{
    public class CsvUtil
    {
        public static DataTable GetCsvData(string folder, string fileName)
        {
            var csvTable = new DataTable();
            using var csvReader = new CsvReader(new StreamReader(File.OpenRead(folder + fileName)), true);
            csvTable.Load(csvReader);
            return csvTable;
        }
    }
}
