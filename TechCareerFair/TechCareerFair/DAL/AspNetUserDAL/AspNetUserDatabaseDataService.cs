using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace TechCareerFair.DAL.AspNetUserDAL
{
    public class AspNetUserDatabaseDataService
    {
        public bool CheckDuplicateEmail(string email, string originalEmail)
        {
            bool isDuplicate = true;
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [Email]");
                sb.Append("FROM [dbo].[AspNetUsers]");
                sb.Append("WHERE [Email] = '" + email + "'");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string e = DatabaseHelper.CheckNullString(reader, 0);
                            isDuplicate = e.Equals(email);
                            if(isDuplicate && e != originalEmail)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return isDuplicate;
        }
    }
}