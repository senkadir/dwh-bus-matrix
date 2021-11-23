using Dwh.Domain.Models;
using System;
using System.Collections.Generic;

namespace Dwh.Domain.Commands
{
    public class CreateFactCommand
    {
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }

        public List<ViewDimensionModel> Dimensions { get; set; }

        public List<Guid> SelectedDimensions { get; set; }
    }
}
