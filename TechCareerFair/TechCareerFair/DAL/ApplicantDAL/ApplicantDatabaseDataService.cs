﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;
using System.Data.SqlClient;
using System.Text;
using System.Data;

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
                
                InitApplicant(applicants, connection);
            }

            return applicants;
        }

        private void InitApplicant(List<applicant> applicants, SqlConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [ApplicantID],[Password],[Email],[FirstName],[LastName],[University],[Alumni],[Profile],[SocialMedia],[Resume],[YearsExperience],[Internship],[Active]");
            sb.Append("FROM [careerfair].[applicant]");
            String sql = sb.ToString();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        applicant app = new applicant();

                        app.ApplicantID = DatabaseHelper.CheckNullInt(reader, 0);
                        app.Password = DatabaseHelper.CheckNullString(reader, 1);
                        app.Email = DatabaseHelper.CheckNullString(reader, 2);
                        app.FirstName = DatabaseHelper.CheckNullString(reader, 3);
                        app.LastName = DatabaseHelper.CheckNullString(reader, 4);
                        app.University = DatabaseHelper.CheckNullString(reader, 5);
                        app.Alumni = DatabaseHelper.CheckNullBool(reader, 6);
                        app.Profile = DatabaseHelper.CheckNullString(reader, 7);
                        app.SocialMedia = DatabaseHelper.CheckNullString(reader, 8);
                        app.Resume = DatabaseHelper.CheckNullByteArray(reader, 9);
                        app.YearsExperience = DatabaseHelper.CheckNullByte(reader, 10);
                        app.Internship = DatabaseHelper.CheckNullBool(reader, 11);
                        app.Active = DatabaseHelper.CheckNullBool(reader, 12);

                        applicants.Add(app);
                    }
                }
            }

            foreach(applicant a in applicants)
            {
                AddFields(a, connection);
            }
            
        }

        private void AddFields(applicant app, SqlConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [Name]");
            sb.Append("FROM [careerfair].[field]");
            sb.Append("JOIN [careerfair].[applicant2field] ON [applicant2field].[Field] = [field].[FieldID]");
            sb.Append("JOIN [careerfair].[applicant] ON [applicant].[ApplicantID] = [applicant2field].[Applicant]");
            sb.Append("WHERE [careerfair].[applicant2field].[Applicant] = " + app.ApplicantID);
            String sql = sb.ToString();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        app.Fields.Add(DatabaseHelper.CheckNullString(reader,0));
                    }
                }
            }
        }

        public void Write(List<applicant> applicants)
        {
            throw new NotImplementedException();
        }

        private void Insert(List<string> fields)
        {

        }

        public void Insert(applicant applicant)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[applicant]([Password],[Email],[FirstName],[LastName],[University],[Alumni],[Profile],[SocialMedia],[Resume],[YearsExperience],[Internship],[Active])");
                string values = "VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NChar, 64).Value = applicant.Password;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 320).Value = applicant.Email;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = applicant.FirstName;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = applicant.LastName;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = applicant.University;
                    command.Parameters.Add("@param6", SqlDbType.Bit).Value = applicant.Alumni;
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, int.MaxValue).Value = applicant.Profile;
                    command.Parameters.Add("@param8", SqlDbType.NVarChar, int.MaxValue).Value = applicant.SocialMedia;
                    command.Parameters.Add("@param9", SqlDbType.VarBinary, int.MaxValue).Value = applicant.Resume;
                    command.Parameters.Add("@param10", SqlDbType.TinyInt).Value = applicant.YearsExperience;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = applicant.Internship;
                    command.Parameters.Add("@param12", SqlDbType.Bit).Value = applicant.Active;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(applicant applicant)
        {
            throw new NotImplementedException();
        }

        public void Update(applicant applicant)
        {
            throw new NotImplementedException();
        }
    }
}