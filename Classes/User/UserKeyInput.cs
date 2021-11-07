using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Classes.User
{
    public static class UserKeyInput
    {
        #region Hook
        static Task hookLoopTask;
        static CancellationTokenSource cts = new CancellationTokenSource();
        static bool terminateThread = false;
        /**<summary>
         * End the thread loop, which in theory will make it stop.
         * </summary>
         */
        public static void stopThread() {
            terminateThread = true;

            cts.Cancel();
            cts.Dispose();
            hookLoopTask.Dispose();
        }
        /**
         <summary>
         Method that starts background thread for KeyPress event handling.
         Should be called in the first lines of the main method.
         </summary>
         */
        public static void InstallHook() {
            terminateThread = false;
            hookLoopTask = new Task(() => {
                ProcessConsoleKeys();
            }, cts.Token);
        }

        public static void ProcessConsoleKeys() {
            while (!terminateThread)
                if (Console.KeyAvailable)
                    KeyPress?.Invoke(Console.ReadKey(true));
            Console.Beep(500, 1000);
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
