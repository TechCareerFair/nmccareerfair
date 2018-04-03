using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.DAL.CareerFairDAL;
using TechCareerFair.DAL.FaqDAL;
using TechCareerFair.Models;

namespace TechCareerFair.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            CareerFairRepository careerFairRepo = new CareerFairRepository();
            careerfair selectCareerFair = careerFairRepo.SelectOne(1);

            return View(selectCareerFair);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQ()
        {
            DAL.FaqDAL.FAQRepository faqRepo = new FAQRepository();
                return View(faqRepo.SelectAll());
        }

       
    }
}