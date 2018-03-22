using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.GalleryDAL
{
    public class GalleryDatabaseDataService : IGalleryDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<gallery> Read()
        {
            List<gallery> galleries = new List<gallery>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [GalleryID],[Directory],[Description]");
                sb.Append("FROM [careerfair].[gallery]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gallery gallery = new gallery();

                            gallery.GalleryID = DatabaseHelper.CheckNullInt(reader, 0);
                            gallery.Directory = DatabaseHelper.CheckNullString(reader, 1);
                            gallery.Description = DatabaseHelper.CheckNullString(reader, 2);

                            galleries.Add(gallery);
                        }
                    }
                }
            }
            return galleries;
        }

        public void Write(List<gallery> galleries)
        {
            throw new NotImplementedException();
        }

        public void Insert(gallery gallery)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[gallery]([Directory],[Description])");
                string values = "VALUES(@param1, @param2)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, int.MaxValue).Value = (object)gallery.Directory ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, int.MaxValue).Value = (object)gallery.Description ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(gallery gallery)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [careerfair].[gallery]");
                sb.Append("WHERE [GalleryID] = " + gallery.GalleryID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(gallery gallery)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[gallery]");
                sb.Append("SET [Directory] = @param1, [Description] = @param2 ");
                sb.Append("WHERE [GalleryID] = " + gallery.GalleryID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, int.MaxValue).Value = (object)gallery.Directory ?? DBNull.Value;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, int.MaxValue).Value = (object)gallery.Description ?? DBNull.Value;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}