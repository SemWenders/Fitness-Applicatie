using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class SetDTO
    {
        public double Weight { get; private set; }
        public Guid SetID { get; private set; }
        public int SetOrder { get; private set; }
        public Guid RoundID { get; private set; }

        public SetDTO(double weight, Guid setID, int setOrder, Guid roundID)
        {
            Weight = weight;
            SetID = setID;
            SetOrder = setOrder;
            RoundID = roundID;
        }

        public SetDTO(double weight, int setOrder)
        {
            Weight = weight;
            SetOrder = setOrder;
        }
    }
}
