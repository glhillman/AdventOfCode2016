using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    internal class DayClass
    {
        public void Part1()
        {
            string data = GenerateData("11100010111110100", 272);
            string chkSum = GenerateCheckSum(data);

            Console.WriteLine("Part1: {0}", chkSum);
        }

        public void Part2()
        {
            string data = GenerateData("11100010111110100", 35651584);
            string chkSum = GenerateCheckSum(data);

            Console.WriteLine("Part2: {0}", chkSum);
        }

        private string GenerateData(string input, int desiredLength)
        {
            int aLen = input.Length;

            char[] a = new char[desiredLength * 2 + 1];
            input.ToCharArray().CopyTo(a, 0);
            while (aLen < desiredLength)
            {
                for (int i = 0, j = aLen-1; i < aLen; i++, j--)
                {
                    a[aLen + i + 1] = a[j] == '1' ? '0' : '1'; // copy & reverse in one step
                }
                a[aLen] = '0'; // set the middle zero
                aLen = aLen * 2 + 1;
            }

            string temp = new string(a);
            return temp.Substring(0, desiredLength);
        }

        private string GenerateCheckSum(string input)
        {
            char[] checkSum = input.ToCharArray();
            int chkIndex = 0;
            int index;
            int chkLen = input.Length;
            do
            {
                for (index = 0, chkIndex = 0; index < chkLen - 1; index += 2, chkIndex++)
                {
                    checkSum[chkIndex] = checkSum[index] == checkSum[index + 1] ? '1' : '0';
                }
                chkLen = chkIndex;
            }
            while (chkLen % 2 == 0);

            string temp = new string(checkSum);
            return temp.Substring(0, chkLen);
        }
    }
}
