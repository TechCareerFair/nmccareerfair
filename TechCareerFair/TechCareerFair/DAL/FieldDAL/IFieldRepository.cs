using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.FieldDAL
{
    interface IFieldRepository
    {
        IEnumerable<field> SelectAll();
        field SelectOne(int id);
        void Insert(field field);
        void Update(field field);
        void Delete(int id);
    }
}
