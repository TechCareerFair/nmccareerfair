using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.DAL;
using TechCareerFair.Models;
using static TechCareerFair.Controllers.ManageController;

namespace TechCareerFair.Controllers
{
    public class ViewProfileController : Controller
    {

        public ActionResult GetUserType(ManageMessageId? message)
        {
            string userName = User.Identity.GetUserName();

            ApplicantRepository ar = new ApplicantRepository();
            applicant applicant = ar.SelectAll().Where(a => a.Email == userName).FirstOrDefault();

            BusinessRepository br = new BusinessRepository();
            business business = br.SelectAll().Where(a => a.Email == userName).FirstOrDefault();

            if (applicant != null)
            {
                return ApplicantViewProfile(applicant, message);
            }
            else if (business != null)
            {
                return BusinessViewProfile(business, message);
            }
            else
            {
                return View("Error");
            }


        }

        public ActionResult BusinessViewProfile(business business, ManageMessageId? message)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            BusinessViewModel businessVM = businessRepository.ToViewModel(business);
            ViewBag.Fields = business.Fields;
            ViewBag.Positions = business.Positions;
            ViewBag.SocialMedia = business.SocialMedia;
            ViewBag.Website = business.Website;

            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            return View("BusinessViewProfile", businessVM);

        }

        // GET: ViewProfile/Edit
        // Business
        public ActionResult EditBusiness(int id)
        {
            BusinessRepository businessRepository = new BusinessRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            BusinessViewModel business = businessRepository.SelectOneAsViewModel(id);
            ViewBag.Positions = business.Positions;
            ViewBag.Fields = business.Fields;
            ViewBag.States = DAL.DataSettings.US_STATES;

            if (User.Identity.GetUserName() == business.Email)
            {
                return View(business);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }

        }

        // POST: ViewProfile/Edit
        // Business
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBusiness([Bind(Include = "BusinessID,Email,BusinessName,FirstName,LastName,Fields,Positions,Street,City,State,Zip,Phone,Alumni,NonProfit,Outlet,Display,DisplayDescription,Attendees,BusinessDescription,Website,SocialMedia,Photo,LocationPreference,ContactMe,PreferEmail")]BusinessViewModel business, HttpPostedFileBase fileUpload, FormCollection collection)
        {
                if (User.Identity.GetUserName() == business.Email)
                {
                    try
                    {
                        ViewBag.Positions = business.Positions;

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
                            business.Photo = DAL.DatabaseHelper.UploadFile(DataSettings.BUSINESS_DIRECTORY, fileUpload, Server);
                        }

                        BusinessRepository businessRepository = new BusinessRepository();
                        businessRepository.UpdateBusinessProfile(businessRepository.ToModel(business), Server.MapPath("~"));

                        return GetUserType(null);

                    }
                    catch (ArgumentException e)
                    {
                        ViewBag.Error = e.Message;
                        return View(business);
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
        }

        public ActionResult ListPositions(int id)
        {

            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            List<position> positions = pr.SelectAll().ToList();

            BusinessRepository businessRepository = new BusinessRepository();
            business business = businessRepository.SelectOne(id);
            ViewBag.Business = business.BusinessName;
            ViewBag.ID = id;

            positions = positions.Where(p => p.Business == id).ToList();

            return View(positions);
        }

        // GET
        public ActionResult CreatePosition(int id)
        {
            BusinessRepository br = new BusinessRepository();
            business bus = br.SelectOne(id);
            ViewBag.Business = bus.BusinessName;
            ViewBag.ID = id;

            position position = new position();
            position.Business = id;

            return View(position);
        }

        // POST
        [HttpPost]
        public ActionResult CreatePosition(position position)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            pr.Insert(position);

            return RedirectToAction("ListPositions", new { id = position.Business });

        }

        // GET
        public ActionResult PositionDetails(int id)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            position position = pr.SelectOne(id);

            BusinessRepository br = new BusinessRepository();
            business bus = br.SelectOne(position.Business);
            ViewBag.Business = bus.BusinessName;

            return View(position);
        }

        // GET
        public ActionResult EditPosition(int id)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            position position = pr.SelectOne(id);

            return View(position);
        }

        // POST
        [HttpPost]
        public ActionResult EditPosition(position position)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            pr.Update(position);

            return RedirectToAction("ListPositions", new { id = position.Business });
        }

        // GET
        public ActionResult DeletePosition(int id)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            position position = pr.SelectOne(id);

            BusinessRepository br = new BusinessRepository();
            business bus = br.SelectOne(position.Business);
            ViewBag.Business = bus.BusinessName;

            return View(position);
        }

        // POST
        [HttpPost]
        public ActionResult DeletePosition(int id, FormCollection collection)
        {
            TechCareerFair.DAL.PositionDAL.PositionRepository pr = new DAL.PositionDAL.PositionRepository();
            position position = pr.SelectOne(id);
            int businessID = position.Business;

            pr.Delete(id);

            return RedirectToAction("ListPositions", new { id = businessID });
        }

        public ActionResult ApplicantViewProfile(applicant applicant, ManageMessageId? message)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            ApplicantViewModel applicantVM = applicantRepository.ToViewModel(applicant);
            ViewBag.Fields = applicant.Fields;
            ViewBag.SocialMedia = applicant.SocialMedia;

            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            return View("ApplicantViewProfile", applicantVM);
        }

        // GET: ViewProfile/Edit
        // Applicant
        public ActionResult EditApplicant(int id)
        {
            ApplicantRepository applicantRepository = new ApplicantRepository();
            TechCareerFair.DAL.FieldDAL.FieldRepository fr = new TechCareerFair.DAL.FieldDAL.FieldRepository();
            ViewBag.AllFields = fr.SelectAll();

            ApplicantViewModel applicant = applicantRepository.SelectOneAsViewModel(id);
            ViewBag.Fields = applicant.Fields;

            if (User.Identity.GetUserName() == applicant.Email)
            {
                return View(applicant);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: ViewProfile/Edit
        // Applicant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditApplicant([Bind(Include = "ApplicantID,Email,FirstName,LastName,Fields,University,Alumni,Profile,SocialMedia,Resume,YearsExperience,Internship")]ApplicantViewModel applicant, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            if (User.Identity.GetUserName() == applicant.Email)
            {
                try
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
                        applicant.Resume = DatabaseHelper.UploadFile(DataSettings.RESUME_DIRECTORY, fileUpload, Server);
                    }

                    ApplicantRepository applicantRepository = new ApplicantRepository();
                    applicantRepository.UpdateApplicantProfile(applicantRepository.ToModel(applicant), Server.MapPath("~"));

                    //applicant = applicantRepository.SelectOne(id);

                    return GetUserType(null);
                }
                catch (ArgumentException e)
                {
                    ViewBag.Error = e.Message;
                    return View(applicant);
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
