using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ManagersBUS
    {
        readonly DAOFactory factory;
        public ManagersBUS()
        {
            this.factory = new DAOFactory();
        }

        public ManagersBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiManager> GetAll(string filter = null, string sort = "FullName DESC")
        {
            return factory.ManagersDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiManager> Paged(string keyword = null, string filter = null, string sort = "FullName DESC", int page = 1, int pageSize = 6)
        {
            return factory.ManagersDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public ApiManager GetSingle(int? id)
        {
            return factory.ManagersDAO.GetSingle(id);
        }

        public ApiManager Add(ApiManager manager)
        {
            return factory.ManagersDAO.Add(manager);
        }

        public ApiManager Update(int? id, ApiManager manager)
        {
            return factory.ManagersDAO.Update(id, manager);
        }

        public Boolean Delete(int? id)
        {
            return factory.ManagersDAO.Delete(id);
        }
    }
}
