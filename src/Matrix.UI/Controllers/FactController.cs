using Matrix.Core.Services;
using Matrix.Domain.Commands;
using Matrix.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> NewFact([FromForm] CreateFactCommand command)
        {
            await _factService.CreateAsync(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:guid}/edit")]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var fact = await _factService.GetAsync(id);
            var dimensions = await _dimensionService.GetAsync();

            EditFactCommand command = new()
            {
                Fact = new ViewFactModel()
                {
                    Id = fact.Id,
                    Name = fact.Name,
                    Order = fact.Order,
                    IsActive = fact.IsActive,
                    Dimensions = fact.Dimensions
                },
                Dimensions = dimensions
            };

            return View(command);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditFact([FromForm] EditFactCommand command)
        {
            await _factService.EditAsync(command);

            return RedirectToAction(nameof(Index));
        }

    }
}
