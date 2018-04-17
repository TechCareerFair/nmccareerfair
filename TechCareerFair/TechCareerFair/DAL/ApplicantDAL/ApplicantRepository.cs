using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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

        private void GetAccountInfoByUserID(ApplicantViewModel app)
        {
            ApplicantViewModel accountInfo = _ds.GetAccountInfoBy(app.Email);
            
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
            //applicant.ApplicantID = NextIdValue();
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

                Email = a.Email,

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

                Email = a.Email,

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

        public IEnumerable<applicant> SelectRange(int startRow, int numberOfRows)
        {
            return _ds.Read(startRow, numberOfRows);
        }

        public IList<ApplicantViewModel> SelectAllAsViewModel()
        {
            List<ApplicantViewModel> applicants = new List<ApplicantViewModel>();
            foreach(applicant a in _applicants)
            {
                ApplicantViewModel avm = ToViewModel(a);
                GetAccountInfoByUserID(avm);

                applicants.Add(avm);
            }

            return applicants;
        }

        public applicant SelectOne(int id)
        {
            applicant selectedApplicant = _applicants.Where(a => a.ApplicantID == id).FirstOrDefault();

            return selectedApplicant;
        }

        public ApplicantViewModel SelectOneAsViewModel(int id)
        {
            applicant app = _applicants.Where(a => a.ApplicantID == id).FirstOrDefault();

            ApplicantViewModel selectedApplicant = ToViewModel(app);
            GetAccountInfoByUserID(selectedApplicant);

            return selectedApplicant;
        }

        private string ApplicantToCSV(applicant a)
        {
            string formatted = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                a.ApplicantID.ToString(), a.Email.Replace(",", string.Empty), a.FirstName.Replace(",", string.Empty), a.LastName.Replace(",", string.Empty),
                a.University.Replace(",", string.Empty), a.Alumni.ToString(), a.Profile.Replace(",", string.Empty), 
                a.SocialMedia.Replace(",", string.Empty), a.YearsExperience.ToString(), a.Internship.ToString(), a.Active.ToString());

            return formatted;
        }

        public void CreateApplicantCSV(string csvFile)
        {
            //Server.MapPath("~" + "/App_Data/")
            ApplicantRepository ar = new ApplicantRepository();
            List<applicant> applicants = ar.SelectAll().ToList();

            using (StreamWriter writer = new StreamWriter(new FileStream(csvFile, FileMode.Create, FileAccess.Write)))
            {
                writer.WriteLine("ApplicantID,Email,FirstName,LastName,University,Alumni,Profile,SocialMedia,YearsExperience,Internship,Active");
                foreach (applicant a in applicants)
                {
                    writer.WriteLine(ar.ApplicantToCSV(a));
                }
            }
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