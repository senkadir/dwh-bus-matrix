using Dwh.Common.Domain;
using System;

namespace Dwh.Domain.Objects
{
    public class MatrixItem : Entity
    {
        public Guid MatrixId { get; set; }

        public Guid FactId { get; set; }

        public Guid DimensionId { get; set; }

        public Matrix Matrix { get; set; }

        public Fact Fact { get; set; }

        public Dimension Dimension { get; set; }
    }
}
