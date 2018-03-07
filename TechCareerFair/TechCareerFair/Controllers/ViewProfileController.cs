using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechCareerFair.Controllers
{
    public class ViewProfileController : Controller
    {
        public ActionResult BusinessViewProfile()
        {
<<<<<<< HEAD
            return View();
        }

        public ActionResult ApplicantViewProfile()
        {
<<<<<<< HEAD
            return View();
=======
            ApplicantRepository applicantRepository = new ApplicantRepository();
            applicant applicant = new applicant();

            using (applicantRepository)
            {
                applicant = applicantRepository.SelectOne(id);
            }
            return View(applicant);
>>>>>>> master
        }
    }
}