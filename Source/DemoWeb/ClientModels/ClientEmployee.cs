using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWeb.ClientModels
{
    public class ClientEmployee
    {
        public ClientEmployee()
        {
            
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<bool> IsManager { get; set; }
        public Nullable<int> ManagerId { get; set; }
    }
}
