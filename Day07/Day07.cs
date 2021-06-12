using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    class Day07
    {
        List<string> _rawData;
        public Day07()
        {
            LoadData();
        }

        public void Part1()
        {

            long rslt = 0;

            foreach (string s in _rawData)
            {
                rslt += SupportsTLS(s);
            }

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {
            long rslt = 0;

            foreach (string s in _rawData)
            {
                rslt += SupportsSSL(s);
            }

            Console.WriteLine("Part2: {0}", rslt);
        }
        private int SupportsTLS(string s)
        {
            bool supportsTLS = false; // assume the best - disqualify below
            bool finished = false;

            int length = s.Length;
            bool insideBrackets = false;
            int index = 0;

            while (!finished && index < length-3)
            {
                switch (s[index])
                {
                    case '[':
                        insideBrackets = true;
                        break;
                    case ']':
                        insideBrackets = false;
                        break;
                    default:
                        if (s[index] == s[index + 3] &&
                            s[index + 1] == s[index + 2] &&
                            s[index] != s[index + 1])
                        {
                            if (insideBrackets)
                            {
                                supportsTLS = false;
                                finished = true;
                                break;
                            }
                            else
                            {
                                supportsTLS = true;
                            }
                        }
                        break;
                }
                index++;
            }

            return supportsTLS ? 1 : 0;
        }

        private int SupportsSSL(string s)
        {
            List<string> abaList = new List<string>();
            List<string> babList = new List<string>();

            bool supportsSSL = false; // assume the best - disqualify below

            int length = s.Length;
            bool insideBrackets = false;
            int index = 0;

            while (index < length - 2)
            {
                switch (s[index])
                {
                    case '[':
                        insideBrackets = true;
                        break;
                    case ']':
                        insideBrackets = false;
                        break;
                    default:
                        if (s[index] == s[index + 2] &&
                            char.IsLetter(s[index+1]) &&
                            s[index] != s[index + 1])
                        {
                            if (insideBrackets)
                            {
                                babList.Add(s.Substring(index, 3));
                            }
                            else
                            {
                                abaList.Add(s.Substring(index, 3));
                            }
                        }
                        break;
                }
                index++;
            }

            foreach (string aba in abaList)
            {
                foreach (string bab in babList)
                {
                    if (aba[0] == bab[1] && aba[1] == bab[0])
                    {
                        supportsSSL = true;
                    }
                }
            }

            return supportsSSL ? 1 : 0;
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
