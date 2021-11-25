using Dwh.Core.Services;
using Dwh.Domain.Commands;
using Dwh.Domain.Models;
using Dwh.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dwh.UI.Controllers
{
    [Route("matrixes")]
    public class MatrixController : BaseController
    {
        private readonly IFactService _factService;
        private readonly IDimensionService _dimensionService;
        private readonly IMatrixService _matrixService;

        public MatrixController(IFactService factService,
                                IDimensionService dimensionService,
                                IMatrixService matrixService)
        {
            _factService = factService;
            _dimensionService = dimensionService;
            _matrixService = matrixService;
        }

        [HttpGet("view")]
        public async Task<IActionResult> Views()
        {
            var dimennsions = await _dimensionService.GetAsync(new GetDimensionsQuery());

            dimennsions = dimennsions.OrderBy(x => x.Order).Where(x => x.IsActive).ToList();

            var facts = await _factService.GetAsync(new GetFactsQuery());

            facts = facts.OrderBy(x => x.Order).Where(x => x.IsActive);

            ViewMatrixModel model = new()
            {
                //Dimensions = dimennsions,
                //Facts = facts
            };

            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var matrixes = await _matrixService.GetAsync();

            return View(matrixes);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("new")]
        public async Task<IActionResult> NewMatrix([FromForm] CreateMatrixCommand command)
        {
            await _matrixService.CreateAsync(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:guid}/edit")]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            EditMatrixModel model = new()
            {
                Matrix = await _matrixService.GetAsync(new GetMatrixQuery()
                {
                    MatrixId = id
                }),
                Facts = await _factService.GetAsync(new GetFactsQuery()),
                Dimensions = await _dimensionService.GetAsync(new GetDimensionsQuery())
            };

            return View(model);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] CreateMatrixItemCommand command)
        {
            await _matrixService.CreateAsync(command);

            return Ok();
        }
    }
}
