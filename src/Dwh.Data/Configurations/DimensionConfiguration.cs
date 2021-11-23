using Dwh.Common.Domain.Configuration;
using Dwh.Domain.Objects;
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
        }
    }
}
