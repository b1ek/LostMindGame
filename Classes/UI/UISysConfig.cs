using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    public class UISysConfig
    {
        public static readonly (ConsoleKey, ConsoleKey) UIMoveUpKey = (ConsoleKey.UpArrow, ConsoleKey.W);
        public static readonly (ConsoleKey, ConsoleKey) UIMoveDownKey = (ConsoleKey.DownArrow, ConsoleKey.S);
        public static readonly (ConsoleKey, ConsoleKey) UIMoveLeftKey = (ConsoleKey.LeftArrow, ConsoleKey.A);
        public static readonly (ConsoleKey, ConsoleKey) UIMoveRightKey = (ConsoleKey.RightArrow, ConsoleKey.D);


        public static readonly (ConsoleKey, ConsoleKey, ConsoleKey) UIEnterKey = (ConsoleKey.E, ConsoleKey.Enter, ConsoleKey.Spacebar);
    }
}
