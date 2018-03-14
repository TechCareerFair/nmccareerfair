using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.Business2FieldDAL
{
    interface IBusiness2FieldRepository
    {
        IEnumerable<business2field> SelectAll();
        business2field SelectOne(int id);
        void Insert(business2field b2f);
        void Update(business2field b2f);
        void Delete(int id);
    }
}
