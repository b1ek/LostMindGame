using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.User
{
    public static class UserKeyInput
    {
        #region isKeyPressed(overloads)
        public static bool isKeyPressed(ConsoleKey key) {
            return Console.ReadKey(true).Key == key;
        }
        public static bool isKeyPressed(ConsoleKey key, bool intercept) {
            return Console.ReadKey(intercept).Key == key;
        }
        public static bool isKeyPressed(char key) {
            return Console.ReadKey(true).KeyChar == key;
        }
        public static bool isKeyPressed(char key, bool intercept) {
            return Console.ReadKey(intercept).KeyChar == key;
        }
        #endregion
        static bool workerStarted = false;
        static Dictionary<ConsoleKey, ulong> keysPressed = new Dictionary<ConsoleKey, ulong>();
        static BackgroundWorker backgroundWorker = new BackgroundWorker();
        public static void startWorker()
        {
            
        }
    }
}
