using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FitTracker.Interface.DTOs;
using FitTracker.Interface.Interfaces;

namespace FitTracker.Persistence
{
    class UserDAL : IUserDAL
    {
        string connectionString = "Data Source=LAPTOP-7SORRU5A; Initial Catalog=FitTracker; Integrated Security=SSPI;";
        public void AddUser(UserDTO user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Name", user.Name);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteUser(string userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public UserDTO GetUser(string userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUser", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    UserDTO userDTO = new UserDTO();
                    while(reader.Read())
                    {
                        userDTO.Name = reader["Name"].ToString();
                        userDTO.UserID = userID;
                        userDTO.Password = reader["Password"].ToString();
                    }
                    return userDTO;
                }
            }
        }
    }
}
