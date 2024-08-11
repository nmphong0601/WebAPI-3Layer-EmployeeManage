using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
