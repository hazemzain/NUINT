using ExcelDataReader;
using Learing_Nunit;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;
//using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Nunit.Tests.Calculator_Modules_UnitTesting
{
    [TestFixture]
    [AllureNUnit]
    public class CalculatorTests
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


        [Test]
        public void Add_TwoValidNumber_ReturnCorrectValue()
        {
            var Cal = new Calculator();
            int result = Cal.Add_Number(1, 2);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Add_TwoNagativeNumber_ReturnCorrectNagativeValue()
        {
            var Cal = new Calculator();
            int result = Cal.Add_Number(-1, -2);
            Assert.That(result, Is.EqualTo(-3));
        }

        // [Test]
        [TestCase(-1, 5, 4)] //positive scenario
        [TestCase(1, -5, -4)]//positive scenario
        [TestCase(1, -5, -3)]//nagative scenario
        //to improve this test case try to read test data from exel 

        public void Add_NagativeNumberAndPositiveNumber_ReturnCorrectValue(int FristValue, int SecondValue, int expectedoutput)
        {
            var Cal = new Calculator();
            int result = Cal.Add_Number(FristValue, SecondValue);
            Assert.That(result, Is.EqualTo(expectedoutput));
        }
        // read test data from exel sheet and path it to testcase
        [Test, TestCaseSource(nameof(GetTestDataFromExcel))]
        public void Add_NagativeNumberAndPositiveNumber_ReturnCorrectValue_WithTDD(int firstValue, int secondValue, int expectedOutput)
        {
            var calculator = new Calculator();
            int result = calculator.Add_Number(firstValue, secondValue);
            Assert.That(result, Is.EqualTo(expectedOutput));
        }
        [Test]
        public void Add_twozero_Return0()
        {
            var Cal = new Calculator();
            int result = Cal.Add_Number(0, 0);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Add_zeroAndOtherValue_ReturnOtherValue()
        {
            var Cal = new Calculator();
            int result = Cal.Add_Number(0, 100);
            Assert.That(result, Is.EqualTo(100));
        }
        //nagative scenarios
        //Adding two large numbers causes an integer overflow
        [Test]
        public void Add_Addingtwolargenumberscausesanintegeroverflow_ReturnException()
        {
            var Cal = new Calculator();
            int result = Cal.Add_Number(int.MaxValue, 100);
            Assert.That(result, Is.EqualTo(2147483747));
        }

        [Test]
        public void Add_TwoLargeNegativeNumbers_ReturnCorrectValue()
        {
            var Cal = new Calculator();
            int result = Cal.Add_Number(int.MinValue + 1, -100);
            Assert.That(result, Is.EqualTo(-2147483747));
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
