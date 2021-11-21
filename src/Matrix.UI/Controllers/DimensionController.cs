using Matrix.Core.Services;
using Matrix.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Matrix.UI.Controllers
{
    [Route("dimensions")]
    public class DimensionController : BaseController
    {
        private readonly IDimensionService _dimensionService;

        public DimensionController(IDimensionService dimensionService)
        {
            _dimensionService = dimensionService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _dimensionService.GetAsync());
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("new")]
        public async Task<IActionResult> NewDimension([FromForm] CreateDimensionCommand command)
        {
            await _dimensionService.CreateAsync(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
