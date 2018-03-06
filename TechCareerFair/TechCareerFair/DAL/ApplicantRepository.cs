using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public class ApplicantRepository
    {
        private List<applicant> _applicants;

        public ApplicantRepository()
        {
            using (ApplicantDatabaseDataService ds = new ApplicantDatabaseDataService())
            {
                _applicants = ds.Read();
            }
        }

        public void Delete(int id)
        {
            var applicant = _applicants.Where(a => a.ApplicantID == id).FirstOrDefault();

            if (applicant != null)
            {
                _applicants.Remove(applicant);
            }

            Save();
        }

        public void Insert(applicant applicant)
        {
            applicant.ApplicantID = NextIdValue();
            _applicants.Add(applicant);

            Save();
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
            }

            Save();
        }

        public void Save()
        {
            //ApplicantDatabaseDataService Write()
        }

        public void Dispose()
        {
            _applicants = null;
        }
    }
}