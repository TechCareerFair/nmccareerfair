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
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using TechCareerFair.CustomAttributes;

namespace TechCareerFair.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //********************
        // Admin Login
        //********************
        //[HttpPost]
        //public ActionResult Index(admin admin)
        //{
        //    if (ValidateAdmin(admin.Username, admin.Password))
        //    {
        //        FormsAuthentication.SetAuthCookie(admin.Username, false);
        //        return RedirectToAction("LandingPage", "Admin");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Invalid username and/or password");
        //    }

        //    return View();
        //}

        //private bool ValidateAdmin(string userName, string password)
        //{
        //    bool isValid = false;

        //    AdminRepository adminRepository = new AdminRepository();
        //    admin selectAdmin = adminRepository.SelectOne(1);

        //    if (selectAdmin.Username == userName)
        //    {
        //        if (selectAdmin.Password.Trim() == password)
        //        {
        //            Session["userID"] = selectAdmin.AdminID;
        //            Session["userName"] = selectAdmin.Username;

        //            isValid = true;
        //        }
        //    }

        //    return isValid;
        //}

        //public ActionResult LogOut()
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Abandon(); // it will clear the session at the end of request
        //    return RedirectToAction("Index");
        //}

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult LandingPage()
        {
            DAL.CareerFairDAL.CareerFairRepository LandingPageRepo = new DAL.CareerFairDAL.CareerFairRepository();

            AdminRepository ar = new AdminRepository();
            ViewBag.Admin = ar.SelectOne(1);

            return View(LandingPageRepo.SelectAll());

        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult LandingPageEdit(int id)
        {
            DAL.CareerFairDAL.CareerFairRepository AddressRepo = new DAL.CareerFairDAL.CareerFairRepository();
            return View(AddressRepo.SelectOne(id));
            
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LandingPageEdit(int id, careerfair LandingPage)
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


        //FAQ//////////////////////////////////////
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult FaqPage()
        {
            DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
            return View(FaqRepo.SelectAll());
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult FaqDetails(int id)
        {
            DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
            faq _faq = FaqRepo.SelectOne(id);
            return View(_faq);
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult FaqCreate()
        {
            return View();
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult FaqCreate(faq _faq)
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

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult FaqEdit(int id)
        {
            DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();

            return View(FaqRepo.SelectOne(id));

        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult FaqEdit(int id, faq _faq)
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

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult FaqDelete(int id)
        {
            DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
            return View(FaqRepo.SelectOne(id));

        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult FaqDelete(int id, faq _faq)
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


        //Field Of Study////////////////////////////////
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult FieldOfStudy()
        {
            DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
            return View(FieldOfStudyRepo.SelectAll());
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult FieldOfStudyCreate()
        {
            return View();
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult FieldOfStudyCreate(field _field)
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

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult FieldOfStudyEdit(int id)
        {
            DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
            return View(FieldOfStudyRepo.SelectOne(id));
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult FieldOfStudyEdit(int id, field _field)
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

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult FieldOfStudyDelete(int id)
        {
            DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
            return View(FieldOfStudyRepo.SelectOne(id));
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult FieldOfStudyDelete(int id, field _field)
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

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult FieldOfStudyDetails(int id)
        {
            DAL.FieldDAL.FieldRepository FieldOfStudyRepo = new DAL.FieldDAL.FieldRepository();
            field _field = FieldOfStudyRepo.SelectOne(id);

            return View(_field);
        }

        //Gallery////////////////////////////////////
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult Gallery()
        {
            DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
            return View(GalleryRepo.SelectAll());
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult GalleryEdit(int id)
        {
            DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
            return View(GalleryRepo.SelectOne(id));
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GalleryEdit(gallery _gallery, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            try
            {
                if (Convert.ToBoolean(collection["removeImage"].Split(',')[0]))
                {
                    _gallery.Directory = "";
                    if ((System.IO.File.Exists(Server.MapPath("~") + _gallery.Directory)))
                    {
                        System.IO.File.Delete(Server.MapPath("~") + _gallery.Directory);
                    }
                }

                if (fileUpload != null)
                {
                    _gallery.Directory = DAL.DatabaseHelper.UploadFile(DataSettings.GALLERY_DIRECTORY, fileUpload, Server);
                }

                DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
                GalleryRepo.Update(_gallery, Server.MapPath("~"));

                return RedirectToAction("Gallery");
            }
            catch
            {
                return View();
            }
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult GalleryDelete(int id)
        {
            DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
            return View(GalleryRepo.SelectOne(id));
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GalleryDelete(int id, gallery _gallery)
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

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult GalleryCreate()
        {
            return View();
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GalleryCreate(gallery _gallery, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            try
            {
                if (fileUpload != null)
                {
                    _gallery.Directory = DAL.DatabaseHelper.UploadFile(DataSettings.GALLERY_DIRECTORY, fileUpload, Server);
                }

                DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
                GalleryRepo.Insert(_gallery);

                return RedirectToAction("Gallery");
            }
            catch
            {
                return View();
            }
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult GalleryDetails(int id)
        {
            DAL.GalleryDAL.GalleryRepository GalleryRepo = new DAL.GalleryDAL.GalleryRepository();
            gallery _gallery = GalleryRepo.SelectOne(id);

            return View(_gallery);
        }

        //Applicants and Business////////////////////////////////////
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult ListApplicants(string sortOrder, string filter, string searchCriteria, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentCriteria = searchCriteria;
            ViewBag.CurrentFilter = filter;

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ApplicantRepository applicantRepository = new ApplicantRepository();
            IEnumerable<ApplicantViewModel> apps = applicantRepository.SelectAllAsViewModel();
                
            apps = FilterApplicants(apps, filter, searchCriteria);

            switch (sortOrder)
            {
                case "FirstName":
                    apps = apps.OrderBy(a => a.FirstName);
                    break;
                case "LastName":
                    apps = apps.OrderBy(a => a.LastName);
                    break;
                case "Active":
                    apps = apps.OrderBy(a => a.Active);
                    break;
                default:
                    apps = apps.OrderBy(a => a.Email);
                    break;
            }

            apps = apps.ToPagedList(pageNumber, pageSize);
            return View(apps);

        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ListApplicants(string searchCriteria, string filter, int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentCriteria = searchCriteria;
            ViewBag.CurrentFilter = filter;

            ApplicantRepository ar = new ApplicantRepository();

            IEnumerable<ApplicantViewModel> apps;
            using (ar)
            {
                apps = ar.SelectAllAsViewModel() as IList<ApplicantViewModel>;
                apps = FilterApplicants(apps, filter, searchCriteria);
            }

            apps = apps.ToPagedList(pageNumber, pageSize);

            return View(apps);
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [NonAction]
        private IEnumerable<ApplicantViewModel> FilterApplicants(IEnumerable<ApplicantViewModel> applicants, string filter, string searchCriteria)
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

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult ListBusinesses(string sortOrder, string filter, string searchCriteria, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentCriteria = searchCriteria;
            ViewBag.CurrentFilter = filter;

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            BusinessRepository businessRepository = new BusinessRepository();
            IEnumerable<BusinessViewModel> businesses = businessRepository.SelectAllAsViewModel();
            FilterBusinesses(businesses, filter, searchCriteria);
                

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

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ListBusinesses(string searchCriteria, string filter, int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentCriteria = searchCriteria;
            ViewBag.CurrentFilter = filter;

            BusinessRepository br = new BusinessRepository();

            IEnumerable<BusinessViewModel> businesses;
            using (br)
            {
                businesses = br.SelectAllAsViewModel() as IList<BusinessViewModel>;
            }

            FilterBusinesses(businesses, filter, searchCriteria);

            businesses = businesses.ToPagedList(pageNumber, pageSize);

            return View(businesses);
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [NonAction]
        private void FilterBusinesses(IEnumerable<BusinessViewModel> businesses, string filter, string searchCriteria)
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
        }

        // GET: Admin/Details/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult ApplicantDetails(int id)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            ApplicantViewModel applicant = applicantRepository.SelectOneAsViewModel(id);

            ViewBag.Fields = applicant.Fields;

            return View(applicant);
        }

        // GET: Admin/Details/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult BusinessDetails(int id)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            BusinessViewModel business = businessRepository.SelectOneAsViewModel(id);
            ViewBag.Fields = business.Fields;
            ViewBag.Positions = business.Positions;

            return View(business);
        }

        // GET: Admin/Create
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult CreateApplicant()
        {
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            return View();
        }

        // POST: Admin/Create
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateApplicant(ApplicantViewModel applicant, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                    IEnumerable<field> iFields = fr.SelectAll();
                    List<field> fields = iFields.ToList();
                    ViewBag.AllFields = iFields;
                    foreach (field f in fields)
                    {
                        bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

                        if (isChecked)
                        {
                            applicant.Fields.Add(f.Name);
                        }
                    }

                    if(fileUpload != null)
                    {
                        applicant.Resume = DAL.DatabaseHelper.UploadFile(DataSettings.RESUME_DIRECTORY, fileUpload, Server);
                    }

                    ApplicantRepository applicantRepository = new ApplicantRepository();
                    applicantRepository.Insert(applicantRepository.ToModel(applicant));

                    EditUserViewModel user = new EditUserViewModel();
                    user.Email = applicant.Email;
                    user.Password = applicant.Password;
                    user.ConfirmPassword = applicant.ConfirmPassword;

                    await CreateUserAsync(user, true);

                    return RedirectToAction("ListApplicants");
                }
                catch(ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return View(applicant);
                }
            }
            else
            {
                return View(applicant);
            }

        }

        // GET: Admin/Create
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult CreateBusiness()
        {
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();
            ViewBag.States = DataSettings.US_STATES;

            return View();
        }

        // POST: Admin/Create
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateBusiness(BusinessViewModel business, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                    IEnumerable<field> iFields = fr.SelectAll();
                    List<field> fields = iFields.ToList();
                    ViewBag.AllFields = iFields;
                    ViewBag.States = DataSettings.US_STATES;

                    foreach (field f in fields)
                    {
                        bool isChecked = Convert.ToBoolean(collection[f.Name].Split(',')[0]);

                        if (isChecked)
                        {
                            business.Fields.Add(f.Name);
                        }
                    }

                    business.State = collection["state"];

                    if(fileUpload != null)
                    {
                        business.Photo = DAL.DatabaseHelper.UploadFile(DataSettings.BUSINESS_DIRECTORY, fileUpload, Server);
                    }

                    BusinessRepository br = new BusinessRepository();
                    br.Insert(br.ToModel(business));

                    EditUserViewModel user = new EditUserViewModel();
                    user.Email = business.Email;
                    user.Password = business.Password;
                    user.ConfirmPassword = business.ConfirmPassword;

                    await CreateUserAsync(user, false);

                    return RedirectToAction("ListBusinesses");
                }
                catch (ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return View(business);
                }
            }
            else
            {
                return View(business);
            }
            
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult EditAdmin(int id)
        {
            AdminRepository ar = new AdminRepository();
            AdminViewModel admin = ar.SelectOneAsViewModel(id);
            admin.OldUsername = admin.Username;
            admin.ConfirmPassword = admin.Password;

            return View(admin);
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditAdmin(AdminViewModel admin)
        {
            if (ModelState.IsValid)
            {
                AdminRepository ar = new AdminRepository();
                ar.Update(ar.ToModel(admin));

                EditUserViewModel user = new EditUserViewModel();
                user.Email = admin.Username;
                user.Password = admin.Password;
                user.ConfirmPassword = admin.Password;

                EditUser(user, admin.OldUsername);

                return RedirectToAction("LandingPage");
            }
            else
            {
                return View(admin);
            }
        }

        // GET: Admin/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult EditApplicant(int id)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            ApplicantViewModel applicant = applicantRepository.SelectOneAsViewModel(id);
            ViewBag.Fields = applicant.Fields;
            applicant.ConfirmPassword = applicant.Password;
            applicant.OldEmail = applicant.Email;

            return View(applicant);
        }

        // POST: Admin/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditApplicant(ApplicantViewModel applicant, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                { 
                TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                    IEnumerable<field> iFields = fr.SelectAll();
                    List<field> fields = iFields.ToList();
                    ViewBag.AllFields = iFields;
                    ViewBag.Fields = applicant.Fields;
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
                    applicant.Resume = DAL.DatabaseHelper.UploadFile(DataSettings.RESUME_DIRECTORY, fileUpload, Server);
                }

                ApplicantRepository applicantRepository = new ApplicantRepository();
                applicantRepository.Update(applicantRepository.ToModel(applicant), Server.MapPath("~"));

                EditUserViewModel user = new EditUserViewModel();
                user.Email = applicant.Email;
                user.Password = applicant.Password;
                user.ConfirmPassword = applicant.Password;

                EditUser(user, applicant.OldEmail);

                return RedirectToAction("ListApplicants");
            }
                catch (ArgumentException e)
            {
                ViewBag.Error = e.Message;
                return View(applicant);
            }
        }
            else
            {
                return View(applicant);
            }
        }

        // GET: Admin/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult EditBusiness(int id)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            BusinessViewModel business = businessRepository.SelectOneAsViewModel(id);
            ViewBag.Positions = business.Positions;
            ViewBag.Fields = business.Fields;
            ViewBag.States = DataSettings.US_STATES;
            business.ConfirmPassword = business.Password;
            business.OldEmail = business.Email;

            return View(business);
        }

        // POST: Admin/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditBusiness(BusinessViewModel business, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.Positions = business.Positions;
                    ViewBag.Fields = business.Fields;
                    ViewBag.States = DataSettings.US_STATES;

                    TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
                    IEnumerable<field> iFields = fr.SelectAll();
                    List<field> fields = iFields.ToList();
                    ViewBag.AllFields = iFields;
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

                    if (Convert.ToBoolean(collection["removeImage"].Split(',')[0]))
                    {
                        business.Photo = "";
                        if ((System.IO.File.Exists(Server.MapPath("~") + business.Photo)))
                        {
                            System.IO.File.Delete(Server.MapPath("~") + business.Photo);
                        }
                    }

                    if (fileUpload != null)
                    {
                        business.Photo = DAL.DatabaseHelper.UploadFile(DataSettings.BUSINESS_DIRECTORY, fileUpload, Server);
                    }

                    BusinessRepository businessRepository = new BusinessRepository();
                    businessRepository.Update(businessRepository.ToModel(business), Server.MapPath("~"));

                    EditUserViewModel user = new EditUserViewModel();
                    user.Email = business.Email;
                    user.Password = business.Password;
                    user.ConfirmPassword = business.Password;

                    EditUser(user, business.OldEmail);

                    return RedirectToAction("ListBusinesses");
                }
                catch(ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return View(business);
                }
            }
            else
            {
                return View(business);
            }
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult ListPositions(int id)
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

        // GET: Admin/Create
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult CreatePosition(int id)
        {
            BusinessRepository br = new BusinessRepository();
            business bus = br.SelectOne(id);
            ViewBag.Business = bus.BusinessName;
            ViewBag.ID = id;

            position position = new position();
            position.Business = id;

            return View(position);
        }

        // POST: Admin/Create
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreatePosition(position position)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            pr.Insert(position);

            return RedirectToAction("ListPositions", new { id = position.Business });
        }

        // GET: Admin/Details/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult PositionDetails(int id)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            position position = pr.SelectOne(id);

            BusinessRepository br = new BusinessRepository();
            business bus = br.SelectOne(position.Business);
            ViewBag.Business = bus.BusinessName;

            return View(position);
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult EditPosition(int id)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            position position = pr.SelectOne(id);

            return View(position);
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditPosition(position position)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            pr.Update(position);

            return RedirectToAction("ListPositions", new { id = position.Business });
        }

        // GET: Admin/Create
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult DeletePosition(int id)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            position position = pr.SelectOne(id);

            BusinessRepository br = new BusinessRepository();
            business bus = br.SelectOne(position.Business);
            ViewBag.Business = bus.BusinessName;

            return View(position);
        }

        // POST: Admin/Create
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeletePosition(int id, FormCollection collection)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            position position = pr.SelectOne(id);
            int businessID = position.Business;

            pr.Delete(id);

            return RedirectToAction("ListPositions", new { id = businessID });
        }

        // GET: Admin/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult DeleteApplicant(int id)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            ApplicantViewModel applicant = applicantRepository.SelectOneAsViewModel(id);
            ViewBag.Fields = applicant.Fields;

            return View(applicant);
        }

        // POST: Admin/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteApplicant(int id, FormCollection collection)
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

        // GET: Admin/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult DeleteBusiness(int id)
        {
                BusinessRepository businessRepository = new BusinessRepository();
                BusinessViewModel business = businessRepository.SelectOneAsViewModel(id);
                ViewBag.Fields = business.Fields;
                ViewBag.Positions = business.Positions;

                return View(business);
        }

        // POST: Admin/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteBusiness(int id, FormCollection collection)
        {
            try
            {
                BusinessRepository businessRepository = new BusinessRepository();
                businessRepository.Delete(id, Server.MapPath("~"));

                return RedirectToAction("ListBusinesses");
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        private async Task<bool> CreateUserAsync([Bind(Include = "Email, Password, ConfirmPassword")] EditUserViewModel user, bool isApplicant)
        {
            var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
            var db = new ApplicationDbContext();
            var newUser = new ApplicationUser();

            newUser.Email = user.Email;
            newUser.UserName = user.Email;
            newUser.PasswordHash = passwordHasher.HashPassword(user.Password);

            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var result = await userManager.CreateAsync(newUser, user.Password);
            if(result.Succeeded)
            {
                //add new user to default role
                string role = "Guest";

                if(isApplicant)
                {
                    role = "Applicant";
                }
                else
                {
                    role = "Business";
                }

                await userManager.AddToRoleAsync(newUser.Id, role);
            }

            return result.Succeeded;
        }

        [NonAction]
        private void EditUser([Bind(Include = "Email, Password, ConfirmPassword")] EditUserViewModel user, string oldEmail)
        {
            var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
            var db = new ApplicationDbContext();
            var editedUser = db.Users.First(u => u.Email == oldEmail);

            editedUser.Email = user.Email;
            editedUser.UserName = user.Email;
            if(editedUser.PasswordHash != user.Password)
            {
                editedUser.PasswordHash = passwordHasher.HashPassword(user.Password);
            }

            db.Entry(editedUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult CreateApplicantCSV()
        {
            string fileName = "accounts.csv";
            string csvFile = Server.MapPath("~" + "/App_Data/") + fileName;
            ApplicantRepository ar = new ApplicantRepository();

            ar.CreateApplicantCSV(csvFile);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(csvFile, contentType, fileName);
        }

        [AuthorizeOrRedirectAttribute(Roles = "Admin")]
        public ActionResult CreateBusinessCSV()
        {
            string fileName = "accounts.csv";
            string csvFile = Server.MapPath("~" + "/App_Data/") + fileName;
            BusinessRepository br = new BusinessRepository();

            br.CreateBusinessCSV(csvFile);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(csvFile, contentType, fileName);
        }
    }
}
