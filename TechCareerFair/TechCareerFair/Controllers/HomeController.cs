using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.DAL.CareerFairDAL;
using TechCareerFair.DAL.FaqDAL;
using TechCareerFair.Models;
using PagedList;
using TechCareerFair.CustomAttributes;
using Microsoft.AspNet.Identity;

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
        [AuthorizeOrRedirectAttribute(Roles = "Business")]
        public ActionResult SearchApp(string sortOrder, string searchCriteria, int? page)
        {
            DAL.BusinessRepository br = new DAL.BusinessRepository();
            if(User.IsInRole("Admin") || br.CheckApproved(User.Identity.GetUserName()))
            {
                TechCareerFair.DAL.ApplicantRepository applicantRepository = new TechCareerFair.DAL.ApplicantRepository();
                TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();

                IEnumerable<ApplicantViewModel> apps = applicantRepository.SelectAllSearchListAsViewModel();
                ViewBag.AllFields = fieldRepo.SelectAll();

                ViewBag.CurrentSort = sortOrder;
                ViewBag.CurrentCriteria = searchCriteria;

                int pageSize = 15;
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
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }
        [AuthorizeOrRedirectAttribute(Roles = "Business")]
        [HttpPost]
        public ActionResult SearchApp(string searchCriteria, int? page, ApplicantViewModel applicant, FormCollection collection)
        {
            DAL.BusinessRepository br = new DAL.BusinessRepository();
            if (User.IsInRole("Admin") || br.CheckApproved(User.Identity.GetUserName()))
            {
                int pageSize = 15;
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
                    apps = ar.SelectAllSearchListAsViewModel();
                    apps = FilterApplicants(apps, fieldsSelected, searchCriteria);
                }

                apps = apps.ToPagedList(pageNumber, pageSize);


                return View(apps);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }       
  
        [NonAction]
        private IEnumerable<ApplicantViewModel> FilterApplicants(IEnumerable<ApplicantViewModel> applicants, List<string> fields, string searchCriteria)
        {
           
            if (searchCriteria != null && searchCriteria != "")
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

            applicants = applicants.Where(a => a.Active);

            return applicants;
        }


        //Search Business
        //GET/Home/SearchBus
        [AuthorizeOrRedirectAttribute(Roles = "Applicant")]
        public ActionResult SearchBus(string sortOrder,string searchCriteria, int? page)
        {
            TechCareerFair.DAL.BusinessRepository businessRepository = new TechCareerFair.DAL.BusinessRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fieldRepo = new TechCareerFair.DAL.FieldDAL.FieldRepository();

            IEnumerable<BusinessViewModel> companies = businessRepository.SelectAllSearchListAsViewModel();
            ViewBag.AllFields = fieldRepo.SelectAll();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentCriteria = searchCriteria;
            

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            companies = FilterBusinesses(companies,null, false, searchCriteria);

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
        [AuthorizeOrRedirectAttribute(Roles = "Applicant")]
        [HttpPost]
        public ActionResult SearchBus(string searchCriteria, int? page, BusinessViewModel business, FormCollection collection)
        {

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentCriteria = searchCriteria;
            bool intern = Convert.ToBoolean(collection["internship"].Split(',')[0]);
            ViewBag.InternFilter = intern;

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
                bus= businessRepo.SelectAllSearchListAsViewModel() as IList<BusinessViewModel>;
                bus = FilterBusinesses(bus, fieldsSelected, intern, searchCriteria);
            }

            bus = bus.ToPagedList(pageNumber, pageSize);
            return View(bus);
        }


       
        [NonAction]
        private IEnumerable<BusinessViewModel> FilterBusinesses(IEnumerable<BusinessViewModel> business, List<string> fields, bool intern, string searchCriteria)
        {

            if (searchCriteria != null && searchCriteria != "")
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

            if(intern)
            {
                business = business.Where(b => b.Positions.Any(p => p.Internship));
            }

            business = business.Where(b => b.Active);
            business = business.Where(b => b.Approved);

            return business;
        }
    }
}