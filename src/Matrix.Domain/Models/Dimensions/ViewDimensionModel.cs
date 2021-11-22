using System;

namespace Matrix.Domain.Models
{
    public class ViewDimensionModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }
    }
}
