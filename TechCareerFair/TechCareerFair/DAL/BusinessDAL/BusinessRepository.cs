using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL
{
    public class BusinessRepository : IBusinessRepository, IDisposable
    {
        private BusinessDatabaseDataService _ds = new BusinessDatabaseDataService();

        public BusinessRepository()
        {
            
        }

        public void GetAccountInfoByUserID(BusinessViewModel bus)
        {
            BusinessViewModel accountInfo = _ds.GetAccountInfoBy(bus.Email);
            
            bus.Password = accountInfo.Password;
        }

        public void Delete(int id, string serverPath)
        {
            var business = SelectOne(id);

            if (business != null)
            {
                //_businesses.Remove(business);
                _ds.Remove(business, serverPath);
            }
        }

        public void Insert(business business)
        {
            //business.BusinessID = NextIdValue();
            //_businesses.Add(business);

            _ds.Insert(business);
        }

        private int NextIdValue()
        {
            int currentMaxId = SelectAll().OrderByDescending(b => b.BusinessID).FirstOrDefault().BusinessID;
            return currentMaxId + 1;
        }

        public business ToModel(BusinessViewModel b)
        {
            return new business
            {
                BusinessID = b.BusinessID,

                Email = b.Email,

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

                Email = b.Email,

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
            return _ds.Read();
        }

        public IEnumerable<business> SelectAllSearchList()
        {
            return _ds.ReadForSearch();
        }

        public IList<BusinessViewModel> SelectAllSearchListAsViewModel()
        {
            List<BusinessViewModel> bvms = new List<BusinessViewModel>();
            IEnumerable<business> businesses = SelectAllSearchList();
            foreach (business b in businesses)
            {
                BusinessViewModel bvm = ToViewModel(b);
                GetAccountInfoByUserID(bvm);

                bvms.Add(bvm);
            }

            return bvms;
        }

        public IEnumerable<business> SelectAllAdminList()
        {
            return _ds.ReadForAdmin();
        }

        public IList<BusinessViewModel> SelectAllAdminListAsViewModel()
        {
            List<BusinessViewModel> bvms = new List<BusinessViewModel>();
            IEnumerable<business> businesses = SelectAllAdminList();
            foreach (business b in businesses)
            {
                BusinessViewModel bvm = ToViewModel(b);
                GetAccountInfoByUserID(bvm);

                bvms.Add(bvm);
            }

            return bvms;
        }

        public IEnumerable<business> SelectRange(int startRow, int numberOfRows)
        {
            return _ds.Read(startRow, numberOfRows);
        }

        public IList<BusinessViewModel> SelectAllAsViewModel()
        {
            List<BusinessViewModel> bvms = new List<BusinessViewModel>();
            IEnumerable<business> businesses = SelectAll();
            foreach (business b in businesses)
            {
                BusinessViewModel bvm = ToViewModel(b);
                GetAccountInfoByUserID(bvm);

                bvms.Add(bvm);
            }

            return bvms;
        }

        public business SelectOne(int id)
        {
            business selectedBusiness = _ds.Read(id);

            return selectedBusiness;
        }

        public business SelectOne(string email)
        {
            business selectedBusiness = _ds.Read(email);

            return selectedBusiness;
        }

        public BusinessViewModel SelectOneAsViewModel(int id)
        {
            business bus = SelectOne(id);

            BusinessViewModel selectedBusiness = ToViewModel(bus);
            GetAccountInfoByUserID(selectedBusiness);

            return selectedBusiness;
        }

        public bool CheckApproved(string email)
        {
            bool isApproved = false;
            BusinessDatabaseDataService service = new BusinessDatabaseDataService();
            isApproved = service.ReadIsApproved(email);

            return isApproved;
        }

        public void Update(business business, string serverPath)
        {
            var oldBusiness = SelectOne(business.BusinessID);

            if (oldBusiness != null)
            {
                //_businesses.Remove(oldBusiness);
                //_businesses.Add(business);
                _ds.Update(business, serverPath, oldBusiness.Photo);
            }
        }

        public void UpdateBusinessProfile(business business, string serverPath)
        {
            var oldBusiness = SelectOne(business.BusinessID);

            if (oldBusiness != null)
            {
                //_businesses.Remove(oldBusiness);
                //_businesses.Add(business);
                _ds.UpdateBusinessProfile(business, serverPath, oldBusiness.Photo);
            }
        }

        public void UpdateBusinessProfile(BusinessViewModel business, string serverPath)
        {
            var oldBusiness = SelectOne(business.BusinessID);

            if (oldBusiness != null)
            {
                //_businesses.Remove(oldBusiness);
                //_businesses.Add(business);
                _ds.UpdateBusinessProfile(ToModel(business), serverPath, oldBusiness.Photo);
            }
        }

        private string BusinessToCSV(business b)
        {
            //sb.Append("BusinessID,Email,BusinessName,ListedPositions,FirstName,LastName,City,State,Street,Zip,Phone,Alumni,NonProfit,Outlet,Display,DisplayDescription,Attendees,BusinessDescription,Website,SocialMedia,LocationPreference,ContactMe,PreferEmail,Approved,Active");

            //string output = b.BusinessID.ToString() + delim + b.Email.Replace(",", string.Empty) + delim + b.BusinessName.Replace(",", string.Empty) + delim +
            //    b.Positions.Count.ToString() + delim + b.FirstName.Replace(",", string.Empty) + delim + b.LastName.Replace(",", string.Empty) + delim +
            //    b.City.Replace(",", string.Empty) + delim + b.State.Replace(",", string.Empty) + delim + b.Street.Replace(",", string.Empty) + delim +
            //    b.Zip.Replace(",", string.Empty) + delim + b.Phone.Replace(",", string.Empty) + delim + b.Alumni.ToString() + delim +
            //    b.NonProfit.ToString() + delim + b.Outlet.ToString() + delim + b.Display.ToString() + delim + b.DisplayDescription.Replace(",", string.Empty) + delim +
            //    b.Attendees.ToString() + delim + b.BusinessDescription.Replace(",", string.Empty) + delim + b.Website.Replace(",", string.Empty) + delim +
            //    b.SocialMedia.Replace(",", string.Empty) + delim + b.LocationPreference.Replace(",", string.Empty) + delim + b.ContactMe.ToString() + delim +
            //    b.PreferEmail.ToString() + delim + b.Approved.ToString() + delim + b.Active.ToString() + delim;

            string formatted = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}",
                b.BusinessID.ToString(), b.Email.Replace(",", string.Empty), b.BusinessName.Replace(",", string.Empty),
                b.Positions.Count.ToString(), b.FirstName.Replace(",", string.Empty), b.LastName.Replace(",", string.Empty),
                b.City.Replace(",", string.Empty), b.State.Replace(",", string.Empty), b.Street.Replace(",", string.Empty),
                b.Zip.Replace(",", string.Empty), b.Phone.Replace(",", string.Empty), b.Alumni.ToString(),
                b.NonProfit.ToString(), b.Outlet.ToString(), b.Display.ToString(), b.DisplayDescription.Replace(",", string.Empty),
                b.Attendees.ToString(), b.BusinessDescription.Replace(",", string.Empty), b.Website.Replace(",", string.Empty),
                b.SocialMedia.Replace(",", string.Empty), b.LocationPreference.Replace(",", string.Empty), b.ContactMe.ToString(),
                b.PreferEmail.ToString(), b.Approved.ToString(), b.Active.ToString());

            return formatted;
        }

        public void CreateBusinessCSV(string csvFile)
        {
            //Server.MapPath("~" + "/App_Data/")
            BusinessRepository br = new BusinessRepository();
            List<business> businesses = br.SelectAll().ToList();

            using (StreamWriter writer = new StreamWriter(new FileStream(csvFile, FileMode.Create, FileAccess.Write)))
            {
                writer.WriteLine("BusinessID,Email,BusinessName,ListedPositions,FirstName,LastName,City,State,Street,Zip,Phone,Alumni,NonProfit,Outlet,Display,DisplayDescription,Attendees,BusinessDescription,Website,SocialMedia,LocationPreference,ContactMe,PreferEmail,Approved,Active");
                foreach (business b in businesses)
                {
                    writer.WriteLine(br.BusinessToCSV(b));
                }
            }
        }

        public void Dispose()
        {
            _ds = null;
        }
    }
}