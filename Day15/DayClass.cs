using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day15
{
    internal class DayClass
    {
        public void Part1()
        {
            List<Disc> discs = LoadData();

            int startTime = Align(discs);

            Console.WriteLine("Part1: {0}", startTime);
        }

        public void Part2()
        {
            List<Disc> discs = LoadData();
            discs.Add(new Disc(discs.Count+1, 11, 0, 0));

            int startTime = Align(discs);

            Console.WriteLine("Part2: {0}", startTime);
        }

        private int Align(List<Disc> discs)
        {
            // get them all 1 second apart
            for (int i = 0; i < discs.Count; i++)
            {
                discs[i].AdjustTime(i + 1);
            }

            // get the disc with greatest Positions value at position 0, then we can use its Positions value to step
            Disc disc = discs.First(c => c.Positions == discs.Max(c => c.Positions));
            int step = disc.Positions - disc.Position;
            for (int i = 0; i < discs.Count; i++)
            {
                discs[i].AdjustTime(step);
            }

            step = disc.Positions;

            int steps = 1;
            int sum;
            do
            {
                steps++;
                sum = 0;
                for (int i = 0; i < discs.Count; i++)
                {
                    sum += discs[i].AdjustTime(step);
                }
            }
            while (sum != 0);

            return discs[0].Time - 1;
        }
        private List<Disc> LoadData()
        {
            List<Disc> discs = new();

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    MatchCollection matches = Regex.Matches(line, "[0-9]+");
                    discs.Add(new Disc(int.Parse(matches[0].Value), int.Parse(matches[1].Value), int.Parse(matches[2].Value), int.Parse(matches[3].Value)));
                }

                file.Close();
            }

            return discs;
        }

    }
}
