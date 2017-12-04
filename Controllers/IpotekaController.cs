using IPOTEKA.UA.Code;
using IPOTEKA.UA.Models;
using IPOTEKA.UA.Repostory;
using IPOTEKA.UA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Controllers
{
    public class IpotekaController : Controller
    {
        MyDbContext _db = new MyDbContext();

        public ActionResult Index()
        {
            ViewBag.Page = "Ipoteka";
            ViewBag.dicProducts = _db.Products.ToList();
            var model = new Application();
            model.Termin = 12;
            model.CreditSum = 500000;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int Termin, decimal CreditSum, int? ProductType, string Schema, Application a)
        {
            ViewBag.Page = "Ipoteka";

            ViewBag.dicProducts = _db.Products.ToList();

            string buttonValue = Request["button"];

            if (buttonValue == "Подати заявку")
            {
                TempData["NewApplication"] = a;
                return RedirectToAction("Index", "Application");
            }
            else
            {
                return View(a);
            }
        }
    }
}
