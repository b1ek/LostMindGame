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
        static Thread hookLoopThread;
        static bool terminateThread = false;
        public static void stopThread() {
            terminateThread = true;
        }
        /**
         <summary>
         Method that starts background thread for KeyPress event handling.
         Should be called in the first lines of the main method.
         </summary>
         */
        public static void installHook() {
            terminateThread = false;
            hookLoopThread = new Thread( () => {
                while (true) {
                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                        KeyPress?.Invoke(consoleKeyInfo);
                    }
                    if (terminateThread) return;
                }
            });
            hookLoopThread.Start();
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

        public static void awaitKeyPress(ConsoleKey key) {
            while (true) {
                if (Console.KeyAvailable) {
                    if (Console.ReadKey(true).Key == key) return;
                }
            }
        }
        public static void awaitKeyPress() {
            while (true) {
                if (Console.KeyAvailable) return;
            }
        }
        public static bool isKeyPressed(ConsoleKey key)
        {
            if (Console.KeyAvailable)
                return Console.ReadKey(true).Key == key;
            return false;
        }
    }
}
