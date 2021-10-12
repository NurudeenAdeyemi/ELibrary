using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace ELibrary.Infrastructure.Persistence.Context.EfConfigurations
{
    public class RoleEntityTypeConfiguration
        : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasIndex(u => u.Name)
                .IsUnique();

            builder.HasMany(rp => rp.UserRoles)
             .WithOne(p => p.Role)
             .HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
