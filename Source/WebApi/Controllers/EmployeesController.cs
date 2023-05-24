using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class EmployeesController : BaseApiController
    {
        private EmployeesBUS service = new EmployeesBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiEmployee> GetAll(string filter = null, string sort = "DOB DESC")
        {
            IEnumerable<ApiEmployee> apiEmployees = new List<ApiEmployee>();
            try
            {
                apiEmployees = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return apiEmployees;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/Employees/Paging")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "DOB DESC", int page = 1, int pageSize = 6)
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
        public ApiEmployee GetSingle(int? id)
        {
            ApiEmployee apiEmployee = new ApiEmployee();
            try
            {
                apiEmployee = service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiEmployee;
        }

        //POST: Insert
        [HttpPost]
        public ApiEmployee Post([FromBody]ApiEmployee apiEmployee)
        {
            try
            {
                apiEmployee = service.Add(apiEmployee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiEmployee;
        }

        //PUST: Update
        [HttpPut]
        public ApiEmployee Put([FromBody]ApiEmployee apiEmployee)
        {
            try
            {
                int? id = apiEmployee.Id;
                apiEmployee = service.Update(id, apiEmployee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiEmployee;
        }

        //DELETE
        [HttpPut]
        public Boolean Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}