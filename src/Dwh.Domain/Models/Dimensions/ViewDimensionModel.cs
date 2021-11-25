using System;
using System.Collections.Generic;

namespace Dwh.Domain.Models
{
    public class ViewDimensionModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }

        public IEnumerable<ViewFactModel> Facts { get; set; }
    }
}
