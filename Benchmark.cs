using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostMind.Classes.User;

namespace LostMind {
    class Benchmark {
        static void Main(string[] args) {

            Stopwatch watch = new Stopwatch();
            watch.Start();
            UserNativeLib.flushConsole();
            watch.Stop();

            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            Console.Clear();
            watch2.Stop();


            UserNativeLib.flushConsole();

            UserNativeLib.printLn("Benchmark results: \n");
            UserNativeLib.printLn($"Native: TICKS: {watch.ElapsedTicks} MS: {watch.ElapsedMilliseconds}");
            UserNativeLib.printLn($".NET: TICKS: {watch2.ElapsedTicks} MS: {watch2.ElapsedMilliseconds}");
        }
    }
}
