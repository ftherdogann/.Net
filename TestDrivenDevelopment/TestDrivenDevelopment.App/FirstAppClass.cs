using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenDevelopment.Apps
{
    public class FirstAppClass
    {
        public decimal Factorial(int n1)
        {
            if (n1 == 0)
            {
                return 1;
            }
            else
            {
                return n1 * Factorial(n1 - 1);
            }
        }

        public  bool CheckPrime(int num)
        {
            for (int i = 2; i < num; i++)
                if (num % i == 0)
                    return false;
            return true;
        }
    }
}
