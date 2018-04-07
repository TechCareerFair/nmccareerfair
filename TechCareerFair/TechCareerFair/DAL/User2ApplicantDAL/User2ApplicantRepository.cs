using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.User2ApplicantDAL
{
    public class User2ApplicantRepository : IUser2ApplicantRepository, IDisposable
    {
        private List<user2applicant> _u2as;
        private User2ApplicantDatabaseDataService _ds = new User2ApplicantDatabaseDataService();

        public User2ApplicantRepository()
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

        public void Insert(user2applicant u2a)
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

        public IEnumerable<user2applicant> SelectAll()
        {
            return _u2as;
        }

        public user2applicant SelectOne(int id)
        {
            user2applicant selectedPosition = _u2as.Where(u => u.ID == id).FirstOrDefault();

            return selectedPosition;
        }

        public List<int> SelectApplicantBy(string userID)
        {
            return _ds.Read(userID);
        }

        public List<string> SelectUserBy(int applicantID)
        {
            return _ds.Read(applicantID);
        }

        public void Update(user2applicant u2a)
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