using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    internal class DayClass
    {

        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            long safeTiles = GenerateRows(40);

            Console.WriteLine("Part1: {0}", safeTiles);
        }

        public void Part2()
        {
            long safeTiles = GenerateRows(400000);

            Console.WriteLine("Part2: {0}", safeTiles);
        }

        private string StartRow { get; set; }

        private long GenerateRows(int nRows)
        {
            long nSafeTiles = 0;
            for (int i = 0; i < StartRow.Length; i++)
            {
                nSafeTiles += StartRow[i] == '.' ? 1 : 0;
            }

            int rowWidth = StartRow.Length;
            char[,] rows = new char[nRows, rowWidth + 2];
            rows[0, 0] = '.';
            rows[0, rowWidth + 1] = '.';
            for (int i = 1; i <= rowWidth; i++)
            {
                rows[0, i] = StartRow[i - 1];
            }

            for (int row = 1; row < nRows; row++)
            {
                rows[row, 0] = '.';
                rows[row, rowWidth + 1] = '.';
                for (int col = 1; col <= rowWidth; col++)
                {
                    if (rows[row - 1, col - 1] != rows[row-1, col+1])
                    {
                        rows[row, col] = '^';
                    }
                    else
                    {
                        rows[row, col] = '.';
                        nSafeTiles++;
                    }
                }
            }
            //Dump(rows);
            return nSafeTiles;
        }

        private void Dump(char[,] rows)
        {
            for (int row = 0; row < rows.GetLength(0); row++)
            {
                for (int col = 1; col < rows.GetLength(1)-1; col++)
                {
                    Console.Write(rows[row, col]);
                }
                Console.WriteLine();
            }
        }
        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                StreamReader file = new StreamReader(inputFile);
                StartRow = file.ReadLine();

                file.Close();
            }
        }

    }
}
