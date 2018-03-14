using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.Applicant2FieldDAL
{
    interface IApplicant2FieldRepository
    {
        IEnumerable<applicant2field> SelectAll();
        applicant2field SelectOne(int id);
        void Insert(applicant2field a2f);
        void Update(applicant2field a2f);
        void Delete(int id);
    }
}
