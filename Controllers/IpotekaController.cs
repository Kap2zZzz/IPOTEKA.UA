﻿using System;
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

        public ActionResult Index()
        {
            ViewBag.Page = "Ipoteka";
            return View();
        }

    }
}
