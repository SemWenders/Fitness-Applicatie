using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FitTracker.Interface.Interfaces;
using FitTracker.Interface.DTOs;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace FitTracker.Persistence
{
    public class TrainingDAL : ITrainingDAL
    {
        //TODO: connection string in appsettings
        string GetExerciseID(string ExerciseName)
        {
            string exerciseID = null;
            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetExerciseID"); //TODO: add stored procedure
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

        public void DeleteTraining(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteTraining", connection); //TODO: Add stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrainingID", trainingID);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<TrainingDTO> GetUserTrainings(string userID)
        {
            List<TrainingDTO> trainings = new List<TrainingDTO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserTrainings", connection); //TODO: add stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrainingDTO training = new TrainingDTO();
                        training.TrainingID = (string) reader["TrainingID"];
                        training.UserID = (string) reader["UserID"];
                        training.Date = (DateTime)reader["Date"];
                        trainings.Add(training);
                    }
                }
            }

            return trainings;
        }

        public WeightTrainingDTO GetWeightTraining(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmdTraining = new SqlCommand("spGetTraining", connection); //TODO: add stored procedure
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                WeightTrainingDTO weightTrainingDTO = new WeightTrainingDTO();
                connection.Open();
                using (SqlDataReader reader = cmdTraining.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        weightTrainingDTO.Date = Convert.ToDateTime(reader["Date"]);
                        weightTrainingDTO.UserID = reader["UserID"].ToString();
                        weightTrainingDTO.TrainingID = trainingID;
                    }
                }
                connection.Close();

                SqlCommand cmdRound = new SqlCommand("spGetRound", connection); //TODO: add stored procedure
                cmdRound.CommandType = System.Data.CommandType.StoredProcedure;
                cmdRound.Parameters.AddWithValue("@TrainingID", trainingID);

                using (SqlDataReader reader = cmdRound.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        RoundDTO roundDTO = new RoundDTO();

                        roundDTO.ExerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                        roundDTO.Exercise = GetExerciseDTO(roundDTO.ExerciseID.ToString());
                        roundDTO.TrainingID = Guid.Parse(trainingID);
                        roundDTO.RoundID = Guid.Parse(reader["ID"].ToString());

                        SqlCommand cmdSet = new SqlCommand("spGetSet", connection); //TODO: add stored procedure
                        cmdSet.CommandType = System.Data.CommandType.StoredProcedure;
                        cmdSet.Parameters.AddWithValue("@RoundID", roundDTO.RoundID);

                        using (SqlDataReader setReader = cmdSet.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                SetDTO setDTO = new SetDTO();
                                setDTO.SetID = Guid.Parse(setReader["SetID"].ToString());
                                setDTO.SetOrder = Convert.ToInt32(setReader["SetOrder"]);
                                setDTO.Weight = Convert.ToInt32(setReader["Weight"]);
                                setDTO.RoundID = roundDTO.RoundID;

                                roundDTO.Sets.Add(setDTO);
                            }
                        }

                        weightTrainingDTO.Rounds.Add(roundDTO);
                    }

                    return weightTrainingDTO;
                }    
            }
        }

        public CardioTrainingDTO GetCardioTraining(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                CardioTrainingDTO cardioTrainingDTO = new CardioTrainingDTO();
                
                SqlCommand cmdTraining = new SqlCommand("spGetTraining", connection); //TODO: add stored procedure
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                using (SqlDataReader reader = cmdTraining.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        cardioTrainingDTO.Date = Convert.ToDateTime(reader["Date"]);
                        cardioTrainingDTO.TrainingID = trainingID;
                        cardioTrainingDTO.UserID = trainingID;
                    }
                }

                SqlCommand cmdCardioTraining = new SqlCommand("spGetCardioTraining", connection); //TODO: add stored procedure
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", connection);

                using (SqlDataReader reader = cmdTraining.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        cardioTrainingDTO.Exercise = GetExerciseDTO(reader["ExerciseID"].ToString());
                        cardioTrainingDTO.Distance = Convert.ToDecimal(reader["Distance"]);
                        cardioTrainingDTO.Time = TimeSpan.Parse(reader["Time"].ToString());
                    }
                }

                return cardioTrainingDTO;
            }
        }

        public void AddWeightTraining(WeightTrainingDTO trainingDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddWeightTraining", connection); //TODO: add stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrainingID", trainingDTO.TrainingID);
                cmd.Parameters.AddWithValue("@UserID", trainingDTO.UserID);
                cmd.Parameters.AddWithValue("@Date", trainingDTO.Date);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            foreach (var round in trainingDTO.Rounds)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmdRound = new SqlCommand("spAddRound", connection); //TODO: add stored procedure
                    cmdRound.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdRound.Parameters.AddWithValue("@RoundID", round.RoundID);
                    cmdRound.Parameters.AddWithValue("@ExercisID", round.Exercise.ExerciseID);
                    cmdRound.Parameters.AddWithValue("@TrainingID", round.TrainingID);

                    connection.Open();
                    cmdRound.ExecuteNonQuery();
                    connection.Close();

                    foreach (var set in round.Sets)
                    {
                        SqlCommand cmdSet = new SqlCommand("spAddSet", connection); //TODO: add stored procedure
                        cmdSet.CommandType = System.Data.CommandType.StoredProcedure;
                        cmdSet.Parameters.AddWithValue("@SetID", set.SetID);
                        cmdSet.Parameters.AddWithValue("@RoundID", set.RoundID);
                        cmdSet.Parameters.AddWithValue("@SetOrder", set.SetOrder);
                        cmdSet.Parameters.AddWithValue("@Weight", set.Weight);

                        connection.Open();
                        cmdSet.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public void AddCardioTraining(CardioTrainingDTO trainingDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddCardioTraining", connection); //TODO: add stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrainingID", trainingDTO.TrainingID);
                cmd.Parameters.AddWithValue("@ExerciseID", trainingDTO.Exercise.ExerciseID);
                cmd.Parameters.AddWithValue("@Distance", trainingDTO.Distance);
                cmd.Parameters.AddWithValue("@Time", trainingDTO.Time);
                cmd.Parameters.AddWithValue("@Date", trainingDTO.Date);
                cmd.Parameters.AddWithValue("@UserID", trainingDTO.UserID);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public ExerciseDTO GetExerciseDTO(string exerciseID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetExercise", connection); //TODO: add stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExerciseID", exerciseID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ExerciseDTO exerciseDTO = new ExerciseDTO();
                    while(reader.Read())
                    {
                        exerciseDTO.ExerciseType = (ExerciseType)Enum.Parse(typeof(ExerciseType), reader["ExerciseType"].ToString());
                        exerciseDTO.Name = reader["Name"].ToString();
                        exerciseDTO.ExerciseID = Guid.Parse(reader["ExerciseID"].ToString());

                    }
                    return exerciseDTO;
                }
            }
        }
    }
}
