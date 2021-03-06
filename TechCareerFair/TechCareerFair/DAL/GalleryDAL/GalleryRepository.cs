﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.GalleryDAL
{
    public class GalleryRepository : IGalleryRepository, IDisposable
    {
        private List<gallery> _galleries;
        private GalleryDatabaseDataService _ds = new GalleryDatabaseDataService();

        public GalleryRepository()
        {
            _galleries = _ds.Read();
        }

        public void Delete(int id, string serverPath)
        {
            var gallery = _galleries.Where(g => g.GalleryID == id).FirstOrDefault();

            if (gallery != null)
            {
                _galleries.Remove(gallery);
                _ds.Remove(gallery, serverPath);
            }
        }

        public void Dispose()
        {
            _galleries = null;
            _ds = null;
        }

        public void Insert(gallery gallery)
        {
            //gallery.GalleryID = NextIdValue();
            //_galleries.Add(gallery);

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

        public void Update(gallery gallery, string serverPath)
        {
            var oldGallery = _galleries.Where(g => g.GalleryID == gallery.GalleryID).FirstOrDefault();

            if (oldGallery != null)
            {
                _galleries.Remove(oldGallery);
                _galleries.Add(gallery);
                _ds.Update(gallery, serverPath, oldGallery.Directory);
            }
        }
    }
}