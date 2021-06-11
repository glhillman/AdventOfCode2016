using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Day02
    {
        List<string> commandSet;

        public Day02()
        {
            LoadData();
        }

        public void Part1()
        {
            char[,] grid = new char[3, 3];
            char[] chars = new char[] {'1', '2','3', '4', '5', '6', '7', '8', '9'};
            int maxX = 2;
            int maxY = 2;
            int index = 0;
            int x = 1;
            int y = 1;

            for (int yy = 0; yy <= maxY; yy++)
            {
                for (int xx = 0; xx <= maxX; xx++)
                {
                    grid[xx, yy] = chars[index++];
                }
            }

            StringBuilder sb = new StringBuilder();

            foreach (string commands in commandSet)
            {
                foreach (char command in commands)
                {
                    switch (command)
                    {
                        case 'U':
                            y = y > 0 ? y - 1 : y;
                            break;
                        case 'D':
                            y = y < maxY ? y + 1 : y;
                            break;
                        case 'L':
                            x = x > 0 ? x - 1 : x;
                            break;
                        case 'R':
                            x = x < maxX ? x + 1 : x;
                            break;
                    }
                }

                sb.Append(grid[x, y]);
            }

            Console.WriteLine("Part1: {0}", sb.ToString());
        }

        public void Part2()
        {
            char[,] grid = new char[5, 5];
            char[] chars = new char[] { ' ', ' ', '1', ' ', ' ', ' ', '2', '3', '4', ' ', '5', '6', '7', '8', '9', ' ', 'A', 'B', 'C', ' ', ' ', ' ', 'D', ' ', ' ' };
            int maxX = 4;
            int maxY = 4;
            int index = 0;
            int x = 0;
            int y = 2;

            for (int yy = 0; yy <= maxY; yy++)
            {
                for (int xx = 0; xx <= maxX; xx++)
                {
                    grid[xx, yy] = chars[index++];
                }
            }

            StringBuilder sb = new StringBuilder();

            foreach (string commands in commandSet)
            {
                foreach (char command in commands)
                {
                    switch (command)
                    {
                        case 'U':
                            if (y > 0 && grid[x,y-1] != ' ')
                            {
                                y--;
                            }
                            break;
                        case 'D':
                            if (y < maxY && grid[x,y+1] != ' ')
                            {
                                y++;
                            }
                            break;
                        case 'L':
                            if (x > 0 && grid[x-1,y] != ' ')
                            {
                                x--;
                            }
                            break;
                        case 'R':
                            if (x < maxX && grid[x+1,y] != ' ')
                            {
                                x++;
                            }
                            break;
                    }
                }

                sb.Append(grid[x, y]);
            }

            Console.WriteLine("Part1: {0}", sb.ToString());
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                commandSet = new List<string>();
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    commandSet.Add(line);
                }

                file.Close();
            }
        }
    }
}
