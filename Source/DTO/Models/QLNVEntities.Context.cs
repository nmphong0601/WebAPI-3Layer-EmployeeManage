using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DTO.Models
{
    public partial class QLNVEntities : DbContext
    {
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"metadata=res://*/Models.QLNVEntities.csdl|res://*/Models.QLNVEntities.ssdl|res://*/Models.QLNVEntities.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|Databases\QLNV.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework&quot;");
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
