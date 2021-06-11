using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day05
{
    class Day05
    {
        MD5 _md5;
        public Day05()
        {
            _md5 = System.Security.Cryptography.MD5.Create();
        }

        public void Part1()
        {
            string baseVal = "abbhdwsy";
            StringBuilder sb = new StringBuilder();
            int index = 1;
            while (sb.Length < 8)
            {
                string testVal = baseVal + index.ToString();
                string md5 = CreateMD5(testVal);
                if (md5.StartsWith("00000"))
                {
                    sb.Append(md5[5]);
                    Console.WriteLine("{0}", sb.ToString());
                }
                index++;
            }

            Console.WriteLine("Part1: {0}", sb.ToString());
        }

        public void Part2()
        {
            string baseVal = "abbhdwsy";
            StringBuilder pwd = new StringBuilder();
            pwd.Append("        ");
            int count = 0;
            int index = 1;
            while (count < 8)
            {
                string testVal = baseVal + index.ToString();
                string md5 = CreateMD5(testVal);
                if (md5.StartsWith("00000"))
                {
                    int i = md5[5] - '0';
                    if (i < 8 && pwd[i] == ' ')
                    {
                        pwd[i] = md5[6];
                        count++;
                        Console.WriteLine("Count: {0}, \"{1}\"", count, pwd.ToString());
                    }
                }
                index++;
            }

            Console.WriteLine("Part2: {0}", pwd.ToString());
        }

        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = _md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-","");
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\test.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    // Process the string
                }

                file.Close();
            }
        }

    }
}
