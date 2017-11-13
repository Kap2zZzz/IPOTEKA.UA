using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Controllers
{
    public class CalculatorController : Controller
    {
        //
        // GET: /Calculator/

        public ActionResult Index()
        {
            ViewBag.Page = "Ipoteka";
            return View();
        }

    }
}
