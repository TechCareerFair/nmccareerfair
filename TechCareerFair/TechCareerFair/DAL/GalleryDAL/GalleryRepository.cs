using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.GalleryDAL
{
    public class PositionRepository : IPositionRepository, IDisposable
    {
        private List<gallery> _galleries;
        private PositionDatabaseDataService _ds = new PositionDatabaseDataService();

        public PositionRepository()
        {
            _galleries = _ds.Read();
        }

        public void Delete(int id)
        {
            var gallery = _galleries.Where(g => g.GalleryID == id).FirstOrDefault();

            if (gallery != null)
            {
                _galleries.Remove(gallery);
                _ds.Remove(gallery);
            }
        }

        public void Dispose()
        {
            _galleries = null;
            _ds = null;
        }

        public void Insert(gallery gallery)
        {
            gallery.GalleryID = NextIdValue();
            _galleries.Add(gallery);

            _ds.Insert(gallery);
        }

        private int NextIdValue()
        {
            int currentMaxId = _galleries.OrderByDescending(g => g.GalleryID).FirstOrDefault().GalleryID;
            return currentMaxId + 1;
        }

        public IEnumerable<gallery> SelectAll()
        {
            return _galleries;
        }

        public gallery SelectOne(int id)
        {
            gallery selectedGallery = _galleries.Where(g => g.GalleryID == id).FirstOrDefault();

            return selectedGallery;
        }

        public void Update(gallery gallery)
        {
            var oldGallery = _galleries.Where(g => g.GalleryID == gallery.GalleryID).FirstOrDefault();

            if (oldGallery != null)
            {
                _galleries.Remove(oldGallery);
                _galleries.Add(gallery);
                _ds.Update(gallery);
            }
        }
    }
}