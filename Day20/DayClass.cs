using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    internal class DayClass
    {
        record BLRange(long min, long max);
        List<BLRange> _blRanges = new();

        public DayClass()
        {
            LoadData();
            _blRanges = _blRanges.OrderBy(bl => bl.min).ToList();
            
            // consolidate overlapping ranges
            bool changed = false;
            int changeCount = 0;
            do
            {
                changed = false;
                for (int i = 0; i < _blRanges.Count - 1; i++)
                {
                    for (int j = i + 1; j < _blRanges.Count; j++)
                    {
                        if (_blRanges[j].min <= _blRanges[i].max+1 && _blRanges[j].max > _blRanges[i].max)
                        {
                            _blRanges[i] = new BLRange(_blRanges[i].min, _blRanges[j].max);
                            _blRanges.RemoveAt(j--);
                            changed = true;
                            changeCount++;
                        }
                        else if (_blRanges[j].min >= _blRanges[i].min && _blRanges[j].max <= _blRanges[i].max)
                        {
                            _blRanges.RemoveAt(j--); // consumed by previous entry
                            changed = true;
                            changeCount++;
                        }
                    }
                }
            }
            while (changed);
        }

        public void Part1()
        {
            long first = 0;

            for (int i = 0; i < _blRanges.Count-1; i++)
            {
                if (_blRanges[i].max + 1 < _blRanges[i+1].min)
                {
                    first = _blRanges[i].max + 1;
                    break;
                }
            }

            Console.WriteLine("Part1: {0}", first);
        }

        public void Part2()
        {
            long nIps = 0;
            for (int i = 0; i < _blRanges.Count-1; i++)
            {
                long diff = _blRanges[i + 1].min - _blRanges[i].max - 1;
                nIps += diff > 0 ? diff : 0;
            }

            Console.WriteLine("Part2: {0}", nIps);
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    string[] split = line.Split('-');
                    _blRanges.Add(new BLRange(long.Parse(split[0]), long.Parse(split[1])));
                }

                file.Close();
            }
        }

    }
}
