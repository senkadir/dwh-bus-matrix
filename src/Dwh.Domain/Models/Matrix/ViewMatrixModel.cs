using System.Collections.Generic;

namespace Dwh.Domain.Models
{
    public class ViewDwhModel
    {
        public List<ViewDimensionModel> Dimensions { get; set; }

        public IEnumerable<ViewFactModel> Facts{ get; set; }
    }
}
