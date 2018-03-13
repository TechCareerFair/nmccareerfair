using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.AdminDAL
{
    public class AdminDatabaseDataService : IAdminDataService, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<admin> Read()
        {
            List<admin> admins = new List<admin>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [AdminID],[Username],[Password]");
                sb.Append("FROM [careerfair].[admin]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            admin admin = new admin();

                            admin.AdminID = DatabaseHelper.CheckNullInt(reader, 0);
                            admin.Username = DatabaseHelper.CheckNullString(reader, 1);
                            admin.Password = DatabaseHelper.CheckNullString(reader, 2);

                            admins.Add(admin);
                        }
                    }
                }
            }

            return admins;
        }

        public void Write(List<admin> applicants)
        {
            throw new NotImplementedException();
        }
    }
}