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

namespace TechCareerFair.Controllers
{
    public class AdminController : Controller
    {
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
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

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

        public ActionResult ListApplicants()
        {
            if (Session["userName"] != null)
            {
                ApplicantRepository applicantRepository = new ApplicantRepository();
                return View(applicantRepository.SelectAll());
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult ListBusinesses()
        {
            if (Session["userName"] != null)
            {
                BusinessRepository businessRepository = new BusinessRepository();
                return View(businessRepository.SelectAll());
            }
            else
            {
                return RedirectToAction("Index");
            }

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
        
        public string UploadFile(string directory, HttpPostedFileBase postedFile)
        {
            string pathName = "";

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                pathName = DateTime.Now.ToBinary().ToString() + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(Server.MapPath("~"+directory) + pathName);

                pathName = directory + pathName;
            }

            return pathName;
        }
    }
}
