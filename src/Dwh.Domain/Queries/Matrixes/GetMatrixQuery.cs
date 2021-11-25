using System;

namespace Dwh.Domain.Queries
{
    public class GetMatrixQuery
    {
        public Guid MatrixId { get; set; }

        public bool? ActiveDimensions { get; set; }

        public bool? ActiveFacts { get; set; }
    }
}
