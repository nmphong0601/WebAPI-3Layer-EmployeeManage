using DTO.Models;
using Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.IFactory;
using DTO.ApiObjects;
using AutoMapper;

namespace DAO.Factory
{
    public class ManagersDAO: IManagersDAO
    {
        private QLNVEntities db = new QLNVEntities();

        public IEnumerable<ApiManager> GetAll(string filter = null, string sort = "FullName DESC")
        {
            var sqlStr = "Select * from Employees" + (filter != null ? " where IsManager = 1 " + filter + " ORDER BY " + sort : " where IsManager = 1 ORDER BY " + sort);

            var managers = db.Employees.SqlQuery(sqlStr).ToList();

            return Mapper.Map<IEnumerable<Employee>, IEnumerable<ApiManager>>(managers);
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
            return Mapper.Map<Employee, ApiManager>(db.Employees.Where(c => c.Id == id).FirstOrDefault());
        }

        public ApiManager Add(ApiManager manager)
        {
            db.Employees.Add(Mapper.Map<ApiManager, Employee>(manager));
            manager.Id = db.SaveChanges();

            return manager;
        }

        public ApiManager Update(int? id, ApiManager manager)
        {
            var managerInDB = db.Employees.Where(c => c.Id == id).FirstOrDefault();
            if (managerInDB != null)
            {
                manager.Id = managerInDB.Id;
                managerInDB = Mapper.Map<ApiManager, Employee>(manager);
                db.Entry(managerInDB).State = System.Data.EntityState.Modified;
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
                        db.Entry(emp).State = System.Data.EntityState.Deleted;
                    }
                }

                isDelete = db.Employees.Remove(managerInDB) != null ? true : false;
                db.Entry(managerInDB).State = System.Data.EntityState.Deleted;

                db.SaveChanges();
            }

            return isDelete;
        }
    }
}
