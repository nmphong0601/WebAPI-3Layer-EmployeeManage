using DemoWeb.Caching;
using DemoWeb.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var l = CSDLQLNV.GetManagers().ToList();
            return View(l);
        }
    }
}