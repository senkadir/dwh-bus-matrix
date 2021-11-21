using Matrix.Core.Services;
using Matrix.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Matrix.UI.Controllers
{
    [Route("facts")]
    public class FactController : BaseController
    {
        private readonly IFactService _factService;
        private readonly IDimensionService _dimensionService;

        public FactController(IFactService factService,
                              IDimensionService dimensionService)
        {
            _factService = factService;
            _dimensionService = dimensionService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _factService.GetAsync());
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var dimensions = await _dimensionService.GetAsync();

            CreateFactCommand command = new()
            {
                Dimensions = dimensions
            };

            return View(command);
        }

        [HttpPost("new")]
        public async Task<IActionResult> NewDimension([FromForm] CreateFactCommand command)
        {
            await _factService.CreateAsync(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
