using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day09
{
    class Day09
    {
        public string _data;
        public Regex rx = new Regex(@"([\d]+)");

        public Day09()
        {
            LoadData();
        }

        public void Part1()
        {
            int index = 0;
            int runLength;
            int runCount;
            int expandedLen = 0;

            while (index < _data.Length)
            {
                switch (_data[index])
                {
                    case '(':
                        int factorLen = (_data.IndexOf(')', index + 1) - index) + 1;
                        MatchCollection factors = rx.Matches(_data.Substring(index, factorLen));
                        runLength = int.Parse(factors[0].Value);
                        runCount = int.Parse(factors[1].Value);
                        index += factorLen;
                        expandedLen += (runLength * runCount);
                        index += runLength;
                        break;
                    default:
                        expandedLen++;
                        index++;
                        break;
                }
            }

            Console.WriteLine("Part1: {0}", expandedLen);
        }

        public void Part2()
        {

            long rslt = Expand(_data, 0, _data.Length);

            Console.WriteLine("Part2: {0}", rslt);
        }

        private long Expand(string data, int anchor, long len)
        {
            long subLen = 0;
            int index = 0;
            long runLength;
            long runCount;

            while (index < len)
            {
                switch (data[anchor+index])
                {
                    case '(':
                        int factorLen = (_data.IndexOf(')', anchor + index + 1) - (anchor + index)) + 1;
                        MatchCollection factors = rx.Matches(_data.Substring(anchor + index, factorLen));
                        runLength = int.Parse(factors[0].Value);
                        runCount = int.Parse(factors[1].Value);
                        index += factorLen;
                        long subStringLen = Expand(data, anchor + index, runLength);
                        index += (int) runLength;
                        subLen += subStringLen * runCount;   
                        break;
                    default:
                        subLen++;
                        index++;
                        break;
                }
            }
            return subLen;
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                StreamReader file = new StreamReader(inputFile);
                _data = file.ReadLine();
                file.Close();
            }
        }
    }
}
