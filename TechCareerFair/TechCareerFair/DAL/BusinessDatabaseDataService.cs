using System;
using System.Collections.Generic;
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
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT TOP (1000)[BusinessID],[Password],[Email],[BusinessName],[FirstName],[LastName],[Street],[ZipCodeID],[Phone],[Alumni],[NonProfit],[Outlet],[Display],[DisplayDescription],[Attendees],[BusinessDescription],[Website],[SocialMedia],[Photo],[LocationPreference],[ContactMe],[Approved],[Active]");
                sb.Append("FROM [careerfair].[business]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            business bus = new business();

                            bus.BusinessID = (int)CheckNullInt(reader, 0);
                            bus.Password = CheckNullString(reader, 1);
                            bus.Email = CheckNullString(reader, 2);
                            bus.BusinessName = CheckNullString(reader, 3);
                            bus.FirstName = CheckNullString(reader, 4);
                            bus.LastName = CheckNullString(reader, 5);
                            bus.Street = CheckNullString(reader, 6);
                            bus.Zip = CheckNullInt(reader, 7);
                            bus.Phone = CheckNullString(reader, 8);
                            bus.Alumni = (bool)CheckNullBool(reader, 9);
                            bus.NonProfit = (bool)CheckNullBool(reader, 10);
                            bus.Outlet = (bool)CheckNullBool(reader, 11);
                            bus.Display = (bool)CheckNullBool(reader, 12);
                            bus.DisplayDescription = CheckNullString(reader, 13);
                            bus.Attendees = CheckNullByte(reader, 14);
                            bus.BusinessDescription = CheckNullString(reader, 15);
                            bus.Website = CheckNullString(reader, 16);
                            bus.SocialMedia = CheckNullString(reader, 17);
                            bus.Photo = CheckNullByteArray(reader, 18);
                            bus.LocationPreference = CheckNullString(reader, 19);
                            bus.ContactMe = (bool)CheckNullBool(reader, 20);
                            bus.Approved = (bool)CheckNullBool(reader, 21);
                            bus.Active = (bool)CheckNullBool(reader, 22);

                            businesses.Add(bus);
                        }
                    }
                }
            }

            return businesses;
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

        public void Write(List<business> businesses)
        {
            //throw new NotImplementedException();
        }
    }
}