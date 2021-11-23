using System;

namespace Dwh.Domain.Commands
{
    public class EditDimensionCommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}
