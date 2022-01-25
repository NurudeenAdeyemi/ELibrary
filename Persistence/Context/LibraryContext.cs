using Domain.Models;
using ELibrary.Infrastructure.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class LibraryContext : DbContext 
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations<LibraryContext>();
            modelBuilder.ConfigureDeletableEntities();
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            this.AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }


        private const string IsDeletedProperty = "IsDeleted";

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[IsDeletedProperty] = false;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues[IsDeletedProperty] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[IsDeletedProperty] = true;
                        break;
                }
            }
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookLending> BookLendings { get; set; }
        public DbSet<BookTransaction> BookTransactions { get; set; }
        public DbSet<BookTransactionItem> BookTransactionItems { get; set; }

        public DbSet<BookTransactionLog> BookTransactionLogs { get; set; }
    }
}
