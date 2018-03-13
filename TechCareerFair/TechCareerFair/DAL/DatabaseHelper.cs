using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
    }
}