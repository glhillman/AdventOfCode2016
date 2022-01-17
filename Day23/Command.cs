using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    enum Cmds
    {
        cpy,
        inc,
        dec,
        jnz,
        tgl,
        mul,
        nop
    };

    internal class Command
    {
        public Command(Cmds cmd, bool xIsVar, long x = 0, bool yIsVar = false, long y = 0, bool zIsVar = false, long z = 0)
        {
            // depending on the cmd, x or y may be constants or indexes into the variables array [0..3] = [a, b, c, d]
            Cmd = cmd;
            XIsVar = xIsVar;
            X = x;
            YIsVar = yIsVar;
            Y = y;
            ZIsVar = zIsVar;
            Z = z;
        }

        public Cmds Cmd { get; set; }
        public bool XIsVar { get; private set; }
        public long X { get; private set; }
        public bool YIsVar { get; private set; }
        public long Y { get; private set; }
        public bool ZIsVar { get; private set; }
        public long Z { get; private set; } 

        public override string ToString()
        {

            string x = XIsVar ? ((char)('a' + X)).ToString() : X.ToString();
            string toString;
            string y;

            switch (Cmd)
            {
                case Cmds.inc:
                case Cmds.dec:
                case Cmds.tgl:
                    toString = string.Format("{0} {1}", Cmd.ToString(), x);
                    break;
                case Cmds.mul:
                    y = YIsVar ? ((char)('a' + Y)).ToString() : Y.ToString();
                    string z = ZIsVar ? ((char)('a' + Z)).ToString() : Z.ToString();
                    toString = string.Format("{0} {1} {2} {3}", Cmd.ToString(), x, y, z);
                    break;
                case Cmds.nop:
                    toString = "nop";
                    break;
                default:
                    y = YIsVar ? ((char)('a' + Y)).ToString() : Y.ToString();
                    toString = string.Format("{0} {1} {2}", Cmd.ToString(), x, y);
                    break;

            }

            return toString;
        }
    }
}
