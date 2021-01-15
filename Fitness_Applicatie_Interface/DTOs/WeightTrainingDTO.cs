using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class WeightTrainingDTO : TrainingDTO
    {
        private List<RoundDTO> rounds;

        //constructor
        public WeightTrainingDTO(List<RoundDTO> rounds, Guid trainingID, Guid userID, DateTime date, TrainingTypeDTO trainingType) : base(trainingID, userID, date, trainingType)
        {
            this.rounds = rounds;
        }

        public WeightTrainingDTO(List<RoundDTO> rounds, Guid userID, DateTime date, TrainingTypeDTO trainingTypeDTO) : base(userID, date, trainingTypeDTO)
        {
            this.rounds = rounds;
        }
        public WeightTrainingDTO()
        {

        }

        //methods
        public List<RoundDTO> GetRounds()
        {
            return rounds;
        }
    }
}
