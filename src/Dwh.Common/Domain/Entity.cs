using System;

namespace Dwh.Common.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
