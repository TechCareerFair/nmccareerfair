using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.DAL;
using TechCareerFair.Models;

namespace TechCareerFair.Controllers
{
    public class ViewProfileController : Controller
    {
        public ActionResult BusinessViewProfile(int id)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            business business = new business();

            using (businessRepository)
            {
                business = businessRepository.SelectOne(id);
            }
            return View(business);
        }

        public ActionResult ApplicantViewProfile(int id)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            applicant applicant = new applicant();

            using (applicantRepository)
            {
                applicant = applicantRepository.SelectOne(id);
            }
            return View(applicant);
        }
    }
}