using Dwh.Common.Domain.Configuration;
using Dwh.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dwh.Data.Configurations
{
    public class MatrixItemConfiguration : EntityConfiguration<MatrixItem>
    {
        public override void Configure(EntityTypeBuilder<MatrixItem> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Matrix)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x=>x.MatrixId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
