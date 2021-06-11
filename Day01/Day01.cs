using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Day01
    {
        int X;
        int Y;
        char Compass;

        List<string> Steps;

        public Day01()
        {
            LoadData();
        }

        public void Part1()
        {
            X = 0;
            Y = 0;
            Compass = 'N';

            foreach (string step in Steps)
            {
                char direction = step[0];
                int count = int.Parse(step.Substring(1));
                switch (Compass)
                {
                    case 'N':
                        Compass = direction == 'L' ? 'W' : 'E';
                        break;
                    case 'S':
                        Compass = direction == 'L' ? 'E' : 'W';
                        break;
                    case 'E':
                        Compass = direction == 'L' ? 'N' : 'S';
                        break;
                    case 'W':
                        Compass = direction == 'L' ? 'S' : 'N';
                        break;
                }
                switch (Compass)
                {
                    case 'N':
                        Y += count;
                        break;
                    case 'S':
                        Y -= count;
                        break;
                    case 'E':
                        X += count;
                        break;
                    case 'W':
                        X -= count;
                        break;
                }
            }

            long rslt = Math.Abs(X) + Math.Abs(Y);

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {
            HashSet<string> points = new HashSet<string>();
            X = 0;
            Y = 0;
            Compass = 'N';
            bool finished = false;

            foreach (string step in Steps)
            {
                char direction = step[0];
                int count = int.Parse(step.Substring(1));
                switch (Compass)
                {
                    case 'N':
                        Compass = direction == 'L' ? 'W' : 'E';
                        break;
                    case 'S':
                        Compass = direction == 'L' ? 'E' : 'W';
                        break;
                    case 'E':
                        Compass = direction == 'L' ? 'N' : 'S';
                        break;
                    case 'W':
                        Compass = direction == 'L' ? 'S' : 'N';
                        break;
                }
                for (int i = 0; i < count; i++)
                { 
                    switch (Compass)
                    {
                        case 'N':
                            Y++;
                            break;
                        case 'S':
                            Y--;
                            break;
                        case 'E':
                            X++;
                            break;
                        case 'W':
                            X--;
                            break;
                    }

                    string xy = string.Format("{0}:{1}", X, Y);
                    if (points.Contains(xy))
                    {
                        finished = true;
                        break;
                    }
                    else
                    {
                        points.Add(xy);
                    }
                }
                if (finished)
                {
                    break;
                }
            }

            long rslt = Math.Abs(X) + Math.Abs(Y);

            Console.WriteLine("Part2: {0}", rslt);
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            Steps = new List<string>();

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                line = file.ReadLine();
                string[] steps = line.Split(',');
                {
                    foreach (string step in steps)
                    {
                        Steps.Add(step.Trim());
                    }    
                }

                file.Close();
            }
        }
    }
}
