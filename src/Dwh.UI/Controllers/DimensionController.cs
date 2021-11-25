using Dwh.Core.Services;
using Dwh.Domain.Commands;
using Dwh.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dwh.UI.Controllers
{
    [Route("dimensions")]
    public class DimensionController : BaseController
    {
        private readonly IDimensionService _dimensionService;
        private readonly IMatrixService _matrixService;

        public DimensionController(IDimensionService dimensionService,
                                   IMatrixService matrixService)
        {
            _dimensionService = dimensionService;
            _matrixService = matrixService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _dimensionService.GetAsync(new GetDimensionsQuery()));
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            CreateDimensionCommand command = new()
            {
                Matrixes = await _matrixService.GetAsync()
            };

            return View(command);
        }

        [HttpPost("new")]
        public async Task<IActionResult> NewDimension([FromForm] CreateDimensionCommand command)
        {
            await _dimensionService.CreateAsync(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:guid}/edit")]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var dimension = await _dimensionService.GetAsync(id);

            EditDimensionCommand command = new()
            {
                Id = dimension.Id,
                Name = dimension.Name,
                Order = dimension.Order,
                IsActive = dimension.IsActive
            };

            return View(command);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditDimension([FromForm] EditDimensionCommand command)
        {
            await _dimensionService.EditAsync(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
