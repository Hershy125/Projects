using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counting_Valleys
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ValleyCount(6, "DDUUUUDD"));
            Console.ReadKey();
        }

        static int ValleyCount(int steps, string path)
        {
            int seaLevel = 0;
            int valleyCount = 0;

            for (int i =0; i < steps; i++)
            {
                if(path[i] == 'U')
                {
                    seaLevel++;
                }
                else
                {
                    seaLevel--;
                }

                if(path[i] == 'D' && seaLevel == -1)
                {
                    valleyCount++;
                }
            }

            return valleyCount;
        }
    }
}
