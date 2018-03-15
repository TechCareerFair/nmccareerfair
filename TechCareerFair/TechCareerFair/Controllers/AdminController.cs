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

namespace TechCareerFair.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult FaqPage()
        {
            DAL.FaqDAL.FAQRepository FaqRepo = new FAQRepository();
            return View(FaqRepo.SelectAll());
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
