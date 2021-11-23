using System;

namespace Matrix.Domain.Models
{
    public class EditDimensionModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}
