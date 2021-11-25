using Dwh.Common.Domain;
using System;
using System.Collections.Generic;

namespace Dwh.Domain.Objects
{
    public class Dimension : Entity
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }

        public Guid MatrixId { get; set; }

        public Matrix Matrix { get; set; }

        public ICollection<MatrixItem> MatrixItems { get; set; }
    }
}
