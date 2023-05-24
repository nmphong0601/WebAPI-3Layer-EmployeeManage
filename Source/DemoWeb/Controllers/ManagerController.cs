﻿using DemoWeb.Caching;
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
        // GET: Managers
        public ActionResult Index()
        {
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
            if (Session["Manangers"] == null)
            {
                Session["Manangers"] = new List<Manager>();
            }
            var managers = Session["Manangers"] as List<Manager>;
            if (managers.Count == 0)
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

            return RedirectToAction("Add", "Manager");
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

            return RedirectToAction("Add", "Manager");
        }

        // DELETE: Manager/Delete
        public ActionResult Delete(int id)
        {
            var manager = CSDLQLNV.GetSingleManager(id);
            if (manager != null)
            {
                CSDLQLNV.RemoveManager(id);
            }
            return RedirectToAction("Index");
        }

        // Post: Manager/Add
        [HttpPost]
        public ActionResult Add(List<Manager> managers)
        {
            Session["Message"] = null;
            if (managers.Count >= 3)
            {
                foreach (var manager in managers)
                {
                    if(manager.Employees.Count >= 30)
                    {
                        var managerInserted = CSDLQLNV.InsertManager(manager);
                    }
                    else
                    {
                        Session["Message"] = "Need to create at least 30 employee for manager " + manager.FullName;
                        return RedirectToAction("Add", "Manager");
                    }
                }
            }
            else
            {
                Session["Message"] = "Need to create at least 3 manager";
                return RedirectToAction("Add", "Manager");
            }
            
            return RedirectToAction("Index");
        }
    }
}