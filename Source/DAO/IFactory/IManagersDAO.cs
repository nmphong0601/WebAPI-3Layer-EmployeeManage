using DTO.ApiObjects;

namespace DAO.IFactory
{
    public interface IManagersDAO
    {
        IEnumerable<ApiManager> GetAll(string filter = null, string sort = "FullName DESC");

        IEnumerable<ApiManager> Paged(string keyword = null, string filter = null, string sort = "FullName DESC", int page = 1, int pageSize = 6);

        ApiManager GetSingle(int? id);

        ApiManager Add(ApiManager manager);

        ApiManager Update(int? id, ApiManager manager);

        Boolean Delete(int? id);

    }
}
