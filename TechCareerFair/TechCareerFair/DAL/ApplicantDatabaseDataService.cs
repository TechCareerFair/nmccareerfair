using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;
using System.Data.SqlClient;
using System.Text;

namespace TechCareerFair.DAL
{
    public class ApplicantDatabaseDataService : IApplicantDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<applicant> Read()
        {
            List<applicant> applicants = new List<applicant>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT TOP (1000) [ApplicantID],[Password],[Email],[FirstName],[LastName],[University],[Alumni],[Profile],[SocialMedia],[Resume],[YearsExperience],[Internship],[Active]");
                sb.Append("FROM [careerfair].[applicant]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applicant app = new applicant();
                            app.ApplicantID = (int)CheckNullInt(reader, 0);
                            app.Password = CheckNullString(reader, 1);
                            app.Email = CheckNullString(reader, 2);
                            app.FirstName = CheckNullString(reader, 3);
                            app.LastName = CheckNullString(reader, 4);
                            app.University = CheckNullString(reader, 5);
                            app.Alumni = CheckNullBool(reader, 6);
                            app.Profile = CheckNullString(reader, 7);
                            app.SocialMedia = CheckNullString(reader, 8);
                            app.Resume = CheckNullByteArray(reader, 9);
                            app.YearsExperience = CheckNullByte(reader, 10);
                            app.Internship = CheckNullBool(reader,11);
                            app.Active = CheckNullBool(reader, 12);

                            applicants.Add(app);
                        }
                    }
                }
            }

            return applicants;
        }

        private string CheckNullString(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetString(i);
            }
            else
            {
                return string.Empty;
            }
        }

        private bool? CheckNullBool(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetBoolean(i);
            }
            else
            {
                return null;
            }
        }

        private byte? CheckNullByte(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetByte(i);
            }
            else
            {
                return null;
            }
        }

        private int? CheckNullInt(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetInt32(i);
            }
            else
            {
                return null;
            }
        }

        private byte[] CheckNullByteArray(SqlDataReader reader, int i)
        {
            if (reader[i] != DBNull.Value)
            {
                return (byte[])reader[i];
            }
            else
            {
                return null;
            }
   
        }

        public void Write(List<applicant> applicants)
        {
            //throw new NotImplementedException();
        }
    }
}