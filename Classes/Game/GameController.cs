using LostMind.Classes.UI;
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
            
            Console.CursorVisible = false;
            Console.Title = "LostMind - Main Menu";
            string titleString = $"   [{locale.gameTitle}] | [0/100] | [Main Menu]";
            string sepString = "  " + new string('-', titleString.Length-1);
            Console.WriteLine(sepString);
            Console.WriteLine(titleString);
            Console.WriteLine(sepString + "\n");
            UserConsoleOutput.SetSize(titleString.Length + 3, 16);
            Viewport viewport = new Viewport(0, 4, titleString.Length + 3, 12);
            viewport.Paint(ConsoleColor.DarkBlue, ConsoleColor.White);
        }
    }
}
