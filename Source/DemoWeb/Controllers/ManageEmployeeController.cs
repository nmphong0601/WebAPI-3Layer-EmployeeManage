using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWeb.Caching;
using DemoWeb.ClientModels;
using DemoWeb.Models;
using DemoWeb.Ultilities;


namespace DemoWeb.Controllers
{

    public class ManageEmployeeController : Controller
    {
        // GET: ManageEmployee
        // khi nào xét premession=1 thì mở nó ra bỏ cái dưới đi
        // [AuthActionFilter(RequiredPermission=1)]
        // [AuthActionFilter]

        // GET: ManageEmployee
        public ActionResult Index()
        {
            var l = CSDLQLNV.GetEmployees().ToList();
            return View(l);

        }

        // GET: ManageEmployee/Delete
        public ActionResult Delete(int id)
        {
            var pD = CSDLQLNV.GetSingleEmployee(id);
            if (pD != null)
            {
                CSDLQLNV.RemoveEmployee(id);
            }
            return RedirectToAction("Index");
        }

        // Post: ManageEmployee/Add
        [HttpPost]
        public ActionResult Add(Employee p)
        {
            return View(p);
        }
    }
}