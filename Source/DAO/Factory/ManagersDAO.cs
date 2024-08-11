using DTO.Models;
using DAO.IFactory;
using DTO.ApiObjects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DAO.Factory
{
    public class ManagersDAO: IManagersDAO
    {
        public IMapper mapper;
        private QLNVEntities db = new QLNVEntities();

        public IEnumerable<ApiManager> GetAll(string filter = null, string sort = "FullName DESC")
        {
            var sqlStr = "Select * from Employees" + (filter != null ? " where IsManager = 1 " + filter + " ORDER BY " + sort : " where IsManager = 1 ORDER BY " + sort);

            var managers = db.Employees.FromSqlRaw(sqlStr).ToList();
            var apiManagers = mapper.Map<IEnumerable<Employee>, IEnumerable<ApiManager>>(managers);

            foreach (var apiManager in apiManagers)
            {
                var sqlEmployeeStr = "Select * from Employees where ManagerId = "+ apiManager.Id + " ORDER BY DOB ASC";
                var employees = db.Employees.FromSqlRaw(sqlEmployeeStr).ToList();

                apiManager.Employees = mapper.Map<List<Employee>, List<ApiEmployee>>(employees);
            }

            return apiManagers;
        }

        public IEnumerable<ApiManager> Paged(string keyword = null, string filter = null, string sort = "FullName DESC", int page = 1, int pageSize = 6)
        {
            var apiManagers = GetAll(filter, sort).Where(m => m.FullName.Contains(keyword))
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return apiManagers;
        }

        public ApiManager GetSingle(int? id)
        {
            return mapper.Map<Employee, ApiManager>(db.Employees.Where(c => c.Id == id).FirstOrDefault());
        }

        public ApiManager Add(ApiManager apiManager)
        {
            var employees = mapper.Map<List<ApiEmployee>, List<Employee>>(apiManager.Employees);
            var manager = mapper.Map<ApiManager, Employee>(apiManager);
            
            manager.IsManager = true;
            manager.Employees = employees;

            db.Employees.Add(manager);
            manager.Id = db.SaveChanges();

            return mapper.Map<Employee, ApiManager>(manager);
        }

        public ApiManager Update(int? id, ApiManager manager)
        {
            var managerInDB = db.Employees.Where(c => c.Id == id).FirstOrDefault();
            if (managerInDB != null)
            {
                manager.Id = managerInDB.Id;
                managerInDB = mapper.Map<ApiManager, Employee>(manager);
                db.Entry(managerInDB).State = EntityState.Modified;
                db.SaveChanges();
            }

            return manager;
        }

        public Boolean Delete(int? id)
        {
            var isDelete = false;

            var managerInDB = db.Employees.Where(c => c.Id == id).FirstOrDefault();
            if (managerInDB != null)
            {
                var employees = db.Employees.Where(p => p.ManagerId == managerInDB.Id).ToList();
                if (employees.Count != 0)
                {
                    foreach (var emp in employees)
                    {
                        db.Employees.Remove(emp);
                        db.Entry(emp).State = EntityState.Deleted;
                    }
                }

                isDelete = db.Employees.Remove(managerInDB) != null ? true : false;
                db.Entry(managerInDB).State = EntityState.Deleted;

                db.SaveChanges();
            }

            return isDelete;
        }
    }
}
