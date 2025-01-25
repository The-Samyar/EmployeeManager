using DataLayer.Models;
using System.Data.Entity;
using System.Reflection.Emit;

namespace DataLayer.Context
{
    public class EmployeeManagerDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RewardHistory> RewardHistorys { get; set; }
    }
}
