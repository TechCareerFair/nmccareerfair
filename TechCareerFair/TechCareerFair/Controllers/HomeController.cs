using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.DAL.CareerFairDAL;
using TechCareerFair.DAL.FaqDAL;
using TechCareerFair.Models;
using PagedList;

namespace TechCareerFair.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            CareerFairRepository careerFairRepo = new CareerFairRepository();
            careerfair selectCareerFair = careerFairRepo.SelectOne(1);

            return View(selectCareerFair);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQ()
        {
            DAL.FaqDAL.FAQRepository faqRepo = new FAQRepository();
            ViewBag.Faq = faqRepo.SelectAll();

            return View();
        }

        //GET/Home/SearchApp
        public ActionResult SearchApp(string sortOrder, string filter, string searchCriteria, int? page)
        {

            TechCareerFair.DAL.ApplicantRepository applicantRepository = new TechCareerFair.DAL.ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();

            IEnumerable<ApplicantViewModel> apps = applicantRepository.SelectAllAsViewModel();
            ViewBag.AllField = fieldRepo.SelectAll();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentCriteria = searchCriteria;
            ViewBag.CurrentFilter = filter;

            int pageSize = 30;
            int pageNumber = (page ?? 1);

            apps = FilterApplicants(apps, filter, searchCriteria);

            switch (sortOrder)
            {
                case "FirstName":
                    apps = apps.OrderBy(a => a.FirstName);
                    break;
                case "LastName":
                    apps = apps.OrderBy(a => a.LastName);
                    break;

                default:                  
                    break;
            }

            apps = apps.ToPagedList(pageNumber, pageSize);
            return View(apps);
        }
        [HttpPost]
        public ActionResult SearchApp(string searchCriteria, string filter, int? page, ApplicantViewModel applicant, FormCollection collection)
        {

            int pageSize = 30;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentCriteria = searchCriteria;
            ViewBag.CurrentFilter = filter;



            TechCareerFair.DAL.ApplicantRepository ar = new TechCareerFair.DAL.ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            IEnumerable<ApplicantViewModel> apps;

            using (ar)
            {
                apps = ar.SelectAllAsViewModel() as IList<ApplicantViewModel>;
                apps = FilterApplicants(apps, filter, searchCriteria);
            }

            apps = apps.ToPagedList(pageNumber, pageSize);
            return View(apps);
        }


            //List<field> fields = fieldRepo.SelectAll().ToList();

            //foreach (field f in fields)
            //{
            //    bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

            //    if (!applicant.Fields.Contains(f.Name) && isChecked)
            //    {
            //        applicant.Fields.Add(f.Name);
            //    }
            //    else if (applicant.Fields.Contains(f.Name) && !isChecked)
            //    {
            //        applicant.Fields.Remove(f.Name);
            //    }
            //}

          
        


        [NonAction]
        private IEnumerable<ApplicantViewModel> FilterApplicants(IEnumerable<ApplicantViewModel> applicants, string filter, string searchCriteria)
        {
           
            if (searchCriteria != null)
            {
                applicants = applicants.Where(a => a.LastName.ToUpper().Contains(searchCriteria.ToUpper()));

            }
          

            return applicants;
        }


        //Search Business
        //GET/Home/SearchBus
        public ActionResult SearchBus(string sortOrder, string filter, string searchCriteria, int? page)
        {

            TechCareerFair.DAL.BusinessRepository businessRepository = new TechCareerFair.DAL.BusinessRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();

            IEnumerable<BusinessViewModel> companies = businessRepository.SelectAllAsViewModel();
            ViewBag.AllField = fieldRepo.SelectAll();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentCriteria = searchCriteria;
            ViewBag.CurrentFilter = filter;

            int pageSize = 30;
            int pageNumber = (page ?? 1);

            companies = FilterBusiness(companies, filter, searchCriteria);

            switch (sortOrder)
            {
                case "FirstName":
                    companies = companies.OrderBy(a => a.FirstName);
                    break;
                case "LastName":
                    companies = companies.OrderBy(a => a.LastName);
                    break;

                default:
                    break;
            }

            companies = companies.ToPagedList(pageNumber, pageSize);
            return View(companies);
        }
        [HttpPost]
        public ActionResult SearchBus(string searchCriteria, string filter, int? page, BusinessViewModel licant, FormCollection collection)
        {

            int pageSize = 30;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentCriteria = searchCriteria;
            ViewBag.CurrentFilter = filter;



            TechCareerFair.DAL.BusinessRepository businessRepo = new TechCareerFair.DAL.BusinessRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            IEnumerable<BusinessViewModel> bus;

            using (businessRepo)
            {
                bus= businessRepo.SelectAllAsViewModel() as IList<BusinessViewModel>;
                bus = FilterBusiness(bus, filter, searchCriteria);
            }

            bus = bus.ToPagedList(pageNumber, pageSize);
            return View(bus);
        }


        //List<field> fields = fieldRepo.SelectAll().ToList();

        //foreach (field f in fields)
        //{
        //    bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

        //    if (!applicant.Fields.Contains(f.Name) && isChecked)
        //    {
        //        applicant.Fields.Add(f.Name);
        //    }
        //    else if (applicant.Fields.Contains(f.Name) && !isChecked)
        //    {
        //        applicant.Fields.Remove(f.Name);
        //    }
        //}





        [NonAction]
        private IEnumerable<BusinessViewModel> FilterBusiness(IEnumerable<BusinessViewModel> business, string filter, string searchCriteria)
        {

            if (searchCriteria != null)
            {
                business = business.Where(a => a.BusinessName.ToUpper().Contains(searchCriteria.ToUpper()));

            }


            return business;
        }




    }
}