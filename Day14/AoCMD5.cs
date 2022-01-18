using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    internal class AoCMD5
    {
        public AoCMD5(string seed, int stretch = 0)
        {
            Seed = seed;
            Stretch = stretch;

            Md5 = MD5.Create();
            Hashes = new();
        }

        private string Seed { get; set; }
        private int Stretch { get; set; }
        private MD5 Md5 { get; set; }
        Dictionary<int, string> Hashes { get; set; }
        public string Hash(int key)
        {
            string? hash;

            if (Hashes.TryGetValue(key, out hash) == false)
            {
                string fullKey = Seed + key.ToString();
                if (Stretch > 0)
                {
                    hash = CreateStretchedMD5(fullKey);
                }
                else
                {
                    hash = CreateMD5(fullKey);
                }
                Hashes[key] = hash;
            }

            return hash;
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = Md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private string CreateStretchedMD5(string input)
        {
            string key = CreateMD5(input);
            for (int i = 0; i < Stretch; i++)
            {
                key = CreateMD5(key);
            }
            return key;
        }
    }
}
