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

        [HttpPost]
        public ActionResult Index(admin admin)
        {
            AdminRepository adminRepository = new AdminRepository();
            admin selectAdmin = adminRepository.SelectOne(1);

            if (ModelState.IsValid)
            {
                if (selectAdmin.Username == admin.Username && selectAdmin.Password.Trim() == admin.Password)
                {
                    Session["userName"] = admin.Username;
                    return RedirectToAction("LandingPage", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                }
            }

            return View();
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
            return View();
        }

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

        [HttpGet]
        public ActionResult FaqEdit(int id)
        {
            DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();

            return View(FaqRepo.SelectOne(id));
        }

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

        public ActionResult ListApplicants()
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            return View(applicantRepository.SelectAll());
        }

        public ActionResult ListBusinesses()
        {
            BusinessRepository businessRepository = new BusinessRepository();
            return View(businessRepository.SelectAll());
        }

        // GET: Admin/Details/5
        public ActionResult ApplicantDetails(int id)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            applicant applicant = applicantRepository.SelectOne(id);
            ViewBag.Fields = applicant.Fields;

            return View(applicant);
        }

        // GET: Admin/Details/5
        public ActionResult BusinessDetails(int id)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            business business = businessRepository.SelectOne(id);
            ViewBag.Fields = business.Fields;
            ViewBag.Positions = business.Positions;

            return View(business);
        }

        // GET: Admin/Create
        public ActionResult CreateApplicant()
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateApplicant(applicant applicant, FormCollection collection)
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

            ApplicantRepository applicantRepository = new ApplicantRepository();
            applicantRepository.Insert(applicant);

            return RedirectToAction("ListApplicants");
        }

        // GET: Admin/Create
        public ActionResult CreateBusiness()
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateBusiness(business business, FormCollection collection)
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

            BusinessRepository br = new BusinessRepository();
            br.Insert(business);

            return RedirectToAction("ListBusinesses");
        }

        // GET: Admin/Edit/5
        public ActionResult EditApplicant(int id)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            applicant applicant = applicantRepository.SelectOne(id);
            ViewBag.Fields = applicant.Fields;

            return View(applicant);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditApplicant(applicant applicant, FormCollection collection)
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

            ApplicantRepository applicantRepository = new ApplicantRepository();
            applicantRepository.Update(applicant);

            return RedirectToAction("ListApplicants");
        }

        // GET: Admin/Edit/5
        public ActionResult EditBusiness(int id)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            business business = businessRepository.SelectOne(id);
            ViewBag.Positions = business.Positions;
            ViewBag.Fields = business.Fields;

            return View(business);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditBusiness(business business, FormCollection collection)
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

            BusinessRepository businessRepository = new BusinessRepository();
            businessRepository.Update(business);

            return RedirectToAction("ListBusinesses");
        }

        // GET: Admin/Delete/5
        public ActionResult DeleteApplicant(int id)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            applicant applicant = applicantRepository.SelectOne(id);
            ViewBag.Fields = applicant.Fields;

            return View(applicant);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult DeleteApplicant(int id, FormCollection collection)
        {
            try
            {
                ApplicantRepository applicantRepository = new ApplicantRepository();
                applicantRepository.Delete(id);

                return RedirectToAction("ListApplicants");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult DeleteBusiness(int id)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            business business = businessRepository.SelectOne(id);
            ViewBag.Fields = business.Fields;
            ViewBag.Positions = business.Positions;

            return View(business);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult DeleteBusiness(int id, FormCollection collection)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            businessRepository.Delete(id);

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
            BusinessRepository businessRepository = new BusinessRepository();
            businessRepository.Delete(id);

            return RedirectToAction("ListBusinesses");
            try
            {

            }
            catch
            {
                return View();
            }
        }
    }
}
