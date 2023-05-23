using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb.Models
{
    public class Manager: Employee
    {
        public Manager()
        {
            Employees = new List<Employee>();
        }

        public IList<Employee> Employees { get; set; }
    }
}