using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    interface IAdminRepository
    {
        IEnumerable<admin> SelectAll();
        admin SelectOne(int id);
        void Insert(admin admin);
        void Update(admin admin);
        void Delete(int id);
    }
}
