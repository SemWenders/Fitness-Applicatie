﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class SetDTO
    {
        public double Weight { get; private set; }
        public Guid SetID { get; private set; }
        public int SetOrder { get; private set; }

        public SetDTO(double weight, Guid setID, int setOrder)
        {
            Weight = weight;
            SetID = setID;
            SetOrder = setOrder;
        }
    }
}
