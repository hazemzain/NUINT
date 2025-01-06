using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learing_Nunit
{
    public class Calculator
    {
        public int Add_Number(int value1, int value2)
        {
            return value1 + value2;
        }
        public int Sub_number(int value1, int value2)
        {
            return value1 - value2;
        }

        public int Multiply_Number(int value1, int value2)
        {
            return value1 * value2;
        }

        public int Divide_Number(int value1, int value2)
        {
            if (value2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return value1 / value2;
        }



    }
}
