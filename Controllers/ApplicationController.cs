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
        public ActionResult Index(Application lm, int step)
        {
            ViewBag.Page = "Application";

            switch (step)
            {
                case 1:
                    {
                        if (MainHelp.IsValidEtap1(lm).Count == 0)
                        {
                            int e = step + 1;
                            ViewBag.Step = e;
                            ModelState.Clear();
                            try
                            {
                                lm.CreateDateTime = DateTime.Now;

                                if (User.Identity.IsAuthenticated)
                                {
                                    lm.CreateUserId = _db.Users.FirstOrDefault(x => x.Login == User.Identity.Name).UserId;
                                }
                                else
                                {
                                    lm.CreateUserId = -1;
                                }

                                _db.Applications.Add(lm);
                                _db.SaveChanges();
                                _db.Dispose();

                                return View(lm);
                            }
                            catch (DbEntityValidationException ex)
                            {
                                ViewBag.Step = step;
                                ModelState.AddModelError("", ex.EntityValidationErrors.ToString());
                                return View(lm);
                            }
                        }
                        else
                        {
                            ViewBag.Step = step;
                            foreach (KeyValuePair<string, string> k in MainHelp.IsValidEtap1(lm))
                            {
                                ModelState.AddModelError(k.Key, k.Value);
                            }
                            return View(lm);
                        }
                    }

                case 2:
                    {
                        if (MainHelp.IsValidEtap2(lm).Count == 0)
                        {
                            int e = step + 1;
                            ViewBag.Step = e;
                            //lm.Xml = MainHelp.CreateXML(lm);
                            //lm.XmlData = System.Text.Encoding.Default.GetBytes(lm.Xml);// MainHelp.CreateXML(lm)
                            _db.Entry(lm).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();
                            _db.Dispose();
                            return View(lm);
                        }
                        else
                        {
                            ViewBag.Step = step;
                            foreach (KeyValuePair<string, string> k in MainHelp.IsValidEtap2(lm))
                            {
                                ModelState.AddModelError(k.Key, k.Value);
                            }
                            return View(lm);
                        }
                    }

                case 3:
                    {
                        try
                        {
                            ViewBag.Step = step + 1;
                            _db.Entry(lm).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();
                            _db.Dispose();
                            return View();
                            //return View(lm);
                        }
                        catch (DbEntityValidationException ex)
                        {
                            ViewBag.Step = step;
                            ModelState.AddModelError("", ex.EntityValidationErrors.ToString());
                            return View(lm);
                        }
                    }

                default: return View();
            }

        }
    }
}
