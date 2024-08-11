namespace DemoWeb.Models
{
    public class Manager: Employee
    {
        public Manager()
        {
            Employees = new List<Employee>();
        }

        public IList<Employee> Employees { get; set; }
    }
}