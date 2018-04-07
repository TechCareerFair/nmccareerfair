using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.Models;

namespace TechCareerFair.Controllers
{
    public class GalleryController : Controller
    {
        private TechCareerFair.DAL.GalleryDAL.GalleryRepository gr = new DAL.GalleryDAL.GalleryRepository();
        // GET: Gallery
        public ActionResult Index()
        {
            return View(gr.SelectAll());
        }

        // GET: Gallery/Details/5
        public ActionResult Details(int id)
        {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (id == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = gr.SelectOne(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Gallery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GalleryID,Directory,Description")] gallery gallery)
        {
            if (ModelState.IsValid)
            {
                gr.Insert(gallery);
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // GET: Gallery/Edit/5
        public ActionResult Edit(int id)
        {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (id == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = gr.SelectOne(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Gallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GalleryID,Directory,Description")] gallery gallery)
        {
            if (ModelState.IsValid)
            {
                gr.Update(gallery, Server.MapPath("~"));
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: Gallery/Delete/5
        public ActionResult Delete(int id)
        {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (id == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = gr.SelectOne(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gr.Delete(id, Server.MapPath("~"));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gr.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
