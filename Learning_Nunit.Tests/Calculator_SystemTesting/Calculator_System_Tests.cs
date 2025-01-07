using ExcelDataReader;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Nunit.Tests.Calculator_SystemTesting
{
    [TestFixture]
    [AllureNUnit]

    public class Calculator_System_Tests
    {

        private static StreamWriter? _logFileWriter;
        private Logger? _logger;
        private static string? _logFilePath;

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            _logFilePath = "D:\\Learning_Test_Project\\Learing_Nunit\\Learning_Nunit.Tests\\LOGS\\TEST.txt";
            _logFileWriter = new StreamWriter(_logFilePath, append: true) { AutoFlush = true };

            _logFileWriter.WriteLine($"[{DateTime.Now}] Test suite started.");
        }
        [SetUp]
        public void TestSetUp()
        {
            // Log the start of a test
            _logFileWriter?.WriteLine($"[{DateTime.Now}] Test started: {TestContext.CurrentContext.Test.Name}");
        }
        public static IEnumerable<object[]> TestData
        {
            get
            {
                foreach (var testCase in ReadTestDataFromExcel())
                {
                    yield return new object[] { testCase.InputExpression, testCase.ExpectedOutput };
                }
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void MainGuiCalculator_WithValidExpression_ReturnsCorrectResult(string input, float expectedResult)
        {
           
            int result = Learing_Nunit.Program.MainGuiCalculator(input);


            Assert.That(result, Is.EqualTo(expectedResult)); ; 
        }



        [TearDown]
        public void TestTearDown()
        {
            // Log the result of a test
            var result = TestContext.CurrentContext.Result.Outcome.Status;
            _logFileWriter?.WriteLine($"[{DateTime.Now}] Test finished: {TestContext.CurrentContext.Test.Name} - Result: {result}");

            // Log any failure message or exception
            if (result != NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                _logFileWriter?.WriteLine($"[{DateTime.Now}] Failure Details: {TestContext.CurrentContext.Result.Message}");
                if (TestContext.CurrentContext.Result.StackTrace != null)
                {
                    _logFileWriter?.WriteLine($"[{DateTime.Now}] Stack Trace: {TestContext.CurrentContext.Result.StackTrace}");
                }
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            // Log the end of the test suite
            _logFileWriter?.WriteLine($"[{DateTime.Now}] Test suite finished.");
            _logFileWriter?.Dispose();
        }

        public static List<TestData> ReadTestDataFromExcel()
        {
            string filePath = "D:\\Learning_Test_Project\\Learing_Nunit\\Learning_Nunit.Tests\\SystemTestData.xlsx";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<TestData> testData = new List<TestData>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet();
                    var table = dataSet.Tables[0]; // Assuming your data is in the first sheet

                    foreach (DataRow row in table.Rows)
                    {
                        // Assuming the first column contains the input expression and the second column contains the expected output
                        string inputExpression = row[0]?.ToString().Trim();
                        if (int.TryParse(row[1]?.ToString().Trim(), out int expectedOutput))
                        {
                            testData.Add(new TestData
                            {
                                InputExpression = inputExpression,
                                ExpectedOutput = expectedOutput
                            });
                        }
                    }
                }
            }

            return testData;
        }

    }
}
