using Matrix.Core.Services;
using Matrix.Domain.Commands;
using Matrix.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("{id:guid}/edit")]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var dimension = await _dimensionService.GetAsync(id);

            EditDimensionModel model = new()
            {
                Id = dimension.Id,
                Name = dimension.Name,
                Order = dimension.Order,
                IsActive = dimension.IsActive
            };

            return View(model);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditDimension([FromForm] EditDimensionModel model)
        {
            await _dimensionService.EditAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
