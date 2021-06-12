using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class CharCount
    {
        public CharCount(char c)
        {
            Char = c;
            Count = 0;
        }

        public char Char { get; private set; }
        public int Count { get; set;}
        public override string ToString()
        {
            return string.Format("{0}: {1}", Char, Count);
        }
    }
}
