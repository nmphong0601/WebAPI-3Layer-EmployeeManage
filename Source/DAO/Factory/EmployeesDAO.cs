using DTO.Models;
using DAO.IFactory;
using AutoMapper;
using DTO.ApiObjects;
using Microsoft.EntityFrameworkCore;

namespace DAO.Factory
{
    public class EmployeesDAO: IEmployeesDAO
    {
        public IMapper mapper;
        private QLNVEntities db = new QLNVEntities();

        public IEnumerable<ApiEmployee> GetAll(string filter = null, string sort = "CatID DESC")
        {
            var sqlStr = "Select * from Employees" + (filter != null ? " where " + filter + " ORDER BY " + sort : " ORDER BY " + sort);

            var employees = db.Employees.FromSqlRaw(sqlStr).ToList();

            return mapper.Map<IEnumerable<Employee>, IEnumerable<ApiEmployee>>(employees);
        }

        public IEnumerable<ApiEmployee> Paged(string keyword = null, string filter = null, string sort = "CatId DESC", int page = 1, int pageSize = 6)
        {
            var employees = GetAll(filter, sort).Where(c => c.FullName.Contains(keyword))
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return employees;
        }

        public ApiEmployee GetSingle(int? id)
        {
            return mapper.Map<Employee, ApiEmployee>(db.Employees.Where(c => c.Id == id).FirstOrDefault());
        }

        public ApiEmployee Add(ApiEmployee employee)
        {
            db.Employees.Add(mapper.Map<ApiEmployee, Employee>(employee));
            employee.Id = db.SaveChanges();

            return employee;
        }

        public ApiEmployee Update(int? id , ApiEmployee apiEmployee)
        {
            var employeeInDB = db.Employees.Where(c => c.Id == id).FirstOrDefault();
            if (employeeInDB != null)
            {
                apiEmployee.Id = employeeInDB.Id;
                employeeInDB = mapper.Map<ApiEmployee, Employee>(apiEmployee);
                db.Entry(employeeInDB).State = EntityState.Modified;
                db.SaveChanges();
            }

            return apiEmployee;
        }

        public Boolean Delete(int? id)
        {
            var isDelete = false;

            var employeeInDB = db.Employees.Where(c => c.Id == id).FirstOrDefault();
            if (employeeInDB != null)
            {
                var employees = db.Employees.Where(p => p.ManagerId == employeeInDB.Id).ToList();
                if (employees.Count != 0)
                {
                    foreach (var emp in employees)
                    {
                        db.Employees.Remove(emp);
                        db.Entry(emp).State = EntityState.Deleted;
                    }
                }

                isDelete = db.Employees.Remove(employeeInDB) != null ? true : false;
                db.Entry(employeeInDB).State = EntityState.Deleted;

                db.SaveChanges();
            }

            return isDelete;
        }
    }
}
