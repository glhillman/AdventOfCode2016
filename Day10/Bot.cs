using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class Bot
    {
        public Bot(int id)
        {
            ID = id;
            Single = -1;
            High = -1;
            Low = -1;
            HighOutput = -1;
            LowOutput = -1;
            HighTarget = -1;
            LowTarget = -1;
            Processed = false;
        }

        public void SetValue(int value)
        {
            if (Single > 0)
            {
                if (value > Single)
                {
                    High = value;
                    Low = Single;
                }
                else
                {
                    High = Single;
                    Low = value;
                }
                Single = -1;
            }
            else
            {
                Single = value;
            }
        }

        public bool HasHighLowValues
        {
            get
            {
                return Single == -1 && High > -1 && Low > -1;
            }
        }

        public int ID { get; private set; }

        public int Single { private get; set; }
        public int High { get; set; }
        public int Low { get; set; }

        public int HighTarget { get; set; }
        public int LowTarget { get; set; }

        public int LowOutput { get; set; }
        public int HighOutput { get; set; }

        public bool Processed { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}, Single: {1}, LowTarget: {2}, HighTarget: {3}, LowValue: {4}, HighValue: {5}, LowOutput: {6}, HighOutput: {7}", ID, Single, LowTarget, HighTarget, Low, High, LowOutput, HighOutput);
        }

    }
}
