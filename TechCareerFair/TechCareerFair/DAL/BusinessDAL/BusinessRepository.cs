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
        private BusinessDatabaseDataService _ds = new BusinessDatabaseDataService();

        public BusinessRepository()
        {
            _businesses = _ds.Read();
        }

        public void Delete(int id, string serverPath)
        {
            var business = _businesses.Where(g => g.BusinessID == id).FirstOrDefault();

            if (business != null)
            {
                _businesses.Remove(business);
                _ds.Remove(business, serverPath);
            }
        }

        public void Insert(business business)
        {
            business.BusinessID = NextIdValue();
            _businesses.Add(business);

            _ds.Insert(business);
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

        public void Update(business business, string serverPath)
        {
            var oldBusiness = _businesses.Where(b => b.BusinessID == business.BusinessID).FirstOrDefault();

            if (oldBusiness != null)
            {
                _businesses.Remove(oldBusiness);
                _businesses.Add(business);
                _ds.Update(business, serverPath, oldBusiness.Photo);
            }
        }

        public void UpdateBusinessProfile(business business, string serverPath)
        {
            var oldBusiness = _businesses.Where(b => b.BusinessID == business.BusinessID).FirstOrDefault();

            if (oldBusiness != null)
            {
                _businesses.Remove(oldBusiness);
                _businesses.Add(business);
                _ds.UpdateBusinessProfile(business, serverPath, oldBusiness.Photo);
            }
        }

        public void Dispose()
        {
            _ds = null;
            _businesses = null;
        }
    }
}