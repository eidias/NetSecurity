using Chinook.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Core.EntityConfigurations
{
    public static class EmployeeConfiguration
    {
        public static readonly IEntityTypeConfiguration<Employee> ForSqlite  = new SqliteEmployeeConfiguration();
    }

    public class SqliteEmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            _ = builder.Property(p => p.Id).HasColumnName("EmployeeId").IsRequired();
            _ = builder.Property(p => p.LastName).HasMaxLength(20).IsRequired();
            _ = builder.Property(p => p.FirstName).HasMaxLength(20).IsRequired();
            _ = builder.Property(p => p.Title).HasMaxLength(30);
            _ = builder.Property(p => p.Address).HasMaxLength(70);
            _ = builder.Property(p => p.City).HasMaxLength(40);
            _ = builder.Property(p => p.State).HasMaxLength(40);
            _ = builder.Property(p => p.Country).HasMaxLength(40);
            _ = builder.Property(p => p.PostalCode).HasMaxLength(10);
            _ = builder.Property(p => p.Phone).HasMaxLength(24);
            _ = builder.Property(p => p.Fax).HasMaxLength(24);
            _ = builder.Property(p => p.Email).HasMaxLength(60);

            _ = builder.HasKey(p => p.Id);
            _ = builder.HasOne(x => x.Boss)
                       .WithMany(x => x.Reportees)
                       .HasForeignKey("ReportsTo")
                       .IsRequired(false);

        }
    }
}
