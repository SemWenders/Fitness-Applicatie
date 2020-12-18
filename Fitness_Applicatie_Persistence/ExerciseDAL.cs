using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FitTracker.Interface.DTOs;
using FitTracker.Interface.Interfaces;

namespace FitTracker.Persistence
{
    public class ExerciseDAL : IExerciseDAL
    {
        string connectionString = "Data Source=LAPTOP-7SORRU5A; Initial Catalog=FitTracker; Integrated Security=SSPI;";
        public void AddExercise(ExerciseDTO exercise)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddExercise", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExerciseID", exercise.ExerciseID);
                cmd.Parameters.AddWithValue("@Name", exercise.Name);
                cmd.Parameters.AddWithValue("@UserID", exercise.UserID);
                cmd.Parameters.AddWithValue("@ExerciseType", exercise.ExerciseType);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteExercise(string exerciseID)
        {
            throw new NotImplementedException();
        }

        public ExerciseDTO GetExerciseDTO(string exerciseID)
        {
            throw new NotImplementedException();
        }
    }
}
