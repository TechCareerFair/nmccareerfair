using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.PositionDAL
{
    interface IPositionRepository
    {
        IEnumerable<position> SelectAll();
        position SelectOne(int id);
        void Insert(position position);
        void Update(position position);
        void Delete(int id);
    }
}
