using Dwh.Common.Domain;
using System.Collections.Generic;

namespace Dwh.Domain.Objects
{
    public class Matrix : Entity
    {
        public string Name { get; set; }

        public ICollection<Dimension> Dimensions { get; set; }

        public ICollection<Fact> Facts { get; set; }

        public ICollection<MatrixItem> Items { get; set; }
    }
}
