using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Classes.Animation
{
    public class Animation
    {
        private FrameSequence frameSequence;
        private User.UserConsoleWriter writer;
        public Animation(FrameSequence fs, int sx, int sy) {
            frameSequence = fs.forgeCopy();
            writer = new User.UserConsoleWriter(sx, sy);
        }

        public void run() {
            var frames = frameSequence;
            for (int i = 0; i < frames.Count; i++) {
                frames[i].displayFrame(writer);
            }
        }


        public static Animation createSimple(string s, int sx, int sy, int frameDelay) {

            var _s = s.Replace("\r", "").Replace("\t", "    ").Split("\n");
            FrameSequence _fs = new FrameSequence();
            var _ss = "";
            for (int i = 0; i < _s.Length; i++) {
                _ss = _ss + _s[i] + "\n";
                _fs.add(new Frame(_ss, frameDelay));
            }
            return new Animation(_fs, sx, sy);
        }
    }
}
