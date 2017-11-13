using IPOTEKA.UA.Models;
using IPOTEKA.UA.Repostory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IPOTEKA.UA.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Page = "Personal";
            return View();
        }

        [HttpPost]
        public ActionResult Index(User u)
        {
            ViewBag.Page = "Personal";
            MyDbContext context = new MyDbContext();
            //ViewBag.Preview = false;

            if ((context.Users.Any(x => x.Login == u.Login)) && (context.Users.Any(x => x.Password == u.Password)))
            {
                User _u = context.Users.FirstOrDefault(x => x.Login == u.Login);
                //Roles.CreateRole(_u.Role);
                //Roles.AddUserToRole(_u.Login, _u.Role);
                FormsAuthentication.SetAuthCookie(_u.Login, false);

                //MyDbContext context = new MyDbContext();

                if (_u.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Agent");
                }

            }
            else
            {
                ModelState.AddModelError("Password", "Логін або пароль не вірний");
                return View();
            }
        }

    }
}
