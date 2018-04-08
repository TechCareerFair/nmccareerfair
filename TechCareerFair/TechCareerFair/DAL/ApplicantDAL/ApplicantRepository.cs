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
        private IList<applicant> _applicants;

        public ApplicantRepository()
        {
            _applicants = _ds.Read();
        }

        public void GetAccountInfoByUserID(ApplicantViewModel app)
        {
            ApplicantViewModel accountInfo = _ds.GetAccountInfoBy(app.UserID);

            app.Email = accountInfo.Email;
            app.Password = accountInfo.Password;
        }

        public void Delete(int id, string serverPath)
        {
            var applicant = _applicants.Where(g => g.ApplicantID == id).FirstOrDefault();

            if (applicant != null)
            {
                //_applicants.Remove(applicant);
                _ds.Remove(applicant, serverPath);
            }
        }

        public void Insert(applicant applicant)
        {
            applicant.ApplicantID = NextIdValue();
            //_applicants.Add(applicant);

            _ds.Insert(applicant);
        }

        private int NextIdValue()
        {
            int currentMaxId = _applicants.OrderByDescending(a => a.ApplicantID).FirstOrDefault().ApplicantID;
            return currentMaxId + 1;
        }

        public applicant ToModel(ApplicantViewModel a)
        {
            return new applicant
            {
                ApplicantID = a.ApplicantID,

                UserID = a.UserID,

                FirstName = a.FirstName,

                LastName = a.LastName,

                University = a.University,

                Alumni = a.Alumni,

                Profile = a.Profile,

                SocialMedia = a.SocialMedia,

                Resume = a.Resume,

                YearsExperience = a.YearsExperience,

                Internship = a.Internship,

                Active = a.Active,

                Fields = a.Fields
            };
        }

        public ApplicantViewModel ToViewModel(applicant a)
        {
             return new ApplicantViewModel
            {

                ApplicantID = a.ApplicantID,

                UserID = a.UserID,

                FirstName = a.FirstName,

                LastName = a.LastName,

                University = a.University,

                Alumni = a.Alumni,

                Profile = a.Profile,

                SocialMedia = a.SocialMedia,

                Resume = a.Resume,

                YearsExperience = a.YearsExperience,

                Internship = a.Internship,

                Active = a.Active,

                Fields = a.Fields
            };
        }

        public IList<applicant> SelectAll()
        {
            return _applicants;
        }

        public IList<ApplicantViewModel> SelectAllAsViewModel()
        {
            List<ApplicantViewModel> applicants = new List<ApplicantViewModel>();
            foreach(applicant a in _applicants)
            {
                ApplicantViewModel avm = ToViewModel(a);

                applicants.Add(avm);
            }

            return applicants;
        }

        public applicant SelectOne(int id)
        {
            applicant selectedApplicant = _applicants.Where(a => a.ApplicantID == id).FirstOrDefault();

            return selectedApplicant;
        }

        public void Update(applicant applicant, string serverPath)
        {
            var oldApplicant = _applicants.Where(a => a.ApplicantID == applicant.ApplicantID).FirstOrDefault();

            if (oldApplicant != null)
            {
                //_applicants.Remove(oldApplicant);
                //_applicants.Add(applicant);
                _ds.Update(applicant, serverPath, oldApplicant.Resume);
            }
        }
        
        public void UpdateApplicantProfile(applicant applicant, string serverPath)
        {
            var oldApplicant = _applicants.Where(a => a.ApplicantID == applicant.ApplicantID).FirstOrDefault();

            if (oldApplicant != null)
            {
                //_applicants.Remove(oldApplicant);
                //_applicants.Add(applicant);
                _ds.UpdateApplicantProfile(applicant, serverPath, oldApplicant.Resume);
            }
        }

        public void Dispose()
        {
            _ds = null;
            _applicants = null;
        }
    }
}