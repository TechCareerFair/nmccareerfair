using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.PositionDAL
{
    public class PositionRepository : IPositionRepository, IDisposable
    {
        private List<position> _positions;
        private PositionDatabaseDataService _ds = new PositionDatabaseDataService();

        public PositionRepository()
        {
            _positions = _ds.Read();
        }

        public void Delete(int id)
        {
            var position = _positions.Where(p => p.PositionID == id).FirstOrDefault();

            if (position != null)
            {
                _positions.Remove(position);
                _ds.Remove(position);
            }
        }

        public void Dispose()
        {
            _positions = null;
            _ds = null;
        }

        public void Insert(position position)
        {
            position.PositionID = NextIdValue();
            _positions.Add(position);

            _ds.Insert(position);
        }

        private int NextIdValue()
        {
            int currentMaxId = _positions.OrderByDescending(p => p.PositionID).FirstOrDefault().PositionID;
            return currentMaxId + 1;
        }

        public IEnumerable<position> SelectAll()
        {
            return _positions;
        }

        public position SelectOne(int id)
        {
            position selectedPosition = _positions.Where(p => p.PositionID == id).FirstOrDefault();

            return selectedPosition;
        }

        public void Update(position position)
        {
            var oldPosition = _positions.Where(p => p.PositionID == position.PositionID).FirstOrDefault();

            if (oldPosition != null)
            {
                _positions.Remove(oldPosition);
                _positions.Add(position);
                _ds.Update(position);
            }
        }
    }
}