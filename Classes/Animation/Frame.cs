using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LostMind.Classes.Animation
{
    public class Frame
    {
        Dictionary<string, string> _meta;
        string _contents;
        int _delay;
        public Frame(Dictionary<string, string> meta, string contents, int delay)
        {
            _meta = meta;
            _contents = contents;
        }
        public Frame(string contents, int delay) {
            Dictionary<string, string> a = new Dictionary<string, string>();
            a.Add("cmdCurVis", "false");
            _meta = a;
            _contents = contents;
            _delay = delay;
        }
        public void displayFrame(User.UserConsoleWriter writer) {
            writer.write(_contents);
            writer._y = writer._sy;
            Thread.Sleep(_delay);
        }
    }
}
