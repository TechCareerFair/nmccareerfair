using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.User2BusinessDAL
{
    public class User2BusinessRepository : IUser2BusinessRepository, IDisposable
    {
        private List<user2business> _u2as;
        private User2BusinessDatabaseDataService _ds = new User2BusinessDatabaseDataService();

        public User2BusinessRepository()
        {
            _u2as = _ds.Read();
        }

        public void Delete(int id)
        {
            var u2a = _u2as.Where(u => u.ID == id).FirstOrDefault();

            if (u2a != null)
            {
                _u2as.Remove(u2a);
                _ds.Remove(u2a);
            }
        }

        public void Dispose()
        {
            _u2as = null;
            _ds = null;
        }

        public void Insert(user2business u2a)
        {
            u2a.ID = NextIdValue();
            _u2as.Add(u2a);

            _ds.Insert(u2a);
        }

        private int NextIdValue()
        {
            int currentMaxId = _u2as.OrderByDescending(u => u.ID).FirstOrDefault().ID;
            return currentMaxId + 1;
        }

        public IEnumerable<user2business> SelectAll()
        {
            return _u2as;
        }

        public user2business SelectOne(int id)
        {
            user2business selectedBusiness = _u2as.Where(u => u.ID == id).FirstOrDefault();

            return selectedBusiness;
        }

        public List<int> SelectBusinessBy(string userID)
        {
            return _ds.Read(userID);
        }

        public List<string> SelectUserBy(int businessID)
        {
            return _ds.Read(businessID);
        }

        public void Update(user2business u2a)
        {
            var oldU2A = _u2as.Where(u => u.ID == u2a.ID).FirstOrDefault();

            if (oldU2A != null)
            {
                _u2as.Remove(oldU2A);
                _u2as.Add(u2a);
                _ds.Update(u2a);
            }
        }
    }
}