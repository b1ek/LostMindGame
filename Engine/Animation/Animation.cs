using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Engine.Animation
{
    public class Animation
    {
        private FrameSequence frameSequence;
        public User.UserConsoleWriter writer;
        private ConsoleColor _color = ConsoleColor.White;
        public ConsoleColor color { get => _color; set { _color = value; } }
        public Animation(FrameSequence fs, int sx, int sy) {
            frameSequence = fs.forgeCopy();
            writer = new User.UserConsoleWriter(sx, sy);
        }
        public Animation(FrameSequence fs, User.UserConsoleWriter consoleWriter) {
            frameSequence = fs.forgeCopy();
            writer = consoleWriter;
        }

        public void run() {
            var frames = frameSequence;
            var y = 0;
            var lc = Console.ForegroundColor;
            Console.ForegroundColor = color;
            for (int i = 0; i < frames.Count; i++) {
                frames[i].displayFrame(writer);
                y++;
            }
            Console.ForegroundColor = lc;
            Console.SetCursorPosition(writer._x, y + 2);
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
        public static Animation createSimple(string s, int sx, int sy, int frameDelay, ConsoleColor color)
        {
            var _s = s.Replace("\r", "").Replace("\t", "    ").Split("\n");
            FrameSequence _fs = new FrameSequence();
            var _ss = "";
            for (int i = 0; i < _s.Length; i++)
            {
                _ss = _ss + _s[i] + "\n";
                _fs.add(new Frame(_ss, frameDelay));
            }
            var _anim = new Animation(_fs, sx, sy);
            _anim.color = color;
            return _anim;
        }
    }
}
