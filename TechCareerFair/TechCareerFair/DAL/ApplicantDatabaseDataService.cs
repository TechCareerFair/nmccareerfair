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
                sb.Append("SELECT TOP (1000) [ApplicantID],[Email],[FirstName],[LastName],[Active]");
                sb.Append("FROM [careerfair].[applicant]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applicant app = new applicant();
                            app.ApplicantID = reader.GetInt32(0);
                            app.Email = (reader.GetString(1));
                            app.FirstName = (reader.GetString(2));
                            app.LastName = (reader.GetString(3));
                            app.Active = reader.GetBoolean(4);

                            applicants.Add(app);
                        }
                    }
                }
            }

            return applicants;
        }

        public void Write(List<applicant> applicants)
        {
            //throw new NotImplementedException();
        }
    }
}