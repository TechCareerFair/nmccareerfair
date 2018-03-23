using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.CareerFairDAL
{
    public class CareerFairDatabaseDataService : ICareerFairDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<careerfair> Read()
        {
            List<careerfair> careerFairs = new List<careerfair>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [CareerFairID],[Phone],[Date],[Address],[About]");
                sb.Append("FROM [careerfair].[careerfair]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            careerfair car = new careerfair();

                            car.CareerFairID = DatabaseHelper.CheckNullInt(reader, 0);
                            car.Phone = DatabaseHelper.CheckNullString(reader, 1);
                            car.Date = DatabaseHelper.CheckNullDateTime(reader, 2);
                            car.Address = DatabaseHelper.CheckNullString(reader, 3);
                            car.About = DatabaseHelper.CheckNullString(reader, 4);

                            careerFairs.Add(car);
                        }
                    }
                }
            }
            return careerFairs;
        }

        public void Write(List<careerfair> careerFairs)
        {
            throw new NotImplementedException();
        }

        public void Update(careerfair careerFair)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[careerfair]");
                sb.Append("SET [Phone] = @param1, Date = @param2, Address = @param3, About = @param4 ");
                sb.Append("WHERE [CareerFairID] = " + careerFair.CareerFairID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, 20).Value = (object)careerFair.Phone ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.DateTime).Value = (object)careerFair.Date ?? DBNull.Value;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = (object)careerFair.Address ?? DBNull.Value;
                    command.Parameters.Add("@param4", SqlDbType.NVarChar, int.MaxValue).Value = (object)careerFair.About ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}