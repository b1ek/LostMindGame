using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// remove unreachable error
#pragma warning disable CS0162

namespace LostMind.Classes.Util {
    static class MachineTest {
        public class PcBrokenException : Exception {
            public PcBrokenException() : base("Your .NET runtime is broken and cannot held basic math/logic operations.\nPlease update your software.") {

            }
        }

        /**<summary>Checks if all math/logic operations are valid<br/>
         * If it will be at least one mistake, throws PcBrokenException.
         * </summary>
         */
        public static void runDefaultNotBrokenTest() {
            int wrongs = 0;
            if (1 + 1 != 2) wrongs++;
            if (4 / 2 != 2) wrongs++;
            if (2 * 2 != 4) wrongs++;
            if (Math.Pow(2, 2) != 4) wrongs++;
            if (1 > 2) wrongs++;
            if (2 < 1) wrongs++;
            if (1 == 2) wrongs++;
            if (false) wrongs++;
            if (!true | false) wrongs++;
            if (false && true) wrongs++;

            if (wrongs != 0 | wrongs > 0) throw new PcBrokenException();
        }
    }
}
