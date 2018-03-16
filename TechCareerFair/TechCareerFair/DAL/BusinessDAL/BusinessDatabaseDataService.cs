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
                InitBusiness(connection, businesses, -1, 0);
            }

            return businesses;
        }

        public List<business> Read(int startRow, int numberOfRows)
        {
            List<business> businesses = new List<business>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                InitBusiness(connection, businesses, startRow, numberOfRows);
            }

            return businesses;
        }

        public List<business> ReadAccountInfo()
        {
            List<business> businesses = new List<business>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [BusinessID],[Password],[Email]");
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

                            businesses.Add(bus);
                        }
                    }
                }
            }

            return businesses;
        }

        private void InitBusiness(SqlConnection connection, List<business> businesses, int startRow, int numberOfRows)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [BusinessID],[Password],[Email],[BusinessName],[FirstName],[LastName],[Street],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active],[PreferEmail]");
            sb.Append("FROM [careerfair].[business]");
            if(startRow >= 0)
            {
                sb.Append("ORDER BY [BusinessID] ASC OFFSET " + startRow+" ROWS FETCH NEXT "+numberOfRows+" ROWS ONLY;");
            }
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
                        bus.Phone = DatabaseHelper.CheckNullString(reader, 7);
                        bus.Alumni = DatabaseHelper.CheckNullBool(reader, 8);
                        bus.NonProfit = DatabaseHelper.CheckNullBool(reader, 9);
                        bus.Outlet = DatabaseHelper.CheckNullBool(reader, 10);
                        bus.Display = DatabaseHelper.CheckNullBool(reader, 11);
                        bus.DisplayDescription = DatabaseHelper.CheckNullString(reader, 12);
                        bus.Attendees = DatabaseHelper.CheckNullByte(reader, 13);
                        bus.BusinessDescription = DatabaseHelper.CheckNullString(reader, 14);
                        bus.Website = DatabaseHelper.CheckNullString(reader, 15);
                        bus.SocialMedia = DatabaseHelper.CheckNullString(reader, 16);
                        bus.Photo = DatabaseHelper.CheckNullString(reader, 17);
                        bus.LocationPreference = DatabaseHelper.CheckNullString(reader, 18);
                        bus.ContactMe = DatabaseHelper.CheckNullBool(reader, 19);
                        bus.Approved = DatabaseHelper.CheckNullBool(reader, 20);
                        bus.Active = DatabaseHelper.CheckNullBool(reader, 21);
                        bus.PreferEmail = DatabaseHelper.CheckNullBool(reader, 22);

                        businesses.Add(bus);
                    }
                }

                foreach(business b in businesses)
                {
                    AddFields(b, connection);
                    AddPositions(b, connection);
                    AddZip(b, connection);
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

        private void AddZip(business bus, SqlConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [ZipCode], [City], [State]");
            sb.Append("FROM [dbo].[zipcode]");
            sb.Append("WHERE [dbo].[zipcode].[Business] = " + bus.BusinessID);
            String sql = sb.ToString();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bus.Zip = DatabaseHelper.CheckNullString(reader, 0);
                        bus.City = DatabaseHelper.CheckNullString(reader, 1);
                        bus.State = DatabaseHelper.CheckNullString(reader, 2);
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

        private void Insert(List<string> fields, int businessID)
        {
            //insert field ID and business ID into new record of mapping table business2field
            using (TechCareerFair.DAL.Business2FieldDAL.Business2FieldDatabaseDataService ds = new Business2FieldDAL.Business2FieldDatabaseDataService())
            {
                List<business2field> b2fs = ds.Read();

                foreach (string name in fields)
                {
                    business2field b2f = new business2field();
                    int fieldIndex = DatabaseHelper.GetFieldIndex(name);

                    b2f.Business = businessID;
                    b2f.Field = fieldIndex;
                    if(b2fs.Where(b => b.Field == fieldIndex).Where(b => b.Business == businessID).Count() == 0)
                    {
                        ds.Insert(b2f);
                    }
                }
            }
        }

        private void Insert(List<position> positions)
        {
            using (TechCareerFair.DAL.PositionDAL.PositionDatabaseDataService ds = new PositionDAL.PositionDatabaseDataService())
            {
                foreach (position p in positions)
                {
                    ds.Insert(p);
                }
            }
        }

        private void Insert(string zipcode, string city, string state, int businessID)
        {
            Zipcode zip = new Zipcode();
            zip.ZipCode = zipcode;
            zip.City = city;
            zip.State = state;
            zip.Business = businessID;

            using (TechCareerFair.DAL.ZipDAL.ZipDatabaseDataService ds = new ZipDAL.ZipDatabaseDataService())
            {
                ds.Insert(zip);
            }
        }

        public void Insert(business business)
        {
            Insert(business.Fields, business.BusinessID);
            Insert(business.Positions);
            Insert(business.Zip, business.City, business.State, business.BusinessID);

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[business]([Password],[Email],[BusinessName],[FirstName],[LastName],[Street],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active],[PreferEmail])");
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
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, 20).Value = business.Phone;
                    command.Parameters.Add("@param8", SqlDbType.Bit).Value = business.Alumni;
                    command.Parameters.Add("@param9", SqlDbType.Bit).Value = business.NonProfit;
                    command.Parameters.Add("@param10", SqlDbType.Bit).Value = business.Outlet;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = business.Display;
                    command.Parameters.Add("@param12", SqlDbType.NVarChar, int.MaxValue).Value = business.DisplayDescription;
                    command.Parameters.Add("@param13", SqlDbType.TinyInt).Value = business.Attendees;
                    command.Parameters.Add("@param14", SqlDbType.NVarChar, int.MaxValue).Value = business.BusinessDescription;
                    command.Parameters.Add("@param15", SqlDbType.NVarChar, int.MaxValue).Value = business.Website;
                    command.Parameters.Add("@param16", SqlDbType.NVarChar, int.MaxValue).Value = business.SocialMedia;
                    command.Parameters.Add("@param17", SqlDbType.NVarChar, int.MaxValue).Value = business.Photo;
                    command.Parameters.Add("@param18", SqlDbType.NVarChar, int.MaxValue).Value = business.LocationPreference;
                    command.Parameters.Add("@param19", SqlDbType.Bit).Value = business.ContactMe;
                    command.Parameters.Add("@param20", SqlDbType.Bit).Value = business.Approved;
                    command.Parameters.Add("@param21", SqlDbType.Bit).Value = business.Active;
                    command.Parameters.Add("@param22", SqlDbType.Bit).Value = business.PreferEmail;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Remove(List<string> fields, int businessID)
        {
            using (TechCareerFair.DAL.Business2FieldDAL.Business2FieldDatabaseDataService ds = new Business2FieldDAL.Business2FieldDatabaseDataService())
            {
                List<business2field> b2fs = ds.Read();

                foreach (string name in fields)
                {
                    business2field b2f = new business2field();
                    int fieldIndex = DatabaseHelper.GetFieldIndex(name);

                    b2f.Business = businessID;
                    b2f.Field = fieldIndex;

                    ds.Remove(b2f);
                }
            }
        }

        private void Remove(List<position> positions)
        {
            using (TechCareerFair.DAL.PositionDAL.PositionDatabaseDataService ds = new PositionDAL.PositionDatabaseDataService())
            {
                foreach (position p in positions)
                {
                    ds.Remove(p);
                }
            }
        }

        private void Remove(string zipcode, string city, string state, int businessID)
        {
            Zipcode zip = new Zipcode();
            zip.ZipCode = zipcode;
            zip.City = city;
            zip.State = state;
            zip.Business = businessID;

            using (TechCareerFair.DAL.ZipDAL.ZipDatabaseDataService ds = new ZipDAL.ZipDatabaseDataService())
            {
                ds.Remove(zip);
            }
        }

        public void Remove(business business)
        {
            Remove(business.Fields, business.BusinessID);
            Remove(business.Positions);
            Remove(business.Zip, business.City, business.State, business.BusinessID);

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [careerfair].[business]");
                sb.Append("WHERE [BusinessID] = " + business.BusinessID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Update(List<string> fields, int businessID)
        {
            using (TechCareerFair.DAL.Business2FieldDAL.Business2FieldDatabaseDataService ds = new Business2FieldDAL.Business2FieldDatabaseDataService())
            {
                List<business2field> b2fs = ds.Read();

                foreach (string name in fields)
                {
                    business2field b2f = new business2field();
                    int fieldIndex = DatabaseHelper.GetFieldIndex(name);

                    b2f.Business = businessID;
                    b2f.Field = fieldIndex;

                    ds.Update(b2f);
                }
            }
        }

        private void Update(List<position> positions)
        {
            using (TechCareerFair.DAL.PositionDAL.PositionDatabaseDataService ds = new PositionDAL.PositionDatabaseDataService())
            {
                foreach (position p in positions)
                {
                    ds.Update(p);
                }
            }
        }

        private void Update(string zipcode, string city, string state, int businessID)
        {
            Zipcode zip = new Zipcode();
            zip.ZipCode = zipcode;
            zip.City = city;
            zip.State = state;
            zip.Business = businessID;

            using (TechCareerFair.DAL.ZipDAL.ZipDatabaseDataService ds = new ZipDAL.ZipDatabaseDataService())
            {
                ds.Update(zip);
            }
        }

        public void Update(business business)
        {
            Update(business.Fields, business.BusinessID);
            Update(business.Positions);
            Update(business.Zip, business.City, business.State, business.BusinessID);

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[applicant]");
                sb.Append("SET [Password] = @param1,[Email] = @param2,[BusinessName] = @param3,[FirstName] = @param4,[LastName] = @param5,[Street] = @param6,[Phone] = @param7,[Alumni] = @param8,[NonProfit] = @param9,[Outlet] = @param10,[Display] = @param11,[DisplayDescription] = @param12,[Attendees] = @param13,[BusinessDescription] = @param14,[Website] = @param15,[SocialMedia] = @param16,[Photo] = @param17,[LocationPreference] = @param18,[ContactMe] = @param19,[Approved] = @param20,[Active] = @param21,[PreferEmail] = @param22");
                sb.Append("WHERE [BusinessID] = " + business.BusinessID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NChar, 64).Value = business.Password;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 320).Value = business.Email;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = business.BusinessName;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = business.FirstName;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = business.LastName;
                    command.Parameters.Add("@param6", SqlDbType.NVarChar, 50).Value = business.Street;
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, 20).Value = business.Phone;
                    command.Parameters.Add("@param8", SqlDbType.Bit).Value = business.Alumni;
                    command.Parameters.Add("@param9", SqlDbType.Bit).Value = business.NonProfit;
                    command.Parameters.Add("@param10", SqlDbType.Bit).Value = business.Outlet;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = business.Display;
                    command.Parameters.Add("@param12", SqlDbType.NVarChar, int.MaxValue).Value = business.DisplayDescription;
                    command.Parameters.Add("@param13", SqlDbType.TinyInt).Value = business.Attendees;
                    command.Parameters.Add("@param14", SqlDbType.NVarChar, int.MaxValue).Value = business.BusinessDescription;
                    command.Parameters.Add("@param15", SqlDbType.NVarChar, int.MaxValue).Value = business.Website;
                    command.Parameters.Add("@param16", SqlDbType.NVarChar, int.MaxValue).Value = business.SocialMedia;
                    command.Parameters.Add("@param17", SqlDbType.NVarChar, int.MaxValue).Value = business.Photo;
                    command.Parameters.Add("@param18", SqlDbType.NVarChar, int.MaxValue).Value = business.LocationPreference;
                    command.Parameters.Add("@param19", SqlDbType.Bit).Value = business.ContactMe;
                    command.Parameters.Add("@param20", SqlDbType.Bit).Value = business.Approved;
                    command.Parameters.Add("@param21", SqlDbType.Bit).Value = business.Active;
                    command.Parameters.Add("@param22", SqlDbType.Bit).Value = business.PreferEmail;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}