using IPOTEKA.UA.Code;
using IPOTEKA.UA.Models;
using IPOTEKA.UA.Repostory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPOTEKA.UA.Controllers
{
    public class PersonalController : Controller
    {
        MyDbContext _db = new MyDbContext();
        //
        // GET: /Personal/
        [HttpGet]
        public ActionResult Index()
        {
            //MainHelp.CreateUserAdmin();
            ViewBag.Page = "Personal";
            //ViewBag.Preview = false;
            if (User.Identity.IsAuthenticated)
            {
                if (_db.Users.Count() == 0)
                {
                    MainHelp.CreateUserAdmin();
                }
                User u = new User();
                u = _db.Users.FirstOrDefault(x => x.Login == User.Identity.Name);

                //ViewBag.Role = u.Role;
                //ViewBag.PIB = u.PIB;
                //ViewBag.appId = id;

                //var tuple = new Tuple<LoanModel, mUser>(new LoanModel(), new mUser());
                //var models = new Tuple<ContextBoundObject.
                //return View(tuple);

                //return View(context.Users);


                //List<LoanModel> l1 = context.Applications.ToList();
                //List<mUser> l2 = context.Users.ToList();

                //var t = new Tuple<List<LoanModel>, List<mUser>>(l1, l2);
                //ViewBag.data = t;
                //object z = new Tuple<List<LoanModel>, List<mUser>>(l1, l2);

                //Tuple<List<IPOTEKA.UA.Models.LoanModel>, List<IPOTEKA.UA.Models.mUser>> t = new Tuple<List<LoanModel>, List<mUser>>(l1, l2);
                //a = 
                if (u != null)
                {
                    if (u.Role == "Admin")
                    {
                        //return RedirectToRoute(new { controller = "Admin", action = "Index" });
                        //return RedirectToRoute("Admin");
                        //return RedirectToRoute(new { controller = "Admin", action = "Admin"});
                        //return RedirectToRoute("Default2");
                        return RedirectToAction("Index", "Admin");
                        //return View("Index", "/Admin/Index", t);
                    }
                    else if (u.Role == "Agent")
                    {
                        return View(_db.Applications);
                    }
                    else { return RedirectToAction("Index", "Home"); }
                }
                else return RedirectToAction("Index", "Admin");



                //return View(context.Applications);
                //return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}
