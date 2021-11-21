using System.Collections.Generic;

namespace Matrix.Domain.Models
{
    public class ViewMatrixModel
    {
        public List<ViewDimensionModel> Dimensions { get; set; }

        public IEnumerable<ViewFactModel> Facts{ get; set; }
    }
}
