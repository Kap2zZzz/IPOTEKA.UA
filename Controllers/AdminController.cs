using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPOTEKA.UA.Models;
using IPOTEKA.UA.Repostory;
using IPOTEKA.UA.Code;
using System.Web.Security;

namespace IPOTEKA.UA.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        MyDbContext _db = new MyDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            using (_db)
            {
                ViewBag.Page = "Personal";
                User u = new User();
                u = _db.Users.FirstOrDefault(x => x.Login == User.Identity.Name);

                ViewBag.Role = u.Role;
                ViewBag.PIB = u.PIB;
                List<Application> l1 = _db.Applications.ToList();
                List<User> l2 = _db.Users.ToList();
                List<Bank> l3 = _db.Banks.ToList();

                var t = new Tuple<List<Application>, List<User>, List<Bank>>(l1, l2, l3);

                return View(t);
            }

        }

        #region Користувачі

        #region Створення користувач

        [HttpGet]
        public ActionResult CreateUser()
        {
            ViewBag.Page = "Personal";
            return View(new User());
        }

        [HttpPost]
        public ActionResult CreateUser(User u)
        {
            ViewBag.Page = "Personal";
            string buttonValue = Request["button"];
            if (buttonValue == "Назад")
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (Create(u))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(u);
                    }
                }
                else
                {
                    //ModelState.AddModelError("PIB", "sadsadsadsadsadsa");
                    return View(u);
                }
                //return RedirectToAction("Index");
            }
        }

        #endregion

        #region Редагування користувача

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            ViewBag.Page = "Personal";
            return View(_db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult EditUser(User u)
        {
            string buttonValue = Request["button"];
            if (buttonValue == "Назад")
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (Save(u))
                {
                    return RedirectToAction("Index");
                }
                return View(u);
            }
        }

        #endregion

        #region Перегляд користувача

        [HttpGet]
        public ActionResult PreviewUser(int id)
        {
            ViewBag.Page = "Personal";
            return View(_db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult PreviewUser(User u)
        {
            string buttonValue = Request["button"];
            if (buttonValue == "Назад")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("EditUser", new { id = u.UserId });
            }
        }

        #endregion

        #endregion

        #region Банки

        #region Створення Банку

        [HttpGet]
        public ActionResult CreateBank()
        {
            var ViewModel = new Bank();
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult CreateBank(Bank bd)
        {
            ViewBag.dicProducts = MainHelp.dicProducts();

            string buttonValue = Request["button"];

            if (buttonValue == "+")
            {
                if (bd.Products == null)
                {
                    bd.Products = new List<Product>();
                }
                bd.Products.Add(new Product());
                return View(bd);
            }
            else if (buttonValue == "Зберегти")
            {
                _db.Banks.Add(bd);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }


            //if (ModelState.IsValid)
            //{
            //    if (Create(bd.Bank))
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View(bd.Bank);
            //    }
            //}
            //else
            //{
            //    return View(bd.Bank);
            //}
        }
        #endregion

        #region Редагування Банку
        [HttpGet]
        public ActionResult EditBank(int id)
        {
            ViewBag.Page = "Personal";
            ViewBag.dicProducts = MainHelp.dicProducts();
            return View(_db.Banks.Find(id));
        }

        [HttpPost]
        public ActionResult EditBank(Bank b)
        {
            ViewBag.dicProducts = MainHelp.dicProducts();

            string buttonValue = Request["button"];

            if (buttonValue == "+")
            {
                if (b.Products == null)
                {
                    b.Products = new List<Product>();
                }
                b.Products.Add(new Product());
                return View(b);
            }
            else if (buttonValue == "Зберегти")
            {
                if (Save(b))
                {
                    return RedirectToAction("PreviewBank", new { id = b.BankID });
                }
                return View(b);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Перегляд Банку

        [HttpGet]
        public ActionResult PreviewBank(int id)
        {
            ViewBag.Page = "Personal";
            return View(_db.Banks.Find(id));
        }

        [HttpPost]
        public ActionResult PreviewBank(Bank b)
        {
            ViewBag.Page = "Personal";
            string buttonValue = Request["button"];
            if (buttonValue == "Назад")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("EditBank", new { id = b.BankID });
            }
        }

        #endregion

        #endregion

        [HttpGet]
        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Delete(string key, int? id)
        {
            using (_db)
            {
                if (key == "User")
                {
                    User u = _db.Users.Find(id);
                    if (u != null)
                    {
                        _db.Users.Remove(u);
                        _db.SaveChanges();
                    }
                }

                if (key == "Application")
                {
                    Application a = _db.Applications.Find(id);
                    if (a != null)
                    {
                        _db.Applications.Remove(a);
                        _db.SaveChanges();
                    }
                }

                if (key == "Bank")
                {
                    Bank b = _db.Banks.Find(id);
                    if (b != null)
                    {
                        _db.Banks.Remove(b);
                        _db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        private bool Create(User u)
        {
            using (_db)
            {
                if (_db.Users.FirstOrDefault(x => x.Login == u.Login) != null)
                {
                    ModelState.AddModelError("Login", "Увага!, Логін: " + u.Login + " вже існує в системі");
                    return false;
                }
                else
                {
                    _db.Users.Add(u);
                    _db.SaveChanges();

                    return true;
                }
            }
        }

        private bool Create(Bank b)
        {
            using (_db)
            {
                _db.Banks.Add(b);
                _db.SaveChanges();
                return true;
            }
        }

        private bool Save(User u)
        {
            using (_db)
            {
                if (_db.Users.FirstOrDefault(x => x.Login == u.Login) != null)
                {
                    ModelState.AddModelError("Login", "Увага!, Логін: " + u.Login + " вже існує в системі");
                    return false;
                }
                else
                {
                    _db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();

                    return true;
                }
            }
        }

        private bool Save(Bank b)
        {
            using (_db)
            {
                _db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                foreach (var p in b.Products)
                {
                    if (p.ProductID > 0)
                    {
                        _db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        _db.Products.Add(p);
                    }
                }

                _db.SaveChanges();

                return true;
            }
        }
    }
}
