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
        string connectionString = "Data Source=LAPTOP-7SORRU5A; Initial Catalog=FitTracker; Integrated Security=SSPI;";
        public Guid GetExerciseID(string ExerciseName)
        {
            Guid exerciseID = Guid.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetExerciseID", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExerciseName", ExerciseName);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                    }
                    connection.Close();
                }
            }

            return exerciseID;
        }

        public void DeleteWeightTraining(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmdTraining = new SqlCommand("spDeleteTraining", connection);
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                connection.Open();
                cmdTraining.ExecuteNonQuery();
                connection.Close();

                SqlCommand cmdWeightTraining = new SqlCommand("spDeleteWeightTraining", connection);
                cmdWeightTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdWeightTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                connection.Open();
                cmdWeightTraining.ExecuteNonQuery();
                connection.Close();

                SqlCommand cmdRound = new SqlCommand("spDeleteRound", connection);
                cmdRound.CommandType = System.Data.CommandType.StoredProcedure;
                cmdRound.Parameters.AddWithValue("@TrainingID", trainingID);

                connection.Open();
                cmdRound.ExecuteNonQuery();
                connection.Close();

                List<RoundDTO> rounds = GetRounds(trainingID);
                foreach (var round in rounds)
                {
                    SqlCommand cmdSet = new SqlCommand("spDeleteSet", connection);
                    cmdSet.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdSet.Parameters.AddWithValue("@RoundID", round.RoundID);

                    connection.Open();
                    cmdSet.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteCardioTraining(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmdTraining = new SqlCommand("spDeleteTraining", connection);
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                connection.Open();
                cmdTraining.ExecuteNonQuery();
                connection.Close();

                SqlCommand cmdCardioTraining = new SqlCommand("spDeleteCardioTraining", connection);
                cmdCardioTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdCardioTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                connection.Open();
                cmdCardioTraining.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<TrainingDTO> GetUserTrainings(string userID)
        {
            List<TrainingDTO> trainings = new List<TrainingDTO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserTrainings", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrainingDTO training = new TrainingDTO();
                        training.TrainingID = Guid.Parse(reader["TrainingID"].ToString());
                        training.UserID = reader["UserID"].ToString();
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
                SqlCommand cmdTraining = new SqlCommand("spGetTraining", connection);
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
                        weightTrainingDTO.TrainingID = Guid.Parse(trainingID.ToString());
                    }
                }
                connection.Close();

                SqlCommand cmdRound = new SqlCommand("spGetRound", connection);
                cmdRound.CommandType = System.Data.CommandType.StoredProcedure;
                cmdRound.Parameters.AddWithValue("@TrainingID", trainingID);
                connection.Open();
                using (SqlDataReader reader = cmdRound.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        RoundDTO roundDTO = new RoundDTO();

                        roundDTO.ExerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                        roundDTO.Exercise = GetExerciseDTO(roundDTO.ExerciseID.ToString());
                        roundDTO.TrainingID = Guid.Parse(trainingID);
                        roundDTO.RoundID = Guid.Parse(reader["ID"].ToString());

                        SqlCommand cmdSet = new SqlCommand("spGetSet", connection);
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
                
                SqlCommand cmdTraining = new SqlCommand("spGetTraining", connection);
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", trainingID);
                connection.Open();
                using (SqlDataReader reader = cmdTraining.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        cardioTrainingDTO.Date = Convert.ToDateTime(reader["Date"]);
                        cardioTrainingDTO.TrainingID = Guid.Parse(trainingID.ToString());
                        cardioTrainingDTO.UserID = trainingID;
                    }
                }

                SqlCommand cmdCardioTraining = new SqlCommand("spGetCardioTraining", connection);
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
                SqlCommand cmd = new SqlCommand("spAddWeightTraining", connection);
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
                    SqlCommand cmdRound = new SqlCommand("spAddRound", connection);
                    cmdRound.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdRound.Parameters.AddWithValue("@RoundID", round.RoundID);
                    cmdRound.Parameters.AddWithValue("@ExerciseID", round.ExerciseID);
                    cmdRound.Parameters.AddWithValue("@TrainingID", trainingDTO.TrainingID);

                    connection.Open();
                    cmdRound.ExecuteNonQuery();
                    connection.Close();

                    foreach (var set in round.Sets)
                    {
                        SqlCommand cmdSet = new SqlCommand("spAddSet", connection);
                        cmdSet.CommandType = System.Data.CommandType.StoredProcedure;
                        cmdSet.Parameters.AddWithValue("@SetID", set.SetID);
                        cmdSet.Parameters.AddWithValue("@RoundID", round.RoundID);
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
                SqlCommand cmd = new SqlCommand("spAddCardioTraining", connection);
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
                SqlCommand cmd = new SqlCommand("spGetExercise", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExerciseID", exerciseID);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ExerciseDTO exerciseDTO = new ExerciseDTO();
                    while(reader.Read())
                    {
                        exerciseDTO.ExerciseType = (ExerciseTypeDTO)Enum.Parse(typeof(ExerciseTypeDTO), reader["ExerciseType"].ToString());
                        exerciseDTO.Name = reader["Name"].ToString();
                        exerciseDTO.ExerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                    }
                    return exerciseDTO;
                }
            }
        }

        public List<RoundDTO> GetRounds(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<RoundDTO> rounds = new List<RoundDTO>();
                SqlCommand cmd = new SqlCommand("spGetRounds", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrainingID", trainingID);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        RoundDTO round = new RoundDTO();
                        round.ExerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                        round.Exercise = GetExerciseDTO(round.ExerciseID.ToString());
                        round.TrainingID = Guid.Parse(trainingID);
                        round.RoundID = Guid.Parse(reader["ID"].ToString());

                        rounds.Add(round);
                    }

                    return rounds;
                }
            }
        }
    }
}
