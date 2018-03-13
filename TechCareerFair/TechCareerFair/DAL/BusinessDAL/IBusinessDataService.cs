using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public interface IBusinessDataService
    {
        List<business> Read();
        void Write(List<business> applicants);
    }
}