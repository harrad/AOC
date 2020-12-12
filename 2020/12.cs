using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        public static double DegToRad(int angle) => (Math.PI / 180) * angle;

        static void Main()
        {
            var text = File.ReadAllLines(@"2020_12_12.txt");

            Question1(text);
            Question2(text);

            Console.ReadKey();
        }

        private static void Question2(string[] text)
        {
            var v = 0;
            var h = 0;

            var wpx = 10;
            var wpy = 1;

            foreach (var i in text)
            {
                var l = i.Substring(0, 1);
                var n = int.Parse(i.Substring(1));

                if (l == "N") wpy += n;
                if (l == "E") wpx += n;
                if (l == "S") wpy -= n;
                if (l == "W") wpx -= n;

                if (l == "R" || l == "L")
                {
                    n = l == "R" ? -n : n;
                    var rad = DegToRad(n);
                    var c = (int)Math.Cos(rad);
                    var s = (int)Math.Sin(rad);
                    var x = wpx * c - wpy * s;
                    var y = wpx * s + wpy * c;

                    wpx = x;
                    wpy = y;
                }

                if (l == "F")
                {
                    h += wpx * n;
                    v += wpy * n;
                }
            }

            Console.WriteLine(Math.Abs(h) + Math.Abs(v));
        }

        private static void Question1(string[] text)
        {
            var v = 0;
            var h = 0;
            var dir = 1;

            foreach (var i in text)
            {
                var l = i.Substring(0, 1);
                var n = int.Parse(i.Substring(1));

                if ((l == "F" && dir == 0) || l == "N") v += n;
                if ((l == "F" && dir == 1) || l == "E") h -= n;
                if ((l == "F" && dir == 2) || l == "S") v -= n;
                if ((l == "F" && dir == 3) || l == "W") h += n;

                if (l == "R" || l == "L")
                {
                    n = l == "R" ? n : -n;
                    dir = (dir + (n / 90) + 4) % 4;
                }
            }

            Console.WriteLine(Math.Abs(h) + Math.Abs(v));
        }
    }
}
