namespace DTO.ApiObjects
{
    public class ApiManager: ApiEmployee
    {
        public ApiManager()
        {
            Employees = new List<ApiEmployee>();
        }
    }
}
