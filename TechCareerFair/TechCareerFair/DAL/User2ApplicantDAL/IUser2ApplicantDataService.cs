using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.User2ApplicantDAL
{
    interface IUser2ApplicantDataService
    {
        List<user2applicant> Read();
        void Write(List<user2applicant> u2a);
    }
}
