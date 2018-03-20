using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public interface IBusinessRepository
    {
        IEnumerable<business> SelectAll();
        business SelectOne(int id);
        void Insert(business business);
        void Update(business business);
        void Delete(int id);
    }
}