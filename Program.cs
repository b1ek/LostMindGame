using System;
using System.Runtime.InteropServices;
using LostMind.Engine.User;
using System.Diagnostics;
using LostMind.Engine.UI;
using LostMind.Engine.Util;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LostMind.Engine.Animation;
using LostMind.Engine.Config;
using System.IO;
using System.Threading;
using LostMind.Game.Game;
using LostMind.Engine.Core;

namespace LostMind
{
    class Program {
        static Core core = new();
        public static Core Core => core;

        /**<summary>
         * Main entry point.
         * </summary>
         */
        static void Main(string[] args) {
            Core.Run();
        }
    }
}
