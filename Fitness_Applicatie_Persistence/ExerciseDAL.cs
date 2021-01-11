using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FitTracker.Interface.DTOs;
using FitTracker.Interface.Interfaces;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;    

namespace FitTracker.Persistence
{
    public class ExerciseDAL : IExerciseDAL
    {
        //string connectionString = "Data Source=LAPTOP-7SORRU5A; Initial Catalog=FitTracker; Integrated Security=SSPI;";
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build().GetConnectionString("DefaultConnection");
        }
        public void AddExercise(ExerciseDTO exercise)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Exercises VALUES(@ExerciseID, @Name, @UserID, @ExerciseType)", connection);
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
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Exercises WHERE ExerciseID = @ExerciseID", connection);
                cmd.Parameters.AddWithValue("@ExerciseID", exerciseID);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public ExerciseDTO GetExerciseDTO(string exerciseID)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Exercises WHERE ExerciseID = @ExerciseID", connection);
                cmd.Parameters.AddWithValue("@ExerciseID", exerciseID);
                connection.Open();
                ExerciseTypeDTO exerciseTypeDTO = ExerciseTypeDTO.Bodyweight;
                string name = null;
                Guid userID = Guid.Empty;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exerciseTypeDTO = (ExerciseTypeDTO)Enum.Parse(typeof(ExerciseTypeDTO), reader["ExerciseType"].ToString());
                        name = reader["Name"].ToString();
                        userID = Guid.Parse(reader["UserID"].ToString());
                    }
                    ExerciseDTO exerciseDTO = new ExerciseDTO(Guid.Parse(exerciseID), name, userID, exerciseTypeDTO);
                    return exerciseDTO;
                }
            }
        }

        public ExerciseDTO GetExerciseDTOByName(string exerciseName)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Exercises WHERE Name = @Name", connection);
                cmd.Parameters.AddWithValue("@Name", exerciseName);
                connection.Open();
                ExerciseTypeDTO exerciseTypeDTO = ExerciseTypeDTO.Bodyweight;
                string exerciseID = null;
                Guid userID = Guid.Empty;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exerciseTypeDTO = (ExerciseTypeDTO)Enum.Parse(typeof(ExerciseTypeDTO), reader["ExerciseType"].ToString());
                        exerciseID = reader["ExerciseID"].ToString();
                        userID = Guid.Parse(reader["UserID"].ToString());
                    }
                    ExerciseDTO exerciseDTO = new ExerciseDTO(Guid.Parse(exerciseID), exerciseName, userID, exerciseTypeDTO);
                    return exerciseDTO;
                }
            }
        }

        public List<ExerciseDTO> GetAllExerciseDTOs()
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                List<ExerciseDTO> exerciseDTOs = new List<ExerciseDTO>();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Exercises", connection);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Guid exerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                        Guid userID = Guid.Parse(reader["UserID"].ToString());
                        ExerciseTypeDTO exerciseType = (ExerciseTypeDTO)Convert.ToInt32(reader["ExerciseType"]);
                        string name = reader["Name"].ToString();
                        ExerciseDTO exerciseDTO = new ExerciseDTO(exerciseID, name, userID, exerciseType);
                        exerciseDTOs.Add(exerciseDTO);
                    }

                    return exerciseDTOs;
                }
            }
        }

        public bool ExerciseExists(string exercisename)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("" +
                    "SELECT COUNT(1) " +
                    "FROM Exercises " +
                    "WHERE Name = @ExerciseName", connection);
                cmd.Parameters.AddWithValue("@ExerciseName", exercisename);
                connection.Open();
                
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
