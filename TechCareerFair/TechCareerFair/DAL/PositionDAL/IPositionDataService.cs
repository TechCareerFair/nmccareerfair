using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.PositionDAL
{
    interface IPositionDataService
    {
        List<position> Read();
        void Write(List<position> positions);
    }
}
