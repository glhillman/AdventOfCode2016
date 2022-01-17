using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    internal class DayClass
    {
        List<Command> _commands;

        public void Part1()
        {
            LoadData();

            long[] vars = { 7, 0, 0, 0 };

            Run(_commands, vars);

            Console.WriteLine("Part1: {0}", vars[0]); // a
        }

        public void Part2()
        {
            LoadData();
            for (int i = 2; i <= 8; i++)
            {
                _commands[i].Cmd = Cmds.nop;
            }
            _commands[9] = new Command(Cmds.mul, true, 0, true, 1, true, 0);

            long[] vars = { 12, 0, 0, 0 };

            Run(_commands, vars);

            Console.WriteLine("Part2: {0}", vars[0]); // a
        }

        public void Run(List<Command> commands, long[] vars)
        {
            int ip = 0;
            int eofIp = commands.Count;

            while (ip < eofIp)
            {
                Command cmd = commands[ip];
                switch (cmd.Cmd)
                {
                    case Cmds.cpy:
                        if (cmd.YIsVar)
                        {
                            vars[cmd.Y] = cmd.XIsVar ? vars[cmd.X] : cmd.X;
                        }
                        ip++;
                        break;
                    case Cmds.inc:
                        if (cmd.XIsVar)
                        {
                            vars[cmd.X]++;
                        }
                        ip++;
                        break;
                    case Cmds.dec:
                        if (cmd.XIsVar)
                        {
                            vars[cmd.X]--;
                        }
                        ip++;
                        break;
                    case Cmds.mul: // this one uses variable for all three arguments
                        vars[cmd.Z] = vars[cmd.X] * vars[cmd.Y];
                        ip++;
                        break;
                    case Cmds.nop:
                        ip++;
                        break;
                    case Cmds.jnz:
                        if ((cmd.XIsVar ? vars[cmd.X] : cmd.X) != 0)
                        {
                            int tmpIp = ip + (int)(cmd.YIsVar ? vars[cmd.Y] : cmd.Y);
                            ip = (tmpIp >= 0 && tmpIp < eofIp) ? tmpIp : ip + 1;
                        }
                        else
                        {
                            ip++;
                        }
                        break;
                    case Cmds.tgl:
                        {
                            int ipOffset = (int)(ip + (cmd.XIsVar ? vars[cmd.X] : cmd.X));
                            if (ipOffset < eofIp)
                            {
                                switch (commands[ipOffset].Cmd)
                                {
                                    case Cmds.inc:
                                        commands[ipOffset].Cmd = Cmds.dec;
                                        break;
                                    case Cmds.dec:
                                        commands[ipOffset].Cmd = Cmds.inc;
                                        break;
                                    case Cmds.cpy:
                                        commands[ipOffset].Cmd = Cmds.jnz;
                                        break;
                                    case Cmds.jnz:
                                        commands[ipOffset].Cmd = Cmds.cpy;
                                        break;
                                    case Cmds.tgl:
                                        commands[ipOffset].Cmd = Cmds.inc;
                                        break;
                                }
                            }
                            ip++;
                        }
                        break;
                }
            }
        }

        private (bool, long) Parse(string token)
        {
            bool isVar;
            long value;

            switch (token)
            {
                case "a":
                case "b":
                case "c":
                case "d":
                    isVar = true;
                    value = token[0] - 'a';
                    break;
                default:
                    isVar = false;
                    value = long.Parse(token);
                    break;
            }

            return (isVar, value);
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                _commands = new();

                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    switch (parts[0])
                    {
                        case "cpy":
                            {
                                (bool xIsVar, long xValue) = Parse(parts[1]);
                                (bool yIsVar, long yValue) = Parse(parts[2]);
                                _commands.Add(new Command(Cmds.cpy, xIsVar, xValue, yIsVar, yValue));
                            }
                            break;
                        case "inc":
                            {
                                (bool xIsVar, long xValue) = Parse(parts[1]);
                                _commands.Add(new Command(Cmds.inc, xIsVar, xValue));
                            }
                            break;
                        case "dec":
                            {
                                (bool xIsVar, long xValue) = Parse(parts[1]);
                                _commands.Add(new Command(Cmds.dec, xIsVar, xValue));
                            }
                            break;
                        case "jnz":
                            {
                                (bool xIsVar, long xValue) = Parse(parts[1]);
                                (bool yIsVar, long yValue) = Parse(parts[2]);
                                _commands.Add(new Command(Cmds.jnz, xIsVar, xValue, yIsVar, yValue));
                            }
                            break;
                        case "tgl":
                            {
                                (bool xIsVar, long xValue) = Parse(parts[1]);
                                _commands.Add(new Command(Cmds.tgl, xIsVar, xValue));
                            }
                            break;
                    }
                }

                file.Close();
            }
        }

    }
}
