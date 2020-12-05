using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class ExerciseDTO
    {
        public string ExerciseID { get; private set; }
        public bool BodyWeight { get; private set; }
        public string Name { get; private set; }
    }
}
