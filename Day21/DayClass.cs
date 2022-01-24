using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    internal class DayClass
    {
        public List<Command> _commands = new();
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            string scrambled = ProcessCommands(_commands, "abcdefgh", false, 0, 1);

            Console.WriteLine("Part1: {0}", scrambled);
        }

        public void Part2()
        {
            //figure out mapping to reverse the rotate based on position. 
            // each index results in a unique landing index
            //for (int pos = 0; pos < 8; pos++)
            //{
            //    int steps = 1 + pos + (pos >= 4 ? 1 : 0);
            //    Console.WriteLine("steps: {0}, startIndex: {1}, stopIndex: {2}", steps, pos, (pos + steps) % 8);
            //}

            string unScrambled = ProcessCommands(_commands, "fbgdceah", true, _commands.Count-1, -1);

            Console.WriteLine("Part2: {0}", unScrambled);
        }

        private string ProcessCommands(List<Command> cmds, string seed, bool reverse, int istart, int istep)
        {
            char[] pwd = seed.ToCharArray();
            int i = istart;
            while (i >= 0 && i < cmds.Count)
            {
                Command cmd = cmds[i];
                switch (cmd.Cmd)
                {
                    case Commands.SwapPosition:
                        if (reverse)
                        {
                            (cmd.X, cmd.Y) = (cmd.Y, cmd.X);
                        }
                        (pwd[cmd.X], pwd[cmd.Y]) = (pwd[cmd.Y], pwd[cmd.X]);
                        break;
                    case Commands.SwapLetters:
                        pwd = new string(pwd).Replace(cmd.XChar, '#').Replace(cmd.YChar, cmd.XChar).Replace('#', cmd.YChar).ToCharArray();
                        break;
                    case Commands.RotateLeft:
                        pwd = Rotate(pwd, cmd.X, reverse ? true : false);
                        break;
                    case Commands.RotateRight:
                        pwd = Rotate(pwd, cmd.X, reverse ? false : true);
                        break;
                    case Commands.RotateBased:
                        int pos = 0;
                        while (pwd[pos] != cmd.XChar)
                        {
                            pos++;
                        }
                        if (reverse)
                        {
                            int[] indexMap = { 7, 0, 4, 1, 5, 2, 6, 3 };
                            int originalIndex = indexMap[pos];
                            if (pos > originalIndex)
                            {
                                pwd = Rotate(pwd, pos - originalIndex, false);
                            }
                            else
                            {
                                pwd = Rotate(pwd, originalIndex - pos, true);
                            }
                        }
                        else
                        {
                            int steps = 1 + pos + (pos >= 4 ? 1 : 0);
                            pwd = Rotate(pwd, steps % pwd.Length, true);
                        }
                        break;
                    case Commands.Reverse:
                        Array.Reverse(pwd, cmd.X, (cmd.Y - cmd.X + 1));
                        break;
                    case Commands.Move:
                        {
                            if (reverse)
                            {
                                (cmd.X, cmd.Y) = (cmd.Y, cmd.X);
                            }
                            string tmp = new string(pwd);
                            string x = tmp.Substring(cmd.X, 1);
                            StringBuilder sb = new StringBuilder(tmp);
                            sb.Remove(cmd.X, 1);
                            sb.Insert(cmd.Y, x);
                            pwd = sb.ToString().ToCharArray();
                        }
                        break;
                }
                i += istep;
            }

            return new string(pwd);
        }

        private char[] Rotate(char[] src, int steps, bool right)
        {
            if (steps > 0)
            {
                char[] dst = new char[src.Length];

                int srcIndex = 0;
                int dstIndex = right ? steps : src.Length - steps;

                for (int i = 0; i < src.Length; i++)
                {
                    dst[dstIndex] = src[srcIndex++];
                    dstIndex = (dstIndex + 1) % src.Length;
                }

                return dst;
            }
            else
                return src;
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
                    int x;
                    int y;
                    char xChar;
                    char yChar;
                    Commands cmd;

                    string[] parts = line.Split(' ');
                    switch (parts[0])
                    {
                        case "swap":
                            if (parts[1] == "position")
                            {
                                x = int.Parse(parts[2]);
                                y = int.Parse(parts[5]);
                                _commands.Add(new Command(Commands.SwapPosition, x, y));
                            }
                            else
                            {
                                xChar = char.Parse(parts[2]);
                                yChar = char.Parse(parts[5]);
                                _commands.Add(new Command(Commands.SwapLetters, xChar, yChar));
                            }
                            break;
                        case "rotate":
                            if (parts[1] == "based")
                            {
                                xChar = char.Parse(parts[6]);
                                _commands.Add(new Command(Commands.RotateBased, xChar));
                            }
                            else
                            {
                                x = int.Parse(parts[2]);
                                cmd = parts[1] == "left" ? Commands.RotateLeft : Commands.RotateRight;
                                _commands.Add(new Command(cmd, x));
                            }
                            break;
                        case "reverse":
                            x = int.Parse(parts[2]);
                            y = int.Parse(parts[4]);
                            _commands.Add(new Command(Commands.Reverse, x, y));
                            break;
                        case "move":
                            x = int.Parse(parts[2]);
                            y = int.Parse(parts[5]);
                            _commands.Add(new Command(Commands.Move, x, y));
                            break;
                        default:
                            throw new ArgumentException("Unknown Command: " + line);
                    }
                }

                file.Close();
            }
        }

    }
}
