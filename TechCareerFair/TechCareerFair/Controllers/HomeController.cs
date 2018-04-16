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
        public ActionResult SearchApp(string sortOrder, string searchCriteria, int? page)
        {

            TechCareerFair.DAL.ApplicantRepository applicantRepository = new TechCareerFair.DAL.ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();

            IEnumerable<ApplicantViewModel> apps = applicantRepository.SelectAllAsViewModel();
            ViewBag.AllFields = fieldRepo.SelectAll();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentCriteria = searchCriteria;

            int pageSize = 30;
            int pageNumber = (page ?? 1);

            apps = FilterApplicants(apps, null, searchCriteria);

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
        public ActionResult SearchApp(string searchCriteria, int? page, ApplicantViewModel applicant, FormCollection collection)
        {

            int pageSize = 30;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentCriteria = searchCriteria;



            TechCareerFair.DAL.ApplicantRepository ar = new TechCareerFair.DAL.ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            IEnumerable<ApplicantViewModel> apps;

            List<string> fieldsSelected = new List<string>();

            List<field> fields = fieldRepo.SelectAll().ToList();

            foreach (field f in fields)
            {
                bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

                if (isChecked)
                {
                    fieldsSelected.Add(f.Name);
                }
            }

            ViewBag.AllFields = fields;
            ViewBag.Fields = fieldsSelected;


            using (ar)
            {
                apps = ar.SelectAllAsViewModel() as IList<ApplicantViewModel>;
                apps = FilterApplicants(apps, fieldsSelected, searchCriteria);
            }

            apps = apps.ToPagedList(pageNumber, pageSize);


            return View(apps);
        }       
  
        [NonAction]
        private IEnumerable<ApplicantViewModel> FilterApplicants(IEnumerable<ApplicantViewModel> applicants, List<string> fields, string searchCriteria)
        {
           
            if (searchCriteria != null)
            {
                applicants = applicants.Where(a => a.LastName.ToUpper().Contains(searchCriteria.ToUpper()));

            }

            if(fields != null && fields.Count > 0)
            {
                foreach(string f in fields)
                {
                    applicants = applicants.Where(a => a.Fields.Contains(f));
                }
                
            }          

            return applicants;
        }


        //Search Business
        //GET/Home/SearchBus
        public ActionResult SearchBus(string sortOrder,string searchCriteria, int? page)
        {

            TechCareerFair.DAL.BusinessRepository businessRepository = new TechCareerFair.DAL.BusinessRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();

            IEnumerable<BusinessViewModel> companies = businessRepository.SelectAllAsViewModel();
            ViewBag.AllFields = fieldRepo.SelectAll();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentCriteria = searchCriteria;
            

            int pageSize = 30;
            int pageNumber = (page ?? 1);

            companies = FilterBusinesses(companies,null, searchCriteria);

            switch (sortOrder)
            {
                case "FirstName":
                    companies = companies.OrderBy(a => a.FirstName);
                    break;
                case "LastName":
                    companies = companies.OrderBy(a => a.LastName);
                    break;
                case "BusinessName":
                    companies = companies.OrderBy(a => a.BusinessName);
                    break;
                default:
                    break;
            }

            companies = companies.ToPagedList(pageNumber, pageSize);
            return View(companies);
        }
        [HttpPost]
        public ActionResult SearchBus(string searchCriteria, int? page, BusinessViewModel business, FormCollection collection)
        {

            int pageSize = 30;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentCriteria = searchCriteria;
        

            TechCareerFair.DAL.BusinessRepository businessRepo = new TechCareerFair.DAL.BusinessRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            IEnumerable<BusinessViewModel> bus;

            List<string> fieldsSelected = new List<string>();

            List<field> fields = fieldRepo.SelectAll().ToList();

            foreach (field f in fields)
            {
                bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

                if (isChecked)
                {
                    fieldsSelected.Add(f.Name);
                }
            }

            ViewBag.AllFields = fields;
            ViewBag.Fields = fieldsSelected;


            using (businessRepo)
            {
                bus= businessRepo.SelectAllAsViewModel() as IList<BusinessViewModel>;
                bus = FilterBusinesses(bus, fieldsSelected, searchCriteria);
            }

            bus = bus.ToPagedList(pageNumber, pageSize);
            return View(bus);
        }


       
        [NonAction]
        private IEnumerable<BusinessViewModel> FilterBusinesses(IEnumerable<BusinessViewModel> business, List<string> fields, string searchCriteria)
        {

            if (searchCriteria != null)
            {
                business = business.Where(b => b.BusinessName.ToUpper().Contains(searchCriteria.ToUpper()));

            }
            if (fields != null && fields.Count > 0)
            {
                foreach (string f in fields)
                {
                    business = business.Where(b => b.Fields.Contains(f));
                }

            }

            business = business.Where(b => b.Approved);

            return business;
        }
    }
}