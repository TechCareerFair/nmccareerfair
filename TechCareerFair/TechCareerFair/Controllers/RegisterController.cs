using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
