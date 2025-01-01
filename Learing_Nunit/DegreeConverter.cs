using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learing_Nunit
{
    public class DegreeConverter
    {
        public static double ToFahrenheit(double Celsuis)
        {
            return (Celsuis * 9 / 5) + 32;

        }

        public static double ToCelsuis(double Fahrenheit)
        {
            return (Fahrenheit * 32 - 32) * 5 / 9;

        }
    }
}
