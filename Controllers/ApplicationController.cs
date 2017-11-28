using IPOTEKA.UA.Code;
using IPOTEKA.UA.Models;
using IPOTEKA.UA.Repostory;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Controllers
{
    public class ApplicationController : Controller
    {
        MyDbContext _db = new MyDbContext();
        //
        // GET: /Application/

        [HttpGet]
        public ActionResult Index()
        {
            var model = new Application();
            var temp = TempData["NewApplication"] as Application;

            if (temp != null)
            {
                model = temp;
            }
            ViewBag.Page = "Application";
            ViewBag.dicProducts = _db.Products.ToList();
            ViewBag.dicSchems = MainHelp.dicSchems();
            TempData["Step"] = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Application a)
        {
            ViewBag.Page = "Application";
            int s = (int)TempData["Step"];

            string buttonValue = Request["button"];

            if (buttonValue == "Назад")
            {
                TempData["NewApplication"] = a;
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.dicProducts = MainHelp.dicProducts();
                ViewBag.dicSchems = MainHelp.dicSchems();

                switch (s)
                {
                    case 1:
                        {
                            if (MainHelp.IsValidEtap1(a).Count == 0)
                            {
                                TempData["Step"] = s + 1;
                                ModelState.Clear();
                                try
                                {
                                    a.CreateDateTime = DateTime.Now;

                                    if (User.Identity.IsAuthenticated)
                                    {
                                        a.CreateUserId = _db.Users.FirstOrDefault(x => x.Login == User.Identity.Name).UserId;
                                    }
                                    else
                                    {
                                        a.CreateUserId = -1;
                                    }

                                    _db.Applications.Add(a);
                                    _db.SaveChanges();
                                    _db.Dispose();

                                    return View(a);
                                }
                                catch (DbEntityValidationException ex)
                                {
                                    TempData["Step"] = s;
                                    ModelState.AddModelError("", ex.EntityValidationErrors.ToString());
                                    return View(a);
                                }
                            }
                            else
                            {
                                TempData["Step"] = s;
                                foreach (KeyValuePair<string, string> k in MainHelp.IsValidEtap1(a))
                                {
                                    ModelState.AddModelError(k.Key, k.Value);
                                }
                                return View(a);
                            }
                        }

                    case 2:
                        {
                            if (MainHelp.IsValidEtap2(a).Count == 0)
                            {
                                TempData["Step"] = s + 1;
                                a.Xml = MainHelp.CreateXML(a);
                                a.XmlData = System.Text.Encoding.Default.GetBytes(a.Xml);// MainHelp.CreateXML(lm)
                                _db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                                _db.SaveChanges();
                                _db.Dispose();
                                SendMail.Send(a);
                                return View(a);
                            }
                            else
                            {
                                TempData["Step"] = s;
                                foreach (KeyValuePair<string, string> k in MainHelp.IsValidEtap2(a))
                                {
                                    ModelState.AddModelError(k.Key, k.Value);
                                }
                                return View(a);
                            }
                        }

                    case 3:
                        {
                            TempData["Step"] = s + 1;
                            //_db.Entry(lm).State = System.Data.Entity.EntityState.Modified;
                            //_db.SaveChanges();
                            //_db.Dispose();
                            return View();
                            //return View(lm);
                        }

                    default: return View();
                }
            }
        }
    }
}
