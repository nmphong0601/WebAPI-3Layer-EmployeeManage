using Microsoft.AspNetCore.Mvc;
using DemoWeb.Caching;
using DemoWeb.Models;
using DemoWeb.Ultilities;

namespace DemoWeb.Controllers
{
    public class ManagerController : Controller
    {
        static int nPerPage = 6;
        // GET: Managers
        public ActionResult Index()
        {
            HttpContext.Session.SetObject("Manangers", new List<Manager>());
            var l = CSDLQLNV.GetManagers().ToList();
            return View(l);
        }

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

        // GET: ManageProduct/Add
        public ActionResult Add()
        {
            var managers = HttpContext.Session.GetObject<List<Manager>>("Manangers");
            if (managers == null)
            {
                HttpContext.Session.SetObject("Manangers", new List<Manager>());
            }
            if (managers?.Count == 0)
            {
                managers.Add(new Manager());
            }

            return View(managers);
        }

        public ActionResult AddManager(List<Manager> managers)
        {
            var manager = new Manager();
            manager.Employees = new List<Employee>();

            managers.Add(manager);
            HttpContext.Session.SetObject("Manangers", managers);

            return RedirectToAction("Add");
        }

        public ActionResult AddEmployee(int managerId, List<Manager> managers)
        {
            var employee = new Employee();

            managers[managerId].Employees.Add(employee);
            HttpContext.Session.SetObject("Manangers", managers);

            return RedirectToAction("Add");
        }

        // Post: Manager/Add
        [HttpPost]
        public ActionResult Add(List<Manager> managers)
        {
            var message = "";

            if (managers != null && managers.Count >= 3)
            {
                foreach (var manager in managers)
                {
                    if (manager.Employees != null && manager.Employees.Count >= 10)
                    {
                        var managerInserted = CSDLQLNV.InsertManager(manager);
                    }
                    else
                    {
                        message = "Need to create at least 30 employee for manager " + manager.FullName;
                    }
                }
            }
            else
            {
                message = "Need to create at least 3 manager";
            }

            HttpContext.Session.SetObject("Message", message);
            HttpContext.Session.SetObject("Manangers", managers);

            return RedirectToAction("Add", "Manager");
        }


        // Post: Manager/Update
        [HttpPost]
        public ActionResult Update(int id, Manager manager)
        {
            manager.Id = id;
            if (manager != null)
            {
                CSDLQLNV.UpdateManager(manager);
            }

            return RedirectToAction("Index");
        }

        // DELETE: Manager/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CSDLQLNV.RemoveManager(id);
            return RedirectToAction("Index");
        }
    }
}