using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.AdminDAL
{
    interface IAdminDataService
    {
        List<admin> Read();
        void Write(List<admin> applicants);
    }
}
