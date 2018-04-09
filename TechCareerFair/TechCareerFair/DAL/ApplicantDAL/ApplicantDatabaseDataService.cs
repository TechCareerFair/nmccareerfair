using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Threading;

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
                
                InitApplicant(applicants, connection, -1, 0);
            }

            return applicants;
        }

        public List<applicant> Read(int startRow, int numberOfRows)
        {
            List<applicant> applicants = new List<applicant>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                InitApplicant(applicants, connection, startRow, numberOfRows);
            }

            return applicants;
        }

        public List<applicant> ReadAccountInfo()
        {
            List<applicant> applicants = new List<applicant>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [ApplicantID],[Password],[Email]");
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

                            applicants.Add(app);
                        }
                    }
                }
            }

            return applicants;
        }

        internal void UpdateApplicantProfile(applicant applicant, object serverPath, string resume)
        {
            throw new NotImplementedException();
        }

        private void InitApplicant(List<applicant> applicants, SqlConnection connection, int startRow, int numberOfRows)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [ApplicantID],[Password],[Email],[FirstName],[LastName],[University],[Alumni],[Profile],[SocialMedia],[Resume],[YearsExperience],[Internship],[Active]");
            sb.Append("FROM [careerfair].[applicant]");
            if (startRow >= 0)
            {
                sb.Append("ORDER BY [ApplicantID] ASC OFFSET " + startRow + " ROWS FETCH NEXT " + numberOfRows + " ROWS ONLY;");
            }
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
                        app.Resume = DatabaseHelper.CheckNullString(reader, 9);
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

        private void Insert(List<string> fields, int applicantID)
        {
            //insert field ID and applicant ID into new record of mapping table applicant2field
            using (TechCareerFair.DAL.Applicant2FieldDAL.Applicant2FieldDatabaseDataService ds = new Applicant2FieldDAL.Applicant2FieldDatabaseDataService())
            {
                List<applicant2field> a2fs = ds.Read();

                foreach (string name in fields)
                {
                    applicant2field a2f = new applicant2field();
                    int fieldIndex = DatabaseHelper.GetFieldIndex(name);

                    a2f.Applicant = applicantID;
                    a2f.Field = fieldIndex;
                    if (a2fs.Where(a => a.Field == fieldIndex).Where(a => a.Applicant == applicantID).Count() == 0)
                    {
                        ds.Insert(a2f);
                    }
                }
            }
        }

        public void Insert(applicant applicant)
        {
            int id = applicant.ApplicantID;

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[applicant]([Password],[Email],[FirstName],[LastName],[University],[Alumni],[Profile],[SocialMedia],[Resume],[YearsExperience],[Internship],[Active])");
                string values = "VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12); SELECT @ID = SCOPE_IDENTITY()";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NChar, 64).Value = (object)applicant.Password ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 320).Value = (object)applicant.Email ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = (object)applicant.FirstName ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = (object)applicant.LastName ?? DBNull.Value;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = (object)applicant.University ?? DBNull.Value;
                    command.Parameters.Add("@param6", SqlDbType.Bit).Value = (object)applicant.Alumni ?? DBNull.Value;
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.Profile ?? DBNull.Value;
                    command.Parameters.Add("@param8", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.SocialMedia ?? DBNull.Value;
                    command.Parameters.Add("@param9", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.Resume ?? DBNull.Value;
                    command.Parameters.Add("@param10", SqlDbType.TinyInt).Value = (object)applicant.YearsExperience ?? DBNull.Value;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = (object)applicant.Internship ?? DBNull.Value;
                    command.Parameters.Add("@param12", SqlDbType.Bit).Value = (object)applicant.Active ?? DBNull.Value;
                    command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();

                    id = (int)command.Parameters["@ID"].Value;
                }
            }
            Insert(applicant.Fields, id);
        }

        private void RemoveAll(int applicantID)
        {
            using (TechCareerFair.DAL.Applicant2FieldDAL.Applicant2FieldDatabaseDataService ds = new Applicant2FieldDAL.Applicant2FieldDatabaseDataService())
            {
                ds.RemoveAll(applicantID);
            }
        }

        public void Remove(applicant applicant, string serverPath)
        {
            RemoveAll(applicant.ApplicantID);

            if ((System.IO.File.Exists(serverPath + applicant.Resume)))
            {
                System.IO.File.Delete(serverPath + applicant.Resume);
            }

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [careerfair].[applicant]");
                sb.Append("WHERE [ApplicantID] = " + applicant.ApplicantID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Update(List<string> fields, int applicantID)
        {
            using (TechCareerFair.DAL.Applicant2FieldDAL.Applicant2FieldDatabaseDataService ds = new Applicant2FieldDAL.Applicant2FieldDatabaseDataService())
            {
                List<applicant2field> a2fs = ds.Read();

                foreach (string name in fields)
                {
                    applicant2field a2f = new applicant2field();
                    int fieldIndex = DatabaseHelper.GetFieldIndex(name);

                    a2f.Applicant = applicantID;
                    a2f.Field = fieldIndex;

                    ds.Update(a2f);
                }
            }
        }

        public void Update(applicant applicant, string serverPath, string oldResume)
        {
            RemoveAll(applicant.ApplicantID);
            Insert(applicant.Fields, applicant.ApplicantID);

            if (oldResume != applicant.Resume)
            {
                if ((System.IO.File.Exists(serverPath + oldResume)))
                {
                    System.IO.File.Delete(serverPath + oldResume);
                }
            }

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[applicant]");
                sb.Append("SET [Password] = @param1,[Email] = @param2,[FirstName] = @param3,[LastName] = @param4,[University] = @param5,[Alumni] = @param6,[Profile] = @param7,[SocialMedia] = @param8,[Resume] = @param9,[YearsExperience] = @param10,[Internship] = @param11,[Active] = @param12");
                sb.Append(" WHERE [ApplicantID] = " + applicant.ApplicantID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NChar, 64).Value = (object)applicant.Password ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 320).Value = (object)applicant.Email ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = (object)applicant.FirstName ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = (object)applicant.LastName ?? DBNull.Value;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = (object)applicant.University ?? DBNull.Value;
                    command.Parameters.Add("@param6", SqlDbType.Bit).Value = (object)applicant.Alumni ?? DBNull.Value;
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.Profile ?? DBNull.Value;
                    command.Parameters.Add("@param8", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.SocialMedia ?? DBNull.Value;
                    command.Parameters.Add("@param9", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.Resume ?? DBNull.Value;
                    command.Parameters.Add("@param10", SqlDbType.TinyInt).Value = (object)applicant.YearsExperience ?? DBNull.Value;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = (object)applicant.Internship ?? DBNull.Value;
                    command.Parameters.Add("@param12", SqlDbType.Bit).Value = (object)applicant.Active ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateApplicantProfile(applicant applicant, string serverPath, string oldResume)
        {
            RemoveAll(applicant.ApplicantID);
            Insert(applicant.Fields, applicant.ApplicantID);

            if (oldResume != applicant.Resume)
            {
                if ((System.IO.File.Exists(serverPath + oldResume)))
                {
                    System.IO.File.Delete(serverPath + oldResume);
                }
            }

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[applicant]");
                sb.Append("[Email] = @param2,[FirstName] = @param3,[LastName] = @param4,[University] = @param5,[Alumni] = @param6,[Profile] = @param7,[SocialMedia] = @param8,[Resume] = @param9,[YearsExperience] = @param10,[Internship] = @param11");
                sb.Append(" WHERE [ApplicantID] = " + applicant.ApplicantID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NChar, 64).Value = (object)applicant.Password ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 320).Value = (object)applicant.Email ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = (object)applicant.FirstName ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = (object)applicant.LastName ?? DBNull.Value;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = (object)applicant.University ?? DBNull.Value;
                    command.Parameters.Add("@param6", SqlDbType.Bit).Value = (object)applicant.Alumni ?? DBNull.Value;
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.Profile ?? DBNull.Value;
                    command.Parameters.Add("@param8", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.SocialMedia ?? DBNull.Value;
                    command.Parameters.Add("@param9", SqlDbType.NVarChar, int.MaxValue).Value = (object)applicant.Resume ?? DBNull.Value;
                    command.Parameters.Add("@param10", SqlDbType.TinyInt).Value = (object)applicant.YearsExperience ?? DBNull.Value;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = (object)applicant.Internship ?? DBNull.Value;
                    command.Parameters.Add("@param12", SqlDbType.Bit).Value = (object)applicant.Active ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}