using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    internal class Disc
    {
        public Disc(int id, int positions, int time, int position)
        {
            ID = id;
            Positions = positions;
            Time = time;
            Steps = position;
            Position = position;
        }

        public int ID { get; private set; }
        public int Positions { get; private set; }
        public int Time { get; set; }
        public int Position { get; set; }
        public int Steps { get; set; }
        public int AdjustTime(int increment)
        {
            Time += increment;
            Steps += increment;
            Position = Steps % Positions;
            return Position;
        }

        public override string ToString()
        {
            return String.Format("ID: {0}, NPos: {1}, Time: {2}, Position: {3}", ID, Positions, Time, Position);
        }
    }
}
