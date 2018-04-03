using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using TechCareerFair.DAL;

namespace TechCareerFair.Controllers
{
    //https://stackoverflow.com/questions/21647201/asp-net-file-upload-doesnt-work-in-windows-azure
    //https://support.microsoft.com/en-us/help/323246/how-to-upload-a-file-to-a-web-server-in-asp-net-by-using-visual-c-net
    //https://docs.microsoft.com/en-us/aspnet/web-pages/overview/data/working-with-files
    //http://highoncoding.com/Articles/689_Uploading_and_Displaying_Files_Using_ASP_NET_MVC_Framework.aspx
    public class UploadTestController : Controller
    {

        // GET: UploadTest
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFile()
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[file];
                if(postedFile != null)
                {
                    postedFile.SaveAs(Server.MapPath(DataSettings.BUSINESS_DIRECTORY) + Path.GetFileName(postedFile.FileName)); //can replace BUSINESS with GALLERY or RESUME
                }
            }

            return View("Index");
        }
    }
}