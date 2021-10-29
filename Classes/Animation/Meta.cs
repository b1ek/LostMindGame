using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Classes.Animation
{
    public class Meta
    {
        public bool cursorVisibility = false;
        public int timeMs = 64;
        public int randTimeMin = -1;
        public int randTimeMax = -1;
        public int fPrintDelay = 02;
        public int fPrintRDlMin = -1;
        public int fPrintRDlMax = -1;

        int _timeMs
        {
            get
            {
                if (randTimeMax != -1 && randTimeMin != -1)
                {
                    return Util.RandomGen.getInt(randTimeMin, randTimeMax);
                }
                if (timeMs != -1)
                    return timeMs;
                return -1;
            }
        }
        int _fpd
        {
            get
            {
                if (fPrintRDlMax != -1 && fPrintRDlMin != -1)
                {
                    return Util.RandomGen.getInt(fPrintRDlMin, fPrintRDlMax);
                }
                if (timeMs != -1)
                    return fPrintDelay;
                return -1;

            }
        }


    }
}
