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
    public class BookAuthorEntityTypeConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.ToTable("BookAuthors");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.BookId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(u => u.AuthorId)
               .HasColumnType("int")
               .IsRequired();
        }
    }
}
