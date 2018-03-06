using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public class BusinessRepository : IBusinessRepository, IDisposable
    {
        private List<business> _businesses;

        public BusinessRepository()
        {
            using (BusinessDatabaseDataService ds = new BusinessDatabaseDataService())
            {
                _businesses = ds.Read();
            }
        }

        public void Delete(int id)
        {
            var business = _businesses.Where(b => b.BusinessID == id).FirstOrDefault();

            if (business != null)
            {
                _businesses.Remove(business);
            }

            Save();
        }

        public void Insert(business business)
        {
            business.BusinessID = NextIdValue();
            _businesses.Add(business);

            Save();
        }

        private int NextIdValue()
        {
            int currentMaxId = _businesses.OrderByDescending(b => b.BusinessID).FirstOrDefault().BusinessID;
            return currentMaxId + 1;
        }

        public IEnumerable<business> SelectAll()
        {
            return _businesses;
        }

        public business SelectOne(int id)
        {
            business selectedBusiness = _businesses.Where(b => b.BusinessID == id).FirstOrDefault();

            return selectedBusiness;
        }

        public void Update(business business)
        {
            var oldBusiness = _businesses.Where(b => b.BusinessID == business.BusinessID).FirstOrDefault();

            if (oldBusiness != null)
            {
                _businesses.Remove(oldBusiness);
                _businesses.Add(business);
            }

            Save();
        }

        public void Save()
        {
            //ApplicantDatabaseDataService Write()
        }

        public void Dispose()
        {
            _businesses = null;
        }
    }
}