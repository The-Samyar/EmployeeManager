using DataLayer.Models;
using System.Data.Entity;

namespace DataLayer.Context
{
    public class EmployeeManagerDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }


    }
}
