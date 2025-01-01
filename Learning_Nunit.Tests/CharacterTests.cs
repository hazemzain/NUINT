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
    public class CharacterTests
    {
        private Character _c ;
        [SetUp]
        void SetUp()
        {
            _c = new Character(Business.Type.Elf);
        }


        [Test]
        public void ShoudSetName()
        {
            string expected = "Hazem";
            Character c=new Character(Business.Type.Elf, expected);
            Assert.That(c.Name, Is.EqualTo(expected));
            Assert.That(c.Name,Is.Not.Empty);
            Assert.That(c.Name, Contains.Substring("em"));
        }

        [Test]
        public void ShoudSetNameasUpperCase()
        {
            string expected = "hazem";
            string actual = "HAZEM";
            Character c = new Character(Business.Type.Elf, expected);
            Assert.That(c.Name, Is.EqualTo(actual));
            Assert.That(c.Name, Is.Not.Empty);
            Assert.That(c.Name, Contains.Substring("em"));
        }

        [Test]
        public void NameModule_NULL_ReturnNULL()
        {
            
            Character c = new Character(Business.Type.Elf);
            Assert.That(c.Name, Is.Null);
            
        }


        [Test]
        public void HealthyModel_100_Return100()
        {

            Character c = new Character(Business.Type.Elf);
            int expectedHealthy = 100;
            Assert.That(c.Health, Is.EqualTo(expectedHealthy));

        }

        [Test]
        public void HealthyModel_90_Return100()
        {

            Character c = new Character(Business.Type.Elf);
            int expectedHealthy = 100;
            Assert.That(c.Health, Is.Not.EqualTo(90));

        }
    }
}
