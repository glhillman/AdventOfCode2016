using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day14
{
    internal class DayClass
    {
        public void Part1()
        {
            int nKeys = 0;
            int i = 0;

            //string baseKey = "abc";
            string baseKey = "ngcjuoqr";

            AoCMD5 md5 = new AoCMD5(baseKey, 0);

            while (nKeys < 64)
            {
                ++i;
                string hash = md5.Hash(i);
                char? seq3 = Find3(hash, i);
                if (seq3 != null)
                {
                    bool found5 = false;
                    for (int j = i+1; j < i + 1000 && found5 == false; j++)
                    {
                        string hash2 = md5.Hash(j);
                        if (Find5(hash2, seq3.Value, j))
                        {
                            found5 = true;
                            //Console.WriteLine("key {0}, i,j = {1},{2} hash1 {3}, hash 2 {4} contains 5 of {5}", nKeys+1, i, j, hash, hash2, seq3.Value);
                            if (++nKeys == 64)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Part1: {0}", i);
        }

        public void Part2()
        {
            int nKeys = 0;
            int i = 0;
            //string baseKey = "abc";
            string baseKey = "ngcjuoqr";
            AoCMD5 md5 = new AoCMD5(baseKey, 2016);

            while (nKeys < 64)
            {
                ++i;
                string hash = md5.Hash(i); ;
                char? seq3 = Find3(hash, i);
                if (seq3 != null)
                {
                    bool found5 = false;
                    for (int j = i + 1; j < i + 1000 && found5 == false; j++)
                    {
                        string hash2 = md5.Hash(j);
                        if (Find5(hash2, seq3.Value, j))
                        {
                            found5 = true;
                            //Console.WriteLine("key {0}, i,j = {1},{2} hash1 {3}, hash 2 {4} contains 5 of {5}", nKeys + 1, i, j, hash, hash2, seq3.Value);
                            if (++nKeys == 64)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Part2: {0}", i);
        }

        private char? Find3(string hash, int index)
        {
            char? result = null;

            for (int i = 0; i < hash.Length-2; i++)
            {
                if (hash[i] == hash[i+1] && hash[i] == hash[i+2])
                {
                    result = hash[i];
                    break;
                }
            }

            return result;
        }

        private bool Find5(string hash, char value, int index)
        {
            bool found = false;
            for (int i = 0; i < hash.Length - 4; i++)
            {
                if (hash[i] == value && hash[i + 1] == value && hash[i + 2] == value && hash[i + 3] == value && hash[i + 4] == value)
                {
                    found = true;
                    break;
                }
            }

            return found;
        }
    }
}
