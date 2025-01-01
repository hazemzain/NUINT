using Learing_Nunit;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Nunit.Tests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        private static StreamWriter? _logFileWriter;
        private Logger? _logger;
        private static string? _logFilePath;

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            _logFilePath = "D:\\Learning_Test_Project\\Learing_Nunit\\Learning_Nunit.Tests\\LOGS\\TEST.txt";//Path.Combine(TestContext.CurrentContext.WorkDirectory, "TEST.txt");
            _logFileWriter = new StreamWriter(_logFilePath, append: true) { AutoFlush = true };
        }

        [SetUp]
        public void SetUp()
        {
            _logger = new Logger("MyApp.Test", InternalTraceLevel.Info, _logFileWriter);
        }

        [TestCase(3, "Fizz")]
        [TestCase(6, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(10, "Buzz")]
        [TestCase(30, "FizzBuzz")]
        [TestCase(60, "FizzBuzz")]
        [TestCase(4, "")]
        public void TestFizaBuzzMehodModel(int InputData, string ExpectedOutput)
        {
            var actualOutput = FizzBuzz.FizzCheck(InputData);
            Assert.That(actualOutput, Is.EqualTo(ExpectedOutput));

            _logger?.Info($"Input: {InputData}, Expected: {ExpectedOutput}, Actual: {actualOutput}");
            _logFileWriter?.Flush(); // Ensure logs are written immediately
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            _logFileWriter?.Dispose();
            Console.WriteLine($"Logs written to: {_logFilePath}");
        }
    }

}