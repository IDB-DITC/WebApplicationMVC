using System.Data.Entity;

namespace WebApplicationEF.Models
{
    public class EmployeeContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public EmployeeContext() : base("mydb")
        {
            
        }
    }
}