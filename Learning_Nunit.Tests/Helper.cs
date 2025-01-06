using ExcelDataReader;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Nunit.Tests
{
    public class Helper
    {
        public static IEnumerable<TestCaseData> GetTestDataFromExcel()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string filePath = "D:\\Learning_Test_Project\\Learing_Nunit\\Learning_Nunit.Tests\\testdata.xlsx";
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            // Skip header row
            reader.Read();

            while (reader.Read())
            {
                int firstValue = int.Parse(reader.GetValue(0)?.ToString() ?? "0");
                int secondValue = int.Parse(reader.GetValue(1)?.ToString() ?? "0");
                int expectedOutput = int.Parse(reader.GetValue(2)?.ToString() ?? "0");

                yield return new TestCaseData(firstValue, secondValue, expectedOutput);
            }
        }

    }
}
