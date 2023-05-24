using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DTO.Models
{
    public static string MDF_Directory
    {
        get
        {
            var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(Path.Combine(directoryPath, "..//Databases"));
        }
    }
    
    public partial class QLNVEntities : DbContext
    {
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=" + MDF_Directory + "\\QLNV.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                
                entity.HasOne(m => m.Manager)
                    .WithMany(e => e.Employee)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employee_Manager");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
            });
        }
    }
}
