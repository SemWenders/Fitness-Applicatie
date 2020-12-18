using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class CardioTraining : Training
    {
        public Exercise Exercise { get; private set; }
        public decimal Distance { get; private set; }
        public TimeSpan Time { get; private set; }
    }
}
