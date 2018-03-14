using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.FaqDAL
{
    interface IFAQDataService
    {
        List<faq> Read();
        void Write(List<faq> faqs);
    }
}