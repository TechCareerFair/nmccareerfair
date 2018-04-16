using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.Applicant2FieldDAL
{
    public class Applicant2FieldRepository : IApplicant2FieldRepository, IDisposable
    {
        private List<applicant2field> _applicant2fields;
        private Applicant2FieldDatabaseDataService _ds = new Applicant2FieldDatabaseDataService();

        public Applicant2FieldRepository()
        {
            _applicant2fields = _ds.Read();
        }

        public void Delete(int id)
        {
            var applicant2field = _applicant2fields.Where(a => a.ApplicantFieldID == id).FirstOrDefault();

            if (applicant2field != null)
            {
                _applicant2fields.Remove(applicant2field);
                _ds.Remove(applicant2field);
            }
        }

        public void Dispose()
        {
            _applicant2fields = null;
            _ds = null;
        }

        public void Insert(applicant2field applicant2field)
        {
            //applicant2field.ApplicantFieldID = NextIdValue();
            //_applicant2fields.Add(applicant2field);

            _ds.Insert(applicant2field);
        }

        private int NextIdValue()
        {
            int currentMaxId = _applicant2fields.OrderByDescending(a => a.ApplicantFieldID).FirstOrDefault().ApplicantFieldID;
            return currentMaxId + 1;
        }

        public IEnumerable<applicant2field> SelectAll()
        {
            return _applicant2fields;
        }

        public applicant2field SelectOne(int id)
        {
            applicant2field selectedapplicant2field = _applicant2fields.Where(a => a.ApplicantFieldID == id).FirstOrDefault();

            return selectedapplicant2field;
        }

        public void Update(applicant2field applicant2field)
        {
            var oldapplicant2field = _applicant2fields.Where(a => a.ApplicantFieldID == applicant2field.ApplicantFieldID).FirstOrDefault();

            if (oldapplicant2field != null)
            {
                _applicant2fields.Remove(oldapplicant2field);
                _applicant2fields.Add(applicant2field);
                _ds.Update(applicant2field);
            }
        }
    }
}