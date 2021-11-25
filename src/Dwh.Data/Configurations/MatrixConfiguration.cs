using Dwh.Common.Domain.Configuration;
using Dwh.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dwh.Data.Configurations
{
    public class MatrixConfiguration : EntityConfiguration<Matrix>
    {
        public override void Configure(EntityTypeBuilder<Matrix> builder)
        {
            base.Configure(builder);

            builder.ToTable("Matrixes");

            builder.Property(x => x.Name)
                   .HasMaxLength(250);

        }
    }
}
