using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechCareerFair.Models;
using TechCareerFair.DAL;
using System.Web.UI.WebControls;

//https://www.aspsnippets.com/Articles/Retrieve-images-using-a-file-path-stored-in-database-in-ASPNet.aspx
//https://stackoverflow.com/questions/21647201/asp-net-file-upload-doesnt-work-in-windows-azure
//https://support.microsoft.com/en-us/help/323246/how-to-upload-a-file-to-a-web-server-in-asp-net-by-using-visual-c-net
//https://docs.microsoft.com/en-us/aspnet/web-pages/overview/data/working-with-files
//http://highoncoding.com/Articles/689_Uploading_and_Displaying_Files_Using_ASP_NET_MVC_Framework.aspx

namespace TechCareerFair.Controllers
{
    public class GalleryController : Controller
    {
        private TechCareerFair.DAL.GalleryDAL.GalleryRepository gr = new DAL.GalleryDAL.GalleryRepository();
        private bool IsPostBack;
        

        // GET: Gallery
        public ActionResult Index()
        {
            return View(gr.SelectAll());
        }

        // GET: Gallery/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
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

        /*protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Files", conn))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvImages.DataSource = dt;
                        gvImages.DataBind();
                    }
                }
            }
        }*/

        /*protected void Upload(object sender, EventArgs e)
        {
            //Extract Image File Name.
            string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);

            //Set the Image File Path.
            string filePath = "~/Uploads/" + fileName;

            //Save the Image File in Folder.
            fileUpload.PostedFile.SaveAs(Server.MapPath(filePath));

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                string sql = "INSERT INTO gallery VALUES(@Name, @Path)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", fileName);
                    cmd.Parameters.AddWithValue("@Path", filePath);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }*/

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadImage()
        {
            foreach (string image in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[image];
                if (postedFile != null)
                {
                    postedFile.SaveAs(Server.MapPath(DataSettings.GALLERY_DIRECTORY) + Path.GetFileName(postedFile.FileName)); 
                }
            }

            return View("Index");
        }

        // GET: Gallery/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
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
            if (id == null)
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
