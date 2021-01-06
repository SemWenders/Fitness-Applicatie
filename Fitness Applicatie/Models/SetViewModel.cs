using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Applicatie.Models
{
    public class SetViewModel
    {
        public int Repitions { get; set; }
        public double Weight { get; set; }
        public int SetOrder { get; set; }
        public Guid SetID { get; set; }
    }
}
