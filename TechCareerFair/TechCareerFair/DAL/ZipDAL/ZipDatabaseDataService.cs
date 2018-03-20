using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.ZipDAL
{
    public class ZipDatabaseDataService : IZipDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<Zipcode> Read()
        {
            List<Zipcode> zipcodes = new List<Zipcode>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [ZipCodeID],[ZipCode],[City],[State],[Business]");
                sb.Append("FROM [dbo].[zipcode]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Zipcode zipcode = new Zipcode();

                            zipcode.ZipCodeID = DatabaseHelper.CheckNullInt(reader, 0);
                            zipcode.ZipCode = DatabaseHelper.CheckNullString(reader, 1);
                            zipcode.City = DatabaseHelper.CheckNullString(reader, 2);
                            zipcode.State = DatabaseHelper.CheckNullString(reader, 2);
                            zipcode.Business = DatabaseHelper.CheckNullInt(reader, 2);

                            zipcodes.Add(zipcode);
                        }
                    }
                }
            }
            return zipcodes;
        }

        public void Write(List<Zipcode> zipcodes)
        {
            throw new NotImplementedException();
        }

        public void Insert(Zipcode zipcode)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[zipcode]([ZipCode],[City],[State],[Business])");
                string values = "VALUES(@param1, @param2, @param3, @param4)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = (object)zipcode.ZipCode ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 50).Value = (object)zipcode.City ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, int.MaxValue).Value = (object)zipcode.State ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, int.MaxValue).Value = (object)zipcode.Business ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(Zipcode zipcode)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [dbo].[zipcode]");
                sb.Append("WHERE [ZipCodeID] = " + zipcode.ZipCodeID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Zipcode zipcode)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [dbo].[zipcode]");
                sb.Append("SET [ZipCode] = @param1, [City] = @param2, [State] = @param3, [Business] = @param4");
                sb.Append(" WHERE [ZipCodeID] = " + zipcode.ZipCodeID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = (object)zipcode.ZipCode ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 50).Value = (object)zipcode.City ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, int.MaxValue).Value = (object)zipcode.State ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, int.MaxValue).Value = (object)zipcode.Business ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}