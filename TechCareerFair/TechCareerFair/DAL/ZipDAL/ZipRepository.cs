using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.ZipDAL
{
    public class ZipRepository : IZipRepository, IDisposable
    {
        private List<Zipcode> _zipcodes;
        private ZipDatabaseDataService _ds = new ZipDatabaseDataService();

        public ZipRepository()
        {
            _zipcodes = _ds.Read();
        }

        public void Delete(int id)
        {
            var zipcode = _zipcodes.Where(z => z.ZipCodeID == id).FirstOrDefault();

            if (zipcode != null)
            {
                _zipcodes.Remove(zipcode);
                _ds.Remove(zipcode);
            }
        }

        public void Dispose()
        {
            _zipcodes = null;
            _ds = null;
        }

        public void Insert(Zipcode zipcode)
        {
            zipcode.ZipCodeID = NextIdValue();
            _zipcodes.Add(zipcode);

            _ds.Insert(zipcode);
        }

        private int NextIdValue()
        {
            int currentMaxId = _zipcodes.OrderByDescending(z => z.ZipCodeID).FirstOrDefault().ZipCodeID;
            return currentMaxId + 1;
        }

        public IEnumerable<Zipcode> SelectAll()
        {
            return _zipcodes;
        }

        public Zipcode SelectOne(int id)
        {
            Zipcode selectedZip = _zipcodes.Where(z => z.ZipCodeID == id).FirstOrDefault();

            return selectedZip;
        }

        public void Update(Zipcode zipcode)
        {
            var oldZip = _zipcodes.Where(z => z.ZipCodeID == zipcode.ZipCodeID).FirstOrDefault();

            if (oldZip != null)
            {
                _zipcodes.Remove(oldZip);
                _zipcodes.Add(zipcode);
                _ds.Update(zipcode);
            }
        }
    }
}