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

        public AdminViewModel ToViewModel(admin a)
        {
            return new AdminViewModel
            {
                AdminID = a.AdminID,
                Username = a.Username,
                Password = a.Password,
                ConfirmPassword = a.Password,
                ContactEmail = a.ContactEmail
            };
        }

        public admin ToModel(AdminViewModel a)
        {
            return new admin
            {
                AdminID = a.AdminID,
                Username = a.Username,
                Password = a.Password,
                ContactEmail = a.ContactEmail
            };
        }

        public admin SelectOne(int id)
        {
            admin selectedAdmin = _admins.Where(a => a.AdminID == id).FirstOrDefault();

            return selectedAdmin;
        }

        public void Update(admin admin)
        {
            var oldAdmin = _admins.Where(a => a.AdminID == admin.AdminID).FirstOrDefault();

            if (oldAdmin != null)
            {
                _ds.Update(admin);
            }
        }
    }
}