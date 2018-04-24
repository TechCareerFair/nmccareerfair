using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.PositionDAL
{
    public class PositionRepository : IPositionRepository, IDisposable
    {
        private PositionDatabaseDataService _ds = new PositionDatabaseDataService();

        public PositionRepository()
        {
        }

        public void Delete(int id)
        {
            var position = SelectAll().Where(p => p.PositionID == id).FirstOrDefault();

            if (position != null)
            {
                _ds.Remove(position);
            }
        }

        public void Dispose()
        {
            _ds = null;
        }

        public void Insert(position position)
        {
            //position.PositionID = NextIdValue();
            //SelectAll().Add(position);

            _ds.Insert(position);
        }

        public IEnumerable<position> SelectAll()
        {
            return _ds.Read();
        }

        public position SelectOne(int id)
        {
            position selectedPosition = _ds.Read(id);

            return selectedPosition;
        }

        public void Update(position position)
        {
            var oldPosition = SelectAll().Where(p => p.PositionID == position.PositionID).FirstOrDefault();

            if (oldPosition != null)
            {
                _ds.Update(position);
            }
        }
    }
}