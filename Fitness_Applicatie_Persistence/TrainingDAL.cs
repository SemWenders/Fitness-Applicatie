using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;

namespace FitTracker.Persistence
{
    class TrainingDAL : ITrainingDAL
    {
        public void AddTraining(TrainingDTO trainingDTO)
        {
            string connectionString = "Server = localhost; Database = master; Trusted_Connection = True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddTraing", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrainingID", trainingDTO.trainingID);
                cmd.Parameters.AddWithValue("@UserID", trainingDTO.UserID);
                cmd.Parameters.AddWithValue("@ExerciseName", trainingDTO.ExerciseName);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                    }
                }
            }
        }

        public void DeleteTraining()
        {
            throw new NotImplementedException();
        }
    }
}
