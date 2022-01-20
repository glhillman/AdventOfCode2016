using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    internal class Elf
    {
        public Elf(int id)
        {
            Id = id;
            NPresents = 1;
            Prev = this;
            Next = this;
        }

        public int Id { get; private set; }
        public int NPresents { get; set; }
        public Elf Prev { get; set; }
        public Elf Next { get; set; }

        public override string ToString()
        {
            return String.Format("Elf {0}, Presents: {1}, Prev: {2}, Next: {3}", Id, NPresents, Prev.Id, Next.Id);
        }
    }
}
