using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Challenges
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SockMatch(6, new int[] { 1, 1, 3, 2, 3, 1 }));
            Console.ReadLine();
        }

        static int SockMatch(int socks, int[] colors)
        {

            int pairCount = 0;
            List<int> colorPairs = new List<int>();

            for(int i = 0; i < socks; i++)
            {
                if (!colorPairs.Contains(colors[i]))
                {
                    colorPairs.Add(colors[i]);
                }
                else
                {
                    pairCount++;
                    colorPairs.Remove(colors[i]);
                }
            }

            return pairCount;
        }
    }
}
