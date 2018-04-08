using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    interface IApplicantRepository
    {
        IList<applicant> SelectAll();
        applicant SelectOne(int id);
        void Insert(applicant applicant);
        void Update(applicant applicant, string serverPath);
        void Delete(int id, string serverPath);
    }
}
