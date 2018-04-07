using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.GalleryDAL
{
    interface IGalleryRepository
    {
        IEnumerable<gallery> SelectAll();
        gallery SelectOne(int id);
        void Insert(gallery gallery);
        void Update(gallery gallery, string serverPath);
        void Delete(int id, string serverPath);
    }
}
