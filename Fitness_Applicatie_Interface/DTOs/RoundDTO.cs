using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class RoundDTO
    {
        public ExerciseDTO Exercise { get; private set; }
        public List<SetDTO> Sets { get; private set; }
    }
}
