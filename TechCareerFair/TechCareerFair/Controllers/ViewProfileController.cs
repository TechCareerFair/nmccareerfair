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
            return View();
        }

        public ActionResult ApplicantViewProfile()
        {
            return View();
        }
    }
}