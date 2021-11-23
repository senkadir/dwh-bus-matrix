using Dwh.Common.Domain.Configuration;
using Dwh.Domain.Objects;
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
        }
    }
}
