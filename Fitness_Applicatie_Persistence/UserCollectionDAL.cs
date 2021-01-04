using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FitTracker.Interface.DTOs;
using FitTracker.Interface.Interfaces;

namespace FitTracker.Persistence
{
    public class UserCollectionDAL : IUserCollectionDAL
    {
        string connectionString = "Data Source=LAPTOP-7SORRU5A; Initial Catalog=FitTracker; Integrated Security=SSPI;";
        public void AddUser(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string userID)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllUsers", connection); //TODO: write stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                List<UserDTO> userDTOs = new List<UserDTO>();
                connection.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        Guid userID = Guid.Parse(reader["UserID"].ToString());
                        string password = reader["Password"].ToString();
                        UserDTO userDTO = new UserDTO(name, userID, password, null, null);
                        userDTOs.Add(userDTO);
                    }
                    return userDTOs;
                }
            }
        }

        public UserDTO GetUser(string userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmdUser = new SqlCommand("spGetUser", connection);
                cmdUser.CommandType = System.Data.CommandType.StoredProcedure;
                cmdUser.Parameters.AddWithValue("@UserID", userID);
                string name = null;
                string password = null;
                connection.Open();
                using (SqlDataReader reader = cmdUser.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader["Name"].ToString();
                        password = reader["Password"].ToString();
                    }
                }

                List<TrainingDTO> trainingDTOs = new List<TrainingDTO>();
                SqlCommand cmdTrainings = new SqlCommand("spGetUserTrainings", connection);
                cmdTrainings.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTrainings.Parameters.AddWithValue("@UserID", userID);

                using (SqlDataReader reader = cmdTrainings.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid trainingID = Guid.Parse(reader["ID"].ToString());
                        DateTime date = (DateTime)reader["Date"];
                        TrainingTypeDTO trainingType = (TrainingTypeDTO)Enum.Parse(typeof(TrainingTypeDTO), reader["TrainingType"].ToString());
                        TrainingDTO training = new TrainingDTO(trainingID, Guid.Parse(userID), date, trainingType);
                        trainingDTOs.Add(training);
                    }
                }
                UserDTO userDTO = new UserDTO(name, Guid.Parse(userID), password, trainingDTOs, null);
                return userDTO;
            }
        }
    }
}
