using DataLayer.Models;
using System.Data.Entity;

namespace DataLayer.Context
{
    public class EmployeeManagerDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Employer)
                .WithMany(emp => emp.Employees)
                .HasForeignKey(e => e.EmployerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Position)
                .WithMany(pos => pos.Employees)
                .HasForeignKey(e => e.PositionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Employer)
                .WithRequired(u => u.User);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Employee)
                .WithRequired(u => u.User);
        }
    }
}
