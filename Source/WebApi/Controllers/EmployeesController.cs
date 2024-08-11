using Microsoft.AspNetCore.Mvc;
using BUS;
using DTO.ApiObjects;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : BaseController
    {
        private readonly ILogger<EmployeesController> _logger;
        private EmployeesBUS service = new EmployeesBUS();

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiEmployee> Get(string filter = null, string sort = "DOB DESC")
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
        [Route("Paging")]
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
        [Route("{id?}")]
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
        public ApiEmployee Post([FromBody] ApiEmployee apiEmployee)
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
        public ApiEmployee Put([FromBody] ApiEmployee apiEmployee)
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
        [Route("{id?}")]
        public Boolean Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}
