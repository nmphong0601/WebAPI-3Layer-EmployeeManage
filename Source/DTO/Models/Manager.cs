using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO.Models
{
    public class Manager: Employee
    {
        public IList<Employee> Employees { get; set; }

        public Manager()
        {
            Employees = new List<Employee>();
        }
    }
}
