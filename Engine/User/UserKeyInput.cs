using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Engine.User
{
    public static class UserKeyInput
    {
        #region Hook
        /**<summary>
         * End the thread loop, which in theory will make it stop.
         * </summary>
         */
        public static void stopThread() {
        }
        /**
         <summary>
         Method that creates background worker for processing console keys.<br/>
         <paramref name="createCancellation"/> Note: if you set this to true, WORKER WILL RUN AUTOMATIALLY.
         </summary>
         */
        public static (BackgroundWorker worker, CancellationTokenSource cancellation)
            CreateWorker(bool createCancellation = true) {
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += (s, e) => { ProcessConsoleKeys(); };
            if (createCancellation) {
                CancellationTokenSource sauce = new CancellationTokenSource();
                work.RunWorkerAsync(sauce.Token);
                return (work, sauce);
            }
            return (work, null);
        }

        public static void IterateLoop() {
            if (Console.KeyAvailable)
                KeyPress?.Invoke(Console.ReadKey(true));
        }

        public static void ProcessConsoleKeys() {
            while (true)
                IterateLoop();
        }

        internal static void CallEvent(ConsoleKeyInfo key)
        {
            KeyPress?.Invoke(key);
        }

        public delegate void KeyPressEvent(ConsoleKeyInfo key);
        /**
         <summary>An event that calls when user pressed a key.</summary>
         */
        public static event KeyPressEvent KeyPress;

        /**
         <summary>not used</summary>
         */
        static void procKey(ConsoleKeyInfo key) {
        }
        #endregion

        public static void awaitKeyPress() {
            while (true) {
                if (Console.KeyAvailable) return;
            }
        }
        public static void awaitKeyPress(ConsoleKey key) {
            while (true) {
                if (Console.KeyAvailable) {
                    if (Console.ReadKey(true).Key == key) return;
                }
            }
        }
        public static void awaitKeyPress(ConsoleKey[] keys) {
            while (true) {
                if (Console.KeyAvailable) {
                    var key = Console.ReadKey(true);
                    foreach (var i in keys) {
                        if (key.Key == i) return;
                    }
                }
            }
        }
        public static bool isKeyPressed(ConsoleKey key)
        {
            if (Console.KeyAvailable)
                return Console.ReadKey(true).Key == key;
            return false;
        }
        public static bool isKeyPressed(ConsoleKey[] keys) {
            bool tru = false;
            if (Console.KeyAvailable) {
                var key = Console.ReadKey(true);
                foreach(var i in keys) {
                    if (i == key.Key) tru = true;
                }
            }
            return tru;
        }
    }
}
