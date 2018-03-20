using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.Applicant2FieldDAL
{
    public class Applicant2FieldDatabaseDataService : IApplicant2FieldDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<applicant2field> Read()
        {
            List<applicant2field> applicant2fields = new List<applicant2field>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [ApplicantFieldID],[Applicant],[Field]");
                sb.Append("FROM [careerfair].[applicant2field]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applicant2field applicant2field = new applicant2field();

                            applicant2field.ApplicantFieldID = DatabaseHelper.CheckNullInt(reader, 0);
                            applicant2field.Applicant = DatabaseHelper.CheckNullInt(reader, 1);
                            applicant2field.Field = DatabaseHelper.CheckNullInt(reader, 2);

                            applicant2fields.Add(applicant2field);
                        }
                    }
                }
            }
            return applicant2fields;
        }

        public void Write(List<applicant2field> applicant2fields)
        {
            throw new NotImplementedException();
        }

        public void Insert(applicant2field applicant2field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[applicant2field]([Applicant],[Field])");
                string values = "VALUES(@param1, @param2)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = (object)applicant2field.Applicant ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.Int).Value = (object)applicant2field.Field ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(applicant2field applicant2field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [careerfair].[applicant2field]");
                sb.Append("WHERE [ApplicantFieldID] = " + applicant2field.ApplicantFieldID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(applicant2field applicant2field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[applicant2field]");
                sb.Append("SET [Applicant] = @param1, [Field] = @param2 ");
                sb.Append("WHERE [ApplicantFieldID] = " + applicant2field.ApplicantFieldID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = (object)applicant2field.Applicant ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.Int).Value = (object)applicant2field.Field ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}