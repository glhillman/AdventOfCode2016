using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    enum Cmds
    {
        cpy,
        inc,
        dec,
        jnz
    };

    internal class Command
    {
        public Command(Cmds cmd, bool xIsVar, int x = 0, bool yIsVar = false, int y = 0)
        {
            // depending on the cmd, x or y may be constants or indexes into the variables array [0..3] = [a, b, c, d]
            Cmd = cmd;
            XIsVar = xIsVar;
            X = x;
            YIsVar = yIsVar;
            Y = y;
        }

        public Cmds Cmd { get; private set; }
        public bool XIsVar { get; private set; }
        public int X { get; private set; }
        public bool YIsVar { get; private set; }
        public int Y { get; private set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4}", Cmd.ToString(), XIsVar ? "var" : "const", X, YIsVar ? "var" : "const", Y);
        }
    }
}
