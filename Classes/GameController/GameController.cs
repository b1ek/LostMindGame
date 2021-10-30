using LostMind.Classes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.GameController
{
    class GameController
    {
        private UserConsoleWriter writer;
        public void startGame() {
            Console.Clear();
            writer = new UserConsoleWriter(3, 1);
            writer.write("Game started");
            UserConsoleOutput.WriteXY(0, 0, "          [LOST MIND]", ConsoleColor.White, ConsoleColor.Black);
        }
    }
}
