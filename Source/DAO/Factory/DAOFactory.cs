using AutoMapper;
using DAO.IFactory;
using DTO.ApiObjects;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Factory
{
    public class DAOFactory: IDAOFactory
    {
        public IEmployeesDAO EmployeesDAO { get { return new EmployeesDAO(); } }
        public IManagersDAO ManagersDAO { get { return new ManagersDAO(); } }
        
        static DAOFactory()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ApiMappingProfile>();
            });
        }
    }

    public class ApiMappingProfile : AutoMapper.Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<ApiEmployee, Employee>().ReverseMap();
            CreateMap<Employee, ApiEmployee>().ReverseMap();

            CreateMap<ApiManager, Employee>().ReverseMap();
            CreateMap<Employee, ApiManager>().ReverseMap();
        }
    }
}
