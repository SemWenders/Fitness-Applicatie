using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    class WeightTrainingDTO : TrainingDTO
    {
        public List<RoundDTO> Rounds { get; private set; }
    }
}
