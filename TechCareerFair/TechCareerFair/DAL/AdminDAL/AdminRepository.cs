using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.AdminDAL
{
    public class AdminRepository : IAdminRepository, IDisposable
    {
        private AdminDatabaseDataService _ds = new AdminDatabaseDataService();
        private List<admin> _admins;

        public AdminRepository()
        {
            _admins = _ds.Read();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Insert(admin admin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<admin> SelectAll()
        {
            return _admins;
        }

        public admin SelectOne(int id)
        {
            admin selectedAdmin = _admins.Where(a => a.AdminID == id).FirstOrDefault();

            return selectedAdmin;
        }

        public void Update(admin admin)
        {
            throw new NotImplementedException();
        }
    }
}