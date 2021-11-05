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


    }
}
