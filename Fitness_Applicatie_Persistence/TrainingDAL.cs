﻿using System;
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
                        Guid trainingID = Guid.Parse(reader["ID"].ToString());
                        DateTime date = (DateTime)reader["Date"];
                        TrainingTypeDTO trainingType = (TrainingTypeDTO)Enum.Parse(typeof(TrainingTypeDTO), reader["TrainingType"].ToString());
                        TrainingDTO training = new TrainingDTO(trainingID, Guid.Parse(userID), date, trainingType);
                        trainings.Add(training);
                    }
                }
            }

            return trainings;
        }

        public TrainingDTO GetTraining(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmdTraining = new SqlCommand("spGetTraining", connection);
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                connection.Open();
                TrainingDTO trainingDTO = new TrainingDTO();
                using (SqlDataReader reader = cmdTraining.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid userID = Guid.Parse(reader["UserID"].ToString());
                        DateTime date = (DateTime)reader["Date"];
                        TrainingTypeDTO trainingType = (TrainingTypeDTO)Enum.Parse(typeof(TrainingTypeDTO), reader["TrainingType"].ToString());
                        trainingDTO = new TrainingDTO(Guid.Parse(trainingID), userID, date, trainingType);
                    }
                }
                connection.Close();
                return trainingDTO;
            }
        }

        public WeightTrainingDTO GetWeightTraining(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmdTraining = new SqlCommand("spGetTraining", connection);
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                DateTime date = DateTime.MinValue;
                Guid userID = Guid.Empty;
                TrainingTypeDTO trainingType = TrainingTypeDTO.Strength;

                connection.Open();
                using (SqlDataReader reader = cmdTraining.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        date = Convert.ToDateTime(reader["Date"]);
                        userID = Guid.Parse(reader["UserID"].ToString());
                        trainingType = (TrainingTypeDTO)Enum.Parse(typeof(TrainingTypeDTO), reader["TrainingType"].ToString());
                    }
                }
                connection.Close();

                SqlCommand cmdRound = new SqlCommand("spGetRound", connection);
                cmdRound.CommandType = System.Data.CommandType.StoredProcedure;
                cmdRound.Parameters.AddWithValue("@TrainingID", trainingID);
                List<RoundDTO> rounds = new List<RoundDTO>();
                connection.Open();
                using (SqlDataReader reader = cmdRound.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Guid exerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                        ExerciseDTO exercise = GetExerciseDTO(exerciseID.ToString());
                        Guid roundID = Guid.Parse(reader["ID"].ToString());

                        SqlCommand cmdSet = new SqlCommand("spGetSet", connection);
                        cmdSet.CommandType = System.Data.CommandType.StoredProcedure;
                        cmdSet.Parameters.AddWithValue("@RoundID", roundID);

                        List<SetDTO> sets = new List<SetDTO>();
                        using (SqlDataReader setReader = cmdSet.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                Guid setID = Guid.Parse(setReader["SetID"].ToString());
                                int setOrder = Convert.ToInt32(setReader["SetOrder"]);
                                double weight = Convert.ToInt32(setReader["Weight"]);

                                SetDTO setDTO = new SetDTO(weight, setID, setOrder);
                                sets.Add(setDTO);
                            }
                        }

                        RoundDTO roundDTO = new RoundDTO(exercise, roundID, Guid.Parse(trainingID), exerciseID, sets);
                        rounds.Add(roundDTO);
                    }

                    WeightTrainingDTO weightTrainingDTO = new WeightTrainingDTO(rounds, Guid.Parse(trainingID), userID, date, trainingType);
                    return weightTrainingDTO;
                }    
            }
        }

        public CardioTrainingDTO GetCardioTraining(string trainingID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                SqlCommand cmdTraining = new SqlCommand("spGetTraining", connection);
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", trainingID);

                DateTime date = DateTime.MinValue;
                Guid userID = Guid.Empty;
                ExerciseDTO exercise = new ExerciseDTO();
                decimal distance = 0;
                TimeSpan time = TimeSpan.MinValue;
                TrainingTypeDTO trainingType = TrainingTypeDTO.Cardio;

                connection.Open();
                using (SqlDataReader reader = cmdTraining.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        date = Convert.ToDateTime(reader["Date"]);
                        userID = Guid.Parse(trainingID);
                        trainingType = (TrainingTypeDTO)Enum.Parse(typeof(TrainingTypeDTO), reader["TrainingType"].ToString());
                    }
                }

                SqlCommand cmdCardioTraining = new SqlCommand("spGetCardioTraining", connection);
                cmdTraining.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTraining.Parameters.AddWithValue("@TrainingID", connection);

                using (SqlDataReader reader = cmdTraining.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        exercise = GetExerciseDTO(reader["ExerciseID"].ToString());
                        distance = Convert.ToDecimal(reader["Distance"]);
                        time = TimeSpan.Parse(reader["Time"].ToString());
                    }
                }

                CardioTrainingDTO cardioTrainingDTO = new CardioTrainingDTO(exercise, distance, time, Guid.Parse(trainingID), userID, date, trainingType);
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

            foreach (var round in trainingDTO.GetRounds())
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

                    foreach (var set in round.GetSets())
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
                ExerciseTypeDTO exerciseType = ExerciseTypeDTO.Bodyweight;
                string name = null;
                Guid userID = Guid.Empty;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        exerciseType = (ExerciseTypeDTO)Enum.Parse(typeof(ExerciseTypeDTO), reader["ExerciseType"].ToString());
                        name = reader["Name"].ToString();
                        userID = Guid.Parse(reader["UserID"].ToString());
                    }
                    ExerciseDTO exerciseDTO = new ExerciseDTO(Guid.Parse(exerciseID), name, userID, exerciseType);
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
                Guid exerciseID = Guid.Empty;
                ExerciseDTO exercise = new ExerciseDTO();
                Guid roundID = Guid.Empty;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        exerciseID = Guid.Parse(reader["ExerciseID"].ToString());
                        exercise = GetExerciseDTO(exerciseID.ToString());
                        roundID = Guid.Parse(reader["ID"].ToString());

                        RoundDTO round = new RoundDTO(exercise, roundID, Guid.Parse(trainingID), exerciseID, null);
                        rounds.Add(round);
                    }

                    return rounds;
                }
            }
        }
    }
}
