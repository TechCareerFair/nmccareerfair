using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.FieldDAL
{
    interface IFieldDataService
    {
        List<field> Read();
        void Write(List<field> fields);
    }
}
