using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    internal class DayClass
    {
        Dictionary<(int, int), char> _map = new();
        public DayClass()
        {
        }

        public void Part1()
        {
            int xSize = 10;
            int ySize = 7;
            int bias = 10;
            //int xSize = 32;
            //int ySize = 40;
            //int bias = 1350;

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    _map[(x, y)] = ComputeChar(x, y, bias);
                }
            }

            DumpMap(xSize, ySize);

            long rslt = 0;

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {

            long rslt = 0;

            Console.WriteLine("Part2: {0}", rslt);
        }

        private char ComputeChar(int x, int y, int bias)
        {
            return Convert.ToString((x * x + 3 * x + 2 * x * y + y + y * y) + bias, 2).Count(c => c == '1') % 2 == 1 ? '#' : '.';
        }

        private void DumpMap(int xMax, int yMax)
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    Console.Write(_map[(x, y)]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
 
    }
}
