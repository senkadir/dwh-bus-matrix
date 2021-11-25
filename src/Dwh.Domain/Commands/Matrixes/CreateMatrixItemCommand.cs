using System;

namespace Dwh.Domain.Commands
{
    public class CreateMatrixItemCommand
    {
        public Guid MatrixId { get; set; }

        public Guid DimensionId { get; set; }

        public Guid FactId { get; set; }
    }
}
