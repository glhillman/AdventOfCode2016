using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    class Day08
    {
        List<string> _instructions;
        
        public enum CMD
        {
            RECT,
            ROTCOL,
            ROTROW
        };
        
        public Day08()
        {
            LoadData();
        }

        public void Part1()
        {
            int NCOLS = 50;
            int NROWS = 6;

            long rslt = 0;
            CMD cmd;
            int row;
            int col;
            int count;

            char[,] grid1 = new char[NROWS, NCOLS];
            char[,] grid2 = new char[NROWS, NCOLS];

            for (row = 0; row < NROWS; row++)
            {
                for (col = 0; col < NCOLS; col++)
                {
                    grid1[row, col] = '.';
                }
            }

            for (int inst = 0; inst < _instructions.Count; inst++)
            {
                cmd = ParseInstruction(_instructions[inst], out row, out col, out count);
                switch (cmd)
                {
                    case CMD.RECT:
                        for (int r = 0; r < row; r++)
                        {
                            for (int c = 0; c < col; c++)
                            {
                                grid1[r, c] = '#';
                            }
                        }
                        break;
                    case CMD.ROTCOL:
                        CopyGrid(grid1, grid2);
                        for (int r = 0; r < NROWS; r++)
                        {
                            int newrow = (r + count) % NROWS;
                            grid2[newrow, col] = grid1[r, col];                        
                        }
                        CopyGrid(grid2, grid1);
                        break;
                    case CMD.ROTROW:
                        CopyGrid(grid1, grid2);
                        for (int c = 0; c < NCOLS; c++)
                        {
                            int newcol = (c + count) % NCOLS;
                            grid2[row, newcol] = grid1[row, c];
                        }
                        CopyGrid(grid2, grid1);
                        break;
                }
            }

            
            for (row = 0; row < NROWS; row++)
            {
                for (col = 0; col < NCOLS; col++)
                {
                    rslt += grid1[row, col] == '#' ? 1 : 0;
                }
            }


            Console.WriteLine("Part1: {0}", rslt);

            DumpGrid(grid1, "Part 2");
        }

        private void CopyGrid(char[,] src, char[,] dst)
        {
            for (int i = 0; i < src.GetLength(0); i++)
            {
                for (int j = 0; j < src.GetLength(1); j++)
                {
                    dst[i, j] = src[i, j];
                }
            }
        }


        private void DumpGrid(char[,] grid, string msg)
        {
            Console.WriteLine(msg);
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (col % 5 == 0)
                    {
                        Console.Write("    ");
                    }
                    Console.Write("{0}", grid[row, col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private CMD ParseInstruction(string inst, out int row, out int col, out int count)
        {
            CMD cmd = CMD.RECT;

            row = -1;
            col = -1;
            count = -1;

            if (inst.StartsWith("rect"))
            {
                string[] rc = inst.Substring("rect ".Length).Split('x');
                col = int.Parse(rc[0]);
                row = int.Parse(rc[1]);
                cmd = CMD.RECT;
            }
            else if (inst.Contains("x="))
            {
                string[] colCount = inst.Substring("rotate column x=".Length).Split(' ', 'b', 'y');
                col = int.Parse(colCount[0]);
                count = int.Parse(colCount[4]);
                cmd = CMD.ROTCOL;
            }
            else if (inst.Contains("y="))
            {
                string[] rowCount = inst.Substring("rotate row y=".Length).Split(' ', 'b', 'y');
                row = int.Parse(rowCount[0]);
                count = int.Parse(rowCount[4]);
                cmd = CMD.ROTROW;
            }

            return cmd;
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                _instructions = new List<string>();

                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _instructions.Add(line);
                }

                file.Close();
            }
        }
    }
}
