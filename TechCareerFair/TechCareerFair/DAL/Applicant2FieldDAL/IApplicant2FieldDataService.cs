using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.Applicant2FieldDAL
{
    interface IApplicant2FieldDataService
    {
        List<applicant2field> Read();
        void Write(List<applicant2field> a2fs);
    }
}
