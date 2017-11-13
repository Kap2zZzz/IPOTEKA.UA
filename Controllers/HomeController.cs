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
        //
        // GET: /Home/

        //[HttpGet]
        ////[Authorize(Roles = "Agent")]
        //public ActionResult Personal(int? id)
        //{
        //    MyDbContext context = new MyDbContext();
        //    //MainHelp.CreateUserAdmin();
        //    ViewBag.Page = "Personal";
        //    //ViewBag.Preview = false;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        mUser u = new mUser();
        //        u = context.Users.FirstOrDefault(x => x.Login == User.Identity.Name);

        //        //ViewBag.Role = u.Role;
        //        //ViewBag.PIB = u.PIB;
        //        //ViewBag.appId = id;

        //        //var tuple = new Tuple<LoanModel, mUser>(new LoanModel(), new mUser());
        //        //var models = new Tuple<ContextBoundObject.
        //        //return View(tuple);

        //        //return View(context.Users);


        //        //List<LoanModel> l1 = context.Applications.ToList();
        //        //List<mUser> l2 = context.Users.ToList();

        //        //var t = new Tuple<List<LoanModel>, List<mUser>>(l1, l2);
        //        //ViewBag.data = t;
        //        //object z = new Tuple<List<LoanModel>, List<mUser>>(l1, l2);

        //        //Tuple<List<IPOTEKA.UA.Models.LoanModel>, List<IPOTEKA.UA.Models.mUser>> t = new Tuple<List<LoanModel>, List<mUser>>(l1, l2);
        //        //a = 

        //        if (u.Role == "Admin")
        //        {
        //            //return RedirectToRoute(new { controller = "Admin", action = "Index" });
        //            //return RedirectToRoute("Admin");
        //            //return RedirectToRoute(new { controller = "Admin", action = "Admin"});
        //            return RedirectToRoute("Default2");
        //            //return RedirectToAction("Admin", "Admin");
        //            //return View("Index", "/Admin/Index", t);
        //        }
        //        else if (u.Role == "Agent")
        //        {
        //            return View(context.Applications);
        //        }
        //        else { return RedirectToAction("Index"); }


        //        //return View(context.Applications);
        //        //return View();
        //    }
        //    else
        //    {
        //        return PartialView("Login");
        //    }

        //}

        //[HttpPost]
        //public ActionResult Login(mUser u)
        //{
        //    ViewBag.Page = "Personal";
        //    MyDbContext context = new MyDbContext();
        //    //ViewBag.Preview = false;

        //    if ((context.Users.Any(x => x.Login == u.Login)) && (context.Users.Any(x => x.Password == u.Password)))
        //    {
        //        mUser _u = context.Users.FirstOrDefault(x => x.Login == u.Login);
        //        //Roles.CreateRole(_u.Role);
        //        //Roles.AddUserToRole(_u.Login, _u.Role);
        //        FormsAuthentication.SetAuthCookie(_u.Login, true);

        //        //MyDbContext context = new MyDbContext();

        //        return RedirectToAction("Personal");

        //        //if (_u.Role == "Admin")
        //        //{
        //        //    return RedirectToAction("Admin");

        //        //}
        //        //else
        //        //{
        //        //    return RedirectToAction("Personal");
        //        //}

        //        //return View("Personal", context.Applications);
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("Password", "Логін або пароль не вірний");
        //        return View();
        //    }

        //    //if (u.Login == "agent" && u.Password == "agent")
        //    //{
        //    //    u.Role = "Agent";
        //    //    FormsAuthentication.SetAuthCookie(u.Login, true);

        //    //    MyDbContext context = new MyDbContext();
        //    //    return View("Personal", context.Applications);
        //    //}
        //    //else
        //    //{
        //    //    ModelState.AddModelError("Password", "Логін або пароль не вірний");
        //    //    return View();
        //    //}
        //}

        [HttpGet]
        public ActionResult Index()
        {
            //FormsAuthentication.SignOut();
            //ViewBag.Page = "About";
            //FormsAuthentication.SetAuthCookie("Yura", true);
            //User
            //Roles.CreateRole("adm_role");
            //Roles.AddUserToRole("Yura", "adm_role");

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
        public ActionResult _ApplicationView(int? id)
        {
            ViewBag.appId = id;
            MyDbContext context = new MyDbContext();
            return View(context.Applications.FirstOrDefault(x => x.ApplicationId == id));
        }

        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public ActionResult Admin()
        //{
        //    return View();
        //}

        //public ActionResult Preview(int id)
        //{
        //    MyDbContext _db = new MyDbContext();
        //    LoanModel lm = _db.Applications.Find(id);
        //    int? _appid = null;

        //    if (lm != null)
        //    {
        //        _appid = lm.ApplicationId;
        //        //ViewBag.Model_ID = lm.ApplicationId;
        //        ViewBag.Preview = true;

        //        //return RedirectToAction("Personal");
        //    }
        //    return RedirectToAction("Personal", _appid);
        //}

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
