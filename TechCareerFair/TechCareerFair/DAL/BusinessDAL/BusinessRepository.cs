using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public class BusinessRepository : IBusinessRepository, IDisposable
    {
        private IEnumerable<business> _businesses;
        private BusinessDatabaseDataService _ds = new BusinessDatabaseDataService();

        public BusinessRepository()
        {
            _businesses = _ds.Read();
        }

        public void GetAccountInfoByUserID(BusinessViewModel bus)
        {
            BusinessViewModel accountInfo = _ds.GetAccountInfoBy(bus.UserID);

            bus.Email = accountInfo.Email;
            bus.Password = accountInfo.Password;
        }

        public void Delete(int id, string serverPath)
        {
            var business = _businesses.Where(g => g.BusinessID == id).FirstOrDefault();

            if (business != null)
            {
                //_businesses.Remove(business);
                _ds.Remove(business, serverPath);
            }
        }

        public void Insert(business business)
        {
            business.BusinessID = NextIdValue();
            //_businesses.Add(business);

            _ds.Insert(business);
        }

        private int NextIdValue()
        {
            int currentMaxId = _businesses.OrderByDescending(b => b.BusinessID).FirstOrDefault().BusinessID;
            return currentMaxId + 1;
        }

        public business ToModel(BusinessViewModel b)
        {
            return new business
            {
                BusinessID = b.BusinessID,

                UserID = b.UserID,

                BusinessName = b.BusinessName,

                FirstName = b.FirstName,

                LastName = b.LastName,

                Street = b.Street,

                City = b.City,

                State = b.State,

                Zip = b.Zip,

                Phone = b.Phone,

                Alumni = b.Alumni,

                NonProfit = b.NonProfit,

                Outlet = b.Outlet,

                Display = b.Display,

                DisplayDescription = b.DisplayDescription,

                Attendees = b.Attendees,

                BusinessDescription = b.BusinessDescription,

                Website = b.Website,

                SocialMedia = b.SocialMedia,

                Photo = b.Photo,

                LocationPreference = b.LocationPreference,

                ContactMe = b.ContactMe,

                PreferEmail = b.PreferEmail,

                Approved = b.Approved,

                Active = b.Active,

                Fields = b.Fields,

                Positions = b.Positions
            };
        }

        public BusinessViewModel ToViewModel(business b)
        {
            return new BusinessViewModel
            {
                BusinessID = b.BusinessID,

                UserID = b.UserID,

                BusinessName = b.BusinessName,

                FirstName = b.FirstName,

                LastName = b.LastName,

                Street = b.Street,

                City = b.City,

                State = b.State,

                Zip = b.Zip,

                Phone = b.Phone,

                Alumni = b.Alumni,

                NonProfit = b.NonProfit,

                Outlet = b.Outlet,

                Display = b.Display,

                DisplayDescription = b.DisplayDescription,

                Attendees = b.Attendees,

                BusinessDescription = b.BusinessDescription,

                Website = b.Website,

                SocialMedia = b.SocialMedia,

                Photo = b.Photo,

                LocationPreference = b.LocationPreference,

                ContactMe = b.ContactMe,

                PreferEmail = b.PreferEmail,

                Approved = b.Approved,

                Active = b.Active,

                Fields = b.Fields,

                Positions = b.Positions
            };
        }

        public IEnumerable<business> SelectAll()
        {
            return _businesses;
        }

        public IEnumerable<business> SelectRange(int startRow, int numberOfRows)
        {
            return _ds.Read(startRow, numberOfRows);
        }

        public IList<BusinessViewModel> SelectAllAsViewModel()
        {
            List<BusinessViewModel> bvms = new List<BusinessViewModel>();
            foreach (business b in _businesses)
            {
                BusinessViewModel bvm = ToViewModel(b);
                GetAccountInfoByUserID(bvm);

                bvms.Add(bvm);
            }

            return bvms;
        }

        public business SelectOne(int id)
        {
            business selectedBusiness = _businesses.Where(b => b.BusinessID == id).FirstOrDefault();

            return selectedBusiness;
        }

        public BusinessViewModel SelectOneAsViewModel(int id)
        {
            business bus = _businesses.Where(b => b.BusinessID == id).FirstOrDefault();

            BusinessViewModel selectedBusiness = ToViewModel(bus);
            GetAccountInfoByUserID(selectedBusiness);

            return selectedBusiness;
        }

        public void Update(business business, string serverPath)
        {
            var oldBusiness = _businesses.Where(b => b.BusinessID == business.BusinessID).FirstOrDefault();

            if (oldBusiness != null)
            {
                //_businesses.Remove(oldBusiness);
                //_businesses.Add(business);
                _ds.Update(business, serverPath, oldBusiness.Photo);
            }
        }

        public void UpdateBusinessProfile(business business, string serverPath)
        {
            var oldBusiness = _businesses.Where(b => b.BusinessID == business.BusinessID).FirstOrDefault();

            if (oldBusiness != null)
            {
                //_businesses.Remove(oldBusiness);
                //_businesses.Add(business);
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