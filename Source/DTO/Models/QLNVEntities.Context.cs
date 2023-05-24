using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;

namespace DTO.Models
{
    public partial class QLNVEntities : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }

        public static string MDF_Directory
        {
            get
            {
                var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                return Path.GetFullPath(Path.Combine(directoryPath, "App_Data//Databases"));
            }
        }

        public static string Connection_String
        {
            get
            {
                return "data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=" + MDF_Directory + "\\QLNV.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;";
            }
        }

        public QLNVEntities()
            : base(Connection_String)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
