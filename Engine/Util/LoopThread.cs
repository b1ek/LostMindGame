using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Engine.Util
{
    public class LoopThread
    {
        bool run = true;
        CancellationTokenSource cts = new CancellationTokenSource();

        /**<summary>
         * Wrap the loop into a thread with ability to stop it. <br/>
         * Pretty basic tho, actually its needed to make your <br/>
         * coding faster and easier.<br/>
         * </summary>
         * <param name="start">NO WHILE LOOPS IN HERE!!! JUST THE CODE THAT IS CALLED EVERY LOOP ITERATION!</param>
         */
        public LoopThread(Action update) {
            ThreadPool.QueueUserWorkItem((obj) => {
                while (run) {
                    if (((CancellationToken) obj).IsCancellationRequested) break;
                    update.Invoke();
                }
            }, cts.Token);
        }
        public void Stop() { run = false; cts.Cancel(); }
    }
}
