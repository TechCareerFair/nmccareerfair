using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.AdminDAL
{
    public class AdminDatabaseDataService : IAdminDataService, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<admin> Read()
        {
            List<admin> admins = new List<admin>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [AdminID],[Username],[ContactEmail]");
                sb.Append("FROM [careerfair].[admin]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            admin admin = new admin();

                            admin.AdminID = DatabaseHelper.CheckNullInt(reader, 0);
                            admin.Username = DatabaseHelper.CheckNullString(reader, 1);
                            admin.ContactEmail = DatabaseHelper.CheckNullString(reader, 2);

                            admins.Add(admin);
                        }
                    }
                }
            }

            return admins;
        }

        public AdminViewModel GetAccountInfoBy(string Email)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [PasswordHash]");
            sb.Append("FROM [dbo].[AspNetUsers]");
            sb.Append("WHERE [Email] = '" + Email + "'");

            String sql = sb.ToString();
            AdminViewModel admin = new AdminViewModel();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            admin.Password = DatabaseHelper.CheckNullString(reader, 0);
                        }
                    }
                }
            }

            return admin;
        }

        public bool ReadIsActive(string email)
        {
            bool isActive = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [Active]");
            sb.Append("FROM [careerfair].[admin]");
            sb.Append("WHERE [Username] = '" + email + "'");

            String sql = sb.ToString();
            BusinessViewModel bus = new BusinessViewModel();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            isActive = DatabaseHelper.CheckNullBool(reader, 0);
                        }
                    }
                }
            }

            return isActive;
        }

        public void Write(List<admin> applicants)
        {
            throw new NotImplementedException();
        }

        public void Update(admin admin)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[admin]");
                sb.Append("SET [Username] = @param2,[ContactEmail] = @param4");
                sb.Append(" WHERE [AdminID] = " + admin.AdminID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 256).Value = (object)admin.Username ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 256).Value = (object)admin.ContactEmail ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}