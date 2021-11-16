using Chinook.Core.Domain;
using Chinook.Core.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Core.DataAccess
{
    public class ChinookContext : DbContext
    {
        public ChinookContext(DbContextOptions<ChinookContext> options)
            : base(options)
        {
            //Only used to call base constuctor.
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (Database.IsSqlite())
            {
                modelBuilder.ApplyConfiguration(CustomerConfiguration.ForSqlite);
                modelBuilder.ApplyConfiguration(EmployeeConfiguration.ForSqlite);

            }
        }
    }
}
