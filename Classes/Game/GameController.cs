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
        UserConsoleWriter writer = new UserConsoleWriter(0, 0);
        Localization.Localization.Localizations locale = Program.localization;
        string titleString;

        public void startGame() {
            Console.Clear();
            
            Console.CursorVisible = false;
            titleString = $"   [{locale.gameTitle}] | [0/100] | [Main Menu]";
            UserConsoleOutput.SetSize(titleString.Length + 3, 16);
            Console.Title = "LostMind - Main Menu";
            string sepString = "  " + new string('-', titleString.Length-1);
            Console.WriteLine(sepString);
            Console.WriteLine(titleString);
            Console.WriteLine(sepString + "\n");
            

        }
    }
}
