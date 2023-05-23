using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWeb.ClientModels
{
    public class ClientManager: ClientEmployee
    {
        public ClientManager()
        {
            Employees = new List<ClientEmployee>();
        }

        public List<ClientEmployee> Employees { get; set; }
    }
}
