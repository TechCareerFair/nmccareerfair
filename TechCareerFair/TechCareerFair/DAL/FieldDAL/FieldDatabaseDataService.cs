using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.FieldDAL
{
    public class FieldDatabaseDataService : IFieldDataService, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<field> Read()
        {
            List<field> fields = new List<field>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [FieldID],[Name]");
                sb.Append("FROM [careerfair].[field]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            field field = new field();

                            field.FieldID = DatabaseHelper.CheckNullInt(reader, 0);
                            field.Name = DatabaseHelper.CheckNullString(reader, 1);
                            
                            fields.Add(field);
                        }
                    }
                }
            }
            return fields;
        }

        public void Write(List<field> fields)
        {
            throw new NotImplementedException();
        }

        public void Insert(field field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[field]([Name])");
                string values = "VALUES(@param1)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, 50).Value = (object)field.Name ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(field field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [careerfair].[field]");
                sb.Append("WHERE [FieldID] = " + field.FieldID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(field field)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[field]");
                sb.Append("SET [Name] = @param1 ");
                sb.Append("WHERE [FieldID] = " + field.FieldID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, 50).Value = (object)field.Name ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}