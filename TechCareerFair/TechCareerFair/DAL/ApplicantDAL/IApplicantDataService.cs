using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    interface IApplicantDataService
    {
        List<applicant> Read();
        void Write(List<applicant> applicants);
    }
}
