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

        public void printTitle() {
            string sepString = "  " + new string('-', titleString.Length-1);
            UserConsoleOutput.WriteXY(0, 0, sepString);
            UserConsoleOutput.WriteXY(0, 1, titleString);
            UserConsoleOutput.WriteXY(0, 2, sepString);
        }

        public void startGame() {
            Console.Clear();
            
            Console.CursorVisible = false;
            titleString = $"   [{locale.gameTitle}] | [0/100] | [Main Menu]";
            UserConsoleOutput.SetSize(titleString.Length + 3, 16);
            Console.Title = "LostMind - Main Menu";

            printTitle();

            Viewport viewport = new Viewport(0, 4, titleString.Length + 3, 13);
            viewport.AddElement(new UITextInput(16));
            viewport.AddElement(new UIButton("hi!"));
            viewport.mainloop();
        }
    }
}
