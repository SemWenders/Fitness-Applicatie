using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    class Round
    {
        public Exercise Exercise { get; private set; }
        public List<Set> Sets { get; private set; }
    }
}
