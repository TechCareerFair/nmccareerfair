using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.PositionDAL
{
    public class PositionDatabaseDataService : IPositionDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<position> Read()
        {
            List<position> positions = new List<position>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [PositionID],[Business],[Name],[Description],[Website],[Internship]");
                sb.Append("FROM [careerfair].[position]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            position position = new position();

                            position.PositionID = DatabaseHelper.CheckNullInt(reader, 0);
                            position.Business = DatabaseHelper.CheckNullInt(reader, 1);
                            position.Name = DatabaseHelper.CheckNullString(reader, 2);
                            position.Description = DatabaseHelper.CheckNullString(reader, 3);
                            position.Website = DatabaseHelper.CheckNullString(reader, 4);
                            position.Internship = DatabaseHelper.CheckNullBool(reader, 5);

                            positions.Add(position);
                        }
                    }
                }
            }
            return positions;
        }

        public void Write(List<position> positions)
        {
            throw new NotImplementedException();
        }

        public void Insert(position position)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[position]([Business],[Name],[Description],[Website],[Internship])");
                string values = "VALUES(@param1, @param2, @param3, @param4, @param5)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = (object)position.Business ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 50).Value = (object)position.Name ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, int.MaxValue).Value = (object)position.Description ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, int.MaxValue).Value = (object)position.Website ?? DBNull.Value;
                    command.Parameters.Add("@param5", SqlDbType.Bit).Value = (object)position.Internship ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(position position)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [careerfair].[position]");
                sb.Append("WHERE [PositionID] = " + position.PositionID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(position position)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[position]");
                sb.Append("SET [Business] = @param1, [Name] = @param2, [Description] = @param3, [Website] = @param4, [Internship] = @param5");
                sb.Append(" WHERE [PositionID] = " + position.PositionID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.Int).Value = (object)position.Business ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, 50).Value = (object)position.Name ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, int.MaxValue).Value = (object)position.Description ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, int.MaxValue).Value = (object)position.Website ?? DBNull.Value;
                    command.Parameters.Add("@param5", SqlDbType.Bit).Value = (object)position.Internship ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}