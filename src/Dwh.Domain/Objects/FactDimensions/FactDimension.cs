using Dwh.Common.Domain;
using System;

namespace Dwh.Domain.Objects
{
    public class FactDimension : Entity
    {
        public Guid FactId { get; set; }

        public Guid DimensionId { get; set; }

        public Fact Fact { get; set; }

        public Dimension Dimension { get; set; }
    }
}
