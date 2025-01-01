using Learing_Nunit;
using NUnit.Framework;
using NUnit.Framework.Internal;
//using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Learning_Nunit.Tests
{
    [TestFixture]
    public class CalculatorTests
    {


        [Test]
        public void Add_TwoValidNumber_ReturnCorrectValue()
        {
            var Cal = new Calculator();
            int result = Cal.Add_Number(1,2);
            Assert.That(result, Is.EqualTo(3));
        }
    }

    public class Calculator
    {
       
           public int Add_Number(int value1,int value2) { 
                return value1 + value2;
            }
        
    }
}
