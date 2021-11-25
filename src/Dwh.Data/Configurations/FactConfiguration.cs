using Dwh.Common.Domain.Configuration;
using Dwh.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dwh.Data.Configurations
{
    public class FactConfiguration : EntityConfiguration<Fact>
    {
        public override void Configure(EntityTypeBuilder<Fact> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                   .HasMaxLength(250);

            builder.HasOne(x => x.Matrix)
                   .WithMany(x => x.Facts)
                   .HasForeignKey(x => x.MatrixId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.MatrixItems)
                   .WithOne(x => x.Fact)
                   .HasForeignKey(x => x.FactId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
