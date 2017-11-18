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
            ViewBag.Page = "Application";
            ViewBag.dicProducts = MainHelp.dicProducts();
            ViewBag.dicSchems = MainHelp.dicSchems();
            ViewBag.Step = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Application a, int step)
        {
            ViewBag.Page = "Application";
            ViewBag.dicProducts = MainHelp.dicProducts();
            ViewBag.dicSchems = MainHelp.dicSchems();

            switch (step)
            {
                case 1:
                    {
                        if (MainHelp.IsValidEtap1(a).Count == 0)
                        {
                            int e = step + 1;
                            ViewBag.Step = e;
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
                                ViewBag.Step = step;
                                ModelState.AddModelError("", ex.EntityValidationErrors.ToString());
                                return View(a);
                            }
                        }
                        else
                        {
                            ViewBag.Step = step;
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
                            int e = step + 1;
                            ViewBag.Step = e;
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
                            ViewBag.Step = step;
                            foreach (KeyValuePair<string, string> k in MainHelp.IsValidEtap2(a))
                            {
                                ModelState.AddModelError(k.Key, k.Value);
                            }
                            return View(a);
                        }
                    }

                case 3:
                    {
                        ViewBag.Step = step + 1;
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
