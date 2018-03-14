using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.CareerFairDAL
{
    public class CareerFairRepository : ICareerFairRepository, IDisposable
    {
        private List<careerfair> _careerFairs;
        private CareerFairDatabaseDataService _ds = new CareerFairDatabaseDataService();

        public CareerFairRepository()
        {
            _careerFairs = _ds.Read();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _careerFairs = null;
            _ds = null;
        }

        public void Insert(careerfair careerfair)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<careerfair> SelectAll()
        {
            return _careerFairs;
        }

        public careerfair SelectOne(int id)
        {
            careerfair selectedCareerFair = _careerFairs.Where(c => c.CareerFairID == id).FirstOrDefault();

            return selectedCareerFair;
        }

        public void Update(careerfair careerfair)
        {
            var oldCareerFair = _careerFairs.Where(c => c.CareerFairID == careerfair.CareerFairID).FirstOrDefault();

            if (oldCareerFair != null)
            {
                _careerFairs.Remove(oldCareerFair);
                _careerFairs.Add(careerfair);
                _ds.Update(careerfair);
            }
        }
    }
}