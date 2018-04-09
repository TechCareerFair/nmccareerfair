using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.DAL;
using TechCareerFair.DAL.FieldDAL;
using TechCareerFair.Models;

namespace TechCareerFair.Controllers
{
    public class RegisterController : Controller
    {

        private List<SelectListItem> _states = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Select a U.S. State", Value="NA"},
            new SelectListItem() {Text="Alabama", Value="AL"},
            new SelectListItem() { Text="Alaska", Value="AK"},
            new SelectListItem() { Text="Arizona", Value="AZ"},
            new SelectListItem() { Text="Arkansas", Value="AR"},
            new SelectListItem() { Text="California", Value="CA"},
            new SelectListItem() { Text="Colorado", Value="CO"},
            new SelectListItem() { Text="Connecticut", Value="CT"},
            new SelectListItem() { Text="District of Columbia", Value="DC"},
            new SelectListItem() { Text="Delaware", Value="DE"},
            new SelectListItem() { Text="Florida", Value="FL"},
            new SelectListItem() { Text="Georgia", Value="GA"},
            new SelectListItem() { Text="Hawaii", Value="HI"},
            new SelectListItem() { Text="Idaho", Value="ID"},
            new SelectListItem() { Text="Illinois", Value="IL"},
            new SelectListItem() { Text="Indiana", Value="IN"},
            new SelectListItem() { Text="Iowa", Value="IA"},
            new SelectListItem() { Text="Kansas", Value="KS"},
            new SelectListItem() { Text="Kentucky", Value="KY"},
            new SelectListItem() { Text="Louisiana", Value="LA"},
            new SelectListItem() { Text="Maine", Value="ME"},
            new SelectListItem() { Text="Maryland", Value="MD"},
            new SelectListItem() { Text="Massachusetts", Value="MA"},
            new SelectListItem() { Text="Michigan", Value="MI"},
            new SelectListItem() { Text="Minnesota", Value="MN"},
            new SelectListItem() { Text="Mississippi", Value="MS"},
            new SelectListItem() { Text="Missouri", Value="MO"},
            new SelectListItem() { Text="Montana", Value="MT"},
            new SelectListItem() { Text="Nebraska", Value="NE"},
            new SelectListItem() { Text="Nevada", Value="NV"},
            new SelectListItem() { Text="New Hampshire", Value="NH"},
            new SelectListItem() { Text="New Jersey", Value="NJ"},
            new SelectListItem() { Text="New Mexico", Value="NM"},
            new SelectListItem() { Text="New York", Value="NY"},
            new SelectListItem() { Text="North Carolina", Value="NC"},
            new SelectListItem() { Text="North Dakota", Value="ND"},
            new SelectListItem() { Text="Ohio", Value="OH"},
            new SelectListItem() { Text="Oklahoma", Value="OK"},
            new SelectListItem() { Text="Oregon", Value="OR"},
            new SelectListItem() { Text="Pennsylvania", Value="PA"},
            new SelectListItem() { Text="Rhode Island", Value="RI"},
            new SelectListItem() { Text="South Carolina", Value="SC"},
            new SelectListItem() { Text="South Dakota", Value="SD"},
            new SelectListItem() { Text="Tennessee", Value="TN"},
            new SelectListItem() { Text="Texas", Value="TX"},
            new SelectListItem() { Text="Utah", Value="UT"},
            new SelectListItem() { Text="Vermont", Value="VT"},
            new SelectListItem() { Text="Virginia", Value="VA"},
            new SelectListItem() { Text="Washington", Value="WA"},
            new SelectListItem() { Text="West Virginia", Value="WV"},
            new SelectListItem() { Text="Wisconsin", Value="WI"},
            new SelectListItem() { Text="Wyoming", Value="WY"}
        };
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: Register/Create
        public ActionResult Applicant()
        {
            FieldDatabaseDataService Fds = new FieldDatabaseDataService();

            List<field> fields = Fds.Read();

            ViewBag.Fields = fields;

            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Applicant(ApplicantViewModel applicant, string rePass, HttpPostedFileBase fileUpload)
        {

            FieldDatabaseDataService Fds = new FieldDatabaseDataService();

            List<field> fields = Fds.Read();

            ViewBag.Fields = fields;
            ViewBag.ErrCheckFields = applicant.Fields;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (applicant.Password != rePass)
            {
                ViewBag.rePassErr = "Passwords must match.";
                return View();
            }

            if (fileUpload != null)
            {
                applicant.Resume = UploadFile(DataSettings.RESUME_DIRECTORY, fileUpload);
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

            applicant.Active = true;
            ar.Insert(ar.ToModel(applicant));

            ViewBag.FullName = applicant.FirstName + " " + applicant.LastName;
            return View("PostSignUp");
        }

        // GET: Register/Create
        public ActionResult Business()
        {
            FieldDatabaseDataService Fds = new FieldDatabaseDataService();

            List<field> fields = Fds.Read();

            ViewBag.Fields = fields;
            ViewBag.States = _states;

            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Business(BusinessViewModel business, HttpPostedFileBase fileUpload, string rePass)
        {
            try
            {
                FieldDatabaseDataService Fds = new FieldDatabaseDataService();

                List<field> fields = Fds.Read();

                ViewBag.Fields = fields;
                ViewBag.ErrCheckFields = business.Fields;
                ViewBag.States = _states;

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (business.Password != rePass)
                {
                    ViewBag.rePassErr = "Passwords must match.";
                    return View();
                }

                if (fileUpload != null)
                {
                    business.Photo = UploadFile(DataSettings.RESUME_DIRECTORY, fileUpload);
                }
                BusinessRepository br = new BusinessRepository();
                List<business> businesses = new List<business>();
                int x = 0;

                businesses = br.SelectAll().ToList();

                while (businesses.Count() > x)
                {
                    if (businesses[x].Email == business.Email)
                    {
                        ViewBag.dupAccountErr = "An account with this email already exist. Please use another email or try loging in wwith this one.";
                        return View();
                    }
                    x++;
                }

                business.Approved = false;
                business.Active = true;
                br.Insert(br.ToModel(business));
                ViewBag.FullName = business.FirstName + " " + business.LastName;

                return View("PostSignUp");
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        public string UploadFile(string directory, HttpPostedFileBase postedFile)
        {
            string pathName = "";

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                pathName = DateTime.Now.ToBinary().ToString() + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(Server.MapPath("~" + directory) + pathName);

                pathName = directory + pathName;
            }

            return pathName;
        }
    }
}