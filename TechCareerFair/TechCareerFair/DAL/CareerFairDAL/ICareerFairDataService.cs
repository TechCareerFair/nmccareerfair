using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.CareerFairDAL
{
    interface ICareerFairDataService
    {
        List<careerfair> Read();
        void Write(List<careerfair> careerfairs);
    }
}
