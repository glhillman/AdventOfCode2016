using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day10
{
    class Day10
    {
        Regex rx;
        List<string> _instructions;
        public Day10()
        {
            rx = new Regex(@"value [\d]+|bot [\d]+|output [\d]+");
            LoadData();
        }

        public void Parts1And2()
        {
            List<Bot> bots = new List<Bot>();
            Dictionary<int, int> outputs = new Dictionary<int, int>();
            int srcBot;
            int lowBot;
            int highBot;
            int lowOut;
            int highOut;
            int value;
            foreach (string instruction in _instructions)
            {
                MatchCollection matches = rx.Matches(instruction);
                if (matches.Count == 2)
                {
                    value = int.Parse(matches[0].Value.Substring("value ".Length));
                    srcBot = int.Parse(matches[1].Value.Substring("bot ".Length));
                    Bot bot = GetOrAddBot(srcBot, bots);
                    bot.SetValue(value);
                }
                else
                {
                    srcBot = int.Parse(matches[0].Value.Substring("bot ".Length));
                    if (matches[1].Value.StartsWith("bot "))
                    {
                        lowBot = int.Parse(matches[1].Value.Substring("bot ".Length));
                        GetOrAddBot(srcBot, bots).LowTarget = lowBot;
                        GetOrAddBot(lowBot, bots);
                    }
                    else
                    {
                        lowOut = int.Parse(matches[1].Value.Substring("output ".Length));
                        GetOrAddBot(srcBot, bots).LowOutput = lowOut;
                    }
                    if (matches[2].Value.StartsWith("bot "))
                    {
                        highBot = int.Parse(matches[2].Value.Substring("bot ".Length));
                        GetOrAddBot(srcBot, bots).HighTarget = highBot;
                        GetOrAddBot(highBot, bots);
                    }
                    else
                    {
                        highOut = int.Parse(matches[2].Value.Substring("output ".Length));
                        GetOrAddBot(srcBot, bots).HighOutput = highOut;
                    }
                }
            }

            ProcessBots(bots, outputs);

            Bot targetBot = bots.FirstOrDefault(b => b.High == 61 && b.Low == 17);

            Console.WriteLine("Part1: {0}", targetBot.ID);
            Console.WriteLine("Parg2: {0}", outputs[0] * outputs[1] * outputs[2]);
        }

        private void ProcessBots(List<Bot> bots, Dictionary<int, int> outputs)
        {
            bool valuesChanged = false;
            do
            {
                valuesChanged = false;
                foreach (Bot bot in bots)
                {
                    if (bot.Processed == false && bot.HasHighLowValues)
                    {
                        if (bot.HighTarget >= 0)
                        {
                            GetOrAddBot(bot.HighTarget, bots).SetValue(bot.High);
                        }
                        else if (bot.HighOutput >= 0)
                        {
                            outputs[bot.HighOutput] = bot.High;
                        }
                        if (bot.LowTarget >= 0)
                        {
                            GetOrAddBot(bot.LowTarget, bots).SetValue(bot.Low);
                        }
                        else if (bot.LowOutput >= 0)
                        {
                            outputs[bot.LowOutput] = bot.Low;
                        }
                        valuesChanged = true;
                        bot.Processed = true;
                    }
                }

            } while (valuesChanged);

        }

        private Bot GetOrAddBot(int id, List<Bot> bots)
        {
            Bot bot = bots.FirstOrDefault(b => b.ID == id);
            if (bot == null)
            {
                bot = new Bot(id);
                bots.Add(bot);
            }

            return bot;
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                _instructions = new List<string>();

                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _instructions.Add(line);
                }

                file.Close();
            }
        }
    }
}
