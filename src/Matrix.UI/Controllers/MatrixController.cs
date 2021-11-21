using Matrix.Core.Services;
using Matrix.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Matrix.UI.Controllers
{
    [Route("matrix")]
    public class MatrixController : BaseController
    {
        private readonly IFactService _factService;
        private readonly IDimensionService _dimensionService;

        public MatrixController(IFactService factService,
                                IDimensionService dimensionService)
        {
            _factService = factService;
            _dimensionService = dimensionService;
        }

        public async Task<IActionResult> Index()
        {
            ViewMatrixModel model = new()
            {
                Dimensions = await _dimensionService.GetAsync(),
                Facts = await _factService.GetAsync()
            };

            return View(model);
        }
    }
}
