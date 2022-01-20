using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    internal class DayClass
    {
        public void Part1()
        {
            int elf = WhiteElephantLeft(3014603);

            Console.WriteLine("Part1: {0}", elf);
        }

        public void Part2()
        {
            int elf = WhiteElephantAcross(3014603);

            Console.WriteLine("Part2: {0}", elf);
        }

        private int WhiteElephantLeft(int nElves)
        {

            List<Elf> elves = new();
            Elf currentElf = new Elf(1);
            Elf nextElf;

            elves.Add(currentElf);
            for (int i = 1; i < nElves; i++)
            {
                Elf elf = new Elf(i + 1);
                currentElf.Prev = elf;
                elf.Prev = elves[i - 1];
                elf.Prev.Next = elf;
                elf.Next = elves[0];
                elves.Add(elf);
            }
            nextElf = currentElf.Next;

            while (true)
            {
                currentElf.NPresents += nextElf.NPresents;
                if (currentElf.NPresents == nElves)
                {
                    break;
                }
                nextElf.NPresents = 0;
                currentElf = currentElf.Next.Next;
                while (currentElf.NPresents == 0)
                {
                    currentElf = currentElf.Next;
                }
                nextElf = currentElf.Next;
                while (nextElf.NPresents == 0)
                {
                    nextElf = nextElf.Next;
                }
            }

            return currentElf.Id;
        }

        private int WhiteElephantAcross(int nElves)
        {

            List<Elf> elves = new();
            Elf currentElf = new Elf(1);
            Elf acrossElf = currentElf;
            int acrossIndex = nElves / 2;

            elves.Add(currentElf);
            for (int i = 1; i < nElves; i++)
            {
                Elf elf = new Elf(i + 1);
                currentElf.Prev = elf;
                elf.Prev = elves[i - 1];
                elf.Prev.Next = elf;
                elf.Next = elves[0];
                elves.Add(elf);
                if (i == acrossIndex)
                {
                    acrossElf = elf;
                }
            }

            while (nElves > 1)
            {
                currentElf.NPresents += acrossElf.NPresents;
                acrossElf.Prev.Next = acrossElf.Next;
                acrossElf.Next.Prev = acrossElf.Prev;
                acrossElf = acrossElf.Next;
                if (nElves % 2 == 1)
                {
                    acrossElf = acrossElf.Next;
                }
                currentElf = currentElf.Next;
                nElves--;
            }

            return currentElf.Id;
        }
    }
}
