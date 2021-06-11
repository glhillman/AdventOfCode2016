using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    class Day04
    {
        List<string> _rooms;
        List<string> _realRooms;

        public Day04()
        {
            LoadData();
        }

        public void Part1()
        {
            int idSum = 0;
            _realRooms = new List<string>();

            List<CharCount> charCounts = new List<CharCount>();
            for (char chr = 'a'; chr <= 'z'; chr++)
            {
                charCounts.Add(new CharCount(chr));
            }

            foreach (string room in _rooms)
            {
                InitCharCounts(charCounts);
                int index = room.LastIndexOf('-');
                string idAndChecksum = room.Substring(index + 1);
                string[] parts = idAndChecksum.Split('[', ']');
                int id = int.Parse(parts[0]);
                string checksum = parts[1];
                string data = room.Substring(0, index);
                data = data.Replace("-", "");

                foreach (char c in data)
                {
                    charCounts.Find(chr => chr.Char == c).Count++;
                }

                CharCount[] ordered = charCounts.OrderByDescending(x => x.Count).ThenBy(x => x.Char).ToArray();

                bool match = true;
                for (int i = 0; i < 5; i++)
                {
                    if (ordered[i].Char != checksum[i])
                    {
                        match = false;
                        break;
                    }
                }
                
                if (match)
                {
                    idSum += id;
                    _realRooms.Add(room);
                }
            }

            Console.WriteLine("Part1: {0}", idSum);
        }

        public void Part2()
        {
            foreach (string room in _realRooms)
            {
                int index = room.LastIndexOf('-');
                string idAndChecksum = room.Substring(index + 1);
                string[] parts = idAndChecksum.Split('[');
                int id = int.Parse(parts[0]);
                string data = room.Substring(0, index);
                data = data.Replace("-", " ");
                
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(TransformChar(data[i], id));
                }

                string plainRoom = sb.ToString();
                if (plainRoom.Contains("north"))
                {
                    Console.WriteLine("{0} - {1}", plainRoom, id);
                    break;
                }
            }
        }

        private char TransformChar(char c, int id)
        {
            char cRtrn;

            if (c == ' ')
            {
                cRtrn = ' ';
            }
            else
            {
                cRtrn = (char)((((c - 'a') + id) % 26) + 'a');
            }

            return cRtrn;
        }

        private void InitCharCounts(List<CharCount> charCounts)
        {
            for (int i = 0; i < charCounts.Count; i++)
            {
                charCounts[i].Count = 0;
            }
        }
        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                _rooms = new List<string>();

                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _rooms.Add(line);
                }

                file.Close();
            }
        }
    }
}
