using ExcelDataReader;
using Learing_Nunit;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Nunit.Tests.Calculator_Modules_UnitTesting
{
    [TestFixture]
    [AllureNUnit]

    public class Calculator_SubModelTests
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

        /***
         * implement the test case for this module
         * ****/
        [Test]
        public void Subtract_TwoPositiveNumbers_ReturnCorrectValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(10, 5);
            Assert.That(result, Is.EqualTo(5));
        }
        [Test]
        public void Subtract_SmallerNumberFromLarger_ReturnNegativeValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(5, 10);
            Assert.That(result, Is.EqualTo(-5));
        }

        [Test]
        public void Subtract_ZeroFromNumber_ReturnTheSameNumber()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(10, 0);
            Assert.That(result, Is.EqualTo(10));
        }
        [Test]
        public void Subtract_PostiveNumberAndNegativeNumber_ReturnCorrectValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(10, -5);
            Assert.That(result, Is.EqualTo(15)); // 10 - (-5) = 15
        }

        [Test]
        public void Subtract_NumberFromMaxValue_ReturnCorrectValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(int.MaxValue, 100);
            Assert.That(result, Is.EqualTo(int.MaxValue - 100));
        }

        [Test]
        public void Subtract_FromMinValue_ReturnCorrectValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(int.MinValue, 1);
            Assert.That(result, Is.EqualTo(-2147483649));
        }


        [Test]
        public void Subtract_LargeNumbers_EnsureCorrectResult()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(1000000, 500000);
            Assert.That(result, Is.EqualTo(500000));
        }

        [Test]
        public void Subtract_TwoNagativeNumbers_EnsureCorrectResult()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(-1000000, -500000);
            Assert.That(result, Is.EqualTo(-500000));
        }

        [Test]
        public void Subtract_IdenticalNumbers_ReturnZero()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(5, 5);
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void Subtract_NumberFromZero_ReturnNegativeValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(0, 5);
            Assert.That(result, Is.EqualTo(-5));
        }


        [Test]
        public void Subtract_LargeNegativeNumbers_ReturnCorrectValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(-1000000, -500000);
            Assert.That(result, Is.EqualTo(-500000));  // -1000000 - (-500000) = -500000
        }

        [Test]
        public void Subtract_ZeroFromNegativeNumber_ReturnPositiveValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(0, -5);
            Assert.That(result, Is.EqualTo(5));  // 0 - (-5) = 5
        }

        [Test]
        public void Subtract_VeryLargeNumbers_ReturnCorrectValue()
        {
            var cal = new Calculator();
            int result = cal.Sub_number(1000000000, 999999999);
            Assert.That(result, Is.EqualTo(1));  // 1000000000 - 999999999 = 1
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
