using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.UI {
    public class NativeUI {
        public enum MessageBoxButton {
            FAIL,
            OK,
            CANCEL,
            ABORT,
            RETRY,
            IGNORE,
            YES,
            NO,
            TRY_AGAIN = 10,
            CONTINUE
        }
        public static MessageBoxButton messageBox(string title = "Message", string message = "No message") => (MessageBoxButton) User.UserNativeLib.displayMessage(message, title);
    }
}
