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
    public class EmployeeController : Controller
    {
        static int nPerPage = 6;
        // GET: Employees
        public ActionResult Index()
        {
            var l = CSDLQLNV.GetEmployees().ToList();
            return View(l);

        }

        public ActionResult GetList(int page=1)
        {
            Dictionary<string, object> result = CSDLQLNV.GetPagedEmployees(page: page, pageSize: nPerPage);

            ViewBag.totalPage = result["totalPage"];
            ViewBag.curPage = result["curPage"];

            var list = result["Collection"] as List<Employee>;

            return View("List", list);

        }

        public ActionResult GetListByManager(int? id, int page = 1)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            Dictionary<string, object> result = CSDLQLNV.GetPagedEmployees(filter: "ManagerId =" + id, page: page, pageSize: nPerPage);

            ViewBag.totalPage = result["totalPage"];
            ViewBag.curPage = result["curPage"];
            ViewBag.cId = id;

            var list = result["Collection"] as List<Employee>;

            return View("ListByManager", list);

        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            var employee = CSDLQLNV.GetSingleEmployee(id);
            List<Employee> listCom = CSDLQLNV.GetEmployees(filter: "ManagerId =" + employee.Id).ToList();
            if (listCom != null)
            {
                ViewBag.ListComment = listCom;
            }
            return View(employee);
        }

        // Post: Employee/Add
        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            var managerInserted = CSDLQLNV.InsertEmployee(employee);
            return RedirectToAction("Index");
        }

        // Post: Employee/Update
        [HttpPost]
        public ActionResult Update(int id, Employee employee)
        {
            employee.Id = id;
            if (employee != null)
            {
                CSDLQLNV.UpdateEmployee(employee);
            }

            return RedirectToAction("Index");
        }

        // DELETE: Employee/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CSDLQLNV.RemoveEmployee(id);
            return RedirectToAction("Index");
        }
    }
}