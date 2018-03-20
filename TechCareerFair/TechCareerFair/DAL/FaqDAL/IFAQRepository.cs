using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.FaqDAL
{
    interface IFAQRepository
    {
        IEnumerable<faq> SelectAll();
        faq SelectOne(int id);
        void Insert(faq faq);
        void Update(faq faq);
        void Delete(faq faq);
    }
}
