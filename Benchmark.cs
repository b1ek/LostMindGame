using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostMind.Classes.User;

namespace LostMind {
    class Benchmark {
        static void Main(string[] args) {

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var nt = UserNativeLib.readAllFileText(@".\Resources\Logo.txt");
            watch.Stop();

            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            var dotnet = File.ReadAllText(@".\Resources\Logo.txt");
            watch2.Stop();


            UserNativeLib.flushConsole();

            UserNativeLib.WriteLn("Benchmark results: \n");
            UserNativeLib.WriteLn($"Native: TICKS: {watch.ElapsedTicks} MS: {watch.ElapsedMilliseconds}");
            UserNativeLib.WriteLn($".NET: TICKS: {watch2.ElapsedTicks} MS: {watch2.ElapsedMilliseconds}");
            UserNativeLib.WriteLn($"{dotnet}");
        }
    }
}
