using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public static class DatabaseHelper
    {
        public static string CheckNullString(SqlDataReader reader, int i)
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

        public static bool CheckNullBool(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetBoolean(i);
            }
            else
            {
                return false;
            }
        }

        public static byte CheckNullByte(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetByte(i);
            }
            else
            {
                return 0;
            }
        }

        public static int CheckNullInt(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetInt32(i);
            }
            else
            {
                return 1;
            }
        }

        public static byte[] CheckNullByteArray(SqlDataReader reader, int i)
        {
            if (reader[i] != DBNull.Value)
            {
                return (byte[])reader[i];
            }
            else
            {
                return new byte[0];
            }

        }

        public static DateTime CheckNullDateTime(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetDateTime(i);
            }
            else
            {
                return new DateTime();
            }

        }

        public static int GetFieldIndex(string field)
        {
            int fieldIndex = 0;

            using (TechCareerFair.DAL.FieldDAL.FieldDatabaseDataService ds = new FieldDAL.FieldDatabaseDataService())
            {
                List<field> fields = ds.Read();

                foreach (field f in fields)
                {
                    if (f.Name.ToLower().Trim() == field.ToLower().Trim())
                    {
                        fieldIndex = f.FieldID;
                        break;
                    }
                }
            }

            return fieldIndex;
        }
        
    }
}