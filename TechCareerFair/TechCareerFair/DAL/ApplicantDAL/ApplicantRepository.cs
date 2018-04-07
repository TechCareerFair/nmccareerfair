using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public class ApplicantRepository : IApplicantRepository, IDisposable
    {
        private ApplicantDatabaseDataService _ds = new ApplicantDatabaseDataService();
        private List<applicant> _applicants;

        public ApplicantRepository()
        {
            _applicants = _ds.Read();
        }

        public void Delete(int id)
        {
            var applicant = _applicants.Where(g => g.ApplicantID == id).FirstOrDefault();

            if (applicant != null)
            {
                _applicants.Remove(applicant);
                _ds.Remove(applicant);
            }
        }

        public void Insert(applicant applicant)
        {
            applicant.ApplicantID = NextIdValue();
            _applicants.Add(applicant);

            _ds.Insert(applicant);
        }

        private int NextIdValue()
        {
            int currentMaxId = _applicants.OrderByDescending(a => a.ApplicantID).FirstOrDefault().ApplicantID;
            return currentMaxId + 1;
        }

        public IEnumerable<applicant> SelectAll()
        {
            return _applicants;
        }

        public applicant SelectOne(int id)
        {
            applicant selectedApplicant = _applicants.Where(a => a.ApplicantID == id).FirstOrDefault();

            return selectedApplicant;
        }

      

        public void Update(applicant applicant)
        {
            var oldApplicant = _applicants.Where(a => a.ApplicantID == applicant.ApplicantID).FirstOrDefault();

            if (oldApplicant != null)
            {
                _applicants.Remove(oldApplicant);
                _applicants.Add(applicant);
                _ds.Update(applicant);
            }
        }

        public void Dispose()
        {
            _ds = null;
            _applicants = null;
        }
    }
}