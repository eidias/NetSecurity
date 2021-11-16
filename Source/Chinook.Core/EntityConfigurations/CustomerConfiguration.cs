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
    public static class CustomerConfiguration
    {
        public static readonly IEntityTypeConfiguration<Customer> ForSqlite = new SqliteCustomerConfiguration();
    }

    public class SqliteCustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            _ = builder.Property(p => p.Id).HasColumnName("CustomerId").IsRequired();
            _ = builder.Property(p => p.FirstName).HasMaxLength(40).IsRequired();
            _ = builder.Property(p => p.LastName).HasMaxLength(20).IsRequired();
            _ = builder.Property(p => p.Company).HasMaxLength(80);
            _ = builder.Property(p => p.Address).HasMaxLength(70);
            _ = builder.Property(p => p.City).HasMaxLength(40);
            _ = builder.Property(p => p.State).HasMaxLength(40);
            _ = builder.Property(p => p.PostalCode).HasMaxLength(10);
            _ = builder.Property(p => p.Phone).HasMaxLength(24);
            _ = builder.Property(p => p.Fax).HasMaxLength(24);
            _ = builder.Property(p => p.Email).HasMaxLength(60).IsRequired();

            _ = builder.HasKey(b => b.Id);
            _ = builder.HasOne(b => b.SupportRep);
        }
    }
}
