using Microsoft.EntityFrameworkCore;

namespace DTO.Models
{
    public partial class QLNVEntities : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }
        //public virtual DbSet<Manager> Managers { get; set; }

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

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString, o => o.CommandTimeout(300)).Options;
        }

        public QLNVEntities() : base(GetOptions(Connection_String))
        {
            ////Disable initializer
            //Database.SetInitializer<QLNVEntities>(null);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().ToTable("Employee");
        }
    }
}
