using Matrix.Common.Domain.Configuration;
using Matrix.Domain.Objects.Dimensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix.Data.Configurations
{
    internal class DimensionConfiguration : EntityConfiguration<Dimension>
    {
        public override void Configure(EntityTypeBuilder<Dimension> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                   .HasMaxLength(250);
        }
    }
}
