using Dwh.Domain.Models;
using System;
using System.Collections.Generic;

namespace Dwh.Domain.Commands
{
    public class EditFactCommand
    {
        public ViewFactModel Fact { get; set; }

        public List<Guid> SelectedDimensions { get; set; }

        public IEnumerable<ViewDimensionModel> Dimensions { get; set; }
    }
}
