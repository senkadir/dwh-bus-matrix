using Matrix.Common.Domain.Configuration;
using Matrix.Domain.Objects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Matrix.Data.Configurations
{
    public class FactDimensionConfiguration : EntityConfiguration<FactDimension>
    {
        public override void Configure(EntityTypeBuilder<FactDimension> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => new { x.FactId, x.DimensionId });
        }
    }
}
