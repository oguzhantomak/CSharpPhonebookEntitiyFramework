using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EFConfigurations.ORM.Entities.EntitySplitting;
using EFConfigurations.ORM.Entities.ManyToMany;

namespace EFConfigurations.ORM.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
            Database.Connection.ConnectionString = "Server=.;database=EFDb;uid=sa;pwd=123";
        }

        public virtual DbSet<Employee> Employees { get; set; }

        //Table Splitting

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Table Splitting

            modelBuilder.Entity<Employee>()
                .HasKey(p => p.EmployeeID)  // Primary Key Belirlenmesi
                .ToTable("Employees");      //SQL'deki tablo adı

            modelBuilder.Entity<EmployeeContactDetail>()
                .HasKey(p => p.EmployeeID)
                .ToTable("Employees");

            modelBuilder.Entity<Employee>()
                .HasRequired(p=>p.EmployeeContactDetail) // Bir personelin detayı olmak zorundadır. public virtual EmployeeContactDetail EmployeeContactDetail { get; set; } şeklinde Employee classında navigation tanımlayarak bağladık.
                .WithRequiredPrincipal(p=>p.Employee); // Bir detay bir personel bağlı olmak zorundadır. public virtual Employee Employee { get; set; } şeklinde EmployeeContactDetails classında navigation tanımlayarak bağladık

            //Many to Many

            modelBuilder.Entity<Teacher>()
                .HasMany(x => x.Students)
                .WithMany(x => x.Teachers)
                .Map(map =>
                {
                    map.ToTable("TeacherStudents");
                    map.MapLeftKey("TeacherID");
                    map.MapRightKey("StudentID");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
