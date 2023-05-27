using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Models
{
    public partial class Employee
    {
        public Employee()
        {
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<bool> IsManager { get; set; }
        public Nullable<int> ManagerId { get; set; }

        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
