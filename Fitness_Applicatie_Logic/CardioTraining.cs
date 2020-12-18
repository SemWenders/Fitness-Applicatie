using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    class CardioTraining : Training
    {
        public Exercise Exercise { get; private set; }
        public int Distance { get; private set; }
        public TimeSpan Time { get; private set; }
    }
}
