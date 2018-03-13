using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public class BusinessDatabaseDataService : IBusinessDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<business> Read()
        {
            List<business> businesses = new List<business>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                InitBusiness(connection, businesses);
            }

            return businesses;
        }

        private void InitBusiness(SqlConnection connection, List<business> businesses)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [BusinessID],[Password],[Email],[BusinessName],[FirstName],[LastName],[Street],[ZipCodeID],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active]");
            sb.Append("FROM [careerfair].[business]");
            String sql = sb.ToString();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        business bus = new business();

                        bus.BusinessID = DatabaseHelper.CheckNullInt(reader, 0);
                        bus.Password = DatabaseHelper.CheckNullString(reader, 1);
                        bus.Email = DatabaseHelper.CheckNullString(reader, 2);
                        bus.BusinessName = DatabaseHelper.CheckNullString(reader, 3);
                        bus.FirstName = DatabaseHelper.CheckNullString(reader, 4);
                        bus.LastName = DatabaseHelper.CheckNullString(reader, 5);
                        bus.Street = DatabaseHelper.CheckNullString(reader, 6);
                        bus.Zip = DatabaseHelper.CheckNullInt(reader, 7);
                        bus.Phone = DatabaseHelper.CheckNullString(reader, 8);
                        bus.Alumni = DatabaseHelper.CheckNullBool(reader, 9);
                        bus.NonProfit = DatabaseHelper.CheckNullBool(reader, 10);
                        bus.Outlet = DatabaseHelper.CheckNullBool(reader, 11);
                        bus.Display = DatabaseHelper.CheckNullBool(reader, 12);
                        bus.DisplayDescription = DatabaseHelper.CheckNullString(reader, 13);
                        bus.Attendees = DatabaseHelper.CheckNullByte(reader, 14);
                        bus.BusinessDescription = DatabaseHelper.CheckNullString(reader, 15);
                        bus.Website = DatabaseHelper.CheckNullString(reader, 16);
                        bus.SocialMedia = DatabaseHelper.CheckNullString(reader, 17);
                        bus.Photo = DatabaseHelper.CheckNullByteArray(reader, 18);
                        bus.LocationPreference = DatabaseHelper.CheckNullString(reader, 19);
                        bus.ContactMe = DatabaseHelper.CheckNullBool(reader, 20);
                        bus.Approved = DatabaseHelper.CheckNullBool(reader, 21);
                        bus.Active = DatabaseHelper.CheckNullBool(reader, 22);

                        businesses.Add(bus);
                    }
                }

                foreach(business b in businesses)
                {
                    AddFields(b, connection);
                    AddPositions(b, connection);
                }
            }
        }

        private void AddFields(business bus, SqlConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [Name]");
            sb.Append("FROM [careerfair].[field]");
            sb.Append("JOIN [careerfair].[business2field] ON [business2field].[Field] = [field].[FieldID]");
            sb.Append("JOIN [careerfair].[business] ON [business].[BusinessID] = [business2field].[Business]");
            sb.Append("WHERE [careerfair].[business2field].[Business] = " + bus.BusinessID);
            String sql = sb.ToString();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bus.Fields.Add(DatabaseHelper.CheckNullString(reader, 0));
                    }
                }
            }
        }

        private void AddPositions(business bus, SqlConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [Name], [Description], [Website], [Internship]");
            sb.Append("FROM [careerfair].[position]");
            sb.Append("WHERE [careerfair].[position].[Business] = " + bus.BusinessID);
            String sql = sb.ToString();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        position pos = new position();

                        pos.Name = DatabaseHelper.CheckNullString(reader, 0);
                        pos.Description = DatabaseHelper.CheckNullString(reader, 1);
                        pos.Website = DatabaseHelper.CheckNullString(reader, 2);
                        pos.Internship = DatabaseHelper.CheckNullBool(reader, 3);

                        bus.Positions.Add(pos);
                    }
                }
            }
        }

        public void Write(List<business> businesses)
        {
            throw new NotImplementedException();
        }

        private void Insert(List<string> fields)
        {

        }

        private void Insert(List<position> positions)
        {

        }

        public void Insert(business business)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[business]([Password],[Email],[BusinessName],[FirstName],[LastName],[Street],[ZipCodeID],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active])");
                string values = "VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15, @param16, @param17, @param18, @param19, @param20, @param21, @param22)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NChar, 64).Value = business.Password;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 320).Value = business.Email;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = business.BusinessName;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = business.FirstName;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = business.LastName;
                    command.Parameters.Add("@param6", SqlDbType.NVarChar, 50).Value = business.Street;
                    command.Parameters.Add("@param7", SqlDbType.Int).Value = business.Zip;
                    command.Parameters.Add("@param8", SqlDbType.NVarChar, 20).Value = business.Phone;
                    command.Parameters.Add("@param9", SqlDbType.Bit).Value = business.Alumni;
                    command.Parameters.Add("@param10", SqlDbType.Bit).Value = business.NonProfit;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = business.Outlet;
                    command.Parameters.Add("@param12", SqlDbType.Bit).Value = business.Display;
                    command.Parameters.Add("@param13", SqlDbType.NVarChar, int.MaxValue).Value = business.DisplayDescription;
                    command.Parameters.Add("@param14", SqlDbType.TinyInt).Value = business.Attendees;
                    command.Parameters.Add("@param15", SqlDbType.NVarChar, int.MaxValue).Value = business.BusinessDescription;
                    command.Parameters.Add("@param16", SqlDbType.NVarChar, int.MaxValue).Value = business.Website;
                    command.Parameters.Add("@param17", SqlDbType.NVarChar, int.MaxValue).Value = business.SocialMedia;
                    command.Parameters.Add("@param18", SqlDbType.VarBinary, int.MaxValue).Value = business.Photo;
                    command.Parameters.Add("@param19", SqlDbType.NVarChar, int.MaxValue).Value = business.LocationPreference;
                    command.Parameters.Add("@param20", SqlDbType.Bit).Value = business.ContactMe;
                    command.Parameters.Add("@param21", SqlDbType.Bit).Value = business.Approved;
                    command.Parameters.Add("@param22", SqlDbType.Bit).Value = business.Active;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(business business)
        {
            throw new NotImplementedException();
        }

        public void Update(business business)
        {
            throw new NotImplementedException();
        }
    }
}