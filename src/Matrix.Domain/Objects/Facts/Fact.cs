using Matrix.Common.Domain;
using System.Collections.Generic;

namespace Matrix.Domain.Objects
{
    public class Fact : Entity
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }

        public ICollection<FactDimension> Dimensions { get; set; }
    }
}
