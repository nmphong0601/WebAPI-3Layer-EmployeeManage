using DemoWeb.Caching;
using DemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb.Controllers
{
    public class ManagerController : Controller
    {
        static int nPerPage = 6;
        // GET: Employee
        
        public ActionResult GetList(int page=1)
        {
            Dictionary<string, object> result = CSDLQLNV.GetPagedManagers(page: page, pageSize: nPerPage);

            ViewBag.totalPage = result["totalPage"];
            ViewBag.curPage = result["curPage"];

            var list = result["Collection"] as List<Manager>;

            return View("List", list);

        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            var employee = CSDLQLNV.GetSingleManager(id);
            List<Manager> listCom = CSDLQLNV.GetManagers(filter: "ManagerId =" + employee.Id).ToList();
            if (listCom != null)
            {
                ViewBag.ListComment = listCom;
            }
            return View(employee);
        }
    }
}