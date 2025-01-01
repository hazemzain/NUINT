using Learing_Nunit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Nunit.Tests
{
    [TestFixture]
    public class DegreeConverterTests
    {
        [Test]
        public void ToFahrenheit_ZeroCelsuis_Return32()
        {
            double result = DegreeConverter.ToFahrenheit(0);

            Assert.That(result, Is.EqualTo(32));

        }

        [Test]
        public void ToCelsuis_1Fahrenheit_Return0()
        {
            double result = DegreeConverter.ToCelsuis(1);

            Assert.That(result, Is.EqualTo(0));

        }
    }
}
