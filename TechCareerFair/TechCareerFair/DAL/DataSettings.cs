using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace TechCareerFair.DAL
{
    public static class DataSettings
    {
        public static readonly string CONNECTION_STRING = "Server=tcp:nmccareerfair.database.windows.net,1433;Initial Catalog=CareerFair;Persist Security Info=False;User ID=piechop;Password=CIT280!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; 
    }
}