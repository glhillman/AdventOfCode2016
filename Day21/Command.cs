using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    public enum Commands
    {
        SwapPosition,
        SwapLetters,
        RotateLeft,
        RotateRight,
        RotateBased,
        Reverse,
        Move
    }

    internal class Command
    {
        public Command(Commands cmd, int x, int y = 0)
        {
            Cmd = cmd;
            X = x;
            Y = y;
            XChar = '*';
            YChar = '*';
        }

        public Command(Commands cmd, char xChar, char yChar = '*')
        {
            Cmd = cmd;
            X = 0;
            Y = 0;
            XChar = xChar;
            YChar = yChar;
        }
        public Commands Cmd { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public char XChar { get; set; }
        public char YChar { get; set; }

        public override string ToString()
        {
            string toString;

            switch (Cmd)
            {
                case Commands.SwapPosition:
                case Commands.Reverse:
                case Commands.Move:
                    toString = string.Format("{0} X: {1}, Y: {2}", Cmd, X, Y);
                    break;
                case Commands.SwapLetters:
                    toString = string.Format("{0} x: {1}, y: {2}", Cmd, XChar, YChar);
                    break;
                case Commands.RotateLeft:
                case Commands.RotateRight:
                    toString = String.Format("{0} X: {1}", Cmd, X);
                    break;
                case Commands.RotateBased:
                    toString = String.Format("{0}, x: {1}", Cmd, XChar);
                    break;
                default:
                    toString = "Unknown!";
                    break;
            }

            return toString;
        }
    }
}
