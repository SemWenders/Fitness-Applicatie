using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class WeightTrainingDTO : TrainingDTO
    {
        public List<RoundDTO> Rounds { get; set; }
    }
}
