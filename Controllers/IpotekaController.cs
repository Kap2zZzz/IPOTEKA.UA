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
            ViewBag.dicProducts = MainHelp.dicProducts();
            ViewBag.dicSchems = MainHelp.dicSchems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int Termin, decimal CreditSum, string ProductType, string Schema)
        {
            //var products = _db.Products.Where(x => x.Name == ProductType);
            var banks = _db.Banks.ToList();
            //_db.Database.Connection.Close();

            List<IpotekaData> resView = new List<IpotekaData>();

            //b.Products

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

            ViewBag.dicProducts = MainHelp.dicProducts();
            ViewBag.Res = resView;
            return View();
        }

    }
}
