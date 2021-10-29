using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.Animation
{
    public class FrameSequence : List<Frame> {

        public FrameSequence(Frame[] frames) {
            for (var i = 0; i > frames.Length; i++)
                Add(frames[i]);
        }
        public FrameSequence() {
        }
        public void add(Frame frame) {
            Add(frame);
        }
        public FrameSequence forgeCopy() {
            return (FrameSequence) MemberwiseClone();
        }
    }
}
