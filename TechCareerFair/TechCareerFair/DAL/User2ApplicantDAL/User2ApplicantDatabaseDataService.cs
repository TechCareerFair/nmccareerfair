using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.User2ApplicantDAL
{
    public class User2ApplicantDatabaseDataService : IUser2ApplicantDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<user2applicant> Read()
        {
            List<user2applicant> u2as = new List<user2applicant>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [ID],[NetUserID],[ApplicantID]");
                sb.Append("FROM [dbo].[user2applicant]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user2applicant u2a = new user2applicant();

                            u2a.ID = DatabaseHelper.CheckNullInt(reader, 0);
                            u2a.NetUserID = DatabaseHelper.CheckNullString(reader, 1);
                            u2a.ApplicantID = DatabaseHelper.CheckNullInt(reader, 2);

                            u2as.Add(u2a);
                        }
                    }
                }
            }
            return u2as;
        }

        public List<string> Read(int applicantID)
        {
            List<string> userIDs = new List<string>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [NetUserID]");
                sb.Append("FROM [dbo].[user2applicant] WHERE [ApplicantID] = " + applicantID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = DatabaseHelper.CheckNullString(reader, 0);

                            userIDs.Add(id);
                        }
                    }
                }
            }
            return userIDs;
        }

        public List<int> Read(string userID)
        {
            List<int> applicantIDs = new List<int>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [ApplicantID]");
                sb.Append("FROM [dbo].[user2applicant] WHERE [NetUserID] = " + userID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = DatabaseHelper.CheckNullInt(reader, 0);

                            applicantIDs.Add(id);
                        }
                    }
                }
            }
            return applicantIDs;
        }

        public void Write(List<user2applicant> u2as)
        {
            throw new NotImplementedException();
        }

        public void Insert(user2applicant u2as)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[user2applicant]([NetUserID],[ApplicantID])");
                string values = "VALUES(@param1, @param2)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, 128).Value = (object)u2as.NetUserID ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.Int).Value = (object)u2as.ApplicantID ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(user2applicant u2a)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [dbo].[user2applicant]");
                sb.Append("WHERE [ID] = " + u2a.ID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(user2applicant u2a)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [dbo].[user2applicant]");
                sb.Append("SET [NetUserID] = @param1, [ApplicantID] = @param2");
                sb.Append(" WHERE [ID] = " + u2a.ID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, 128).Value = (object)u2a.NetUserID ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.Int).Value = (object)u2a.ApplicantID ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}