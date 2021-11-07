using LostMind.Classes.UI;
using LostMind.Classes.User;
using LostMind.Classes.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.Threading;

namespace LostMind.Classes.GameController
{
    // THIS IS NOT A PART OF A GAME ENGINE
    internal class GameController {
        Viewport titleView;
        int consoleWidth = 0;
        int consoleHeight = 16;
        const int maxXP = 100;
        int xp = 0;
        Locale locale = new();
        public void run() {
            string title = $"[{locale.gameTitle}] | [{xp}/{maxXP}] | [{locale.mainMenu}]";
            string sep = new string('-', title.Length + 2);

            consoleWidth = title.Length + 3;
            consoleHeight = Convert.ToInt32(consoleWidth / 2);
            UCO.TrySetSize(consoleWidth, consoleHeight);

            titleView = new Viewport(0, 0, consoleWidth, 3);
            titleView.AddElement(new UILabel(" " + sep));
            titleView.AddElement(new UILabel("  " + title));
            titleView.AddElement(new UILabel(" " + sep));

            Viewport mainMenu = new Viewport(0, 4, consoleWidth, consoleHeight - titleView.rectHeight);

            mainMenu.marginLeft = 4;

            mainMenu.AddElement(new UIButton("Start", () => { mainMenu.breakMainLoop(); }));
            mainMenu.AddElement(new UIButton("Options"));
            mainMenu.AddElement(new UIButton("Exit game", () => { Program.DoSafeExit(); }));
            mainMenu.DrawElements();
            if (UserKeyInput.isKeyPressed(new ConsoleKey[] { ConsoleKey.Spacebar, ConsoleKey.Enter }))UserKeyInput.awaitKeyPress(new ConsoleKey[] { ConsoleKey.Spacebar, ConsoleKey.Enter });
            mainMenu.mainloop();
            mainMenu.RemoveAllElements();

            mainMenu.AddElement(new UILabel("Enter your nickname:"));
            mainMenu.AddElement(new UITextInput(24));
            mainMenu.AddElement(new UIButton("SUBMIT"));
            mainMenu.mainloop();

            Program.DoSafeExit();
        }
    }
}
