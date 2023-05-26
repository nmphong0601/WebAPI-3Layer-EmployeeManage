using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter full name.")]
        public string FullName { get; set; }

        public Nullable<System.DateTime> DOB { get; set; }

        public Nullable<bool> IsManager { get; set; }

        public Nullable<int> ManagerId { get; set; }
    }
}