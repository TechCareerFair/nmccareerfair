using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.User2BusinessDAL
{
    interface IUser2BusinessRepository
    {
        IEnumerable<user2business> SelectAll();
        user2business SelectOne(int id);
        void Insert(user2business u2a);
        void Update(user2business u2a);
        void Delete(int id);
    }
}
