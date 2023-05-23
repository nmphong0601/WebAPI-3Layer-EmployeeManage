using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWeb.Caching;
using DemoWeb.Models;
using DemoWeb.Ultilities;

namespace DemoWeb.Controllers
{

    public class ManageManagerController : Controller
    {
        // GET: ManageManager
        // khi nào xét premession=1 thì mở nó ra bỏ cái dưới đi
        // [AuthActionFilter(RequiredPermission=1)]
        // [AuthActionFilter]

        // GET: ManageManager
        public ActionResult Index()
        {
            var l = CSDLQLNV.GetManagers().ToList();
            return View(l);
        }

        // GET: ManageProduct/Add
        public ActionResult Add()
        {
            if (Session["Manangers"] == null)
            {
                Session["Manangers"] = new List<Manager>();
            }
            var managers = Session["Manangers"] as List<Manager>;
            if(managers.Count == 0)
            {
                managers.Add(new Manager());
            }
            
            return View(managers);
        }

        public ActionResult AddManager()
        {
            var manager = new Manager();
            manager.Employees = new List<Employee>();

            if (Session["Manangers"] == null)
            {
                Session["Manangers"] = new List<Manager>();
            }
            var managers = Session["Manangers"] as List<Manager>;

            managers.Add(manager);
            Session["Manangers"] = managers;

            return RedirectToAction("Add", "ManageManager");
        }

        public ActionResult AddEmployee(int managerId)
        {
            var employee = new Employee();

            if (Session["Manangers"] == null)
            {
                Session["Manangers"] = new List<Manager>();
            }
            var managers = Session["Manangers"] as List<Manager>;

            managers[managerId].Employees.Add(employee);
            Session["Manangers"] = managers;

            return RedirectToAction("Add", "ManageManager");
        }

        // GET: ManageManager/Delete
        public ActionResult Delete(int id)
        {
            var manager = CSDLQLNV.GetSingleManager(id);
            if (manager != null)
            {
                CSDLQLNV.RemoveManager(id);
            }
            return RedirectToAction("Index");
        }

        // Post: ManageManager/Add
        [HttpPost]
        public ActionResult Add(Manager manager)
        {
            manager = CSDLQLNV.InsertManager(manager);
            return RedirectToAction("Index");
        }
        // Post: ManageManager/Add
        [HttpPost]
        public ActionResult Add(List<Manager> managers)
        {
            foreach (var manager in managers)
            {
                var managerInserted = CSDLQLNV.InsertManager(manager);
            }
            return RedirectToAction("Index");
        }
    }
}