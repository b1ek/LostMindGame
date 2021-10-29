using System;
using System.Threading;
using System.Runtime.InteropServices;
using LostMind.Classes.User;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using LostMind.Classes.Animation;

namespace LostMind
{
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        static void Main(string[] args) {
            var cp = Console.GetCursorPosition();
            Animation anim = Animation.createSimple(File.ReadAllText(@"C:\Users\blek\source\repos\MyLifeGame\Resources\Logo.txt"), cp.Left+16, cp.Top+1, 32);
            anim.run();

            UserConsoleWriter writer = new UserConsoleWriter(Console.CursorLeft, Console.CursorTop);
            writer.fancyWrite("\n1114567547").Wait();

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 3);
        }
    }
}
