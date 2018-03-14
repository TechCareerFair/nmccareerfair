using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.ZipDAL
{
    interface IZipDataService
    {
        List<Zipcode> Read();
        void Write(List<Zipcode> zipcodes);
    }
}
