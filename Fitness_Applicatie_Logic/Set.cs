using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Logic
{
    public class Set
    {
        public double Weight { get; private set; }
        public int SetOrder { get; private set; }
        public Guid SetID { get; private set; }

        //constructor
        public Set(double weight, int setOrder, Guid setID)
        {
            Weight = weight;
            SetOrder = setOrder;
            SetID = setID;
        }
    }
}
