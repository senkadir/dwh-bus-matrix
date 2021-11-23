using Dwh.Common.Domain;
using System.Collections.Generic;

namespace Dwh.Domain.Objects
{
    public class Dimension : Entity
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }

        public ICollection<FactDimension> FactDimensions { get; set; }
    }
}
