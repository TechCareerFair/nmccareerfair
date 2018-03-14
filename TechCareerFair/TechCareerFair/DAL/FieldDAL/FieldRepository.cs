using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.FieldDAL
{
    public class FieldRepository : IFieldRepository, IDisposable
    {
        private List<field> _fields;
        private FieldDatabaseDataService _ds = new FieldDatabaseDataService();

        public FieldRepository()
        {
            _fields = _ds.Read();
        }

        public void Delete(int id)
        {
            var field = _fields.Where(f => f.FieldID == id).FirstOrDefault();

            if (field != null)
            {
                _fields.Remove(field);
                _ds.Remove(field);
            }
        }

        public void Dispose()
        {
            _fields = null;
            _ds = null;
        }

        public void Insert(field field)
        {
            field.FieldID = NextIdValue();
            _fields.Add(field);

            _ds.Insert(field);
        }

        private int NextIdValue()
        {
            int currentMaxId = _fields.OrderByDescending(f => f.FieldID).FirstOrDefault().FieldID;
            return currentMaxId + 1;
        }

        public IEnumerable<field> SelectAll()
        {
            return _fields;
        }

        public field SelectOne(int id)
        {
            field selectedField = _fields.Where(f => f.FieldID == id).FirstOrDefault();

            return selectedField;
        }

        public void Update(field field)
        {
            var oldField = _fields.Where(f => f.FieldID == field.FieldID).FirstOrDefault();

            if (oldField != null)
            {
                _fields.Remove(oldField);
                _fields.Add(field);
                _ds.Update(field);
            }
        }
    }
}