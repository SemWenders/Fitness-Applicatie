﻿using System;
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteExercise", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExerciseID", exerciseID);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public ExerciseDTO GetExerciseDTO(string exerciseID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetExercise", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExerciseID", exerciseID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ExerciseDTO exerciseDTO = new ExerciseDTO();
                    while (reader.Read())
                    {
                        exerciseDTO.ExerciseType = (ExerciseTypeDTO)Enum.Parse(typeof(ExerciseTypeDTO), reader["ExerciseType"].ToString());
                        exerciseDTO.Name = reader["Name"].ToString();
                        exerciseDTO.ExerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                    }
                    return exerciseDTO;
                }
            }
        }
    }
}