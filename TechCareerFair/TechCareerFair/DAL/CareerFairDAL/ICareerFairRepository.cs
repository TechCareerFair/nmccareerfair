using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.CareerFairDAL
{
    interface ICareerFairRepository
    {
        IEnumerable<careerfair> SelectAll();
        careerfair SelectOne(int id);
        void Insert(careerfair careerfair);
        void Update(careerfair careerfair);
        void Delete(int id);
    }
}
