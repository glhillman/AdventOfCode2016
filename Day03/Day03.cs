using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day03
{
    class Day03
    {
        List<string> _potentials;

        public Day03()
        {
            LoadData();
        }

        public void Part1()
        {
            int validCount = 0;

            Regex r = new Regex(@"([\d]+)");
            foreach (string potential in _potentials)
            {
                MatchCollection matches = r.Matches(potential);

                int side1 = int.Parse(matches[0].Value);
                int side2 = int.Parse(matches[1].Value);
                int side3 = int.Parse(matches[2].Value);

                validCount += ValidTest(side1, side2, side3);
            }

            Console.WriteLine("Part1: {0}", validCount);
        }

        public void Part2()
        {
            int validCount = 0;
            int index = 0;

            Regex r = new Regex(@"([\d]+)");
            while (index < _potentials.Count)
            { 
                MatchCollection matches1 = r.Matches(_potentials[index++]);
                MatchCollection matches2 = r.Matches(_potentials[index++]);
                MatchCollection matches3 = r.Matches(_potentials[index++]);

                for (int i = 0; i < 3; i++)
                {
                    int side1 = int.Parse(matches1[i].Value);
                    int side2 = int.Parse(matches2[i].Value);
                    int side3 = int.Parse(matches3[i].Value);

                    validCount += ValidTest(side1, side2, side3);
                }
            }

            Console.WriteLine("Part1: {0}", validCount);
        }

        int ValidTest(int side1, int side2, int side3)
        {
            int isValid = 0;

            if (side1 + side2 > side3 &&
                side1 + side3 > side2 &&
                side2 + side3 > side1)
            {
                isValid = 1;
            }

            return isValid;
        }

            private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                _potentials = new List<string>();
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _potentials.Add(line);
                }

                file.Close();
            }
        }
    }
}
