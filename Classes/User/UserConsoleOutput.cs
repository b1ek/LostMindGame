using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.User
{
    public static class UserConsoleOutput
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        public static async Task beep(int freq, int duration) {
            Beep(freq, duration);
            await Task.Delay(0);
        }

        public static async Task WriteFancy(string val) {
            var cv = val.ToCharArray();
            foreach (var c in cv) {
                await Task.Delay(Util.RandomGen.getInt(1, 2));
                await beep(800, 1);
                Console.Write(c);
            }
            Console.Write("\n");
        }

        public static void WriteXY(int x, int y, string val) {
            var op = Console.GetCursorPosition();
            Console.SetCursorPosition(x, y);
            Console.Write(val);
            Console.SetCursorPosition(op.Left, op.Top);
        }

        public static void WriteLineXY(int x, int y, string val) {
            var op = Console.GetCursorPosition();
            Console.SetCursorPosition(x, y);
            Console.WriteLine(val);
            Console.SetCursorPosition(op.Left, op.Top);
        }
    }
}
