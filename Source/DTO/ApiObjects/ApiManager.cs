using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiManager: ApiEmployee
    {
        public ApiManager()
        {
            Employees = new List<ApiEmployee>();
        }

        public List<ApiEmployee> Employees { get; set; }
    }
}
