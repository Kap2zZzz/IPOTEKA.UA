using IPOTEKA.UA.Code;
using IPOTEKA.UA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Controllers
{
    public class IpotekaController : Controller
    {
        //
        // GET: /Ipoteka/

        public ActionResult Index()
        {
            ViewBag.Page = "Ipoteka";
            var model = new Application();
            ViewBag.dicProducts = MainHelp.dicProducts();
            ViewBag.dicSchems = MainHelp.dicSchems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int? Termin)
        {
            return View();
        }

    }
}
