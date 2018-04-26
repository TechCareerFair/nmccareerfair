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

        public business Read(int id)
        {
            business business = null;
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [BusinessID],[Email],[BusinessName],[FirstName],[LastName],[Street],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active],[PreferEmail]");
                sb.Append("FROM [careerfair].[business]");
                sb.Append("WHERE [BusinessID] = '" + id+"'");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            business = new business();

                            business.BusinessID = DatabaseHelper.CheckNullInt(reader, 0);
                            business.Email = DatabaseHelper.CheckNullString(reader, 1);
                            business.BusinessName = DatabaseHelper.CheckNullString(reader, 2);
                            business.FirstName = DatabaseHelper.CheckNullString(reader, 3);
                            business.LastName = DatabaseHelper.CheckNullString(reader, 4);
                            business.Street = DatabaseHelper.CheckNullString(reader, 5);
                            business.Phone = DatabaseHelper.CheckNullString(reader, 6);
                            business.Alumni = DatabaseHelper.CheckNullBool(reader, 7);
                            business.NonProfit = DatabaseHelper.CheckNullBool(reader, 8);
                            business.Outlet = DatabaseHelper.CheckNullBool(reader, 9);
                            business.Display = DatabaseHelper.CheckNullBool(reader, 10);
                            business.DisplayDescription = DatabaseHelper.CheckNullString(reader, 11);
                            business.Attendees = DatabaseHelper.CheckNullByte(reader, 12);
                            business.BusinessDescription = DatabaseHelper.CheckNullString(reader, 13);
                            business.Website = DatabaseHelper.CheckNullString(reader, 14);
                            business.SocialMedia = DatabaseHelper.CheckNullString(reader, 15);
                            business.Photo = DatabaseHelper.CheckNullString(reader, 16);
                            business.LocationPreference = DatabaseHelper.CheckNullString(reader, 17);
                            business.ContactMe = DatabaseHelper.CheckNullBool(reader, 18);
                            business.Approved = DatabaseHelper.CheckNullBool(reader, 19);
                            business.Active = DatabaseHelper.CheckNullBool(reader, 20);
                            business.PreferEmail = DatabaseHelper.CheckNullBool(reader, 21);
                        }
                    }
                        AddFields(business, connection);
                        AddPositions(business, connection);
                        AddZip(business, connection);
                }
            }

            return business;
        }

        public business Read(string email)
        {
            business business = null;
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [BusinessID],[Email],[BusinessName],[FirstName],[LastName],[Street],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active],[PreferEmail]");
                sb.Append("FROM [careerfair].[business]");
                sb.Append("WHERE [Email] = '" + email + "'");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            business = new business();

                            business.BusinessID = DatabaseHelper.CheckNullInt(reader, 0);
                            business.Email = DatabaseHelper.CheckNullString(reader, 1);
                            business.BusinessName = DatabaseHelper.CheckNullString(reader, 2);
                            business.FirstName = DatabaseHelper.CheckNullString(reader, 3);
                            business.LastName = DatabaseHelper.CheckNullString(reader, 4);
                            business.Street = DatabaseHelper.CheckNullString(reader, 5);
                            business.Phone = DatabaseHelper.CheckNullString(reader, 6);
                            business.Alumni = DatabaseHelper.CheckNullBool(reader, 7);
                            business.NonProfit = DatabaseHelper.CheckNullBool(reader, 8);
                            business.Outlet = DatabaseHelper.CheckNullBool(reader, 9);
                            business.Display = DatabaseHelper.CheckNullBool(reader, 10);
                            business.DisplayDescription = DatabaseHelper.CheckNullString(reader, 11);
                            business.Attendees = DatabaseHelper.CheckNullByte(reader, 12);
                            business.BusinessDescription = DatabaseHelper.CheckNullString(reader, 13);
                            business.Website = DatabaseHelper.CheckNullString(reader, 14);
                            business.SocialMedia = DatabaseHelper.CheckNullString(reader, 15);
                            business.Photo = DatabaseHelper.CheckNullString(reader, 16);
                            business.LocationPreference = DatabaseHelper.CheckNullString(reader, 17);
                            business.ContactMe = DatabaseHelper.CheckNullBool(reader, 18);
                            business.Approved = DatabaseHelper.CheckNullBool(reader, 19);
                            business.Active = DatabaseHelper.CheckNullBool(reader, 20);
                            business.PreferEmail = DatabaseHelper.CheckNullBool(reader, 21);
                        }
                    }
                    AddFields(business, connection);
                    AddPositions(business, connection);
                    AddZip(business, connection);
                }
            }

            return business;
        }

        public List<business> ReadForAdmin()
        {
            List<business> businesses = new List<business>();
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [BusinessID],[Email],[BusinessName],[Phone],[ContactMe],[Approved],[Active],[PreferEmail]");
                sb.Append("FROM [careerfair].[business]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            business business = new business();

                            business.BusinessID = DatabaseHelper.CheckNullInt(reader, 0);
                            business.Email = DatabaseHelper.CheckNullString(reader, 1);
                            business.BusinessName = DatabaseHelper.CheckNullString(reader, 2);
                            business.Phone = DatabaseHelper.CheckNullString(reader, 3);
                            business.ContactMe = DatabaseHelper.CheckNullBool(reader, 4);
                            business.Approved = DatabaseHelper.CheckNullBool(reader, 5);
                            business.Active = DatabaseHelper.CheckNullBool(reader, 6);
                            business.PreferEmail = DatabaseHelper.CheckNullBool(reader, 7);

                            businesses.Add(business);
                        }
                    }
                }
            }

            return businesses;
        }

        public List<business> ReadForSearch()
        {
            List<business> businesses = new List<business>();
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [BusinessID],[Email],[BusinessName],[Website],[Phone],[Approved],[Active]");
                sb.Append("FROM [careerfair].[business]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            business business = new business();

                            business.BusinessID = DatabaseHelper.CheckNullInt(reader, 0);
                            business.Email = DatabaseHelper.CheckNullString(reader, 1);
                            business.BusinessName = DatabaseHelper.CheckNullString(reader, 2);
                            business.Website = DatabaseHelper.CheckNullString(reader, 3);
                            business.Phone = DatabaseHelper.CheckNullString(reader, 4);
                            business.Approved = DatabaseHelper.CheckNullBool(reader, 5);
                            business.Active = DatabaseHelper.CheckNullBool(reader, 6);

                            businesses.Add(business);
                        }
                    }
                    foreach (business b in businesses)
                    {
                        AddFields(b, connection);
                        AddPositions(b, connection);
                    }
                }
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
                            bus.Email = DatabaseHelper.CheckNullString(reader, 1);

                            businesses.Add(bus);
                        }
                    }
                }
            }

            return businesses;
        }

        public bool ReadIsActive(string email)
        {
            bool isActive = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [Active]");
            sb.Append("FROM [careerfair].[business]");
            sb.Append("WHERE [Email] = '" + email + "'");

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

        public bool ReadIsApproved(string email)
        {
            bool isApproved = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [Approved]");
            sb.Append("FROM [careerfair].[business]");
            sb.Append("WHERE [Email] = '" + email + "'");

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
                            isApproved = DatabaseHelper.CheckNullBool(reader, 0);
                        }
                    }
                }
            }

            return isApproved;
        }

        private void InitBusiness(SqlConnection connection, List<business> businesses, int startRow, int numberOfRows)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [BusinessID],[Email],[BusinessName],[FirstName],[LastName],[Street],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active],[PreferEmail]");
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
                        bus.Email = DatabaseHelper.CheckNullString(reader, 1);
                        bus.BusinessName = DatabaseHelper.CheckNullString(reader, 2);
                        bus.FirstName = DatabaseHelper.CheckNullString(reader, 3);
                        bus.LastName = DatabaseHelper.CheckNullString(reader, 4);
                        bus.Street = DatabaseHelper.CheckNullString(reader, 5);
                        bus.Phone = DatabaseHelper.CheckNullString(reader, 6);
                        bus.Alumni = DatabaseHelper.CheckNullBool(reader, 7);
                        bus.NonProfit = DatabaseHelper.CheckNullBool(reader, 8);
                        bus.Outlet = DatabaseHelper.CheckNullBool(reader, 9);
                        bus.Display = DatabaseHelper.CheckNullBool(reader, 10);
                        bus.DisplayDescription = DatabaseHelper.CheckNullString(reader, 11);
                        bus.Attendees = DatabaseHelper.CheckNullByte(reader, 12);
                        bus.BusinessDescription = DatabaseHelper.CheckNullString(reader, 13);
                        bus.Website = DatabaseHelper.CheckNullString(reader, 14);
                        bus.SocialMedia = DatabaseHelper.CheckNullString(reader, 15);
                        bus.Photo = DatabaseHelper.CheckNullString(reader, 16);
                        bus.LocationPreference = DatabaseHelper.CheckNullString(reader, 17);
                        bus.ContactMe = DatabaseHelper.CheckNullBool(reader, 18);
                        bus.Approved = DatabaseHelper.CheckNullBool(reader, 19);
                        bus.Active = DatabaseHelper.CheckNullBool(reader, 20);
                        bus.PreferEmail = DatabaseHelper.CheckNullBool(reader, 21);

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

        public BusinessViewModel GetAccountInfoBy(string Email)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [PasswordHash]");
            sb.Append("FROM [dbo].[AspNetUsers]");
            sb.Append("WHERE [Email] = '" + Email + "'");

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
                            bus.Password = DatabaseHelper.CheckNullString(reader, 0);
                        }
                    }
                }
            }

            return bus;
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
            sb.Append("SELECT [PositionID], [Name], [Description], [Website], [Internship]");
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

                        pos.PositionID = DatabaseHelper.CheckNullInt(reader, 0);
                        pos.Name = DatabaseHelper.CheckNullString(reader, 1);
                        pos.Description = DatabaseHelper.CheckNullString(reader, 2);
                        pos.Website = DatabaseHelper.CheckNullString(reader, 3);
                        pos.Internship = DatabaseHelper.CheckNullBool(reader, 4);

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
            int id = business.BusinessID;
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[business]([Email],[BusinessName],[FirstName],[LastName],[Street],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active],[PreferEmail])");
                string values = "VALUES(@param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15, @param16, @param17, @param18, @param19, @param20, @param21, @param22); SELECT @ID = SCOPE_IDENTITY()";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 128).Value = (object)business.Email ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = (object)business.BusinessName ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = (object)business.FirstName ?? DBNull.Value;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = (object)business.LastName ?? DBNull.Value;
                    command.Parameters.Add("@param6", SqlDbType.NVarChar, 50).Value = (object)business.Street ?? DBNull.Value;
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, 20).Value = (object)business.Phone ?? DBNull.Value;
                    command.Parameters.Add("@param8", SqlDbType.Bit).Value = (object)business.Alumni ?? DBNull.Value;
                    command.Parameters.Add("@param9", SqlDbType.Bit).Value = (object)business.NonProfit ?? DBNull.Value;
                    command.Parameters.Add("@param10", SqlDbType.Bit).Value = (object)business.Outlet ?? DBNull.Value;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = (object)business.Display ?? DBNull.Value;
                    command.Parameters.Add("@param12", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.DisplayDescription ?? DBNull.Value;
                    command.Parameters.Add("@param13", SqlDbType.TinyInt).Value = (object)business.Attendees ?? DBNull.Value;
                    command.Parameters.Add("@param14", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.BusinessDescription ?? DBNull.Value;
                    command.Parameters.Add("@param15", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.Website ?? DBNull.Value;
                    command.Parameters.Add("@param16", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.SocialMedia ?? DBNull.Value;
                    command.Parameters.Add("@param17", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.Photo ?? DBNull.Value;
                    command.Parameters.Add("@param18", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.LocationPreference ?? DBNull.Value;
                    command.Parameters.Add("@param19", SqlDbType.Bit).Value = (object)business.ContactMe ?? DBNull.Value;
                    command.Parameters.Add("@param20", SqlDbType.Bit).Value = (object)business.Approved ?? DBNull.Value;
                    command.Parameters.Add("@param21", SqlDbType.Bit).Value = (object)business.Active ?? DBNull.Value;
                    command.Parameters.Add("@param22", SqlDbType.Bit).Value = (object)business.PreferEmail ?? DBNull.Value;
                    command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();

                    id = (int)command.Parameters["@ID"].Value;
                }
            }
            Insert(business.Fields, id);
            Insert(business.Positions);
            Insert(business.Zip, business.City, business.State, id);
        }

        private void RemoveAll(int businessID)
        {
            using (TechCareerFair.DAL.Business2FieldDAL.Business2FieldDatabaseDataService ds = new Business2FieldDAL.Business2FieldDatabaseDataService())
            {
                 ds.RemoveAll(businessID);
            }
        }

        private void RemoveUser(business business, SqlConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM [dbo].[AspNetUsers]");
            sb.Append("WHERE [Email] = '" + business.Email + "'");
            String sql = sb.ToString();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
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
                ds.Remove(businessID);
            }
        }

        public void Remove(business business, string serverPath)
        {
            //Remove(business.Positions);
            RemoveAll(business.BusinessID);
            Remove(business.Zip, business.City, business.State, business.BusinessID);

            if ((System.IO.File.Exists(serverPath + business.Photo)))
            {
                System.IO.File.Delete(serverPath + business.Photo);
            }

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                RemoveUser(business, connection);

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
                ds.Update(zip, businessID);
            }
        }

        public void Update(business business, string serverPath, string oldImagePath)
        {
            RemoveAll(business.BusinessID);
            Insert(business.Fields, business.BusinessID);

            Update(business.Positions);
            Update(business.Zip, business.City, business.State, business.BusinessID);

            if(oldImagePath != business.Photo)
            {
                if ((System.IO.File.Exists(serverPath + oldImagePath)))
                {
                    System.IO.File.Delete(serverPath + oldImagePath);
                }
            }

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[business]");
                sb.Append("SET [Email] = @param2,[BusinessName] = @param3,[FirstName] = @param4,[LastName] = @param5,[Street] = @param6,[Phone] = @param7,[Alumni] = @param8,[NonProfit] = @param9,[Outlet] = @param10,[Display] = @param11,[DisplayDescription] = @param12,[Attendees] = @param13,[BusinessDescription] = @param14,[Website] = @param15,[SocialMedia] = @param16,[Photo] = @param17,[LocationPreference] = @param18,[ContactMe] = @param19,[Approved] = @param20,[Active] = @param21,[PreferEmail] = @param22 ");
                sb.Append("WHERE [BusinessID] = " + business.BusinessID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 128).Value = (object)business.Email ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = (object)business.BusinessName ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = (object)business.FirstName ?? DBNull.Value;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = (object)business.LastName ?? DBNull.Value;
                    command.Parameters.Add("@param6", SqlDbType.NVarChar, 50).Value = (object)business.Street ?? DBNull.Value;
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, 20).Value = (object)business.Phone ?? DBNull.Value;
                    command.Parameters.Add("@param8", SqlDbType.Bit).Value = (object)business.Alumni ?? DBNull.Value;
                    command.Parameters.Add("@param9", SqlDbType.Bit).Value = (object)business.NonProfit ?? DBNull.Value;
                    command.Parameters.Add("@param10", SqlDbType.Bit).Value = (object)business.Outlet ?? DBNull.Value;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = (object)business.Display ?? DBNull.Value;
                    command.Parameters.Add("@param12", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.DisplayDescription ?? DBNull.Value;
                    command.Parameters.Add("@param13", SqlDbType.TinyInt).Value = (object)business.Attendees ?? DBNull.Value;
                    command.Parameters.Add("@param14", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.BusinessDescription ?? DBNull.Value;
                    command.Parameters.Add("@param15", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.Website ?? DBNull.Value;
                    command.Parameters.Add("@param16", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.SocialMedia ?? DBNull.Value;
                    command.Parameters.Add("@param17", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.Photo ?? DBNull.Value;
                    command.Parameters.Add("@param18", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.LocationPreference ?? DBNull.Value;
                    command.Parameters.Add("@param19", SqlDbType.Bit).Value = (object)business.ContactMe ?? DBNull.Value;
                    command.Parameters.Add("@param20", SqlDbType.Bit).Value = (object)business.Approved ?? DBNull.Value;
                    command.Parameters.Add("@param21", SqlDbType.Bit).Value = (object)business.Active ?? DBNull.Value;
                    command.Parameters.Add("@param22", SqlDbType.Bit).Value = (object)business.PreferEmail ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBusinessProfile(business business, string serverPath, string oldImagePath)
        {
            RemoveAll(business.BusinessID);
            Insert(business.Fields, business.BusinessID);

            Update(business.Positions);
            Update(business.Zip, business.City, business.State, business.BusinessID);

            if (oldImagePath != business.Photo)
            {
                if ((System.IO.File.Exists(serverPath + oldImagePath)))
                {
                    System.IO.File.Delete(serverPath + oldImagePath);
                }
            }

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[business]");
                sb.Append("SET [BusinessName] = @param3,[FirstName] = @param4,[LastName] = @param5,[Street] = @param6,[Phone] = @param7,[Alumni] = @param8,[NonProfit] = @param9,[Outlet] = @param10,[Display] = @param11,[DisplayDescription] = @param12,[Attendees] = @param13,[BusinessDescription] = @param14,[Website] = @param15,[SocialMedia] = @param16,[Photo] = @param17,[LocationPreference] = @param18,[ContactMe] = @param19,[PreferEmail] = @param22 ");
                sb.Append("WHERE [BusinessID] = " + business.BusinessID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = (object)business.BusinessName ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, 50).Value = (object)business.FirstName ?? DBNull.Value;
                    command.Parameters.Add("@param5", SqlDbType.NVarChar, 50).Value = (object)business.LastName ?? DBNull.Value;
                    command.Parameters.Add("@param6", SqlDbType.NVarChar, 50).Value = (object)business.Street ?? DBNull.Value;
                    command.Parameters.Add("@param7", SqlDbType.NVarChar, 20).Value = (object)business.Phone ?? DBNull.Value;
                    command.Parameters.Add("@param8", SqlDbType.Bit).Value = (object)business.Alumni ?? DBNull.Value;
                    command.Parameters.Add("@param9", SqlDbType.Bit).Value = (object)business.NonProfit ?? DBNull.Value;
                    command.Parameters.Add("@param10", SqlDbType.Bit).Value = (object)business.Outlet ?? DBNull.Value;
                    command.Parameters.Add("@param11", SqlDbType.Bit).Value = (object)business.Display ?? DBNull.Value;
                    command.Parameters.Add("@param12", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.DisplayDescription ?? DBNull.Value;
                    command.Parameters.Add("@param13", SqlDbType.TinyInt).Value = (object)business.Attendees ?? DBNull.Value;
                    command.Parameters.Add("@param14", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.BusinessDescription ?? DBNull.Value;
                    command.Parameters.Add("@param15", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.Website ?? DBNull.Value;
                    command.Parameters.Add("@param16", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.SocialMedia ?? DBNull.Value;
                    command.Parameters.Add("@param17", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.Photo ?? DBNull.Value;
                    command.Parameters.Add("@param18", SqlDbType.NVarChar, int.MaxValue).Value = (object)business.LocationPreference ?? DBNull.Value;
                    command.Parameters.Add("@param19", SqlDbType.Bit).Value = (object)business.ContactMe ?? DBNull.Value;
                    command.Parameters.Add("@param20", SqlDbType.Bit).Value = (object)business.Approved ?? DBNull.Value;
                    command.Parameters.Add("@param21", SqlDbType.Bit).Value = (object)business.Active ?? DBNull.Value;
                    command.Parameters.Add("@param22", SqlDbType.Bit).Value = (object)business.PreferEmail ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}