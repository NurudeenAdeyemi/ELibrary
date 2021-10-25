using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context.EFConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasMany(u => u.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
