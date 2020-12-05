using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;

namespace FitTracker.Persistence
{
    public class TrainingDAL : ITrainingDAL
    {
        string connectionString = "Server = localhost; Database = master; Trusted_Connection = True;";

        public void AddTraining(TrainingDTO trainingDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddTraining", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrainingID", trainingDTO.trainingID);
                cmd.Parameters.AddWithValue("@UserID", trainingDTO.UserID);
                cmd.Parameters.AddWithValue("@Date", trainingDTO.Date);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            for (int i = 0; i < trainingDTO.Sets.Count ; i++)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAddTraining_Exercise");
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TrainingID", trainingDTO.trainingID);
                    cmd.Parameters.AddWithValue("@ExerciseID", GetExerciseID(trainingDTO.ExerciseNames[i].Name));
                    cmd.Parameters.AddWithValue("@SetNumber", i+1);
                    cmd.Parameters.AddWithValue("@Weight", trainingDTO.Sets[i]);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public string GetExerciseID(string ExerciseName)
        {
            string exerciseID = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetExerciseID");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExerciseName", ExerciseName);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exerciseID = reader.ToString();
                    }
                    connection.Close();
                }
            }

            return exerciseID;
        }

        public void DeleteTraining(TrainingDTO trainingDTO)
        {
            string connectionString = "Server = localhost; Database = master; Trusted_Connection = True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteTraining", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrainingID", trainingDTO.trainingID);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
