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

        public UserDTO GetUser(string userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmdUser = new SqlCommand("spGetUser", connection);
                cmdUser.CommandType = System.Data.CommandType.StoredProcedure;
                cmdUser.Parameters.AddWithValue("@UserID", userID);
                UserDTO userDTO = new UserDTO();
                connection.Open();
                using (SqlDataReader reader = cmdUser.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userDTO.Name = reader["Name"].ToString();
                        userDTO.UserID = userID;
                        userDTO.Password = reader["Password"].ToString();
                    }
                }

                SqlCommand cmdTrainings = new SqlCommand("spGetUserTrainings", connection);
                cmdTrainings.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTrainings.Parameters.AddWithValue("@UserID", userID);

                using (SqlDataReader reader = cmdTrainings.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrainingDTO training = new TrainingDTO();
                        training.TrainingID = Guid.Parse(reader["ID"].ToString());
                        training.UserID = reader["UserID"].ToString();
                        training.Date = (DateTime)reader["Date"];
                        userDTO.Trainings.Add(training);
                    }
                }
                return userDTO;
            }
        }
    }
}
