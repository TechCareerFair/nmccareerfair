using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.GalleryDAL
{
    interface IGalleryDataService
    {
        List<gallery> Read();
        void Write(List<gallery> galleries);
    }
}
