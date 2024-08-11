namespace DAO.IFactory
{
    public class IDAOFactory
    {
        IEmployeesDAO EmployeesDAO { get; }
        IManagersDAO ManagersDAO { get; }
    }
}
