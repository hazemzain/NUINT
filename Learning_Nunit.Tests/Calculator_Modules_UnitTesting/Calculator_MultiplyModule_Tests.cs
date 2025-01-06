using NUnit.Framework.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Learing_Nunit;

namespace Learning_Nunit.Tests.Calculator_Modules_UnitTesting
{
    public class Calculator_MultiplyModule_Tests
    {
        private static StreamWriter? _logFileWriter;
        private Logger? _logger;
        private static string? _logFilePath;

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            _logFilePath = "D:\\Learning_Test_Project\\Learing_Nunit\\Learning_Nunit.Tests\\LOGS\\TEST.txt";//Path.Combine(TestContext.CurrentContext.WorkDirectory, "TEST.txt");
            _logFileWriter = new StreamWriter(_logFilePath, append: true) { AutoFlush = true };

            _logFileWriter.WriteLine($"[{DateTime.Now}] Test suite started.");
        }
        [SetUp]
        public void TestSetUp()
        {
            // Log the start of a test
            _logFileWriter?.WriteLine($"[{DateTime.Now}] Test started: {TestContext.CurrentContext.Test.Name}");
        }



        /**
         * here implementation  testcases for this module
         */
        [Test]
        public void Multiply_TwoPositiveNumbers_ReturnCorrectValue()
        {
            var cal = new Calculator();
            int result = cal.Multiply_Number(4, 5);
            Assert.That(result, Is.EqualTo(20));
        }
        [Test]
        public void Multiply_ZeroAndNumber_ReturnZero()
        {
            var cal = new Calculator();
            int result = cal.Multiply_Number(0, 100);
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void Multiply_TwoNegativeNumbers_ReturnPositiveValue()
        {
            var cal = new Calculator();
            int result = cal.Multiply_Number(-4, -5);
            Assert.That(result, Is.EqualTo(20)); 
        }
        [Test]
        public void Multiply_LargeNumbers_ReturnCorrectResult()
        {
            var cal = new Calculator();
            int result = cal.Multiply_Number(100000, 1000);
            Assert.That(result, Is.EqualTo(100000000)); 
        }
        [Test]
        public void Multiply_WithIntMaxValue_ReturnCorrectResult()
        {
            var cal = new Calculator();
            int result = cal.Multiply_Number(int.MaxValue, 2);
            Assert.That(result, Is.EqualTo(4294967294)); 
        }

        [Test]
        public void Multiply_WithIntMinValue_ReturnCorrectResult()
        {
            var cal = new Calculator();
            int result = cal.Multiply_Number(int.MinValue, 2);
            Assert.That(result, Is.EqualTo(-4294967294)); 
        }

        [Test]
        public void Multiply_LargeNegativeAndPositiveNumbers_ReturnCorrectValue()
        {
            var cal = new Calculator();
            int result = cal.Multiply_Number(-1000000, 500);
            Assert.That(result, Is.EqualTo(-500000000)); 
        }
        [Test]
        public void Multiply_TwoIdenticalNumbers_ReturnCorrectResult()
        {
            var cal = new Calculator();
            int result = cal.Multiply_Number(7, 7);
            Assert.That(result, Is.EqualTo(49)); // 7 * 7 = 49
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
