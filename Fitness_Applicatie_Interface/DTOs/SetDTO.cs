using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class SetDTO
    {
        public double Weight { get; set; }
        public Guid SetID { get; set; }
        public int SetOrder { get; set; }
    }
}
