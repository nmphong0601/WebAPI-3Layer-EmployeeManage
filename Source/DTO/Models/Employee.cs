using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<bool> IsManager { get; set; }
        public Nullable<int> ManagerId { get; set; }
    }
}
