using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.User2ApplicantDAL
{
    interface IUser2ApplicantRepository
    {
        IEnumerable<user2applicant> SelectAll();
        user2applicant SelectOne(int id);
        void Insert(user2applicant u2a);
        void Update(user2applicant u2a);
        void Delete(int id);
    }
}
