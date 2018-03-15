using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.DAL;
using TechCareerFair.Models;

namespace TechCareerFair.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: Register/Create
        public ActionResult Applicant()
        {
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Applicant(applicant applicant, string rePass)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (applicant.Password != rePass)
                {
                    ViewBag.rePassErr = "Passwords must match.";
                    return View();
                }

                ApplicantRepository ar = new ApplicantRepository();
                List<applicant> applicants = new List<applicant>();
                int x = 0;

                applicants = ar.SelectAll().ToList();

                while (applicants.Count() > x)
                {
                    if (applicants[x].Email == applicant.Email)
                    {
                        ViewBag.dupAccountErr = "An account with this email already exist. Please use another email or try loging in wwith this one.";
                        return View();
                    }
                    x++;
                }

                ar.Insert(applicant);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Register/Create
        public ActionResult Business()
        {
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Business(business business, string rePass)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (business.Password != rePass)
                {
                    ViewBag.rePassErr = "Passwords must match.";
                    return View();
                }

                BusinessRepository br = new BusinessRepository();
                List<business> applicants = new List<business>();
                int x = 0;

                applicants = br.SelectAll().ToList();

                while (applicants.Count() > x)
                {
                    if (applicants[x].Email == business.Email)
                    {
                        ViewBag.dupAccountErr = "An account with this email already exist. Please use another email or try loging in wwith this one.";
                        return View();
                    }
                    x++;
                }

                br.Insert(business);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
