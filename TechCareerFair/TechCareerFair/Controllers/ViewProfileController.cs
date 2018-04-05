using System;
using System.Collections.Generic;
using System.IO;
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

        // GET: ViewProfile/Edit
        // Business
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

        // POST: ViewProfile/Edit
        // Business
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBusiness([Bind(Include = "BusinessID,Email,BusinessName,FirstName,LastName,Fields,Street,City,State,Zip,Phone,Alumni,NonProfit,Outlet,Display,DisplayDescription,Attendees,BusinessDescription,Website,SocialMedia,Photo,LocationPreference,ContactMe,PreferEmail")]business business, HttpPostedFileBase fileUpload, FormCollection collection)
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

                if (Convert.ToBoolean(collection["removeImage"].Split(',')[0]))
                {
                    business.Photo = "";
                    if ((System.IO.File.Exists(Server.MapPath("~") + business.Photo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~") + business.Photo);
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

        // GET: ViewProfile/Edit
        // Applicant
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

        // POST: ViewProfile/Edit
        // Applicant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditApplicant([Bind(Include = "ApplicantID,Email,FirstName,LastName,Fields,University,Alumni,Profile,SocialMedia,Resume,YearsExperience,Internship")]applicant applicant, HttpPostedFileBase fileUpload, FormCollection collection)
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

                //applicant = applicantRepository.SelectOne(id);

                return RedirectToAction("ApplicantViewProfile", new { id = applicant.ApplicantID });
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
                postedFile.SaveAs(Server.MapPath("~" + directory) + pathName);

                pathName = directory + pathName;
            }

            return pathName;
        }
    }
}
