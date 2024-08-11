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
        public IEmployeesDAO EmployeesDAO { 
            get { 
                var managerDAO = new EmployeesDAO();
                managerDAO.mapper = mapper;
                return managerDAO;
            } 
        }
        public IManagersDAO ManagersDAO { 
            get {
                var managerDAO = new ManagersDAO();
                managerDAO.mapper = mapper;
                return managerDAO;
            } 
        }

        public static readonly IMapper mapper;

        static DAOFactory()
        {
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<ApiMappingProfile>();
            //});

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ApiMappingProfile>();
            });

            mapper = config.CreateMapper();
        }
    }

    public class ApiMappingProfile : AutoMapper.Profile
    {
        public ApiMappingProfile()
        {
            //CreateMap<ApiEmployee, Employee>().ForMember(dest => dest.Manager, conf => conf.MapFrom(src => src.Manager))
            //                                  .ForMember(dest => dest.Employees, conf => conf.MapFrom(src => src.Employees));
            //CreateMap<Employee, ApiEmployee>().ForMember(dest => dest.Manager, conf => conf.MapFrom(src => src.Manager))
            //                                  .ForMember(dest => dest.Employees, conf => conf.MapFrom(src => src.Employees));

            //CreateMap<ApiManager, Employee>().ForMember(dest => dest.Employees, conf => conf.MapFrom(src => src.Employees));
            //CreateMap<Employee, ApiManager>().ForMember(dest => dest.Employees, conf => conf.MapFrom(src => src.Employees));

            CreateMap<ApiEmployee, Employee>().ForMember(dest => dest.Employees, option => option.Ignore());
            CreateMap<Employee, ApiEmployee>().ForMember(dest => dest.Employees, option => option.Ignore());

            CreateMap<ApiManager, Employee>().ForMember(dest => dest.Employees, option => option.Ignore());
            CreateMap<Employee, ApiManager>().ForMember(dest => dest.Employees, option => option.Ignore());
        }
    }
}
