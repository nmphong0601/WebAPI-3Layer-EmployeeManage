﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiEmployee: ApiEntity
    {
        public ApiEmployee()
        {
            Employees = new List<ApiEmployee>();
        }

        public int? Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DOB { get; set; }
        public bool? IsManager { get; set; }
        public int? ManagerId { get; set; }

        public ApiManager Manager { get; set; }
        public List<ApiEmployee> Employees { get; set; }
    }
}
