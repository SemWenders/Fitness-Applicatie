using System;
using System.Collections.Generic;
using System.Text;

namespace FitTracker.Interface.DTOs
{
    public class TrainingDTO
    {
        public string TrainingID { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; }
    }
}
