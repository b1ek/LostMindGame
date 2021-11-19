using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Engine.User {
    public class UserNativeLib {
        /**<summary>Prints string value to specific location.<br/>Takes approximately 0.152 milliseconds to execute on 3.7 GHz clock.</summary>*/
        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern bool printToXY(string value, int x, int y);

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern bool setCurPos(int x, int y);

        public struct COORD {
            public short x;
            public short y;
        }

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern COORD getConsoleCurPos();

        public static int ConsoleCursor_X { get => getConsoleCurPos().x; set { setCurPos(value, getConsoleCurPos().y); } }
        public static int ConsoleCursor_Y { get => getConsoleCurPos().y; set { setCurPos(getConsoleCurPos().x, value); } }

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        static extern bool print_(string value = "");

        public static void Write(string value) {
            print_(value);
        }
        public static void Write(char c) {
            print_(new string(c, 1));
        }

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        static extern bool printLn(string value = "");

        public static void WriteLn(string value) {
            print_(value + "\n");
        }
        public static void WriteLn(char c) {
            print_(new string(c, 1) + "\n");
        }

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern void placeButton(string buttonText, int x, int y);

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern void flushConsole();

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern string readAllFileText(string path);

        [DllImport(@".\Resources\Libraries\NativeLib.dll")]
        public static extern void centerWindow();

        public const uint MB_ABORTRETRYIGNORE = 2;
        public const uint MB_CANCELTRYCONTINUE = 6;
        public const uint MB_HELP = 4000;
        public const uint MB_OK = 0;
        public const uint MB_OKCANCEL = 1;
        public const uint MB_RETRYCANCEL = 5;
        public const uint MB_YESNO = 4;
        public const uint MB_YESNOCANCEL = 3;

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern int displayMessage(string message = "No message", string title = "Message", uint styles = MB_OK);
    }
}
