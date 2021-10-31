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
        public static readonly ConsoleKey[] UIMoveUpKey = { ConsoleKey.UpArrow, ConsoleKey.W };
        public static readonly ConsoleKey[] UIMoveDownKey = {ConsoleKey.DownArrow, ConsoleKey.S};
        public static readonly ConsoleKey[] UIMoveLeftKey = {ConsoleKey.LeftArrow, ConsoleKey.A};
        public static readonly ConsoleKey[] UIMoveRightKey = {ConsoleKey.RightArrow, ConsoleKey.D};


        public static readonly ConsoleKey[] UIEnterKey = {ConsoleKey.E, ConsoleKey.Enter, ConsoleKey.Spacebar};
    }
}
