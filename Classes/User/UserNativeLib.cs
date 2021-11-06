using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Classes.User {
    public class UserNativeLib {
        /**<summary>Prints string value to specific location.<br/>Takes approximately 0.152 milliseconds to execute on 3.7 GHz clock.</summary>*/
        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern bool printToXY(string value, int x, int y);

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        static extern bool print(string value = "");

        public static void Write(string value) {
            print(value);
        }
        public static void Write(char c) {
            print(new string(c, 1));
        }

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern bool printLn(string value = "");

        public static void WriteLn(string value) {
            print(value + "\n");
        }
        public static void WriteLn(char c) {
            print(new string(c, 1) + "\n");
        }

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern void placeButton(string buttonText, int x, int y);

        [DllImport(@"Resources\Libraries\NativeLib.dll")]
        public static extern void flushConsole();

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
