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
        //
        // GET: /Ipoteka/
        MyDbContext _db = new MyDbContext();

        public ActionResult Index()
        {
            ViewBag.Page = "Ipoteka";
            var model = new Application();
            model.Termin = 12;
            model.CreditSum = 500000;
            ViewBag.dicProducts = MainHelp.dicProducts();
            ViewBag.dicSchems = MainHelp.dicSchems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int Termin, decimal CreditSum, string ProductType, string Schema, Application a)
        {
            ViewBag.Page = "Ipoteka";
            ViewBag.dicProducts = MainHelp.dicProducts();

            if ((ProductType == null) || (ProductType == string.Empty))
            {
                ModelState.AddModelError("ProductType", "Поле [Продукт] не вибрано!");
                return View(a);
            }
            else
            {
                var banks = _db.Banks.ToList();

                List<IpotekaData> resView = new List<IpotekaData>();

                foreach (Bank b in banks)
                {
                    foreach (Product p in b.Products.Where(x => x.Name == ProductType))
                    {
                        IpotekaData temp = new IpotekaData();
                        temp.Bank = b.Name;
                        temp.Rate = p.Rate.ToString();
                        temp.Commission = p.Commission.ToString();
                        Calculation.GetResults(CreditSum, Termin, p.Rate, true, out temp.MMP, out temp.RealRate, out temp.EffectiveRate);
                        resView.Add(temp);
                    }
                }

                ViewBag.Res = resView;

                return View(a);
            }
        }

    }
}
