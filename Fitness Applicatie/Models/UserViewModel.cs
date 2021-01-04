using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitTracker.Logic;

namespace Fitness_Applicatie.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string UserID { get; set; }
        public List<Training> Trainings { get; set; }
    }
}
