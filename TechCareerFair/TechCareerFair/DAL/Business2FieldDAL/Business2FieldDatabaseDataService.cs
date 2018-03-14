using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.Business2FieldDAL
{
    public class Business2FieldDatabaseDataService : IBusiness2FieldDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<business2field> Read()
        {
            List<business2field> business2fields = new List<business2field>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [BusinessFieldID],[Business],[Field]");
                sb.Append("FROM [careerfair].[business2field]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            business2field business2field = new business2field();

                            business2field.BusinessFieldID = DatabaseHelper.CheckNullInt(reader, 0);
                            business2field.Business = DatabaseHelper.CheckNullInt(reader, 1);
                            business2field.Field = DatabaseHelper.CheckNullInt(reader, 2);

                            business2fields.Add(business2field);
                        }
                    }
                }
            }
            return business2fields;
        }

        public void Write(List<business2field> business2fields)
        {
            throw new NotImplementedException();
        }

        public void Insert(business2field business2field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[business2field]([Business],[Field])");
                string values = "VALUES(@param1, @param2)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = business2field.Business;
                    command.Parameters.Add("@param2", SqlDbType.Int).Value = business2field.Field;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(business2field business2field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [careerfair].[business2field]");
                sb.Append("WHERE [BusinessFieldID] = " + business2field.BusinessFieldID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(business2field business2field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[business2field]");
                sb.Append("SET [Business] = @param1, [Field] = @param2");
                sb.Append("WHERE [BusinessFieldID] = " + business2field.BusinessFieldID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = business2field.Business;
                    command.Parameters.Add("@param2", SqlDbType.Int).Value = business2field.Field;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}