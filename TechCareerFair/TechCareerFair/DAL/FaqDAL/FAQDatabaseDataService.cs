using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.FaqDAL
{
    public class FAQDatabaseDataService : IFAQDataService, IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<faq> Read()
        {
            List<faq> faqs = new List<faq>();

            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT [FaqID],[Question],[Answer],[Website],[ApplicantQ]");
                sb.Append("FROM [careerfair].[faq]");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            faq faq = new faq();

                            faq.FaqID = DatabaseHelper.CheckNullInt(reader, 0);
                            faq.Question = DatabaseHelper.CheckNullString(reader, 1);
                            faq.Answer = DatabaseHelper.CheckNullString(reader, 2);
                            faq.Website = DatabaseHelper.CheckNullString(reader, 3);
                            faq.IsApplicantQ = DatabaseHelper.CheckNullBool(reader, 4);

                            faqs.Add(faq);
                        }
                    }
                }
            }
            return faqs;
        }

        public void Write(List<faq> faqs)
        {
            throw new NotImplementedException();
        }

        public void Insert(faq faq)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [careerfair].[faq]([Question],[Answer],[Website],[ApplicantQ])");
                string values = "VALUES(@param1, @param2, @param3, @param4)";
                sb.Append(values);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, int.MaxValue).Value = faq.Question;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, int.MaxValue).Value = faq.Answer;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, int.MaxValue).Value = faq.Website;
                    command.Parameters.Add("@param4", SqlDbType.Bit).Value = faq.IsApplicantQ;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove(faq faq)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE FROM [careerfair].[faq]");
                sb.Append("WHERE [FaqID] = " + faq.FaqID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(faq faq)
        {
            using (SqlConnection connection = new SqlConnection(DataSettings.CONNECTION_STRING))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [careerfair].[faq]");
                sb.Append("SET [Question] = @param1, [Answer] = @param2, [Website] = @param3, [ApplicantQ] = @param4");
                sb.Append("WHERE [FaqID] = " + faq.FaqID);
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@param1", SqlDbType.NVarChar, int.MaxValue).Value = faq.Question;
                    command.Parameters.Add("@param2", SqlDbType.NVarChar, int.MaxValue).Value = faq.Answer;
                    command.Parameters.Add("@param3", SqlDbType.NVarChar, int.MaxValue).Value = faq.Website;
                    command.Parameters.Add("@param4", SqlDbType.Bit).Value = faq.IsApplicantQ;
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}