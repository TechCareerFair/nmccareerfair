using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.Business2FieldDAL
{
    interface IBusiness2FieldDataService
    {
        List<business2field> Read();
        void Write(List<business2field> b2fs);
    }
}
