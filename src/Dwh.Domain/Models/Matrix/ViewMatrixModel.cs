using System;
using System.Collections.Generic;
using System.Linq;

namespace Dwh.Domain.Models
{
    public class ViewMatrixModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ViewDimensionModel> Dimensions
        {
            get
            {
                return Items.Select(x => x.Dimension)
                            .OrderBy(x => x.Order)
                            .GroupBy(x => x.Id)
                            .Select(x => x.First());
            }
        }

        public IEnumerable<ViewFactModel> Facts
        {
            get
            {
                return Items.Select(x => x.Fact)
                            .OrderBy(x => x.Order)
                            .GroupBy(x => x.Id)
                            .Select(x => x.First());
            }
        }

        public List<ViewMatrixItemModel> Items { get; set; }
    }
}
