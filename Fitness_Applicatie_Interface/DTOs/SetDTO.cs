using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class SetDTO
    {
        public int Weight { get; set; }
        public Guid SetID { get; set; }
        public Guid RoundID { get; set; }
        public int SetOrder { get; set; }
    }
}
