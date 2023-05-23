using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ManagersController : BaseApiController
    {
        private ManagersBUS service = new ManagersBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiManager> GetAll(string filter = null, string sort = "FullName DESC")
        {
            IEnumerable<ApiManager> apiManagers = new List<ApiManager>();
            try
            {
                apiManagers = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiManagers;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/Managers/Paging")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "FullName DESC", int page = 1, int pageSize = 6)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            try
            {
                int total = service.GetAll(filter).Count();
                int totalPage = total / pageSize + (total % pageSize > 0 ? 1 : 0);
                if (page < 1)
                {
                    page = 1;
                }
                if (page > totalPage)
                {
                    page = totalPage;
                }

                var apiUsers = service.Paged(keyword, filter, sort, page, pageSize);

                result["totalPage"] = totalPage;
                result["curPage"] = page;
                result["Collection"] = apiUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        //GET: Gingle
        [HttpGet]
        public ApiManager GetSingle(int? id)
        {
            ApiManager apiManager = new ApiManager();
            try
            {
                apiManager = service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiManager;
        }

        //POST: Insert
        [HttpPost]
        public ApiManager Post([FromBody] ApiManager apiCategorie)
        {
            try
            {
                apiCategorie = service.Add(apiCategorie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCategorie;
        }

        //PUST: Update
        [HttpPut]
        public ApiManager Put([FromBody] ApiManager apiCategorie)
        {
            try
            {
                int? id = apiCategorie.Id;
                apiCategorie = service.Update(id, apiCategorie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCategorie;
        }

        //DELETE
        [HttpPut]
        public Boolean Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}