using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class EmployeesBUS
    {
        readonly DAOFactory factory;
        public EmployeesBUS()
        {
            this.factory = new DAOFactory();
        }

        public EmployeesBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiEmployee> GetAll(string filter = null, string sort = "DOB DESC")
        {
            return factory.EmployeesDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiEmployee> Paged(string keyword = null, string filter = null, string sort = "DOB DESC", int page = 1, int pageSize = 6)
        {
            return factory.EmployeesDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public ApiEmployee GetSingle(int? id)
        {
            return factory.EmployeesDAO.GetSingle(id);
        }

        public ApiEmployee Add(ApiEmployee employee)
        {
            return factory.EmployeesDAO.Add(employee);
        }

        public ApiEmployee Update(int? id , ApiEmployee employee)
        {
            return factory.EmployeesDAO.Update(id, employee);
        }

        public Boolean Delete(int? id)
        {
            return factory.EmployeesDAO.Delete(id);
        }
    }
}
