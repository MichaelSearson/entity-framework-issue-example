using EntityFrameworkIssueExample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkIssueExample
{
    public class EntityFrameworkDbContext : DbContext
    {
        public EntityFrameworkDbContext(DbContextOptions<EntityFrameworkDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                // For some reason this self referencing key generates a unique
                // constraint in the migration script if we don't set this here.

                // "ModifiedByAccountId" does not have this issue! I suspect this might 
                // the root cause of the NULL data issue.
                entity.HasIndex(e => e.AddedByAccountId).IsUnique(false);
            });
        }
    }
}