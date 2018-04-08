using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using TechCareerFair.DAL;
using TechCareerFair.Models;
using TechCareerFair.DAL.AdminDAL;
using TechCareerFair.DAL.FaqDAL;
using System.Web.Security;
using System.IO;
using PagedList;

namespace TechCareerFair.Controllers
{
    public class AdminController : Controller
    {
        private List<SelectListItem> _states = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Select a U.S. State", Value="NA"},
            new SelectListItem() {Text="Alabama", Value="AL"},
            new SelectListItem() { Text="Alaska", Value="AK"},
            new SelectListItem() { Text="Arizona", Value="AZ"},
            new SelectListItem() { Text="Arkansas", Value="AR"},
            new SelectListItem() { Text="California", Value="CA"},
            new SelectListItem() { Text="Colorado", Value="CO"},
            new SelectListItem() { Text="Connecticut", Value="CT"},
            new SelectListItem() { Text="District of Columbia", Value="DC"},
            new SelectListItem() { Text="Delaware", Value="DE"},
            new SelectListItem() { Text="Florida", Value="FL"},
            new SelectListItem() { Text="Georgia", Value="GA"},
            new SelectListItem() { Text="Hawaii", Value="HI"},
            new SelectListItem() { Text="Idaho", Value="ID"},
            new SelectListItem() { Text="Illinois", Value="IL"},
            new SelectListItem() { Text="Indiana", Value="IN"},
            new SelectListItem() { Text="Iowa", Value="IA"},
            new SelectListItem() { Text="Kansas", Value="KS"},
            new SelectListItem() { Text="Kentucky", Value="KY"},
            new SelectListItem() { Text="Louisiana", Value="LA"},
            new SelectListItem() { Text="Maine", Value="ME"},
            new SelectListItem() { Text="Maryland", Value="MD"},
            new SelectListItem() { Text="Massachusetts", Value="MA"},
            new SelectListItem() { Text="Michigan", Value="MI"},
            new SelectListItem() { Text="Minnesota", Value="MN"},
            new SelectListItem() { Text="Mississippi", Value="MS"},
            new SelectListItem() { Text="Missouri", Value="MO"},
            new SelectListItem() { Text="Montana", Value="MT"},
            new SelectListItem() { Text="Nebraska", Value="NE"},
            new SelectListItem() { Text="Nevada", Value="NV"},
            new SelectListItem() { Text="New Hampshire", Value="NH"},
            new SelectListItem() { Text="New Jersey", Value="NJ"},
            new SelectListItem() { Text="New Mexico", Value="NM"},
            new SelectListItem() { Text="New York", Value="NY"},
            new SelectListItem() { Text="North Carolina", Value="NC"},
            new SelectListItem() { Text="North Dakota", Value="ND"},
            new SelectListItem() { Text="Ohio", Value="OH"},
            new SelectListItem() { Text="Oklahoma", Value="OK"},
            new SelectListItem() { Text="Oregon", Value="OR"},
            new SelectListItem() { Text="Pennsylvania", Value="PA"},
            new SelectListItem() { Text="Rhode Island", Value="RI"},
            new SelectListItem() { Text="South Carolina", Value="SC"},
            new SelectListItem() { Text="South Dakota", Value="SD"},
            new SelectListItem() { Text="Tennessee", Value="TN"},
            new SelectListItem() { Text="Texas", Value="TX"},
            new SelectListItem() { Text="Utah", Value="UT"},
            new SelectListItem() { Text="Vermont", Value="VT"},
            new SelectListItem() { Text="Virginia", Value="VA"},
            new SelectListItem() { Text="Washington", Value="WA"},
            new SelectListItem() { Text="West Virginia", Value="WV"},
            new SelectListItem() { Text="Wisconsin", Value="WI"},
            new SelectListItem() { Text="Wyoming", Value="WY"}
        };

        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //********************
        // Admin Login
        //********************
        [HttpPost]
        public ActionResult Index(admin admin)
        {
            if (ModelState.IsValid)
            {
                if (ValidateAdmin(admin.Username, admin.Password))
                {
                    FormsAuthentication.SetAuthCookie(admin.Username, false);
                    return RedirectToAction("LandingPage", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                }
            }

            return View();
        }

        private bool ValidateAdmin(string userName, string password)
        {
            bool isValid = false;

            AdminRepository adminRepository = new AdminRepository();
            admin selectAdmin = adminRepository.SelectOne(1);

            if (selectAdmin.Username == userName)
            {
                if (selectAdmin.Password.Trim() == password)
                {
                    Session["userID"] = selectAdmin.AdminID;
                    Session["userName"] = selectAdmin.Username;

                    isValid = true;
                }
            }

            return isValid;
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index");
        }

        public ActionResult LandingPage()
        {
            if (Session["userName"] != null)
            {
                DAL.CareerFairDAL.CareerFairRepository LandingPageRepo = new DAL.CareerFairDAL.CareerFairRepository();
                return View(LandingPageRepo.SelectAll());
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult LandingPageEdit(int id)
        {
            if (Session["userName"] != null)
            {
                DAL.CareerFairDAL.CareerFairRepository AddressRepo = new DAL.CareerFairDAL.CareerFairRepository();
                return View(AddressRepo.SelectOne(id));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult LandingPageEdit(int id, careerfair LandingPage)
        {
            if (Session["userName"] != null)
            {
                try
                {
                    DAL.CareerFairDAL.CareerFairRepository careerFairLandingPage = new DAL.CareerFairDAL.CareerFairRepository();
                    careerFairLandingPage.Update(LandingPage);

                    return RedirectToAction("LandingPage");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        //FAQ//////////////////////////////////////
        public ActionResult FaqPage()
        {
            if (Session["userName"] != null)
            {
                DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
                return View(FaqRepo.SelectAll());
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult FaqDetails(int id)
        {
            if (Session["userName"] != null)
            {
                DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
                faq _faq = FaqRepo.SelectOne(id);
                return View(_faq);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult FaqCreate()
        {
            if (Session["userName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult FaqCreate(faq _faq)
        {
            if (Session["userName"] != null)
            {
                try
                {
                    DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
                    FaqRepo.Insert(_faq);

                    return RedirectToAction("FaqPage");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult FaqEdit(int id)
        {
            if (Session["userName"] != null)
            {
                DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();

                return View(FaqRepo.SelectOne(id));
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult FaqEdit(int id, faq _faq)
        {
            if (Session["userName"] != null)
            {
                try
                {
                    DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
                    FaqRepo.Update(_faq);

                    return RedirectToAction("FaqPage");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult FaqDelete(int id)
        {

            if (Session["userName"] != null)
            {
                DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
                return View(FaqRepo.SelectOne(id));
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult FaqDelete(int id, faq _faq)
        {

            if (Session["userName"] != null)
            {
                try
                {
                    DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
                    FaqRepo.Delete(id);

                    return RedirectToAction("FaqPage");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        //Field Of Study////////////////////////////////
        public ActionResult FieldOfStudy()
        {
            if (Session["userName"] != null)
            {
                DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
                return View(FieldOfStudyRepo.SelectAll());
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult FieldOfStudyCreate()
        {
            if (Session["userName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }  
        }

        [HttpPost]
        public ActionResult FieldOfStudyCreate(field _field)
        {

            if (Session["userName"] != null)
            {
                try
                {
                    DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
                    FieldOfStudyRepo.Insert(_field);

                    return RedirectToAction("FieldOfStudy");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult FieldOfStudyEdit(int id)
        {
            if (Session["userName"] != null)
            {

                DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
                return View(FieldOfStudyRepo.SelectOne(id));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult FieldOfStudyEdit(int id, field _field)
        {

            if (Session["userName"] != null)
            {
                try
                {
                    DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
                    FieldOfStudyRepo.Update(_field);

                    return RedirectToAction("FieldOfStudy");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult FieldOfStudyDelete(int id)
        {

            if (Session["userName"] != null)
            {
                DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
                return View(FieldOfStudyRepo.SelectOne(id));
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult FieldOfStudyDelete(int id, field _field)
        {

            if (Session["userName"] != null)
            {
                try
                {
                    DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
                    FieldOfStudyRepo.Delete(id);

                    return RedirectToAction("FieldOfStudy");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult FieldOfStudyDetails(int id)
        {

            if (Session["userName"] != null)
            {
                DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
                field _field = FieldOfStudyRepo.SelectOne(id);

                return View(_field);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //Gallery////////////////////////////////////
        public ActionResult Gallery()
        {
            if (Session["userName"] != null)
            {
                DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
                return View(GalleryRepo.SelectAll());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult GalleryEdit(int id)
        {
            if (Session["userName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GalleryEdit(int id, gallery _gallery)
        {
            if (Session["userName"] != null)
            {
                try
                {
                    DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
                    GalleryRepo.Update(_gallery, Server.MapPath("~"));

                    return RedirectToAction("FieldOfStudy");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult GalleryDelete(int id)
        {
            if (Session["userName"] != null)
            {
                DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
                return View(GalleryRepo.SelectOne(id));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GalleryDelete(int id, gallery _gallery)
        {

            if (Session["userName"] != null)
            {
                try
                {
                    DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
                    GalleryRepo.Delete(id, Server.MapPath("~"));

                    return RedirectToAction("Gallery");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult GalleryCreate()
        {
            if (Session["userName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GalleryCreate(gallery _gallery)
        {

            if (Session["userName"] != null)
            {
                try
                {
                    DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
                    GalleryRepo.Insert(_gallery);

                    return RedirectToAction("Gallery");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult GalleryDetails(int id)
        {

            if (Session["userName"] != null)
            {
                DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
                gallery _gallery = GalleryRepo.SelectOne(id);

                return View(_gallery);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        //Applicants and Business////////////////////////////////////
        public ActionResult ListApplicants(string sortOrder, string filter, string searchCriteria, int? page)
        {
            if (Session["userName"] != null)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.CurrentCriteria = searchCriteria;
                ViewBag.CurrentFilter = filter;

                int pageSize = 30;
                int pageNumber = (page ?? 1);

                ApplicantRepository applicantRepository = new ApplicantRepository();
                IEnumerable<applicant> applicants = applicantRepository.SelectAll();

                applicants = FilterApplicants(applicants, filter, searchCriteria);

                switch (sortOrder)
                {
                    case "FirstName":
                        applicants = applicants.OrderBy(a => a.FirstName);
                        break;
                    case "LastName":
                        applicants = applicants.OrderBy(a => a.LastName);
                        break;
                    case "Active":
                        applicants = applicants.OrderBy(a => a.Active);
                        break;
                    default:
                        applicants = applicants.OrderBy(a => a.Email);
                        break;
                }

                applicants = applicants.ToPagedList(pageNumber, pageSize);
                return View(applicants);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult ListApplicants(string searchCriteria, string filter, int? page)
        {
            if (Session["userName"] != null)
            {
                int pageSize = 30;
                int pageNumber = (page ?? 1);

                ViewBag.CurrentCriteria = searchCriteria;
                ViewBag.CurrentFilter = filter;

                ApplicantRepository ar = new ApplicantRepository();

                IEnumerable<applicant> applicants;
                using (ar)
                {
                    applicants = ar.SelectAll() as IList<applicant>;
                }

                applicants = FilterApplicants(applicants, filter, searchCriteria);

                applicants = applicants.ToPagedList(pageNumber, pageSize);

                return View(applicants);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [NonAction]
        private IEnumerable<applicant> FilterApplicants(IEnumerable<applicant> applicants, string filter, string searchCriteria)
        {
            if (searchCriteria != null)
            {
                applicants = applicants.Where(a => a.LastName.ToUpper().Contains(searchCriteria.ToUpper()));
            }

            if (filter != null)
            {
                switch (filter)
                {
                    case "Active":
                        applicants = applicants.Where(a => a.Active);
                        break;
                    case "Inactive":
                        applicants = applicants.Where(a => !a.Active);
                        break;
                }
            }
            return applicants;
        }

        public ActionResult ListBusinesses(string sortOrder, string filter, string searchCriteria, int? page)
        {
            if (Session["userName"] != null)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.CurrentCriteria = searchCriteria;
                ViewBag.CurrentFilter = filter;

                int pageSize = 30;
                int pageNumber = (page ?? 1);

                BusinessRepository businessRepository = new BusinessRepository();
                IEnumerable<business> businesses = businessRepository.SelectAll();

                businesses = FilterBusinesses(businesses, filter, searchCriteria);

                switch (sortOrder)
                {
                    case "BusinessName":
                        businesses = businesses.OrderBy(b => b.BusinessName);
                        break;
                    case "ContactMe":
                        businesses = businesses.OrderBy(b => b.ContactMe);
                        break;
                    case "PreferEmail":
                        businesses = businesses.OrderBy(b => b.PreferEmail);
                        break;
                    case "Active":
                        businesses = businesses.OrderBy(b => b.Active);
                        break;
                    case "Approved":
                        businesses = businesses.OrderBy(b => b.Approved);
                        break;
                    default:
                        businesses = businesses.OrderBy(b => b.Email);
                        break;
                }

                businesses = businesses.ToPagedList(pageNumber, pageSize);
                return View(businesses);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult ListBusinesses(string searchCriteria, string filter, int? page)
        {
            if (Session["userName"] != null)
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);

                ViewBag.CurrentCriteria = searchCriteria;
                ViewBag.CurrentFilter = filter;

                BusinessRepository br = new BusinessRepository();

                IEnumerable<business> businesses;
                using (br)
                {
                    businesses = br.SelectAll() as IList<business>;
                }

                businesses = FilterBusinesses(businesses, filter, searchCriteria);

                businesses = businesses.ToPagedList(pageNumber, pageSize);

                return View(businesses);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [NonAction]
        private IEnumerable<business> FilterBusinesses(IEnumerable<business> businesses, string filter, string searchCriteria)
        {
            if (searchCriteria != null)
            {
                businesses = businesses.Where(b => b.BusinessName.ToUpper().Contains(searchCriteria.ToUpper()));
            }

            if (filter != null)
            {
                switch (filter)
                {
                    case "Approved":
                        businesses = businesses.Where(b => b.Approved);
                        break;
                    case "NotApproved":
                        businesses = businesses.Where(b => !b.Approved);
                        break;
                    case "Active":
                        businesses = businesses.Where(b => b.Active);
                        break;
                    case "Inactive":
                        businesses = businesses.Where(b => !b.Active);
                        break;
                }
            }

            return businesses;
        }

        // GET: Admin/Details/5
        public ActionResult ApplicantDetails(int id)
        {
            if (Session["userName"] != null)
            {
                ApplicantRepository applicantRepository = new ApplicantRepository();
                applicant applicant = applicantRepository.SelectOne(id);
                ViewBag.Fields = applicant.Fields;

                return View(applicant);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // GET: Admin/Details/5
        public ActionResult BusinessDetails(int id)
        {
            if (Session["userName"] != null)
            {
                BusinessRepository businessRepository = new BusinessRepository();
                business business = businessRepository.SelectOne(id);
                ViewBag.Fields = business.Fields;
                ViewBag.Positions = business.Positions;

                return View(business);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // GET: Admin/Create
        public ActionResult CreateApplicant()
        {
            if (Session["userName"] != null)
            {
                ApplicantRepository applicantRepository = new ApplicantRepository();
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                ViewBag.AllFields = fr.SelectAll();

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateApplicant(applicant applicant, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                List<field> fields = fr.SelectAll().ToList();
                foreach (field f in fields)
                {
                    bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

                    if (isChecked)
                    {
                        applicant.Fields.Add(f.Name);
                    }
                }

                applicant.Resume = UploadFile(DataSettings.RESUME_DIRECTORY, fileUpload);

                ApplicantRepository applicantRepository = new ApplicantRepository();
                applicantRepository.Insert(applicant);

                return RedirectToAction("ListApplicants");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // GET: Admin/Create
        public ActionResult CreateBusiness()
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                ViewBag.AllFields = fr.SelectAll();
                ViewBag.States = _states;

                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateBusiness(business business, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                List<field> fields = fr.SelectAll().ToList();
                foreach (field f in fields)
                {
                    bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

                    if (isChecked)
                    {
                        business.Fields.Add(f.Name);
                    }
                }

                business.State = collection["state"];
                
                business.Photo = UploadFile(DataSettings.BUSINESS_DIRECTORY, fileUpload);

                BusinessRepository br = new BusinessRepository();
                br.Insert(business);

                return RedirectToAction("ListBusinesses");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }



        // GET: Admin/Edit/5
        public ActionResult EditApplicant(int id)
        {
            if (Session["userName"] != null)
            {
                ApplicantRepository applicantRepository = new ApplicantRepository();
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                ViewBag.AllFields = fr.SelectAll();

                applicant applicant = applicantRepository.SelectOne(id);
                ViewBag.Fields = applicant.Fields;

                return View(applicant);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditApplicant(applicant applicant, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                List<field> fields = fr.SelectAll().ToList();
                foreach (field f in fields)
                {
                    bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

                    if (!applicant.Fields.Contains(f.Name) && isChecked)
                    {
                        applicant.Fields.Add(f.Name);
                    }
                    else if (applicant.Fields.Contains(f.Name) && !isChecked)
                    {
                        applicant.Fields.Remove(f.Name);
                    }
                }

                if (Convert.ToBoolean(collection["removeResume"].Split(',')[0]))
                {
                    applicant.Resume = "";
                    if ((System.IO.File.Exists(Server.MapPath("~") + applicant.Resume)))
                    {
                        System.IO.File.Delete(Server.MapPath("~") + applicant.Resume);
                    }
                }

                if (fileUpload != null)
                {
                    applicant.Resume = UploadFile(DataSettings.RESUME_DIRECTORY, fileUpload);
                }

                ApplicantRepository applicantRepository = new ApplicantRepository();
                applicantRepository.Update(applicant, Server.MapPath("~"));

                return RedirectToAction("ListApplicants");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // GET: Admin/Edit/5
        public ActionResult EditBusiness(int id)
        {
            if (Session["userName"] != null)
            {
                BusinessRepository businessRepository = new BusinessRepository();
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                ViewBag.AllFields = fr.SelectAll();

                business business = businessRepository.SelectOne(id);
                ViewBag.Positions = business.Positions;
                ViewBag.Fields = business.Fields;
                ViewBag.States = _states;

                return View(business);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditBusiness(business business, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                List<field> fields = fr.SelectAll().ToList();
                foreach (field f in fields)
                {
                    bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

                    if (!business.Fields.Contains(f.Name) && isChecked)
                    {
                        business.Fields.Add(f.Name);
                    }
                    else if (business.Fields.Contains(f.Name) && !isChecked)
                    {
                        business.Fields.Remove(f.Name);
                    }
                }

                if(Convert.ToBoolean(collection["removeImage"].Split(',')[0]))
                {
                    business.Photo = "";
                    if ((System.IO.File.Exists(Server.MapPath("~")+business.Photo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~")+business.Photo);
                    }
                }

                if (fileUpload != null)
                {
                    business.Photo = UploadFile(DataSettings.BUSINESS_DIRECTORY, fileUpload);
                }

                BusinessRepository businessRepository = new BusinessRepository();
                businessRepository.Update(business, Server.MapPath("~"));

                return RedirectToAction("ListBusinesses");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult ListPositions(int id)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
                List<position> positions = pr.SelectAll().ToList();

                BusinessRepository businessRepository = new BusinessRepository();
                business business = businessRepository.SelectOne(id);
                ViewBag.Business = business.BusinessName;
                ViewBag.ID = id;

                positions = positions.Where(p => p.Business == id).ToList();

                return View(positions);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Create
        public ActionResult CreatePosition(int id)
        {
            if (Session["userName"] != null)
            {
                BusinessRepository br = new BusinessRepository();
                business bus = br.SelectOne(id);
                ViewBag.Business = bus.BusinessName;
                ViewBag.ID = id;

                position position = new position();
                position.Business = id;

                return View(position);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreatePosition(position position)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
                pr.Insert(position);

                return RedirectToAction("ListPositions", new { id = position.Business });
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // GET: Admin/Details/5
        public ActionResult PositionDetails(int id)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
                position position = pr.SelectOne(id);

                BusinessRepository br = new BusinessRepository();
                business bus = br.SelectOne(position.Business);
                ViewBag.Business = bus.BusinessName;

                return View(position);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult EditPosition(int id)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
                position position = pr.SelectOne(id);

                return View(position);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditPosition(position position)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
                pr.Update(position);

                return RedirectToAction("ListPositions", new { id = position.Business });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/Create
        public ActionResult DeletePosition(int id)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
                position position = pr.SelectOne(id);

                BusinessRepository br = new BusinessRepository();
                business bus = br.SelectOne(position.Business);
                ViewBag.Business = bus.BusinessName;

                return View(position);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult DeletePosition(int id, FormCollection collection)
        {
            if (Session["userName"] != null)
            {
                TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
                position position = pr.SelectOne(id);
                int businessID = position.Business;

                pr.Delete(id);

                return RedirectToAction("ListPositions", new { id = businessID });
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // GET: Admin/Delete/5
        public ActionResult DeleteApplicant(int id)
        {
            if (Session["userName"] != null)
            {
                ApplicantRepository applicantRepository = new ApplicantRepository();
                applicant applicant = applicantRepository.SelectOne(id);
                ViewBag.Fields = applicant.Fields;

                return View(applicant);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult DeleteApplicant(int id, FormCollection collection)
        {
            if (Session["userName"] != null)
            {
                try
                {
                    ApplicantRepository applicantRepository = new ApplicantRepository();
                    applicantRepository.Delete(id, Server.MapPath("~"));

                    return RedirectToAction("ListApplicants");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // GET: Admin/Delete/5
        public ActionResult DeleteBusiness(int id)
        {
            if (Session["userName"] != null)
            {
                BusinessRepository businessRepository = new BusinessRepository();
                business business = businessRepository.SelectOne(id);
                ViewBag.Fields = business.Fields;
                ViewBag.Positions = business.Positions;

                return View(business);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult DeleteBusiness(int id, FormCollection collection)
        {
            if (Session["userName"] != null)
            {
                try
                {
                    BusinessRepository businessRepository = new BusinessRepository();
                    businessRepository.Delete(id, Server.MapPath("~"));

            return RedirectToAction("ListBusinesses");
            try
            {
                
            }
            catch
            {
                return View();
            }
        }

        public ActionResult pos(int id, FormCollection collection)
        {
            string pathName = "";

            return RedirectToAction("ListBusinesses");
            try
            {
                pathName = DateTime.Now.ToBinary().ToString() + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(Server.MapPath("~"+directory) + pathName);

                pathName = directory + pathName;
            }

            return pathName;
        }
    }
}
