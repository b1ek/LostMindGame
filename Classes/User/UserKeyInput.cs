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
        static Thread hookLoopThread;
        public static void installHook() {
            
        }

        public static event EventHandler KeyPress;
        static void procKey(ConsoleKey key) {

        }
    }
}
