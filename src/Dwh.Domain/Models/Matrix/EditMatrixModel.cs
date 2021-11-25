using System.Collections.Generic;

namespace Dwh.Domain.Models
{
    public class EditMatrixModel
    {
        public ViewMatrixModel Matrix { get; set; }

        public IEnumerable<ViewDimensionModel> Dimensions { get; set; }

        public IEnumerable<ViewFactModel> Facts { get; set; }
    }
}
