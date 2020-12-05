using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    class Training
    {
        public string trainingID { get; private set; }
        public DateTime Date { get; private set; }
        public List<int> Sets { get; private set; } //list of weight of each set
    }
}
