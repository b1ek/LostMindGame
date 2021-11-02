using LostMind.Classes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.GameController
{
    public class GameController
    {
        private UserConsoleWriter writer = new UserConsoleWriter(0, 0);
        private Localization.Localization.Localizations locale = Program.localization;

        public void startGame() {
            Console.Clear();
            
            Console.CursorVisible = true;
            string titleString = $"[{locale.gameTitle}] | [0/100] | [Main Menu]";
            string sepString = new string('-', titleString.Length);
            Console.WriteLine(sepString);
            Console.WriteLine(titleString);
            Console.WriteLine(sepString);
            Console.WriteLine();

        }
    }
}
