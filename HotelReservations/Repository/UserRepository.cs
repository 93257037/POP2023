using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservations.Repository
{
    internal class UserRepository : IUserRepository
    {
        public List<User> GetAll()
        {
            var users = new List<User>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                var commandText = "SELECT * FROM \"user\"";
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "user");

                foreach (DataRow row in dataSet.Tables["user"]!.Rows)
                {
                    var user = new User()
                    {
                        Id = (int)row["user_id"],
                        FirstName = row["first_name"] as string,
                        LastName = row["last_name"] as string,
                        JMBG = row["JMBG"] as string,
                        Username = row["username"] as string,
                        Password = row["password"] as string,
                        UserType = row["user_type"] as string,
                    };

                    users.Add(user);
                }
            }

            return users;
        }

        public int Insert(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO ""user"" (first_name, last_name, JMBG, username, password, user_type) OUTPUT inserted.user_id
                    VALUES (@first_name, @last_name, @JMBG, @username, @password, @user_type)
                ";

                command.Parameters.Add(new SqlParameter("first_name", user.FirstName));
                command.Parameters.Add(new SqlParameter("last_name", user.LastName));
                command.Parameters.Add(new SqlParameter("JMBG", user.JMBG));
                command.Parameters.Add(new SqlParameter("username", user.Username));
                command.Parameters.Add(new SqlParameter("password", user.Password));
                command.Parameters.Add(new SqlParameter("user_type", user.UserType));

                return (int)command.ExecuteScalar();
            }
        }


        public void Update(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE ""user"" 
                    SET first_name=@first_name, last_name=@last_name, JMBG=@JMBG, username=@username, password=@password, user_type=@user_type
                    WHERE user_id=@user_id
                ";

                command.Parameters.Add(new SqlParameter("user_id", user.Id));
                command.Parameters.Add(new SqlParameter("first_name", user.FirstName));
                command.Parameters.Add(new SqlParameter("last_name", user.LastName));
                command.Parameters.Add(new SqlParameter("JMBG", user.JMBG));
                command.Parameters.Add(new SqlParameter("username", user.Username));
                command.Parameters.Add(new SqlParameter("password", user.Password));
                command.Parameters.Add(new SqlParameter("user_type", user.UserType));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int userId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = "DELETE FROM \"user\" WHERE user_id = @user_id";

                command.Parameters.Add(new SqlParameter("user_id", userId));

                command.ExecuteNonQuery();
            }
        }
    }
}
