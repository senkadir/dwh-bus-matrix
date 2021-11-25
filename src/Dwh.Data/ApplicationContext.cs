using Dwh.Common.Domain;
using Dwh.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dwh.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Dimension> Dimensions { get; set; }

        public DbSet<Fact> Facts { get; set; }

        public DbSet<Matrix> Matrixes { get; set; }

        public DbSet<MatrixItem> MatrixItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Identifier).Assembly);

            ApplyManyToManyRelations(modelBuilder);
        }

        private void ApplyManyToManyRelations(ModelBuilder modelBuilder)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();

            addedEntries.ForEach(added =>
            {
                if (added.Entity is Entity)
                {
                    added.Property(nameof(Entity.CreatedAt)).CurrentValue = DateTime.UtcNow;
                    added.Property(nameof(Entity.ModifiedAt)).CurrentValue = DateTime.UtcNow;
                }
            });

            var updatedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();

            updatedEntries.ForEach(added =>
            {
                if (added.Entity is Entity)
                {
                    added.Property(nameof(Entity.ModifiedAt)).CurrentValue = DateTime.Now;
                }
            });

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
