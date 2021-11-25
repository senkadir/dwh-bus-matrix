using Dwh.Domain.Models;
using System;
using System.Collections.Generic;

namespace Dwh.Domain.Commands
{
    public class CreateDimensionCommand
    {
        public Guid MatrixId { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<ViewMatrixModel> Matrixes { get; set; }
    }
}
