using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPOTEKA.UA.Models;
using IPOTEKA.UA.Repostory;
using IPOTEKA.UA.Code;
using System.Web.Security;
using System.Data.Entity.Validation;

namespace IPOTEKA.UA.Controllers
{
    public class HomeController : Controller
    {
        

        

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "adm_role")]
        public ActionResult Results()
        {
            MyDbContext context = new MyDbContext();
            return View(context.Applications);
        }

        public ActionResult Delete(int id)
        {
            MyDbContext _db = new MyDbContext();
            Application lm = _db.Applications.Find(id);
            if (lm != null)
            {
                _db.Applications.Remove(lm);
                _db.SaveChanges();
                _db.Dispose();
            }
            return RedirectToAction("Personal");
        }

        [HttpGet]
        public FileResult DownloadFile(int id)
        {
            MyDbContext _db = new MyDbContext();
            Application lm = _db.Applications.Find(id);
            try
            {
                return File(lm.XmlData, "xml", lm.ApplicationId.ToString() + " " + lm.Sname.ToString() + ".xml");
            }
            catch
            {
                RedirectToAction("Personal");
                return null;
            }
        }

    }
}
