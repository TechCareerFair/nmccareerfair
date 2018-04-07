using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.User2BusinessDAL
{
    interface IUser2BusinessDataService
    {
        List<user2business> Read();
        void Write(List<user2business> u2a);
    }
}
