using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    internal class DayClass
    {
        List<Command> _commands = new();
        public DayClass()
        {
            LoadData();
        }

        
        public void Part1()
        {
            int[] vars = { 0, 0, 0, 0 };

            Run(_commands, vars);

            Console.WriteLine("Part1: {0}", vars[0]); // a
        }

        public void Part2()
        {
            int[] vars = { 0, 0, 1, 0 };

            Run(_commands, vars);

            Console.WriteLine("Part2: {0}", vars[0]); // a
        }

        public void Run(List<Command> commands, int[] vars)
        {
            int ip = 0;
            int eofIp = commands.Count;

            while (ip < eofIp)
            {
                Command cmd = commands[ip];
                switch (cmd.Cmd)
                {
                    case Cmds.cpy:
                        vars[cmd.Y] = cmd.XIsVar ? vars[cmd.X] : cmd.X;
                        ip++;
                        break;
                    case Cmds.inc:
                        vars[cmd.X]++;
                        ip++;
                        break;
                    case Cmds.dec:
                        vars[cmd.X]--;
                        ip++;
                        break;
                    case Cmds.jnz:
                        if (vars[cmd.X] != 0)
                        {
                            ip += cmd.YIsVar ? vars[cmd.Y] : cmd.Y;
                        }
                        else
                        {
                            ip++;
                        }
                        break;
                }
            }
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

                    string[] parts = line.Split(' ');
                    switch (parts[0])
                    {
                        case "cpy":
                            {
                                (bool xIsVar, int xValue) = Parse(parts[1]);
                                (bool yIsVar, int yValue) = Parse(parts[2]);
                                _commands.Add(new Command(Cmds.cpy, xIsVar, xValue, yIsVar, yValue));
                            }
                            break;
                        case "inc":
                            {
                                (bool xIsVar, int xValue) = Parse(parts[1]);
                                _commands.Add(new Command(Cmds.inc, xIsVar, xValue));
                            }
                            break;
                        case "dec":
                            {
                                (bool xIsVar, int xValue) = Parse(parts[1]);
                                _commands.Add(new Command(Cmds.dec, xIsVar, xValue));
                            }
                            break;
                        case "jnz":
                            {
                                (bool xIsVar, int xValue) = Parse(parts[1]);
                                (bool yIsVar, int yValue) = Parse(parts[2]);
                                _commands.Add(new Command(Cmds.jnz, xIsVar, xValue, yIsVar, yValue));
                            }
                            break;
                    }
                    // Process the string
                }

                file.Close();
            }
        }

        private (bool, int) Parse(string token)
        {
            bool isVar;
            int value;

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
                    value = int.Parse(token);
                    break;
            }

            return (isVar, value);
        }
    }
}
