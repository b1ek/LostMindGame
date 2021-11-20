using LostMind.Engine.UI;
using LostMind.Engine.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.Game {
    public class GamePlace {
        private protected int screenWidth = 0;
        private protected int screenHeight = 0;
        private protected int screenMarginLeft = 0;
        private protected int screenMarginTop = 0;

        private protected int cursorX = 0;
        private protected int cursorY = 0;


        public GamePlace(int width = 5, int height = 10, int marginLeft = 0, int marginTop = 3) {
            screenWidth = width;
            screenHeight = height;
            screenMarginLeft = marginLeft;
            screenMarginTop = marginTop;
        }

        private protected void Write(string text) {
            string[] spl = text.Replace('\n', '\0').Split("\n");
            if (spl.Length > 1) {
                foreach (string value in spl) {
                    UserNativeLib.printToXY(value, cursorX, cursorY);

                }
            }
        }
    }
}
