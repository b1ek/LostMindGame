using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.UI
{
    /**
     <summary>
        Ui defeault keybinds. They will be changable by user in future.
     </summary>
     */
    public class UISysConfig
    {
        // it should be enum, but i have no goddamn idea how do I define
        // arrays in Enum so its like this
        public static readonly ConsoleKey[] UIMoveUpKey = { ConsoleKey.UpArrow};
        public static readonly ConsoleKey[] UIMoveDownKey = {ConsoleKey.DownArrow};
        public static readonly ConsoleKey[] UIMoveLeftKey = {ConsoleKey.LeftArrow};
        public static readonly ConsoleKey[] UIMoveRightKey = {ConsoleKey.RightArrow};


        public static readonly ConsoleKey[] UIEnterKey = {ConsoleKey.Enter};
    }
}
