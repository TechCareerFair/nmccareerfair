using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.Business2FieldDAL
{
    public class Business2FieldRepository : IBusiness2FieldRepository, IDisposable
    {
        private List<business2field> _business2fields;
        private Business2FieldDatabaseDataService _ds = new Business2FieldDatabaseDataService();

        public Business2FieldRepository()
        {
            _business2fields = _ds.Read();
        }

        public void Delete(int id)
        {
            var business2field = _business2fields.Where(b => b.BusinessFieldID == id).FirstOrDefault();

            if (business2field != null)
            {
                _business2fields.Remove(business2field);
                _ds.Remove(business2field);
            }
        }

        public void Dispose()
        {
            _business2fields = null;
            _ds = null;
        }

        public void Insert(business2field business2field)
        {
            business2field.BusinessFieldID = NextIdValue();
            _business2fields.Add(business2field);

            _ds.Insert(business2field);
        }

        private int NextIdValue()
        {
            int currentMaxId = _business2fields.OrderByDescending(b => b.BusinessFieldID).FirstOrDefault().BusinessFieldID;
            return currentMaxId + 1;
        }

        public IEnumerable<business2field> SelectAll()
        {
            return _business2fields;
        }

        public business2field SelectOne(int id)
        {
            business2field selectedbusiness2field = _business2fields.Where(b => b.BusinessFieldID == id).FirstOrDefault();

            return selectedbusiness2field;
        }

        public void Update(business2field business2field)
        {
            var oldbusiness2field = _business2fields.Where(b => b.BusinessFieldID == business2field.BusinessFieldID).FirstOrDefault();

            if (oldbusiness2field != null)
            {
                _business2fields.Remove(oldbusiness2field);
                _business2fields.Add(business2field);
                _ds.Update(business2field);
            }
        }
    }
}