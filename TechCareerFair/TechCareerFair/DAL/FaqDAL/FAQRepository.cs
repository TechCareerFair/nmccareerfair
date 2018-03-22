using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechCareerFair.Models;

namespace TechCareerFair.DAL.FaqDAL
{
    public class FAQRepository : IFAQRepository, IDisposable
    {
        private List<faq> _faqs;
        private FAQDatabaseDataService _ds = new FAQDatabaseDataService();

        public FAQRepository()
        {
            _faqs = _ds.Read();
        }

        public void Delete(int id)
        {
            var faq = _faqs.Where(g => g.FaqID == id).FirstOrDefault();

            if (faq != null)
            {
                _faqs.Remove(faq);
                _ds.Remove(faq);
            }
        }

        public void Dispose()
        {
            _faqs = null;
            _ds = null;
        }

        public void Insert(faq faq)
        {
            faq.FaqID = NextIdValue();
            _faqs.Add(faq);

            _ds.Insert(faq);
        }

        private int NextIdValue()
        {
            int currentMaxId = _faqs.OrderByDescending(f => f.FaqID).FirstOrDefault().FaqID;
            return currentMaxId + 1;
        }

        public IEnumerable<faq> SelectAll()
        {
            return _faqs;
        }

        public faq SelectOne(int id)
        {
            faq selectedFAQ = _faqs.Where(f => f.FaqID == id).FirstOrDefault();

            return selectedFAQ;
        }

        public void Update(faq faq)
        {
            var oldFAQ = _faqs.Where(f => f.FaqID == faq.FaqID).FirstOrDefault();

            if (oldFAQ != null)
            {
                _faqs.Remove(oldFAQ);
                _faqs.Add(faq);
                _ds.Update(faq);
            }
        }
    }
}