using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class Day06
    {
        List<string> _rawData;

        public Day06()
        {
            LoadData();
        }

        public void Part1()
        {
            List<List<CharCount>> charCounts = new List<List<CharCount>>();
            
            for (int i=0; i<_rawData[0].Length; i++)
            {
                charCounts.Add(CreateCharCount());
            }

            foreach (string data in _rawData)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    charCounts[i].Find(r => r.Char == data[i]).Count++;
                }
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < charCounts.Count; i++)
            {
                sb.Append(charCounts[i].OrderByDescending(r => r.Count).First().Char);
            }

            Console.WriteLine("Part1: {0}", sb.ToString()); ;
        }

        public void Part2()
        {

            List<List<CharCount>> charCounts = new List<List<CharCount>>();

            for (int i = 0; i < _rawData[0].Length; i++)
            {
                charCounts.Add(CreateCharCount());
            }

            foreach (string data in _rawData)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    charCounts[i].Find(r => r.Char == data[i]).Count++;
                }
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < charCounts.Count; i++)
            {
                var x = charCounts[i].OrderBy(r => r.Count);
                sb.Append(charCounts[i].OrderBy(r => r.Count).First(r => r.Count > 0).Char);
            }

            Console.WriteLine("Part2: {0}", sb.ToString()); ;
        }

        List<CharCount> CreateCharCount()
        {
            List<CharCount> charCount = new List<CharCount>();

            for (char c = 'a'; c <= 'z'; c++)
            {
                charCount.Add(new CharCount(c));
            }

            return charCount;
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                _rawData = new List<string>();

                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _rawData.Add(line);
                }

                file.Close();
            }
        }
    }
}
