using Business;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Nunit.Tests
{
    [TestFixture]
    public class SerialPortParserTests
    {
        [Test]
        public void ParsePort_COM1_Return1()
        {
            int result = SerialPortParser.ParsePort("COM1");

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
