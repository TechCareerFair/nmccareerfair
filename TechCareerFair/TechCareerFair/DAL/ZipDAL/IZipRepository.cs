using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.ZipDAL
{
    interface IZipRepository
    {
        IEnumerable<Zipcode> SelectAll();
        Zipcode SelectOne(int id);
        void Insert(Zipcode zipcode);
        void Update(Zipcode zipcode);
        void Delete(int id);
    }
}
