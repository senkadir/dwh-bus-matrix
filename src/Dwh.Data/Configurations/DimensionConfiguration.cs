using Dwh.Common.Domain.Configuration;
using Dwh.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dwh.Data.Configurations
{
    internal class DimensionConfiguration : EntityConfiguration<Dimension>
    {
        public override void Configure(EntityTypeBuilder<Dimension> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                   .HasMaxLength(250);

            builder.HasOne(x => x.Matrix)
                   .WithMany(x => x.Dimensions)
                   .HasForeignKey(x => x.MatrixId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.MatrixItems)
                   .WithOne(x => x.Dimension)
                   .HasForeignKey(x => x.DimensionId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
