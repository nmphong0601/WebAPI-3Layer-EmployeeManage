using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DTO.Models
{
    [NotMapped]
    public class Manager: Employee
    {
        public Manager()
        {
            Employees = new List<Employee>();
        }
    }
}
