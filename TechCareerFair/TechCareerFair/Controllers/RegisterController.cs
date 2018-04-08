﻿using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
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
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: Register/Create
        public ActionResult Applicant()
        {
            //FieldRepository fr = new FieldRepository();

            List<field> fields = new List<field>();

            //Test Data
            fields.Add(new field { FieldID = 1, Name = "Networking" });
            fields.Add(new field { FieldID = 2, Name = "Web Design" });
            fields.Add(new field { FieldID = 3, Name = "System Analyst" });

            ViewBag.Fields = fields;

            //fields = fr.SelectAll() as List<field>;
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Applicant(applicant applicant, string rePass)
        {
            List<field> fields = new List<field>();

            //Test Data
            fields.Add(new field { FieldID = 1, Name = "Networking" });
            fields.Add(new field { FieldID = 2, Name = "Web Design" });
            fields.Add(new field { FieldID = 3, Name = "System Analyst" });

            ViewBag.Fields = fields;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (/*applicant.Password != rePass*/false)
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
                    if (/*applicants[x].Email == applicant.Email*/false)
                    {
                        ViewBag.dupAccountErr = "An account with this email already exist. Please use another email or try loging in wwith this one.";
                        return View();
                    }
                    x++;
                }

                
                applicant.Active = true;
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
            //FieldRepository fr = new FieldRepository();

            List<field> fields = new List<field>();

            //Test Data
            fields.Add(new field { FieldID = 1, Name = "Networking" });
            fields.Add(new field { FieldID = 2, Name = "Web Design" });
            fields.Add(new field { FieldID = 3, Name = "System Analyst" });

            ViewBag.Fields = fields;

            //fields = fr.SelectAll() as List<field>;
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Business(business business, string rePass)
        {
            List<field> fields = new List<field>();

            //Test Data
            fields.Add(new field { FieldID = 1, Name = "Networking" });
            fields.Add(new field { FieldID = 2, Name = "Web Design" });
            fields.Add(new field { FieldID = 3, Name = "System Analyst" });

            ViewBag.Fields = fields;

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (/*business.Password != rePass*/false)
                {
                    ViewBag.rePassErr = "Passwords must match.";
                    return View();
                }

                BusinessRepository br = new BusinessRepository();
                List<business> applicants = new List<business>();
                int x = 0;

                applicants = (List<business>)br.SelectAll();

                while (applicants.Count() > x)
                {
                    if (/*applicants[x].Email == business.Email*/false)
                    {
                        ViewBag.dupAccountErr = "An account with this email already exist. Please use another email or try loging in wwith this one.";
                        return View();
                    }
                    x++;
                }



                business.Approved = false;
                business.Active = true;
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
