using DTO.ApiObjects;

namespace DAO.IFactory
{
    public interface IEmployeesDAO
    {
        IEnumerable<ApiEmployee> GetAll(string filter = null, string sort = "DOB DESC");

        IEnumerable<ApiEmployee> Paged(string keyword = null, string filter = null, string sort = "DOB DESC", int page = 1, int pageSize = 6);

        ApiEmployee GetSingle(int? id);

        ApiEmployee Add(ApiEmployee employee);

        ApiEmployee Update(int? id, ApiEmployee employee);

        Boolean Delete(int? id);
    }
}
