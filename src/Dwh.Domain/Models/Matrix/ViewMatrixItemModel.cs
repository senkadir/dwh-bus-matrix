using System;

namespace Dwh.Domain.Models
{
    public class ViewMatrixItemModel
    {
        public Guid Id { get; set; }

        public ViewFactModel Fact { get; set; }

        public ViewDimensionModel Dimension { get; set; }
    }
}
