using Dwh.Core.Services;
using Dwh.Domain.Commands;
using Dwh.Domain.Models;
using Dwh.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dwh.UI.Controllers
{
    [Route("facts")]
    public class FactController : BaseController
    {
        private readonly IFactService _factService;
        private readonly IMatrixService _matrixService;

        public FactController(IFactService factService,
                              IMatrixService matrixService)
        {
            _factService = factService;
            _matrixService = matrixService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _factService.GetAsync(new GetFactsQuery()));
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            CreateFactCommand command = new()
            {
                Matrixes = await _matrixService.GetAsync()
            };

            return View(command);
        }

        [HttpPost("new")]
        public async Task<IActionResult> NewFact([FromForm] CreateFactCommand command)
        {
            await _factService.CreateAsync(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("api/new")]
        public async Task<IActionResult> NewFactApi([FromBody] CreateFactCommand command)
        {
            command.IsActive = true;

            await _factService.CreateAsync(command);

            return Ok();
        }

        [HttpGet("{id:guid}/edit")]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var fact = await _factService.GetAsync(id);

            EditFactCommand command = new()
            {
                Fact = new ViewFactModel()
                {
                    Id = fact.Id,
                    Name = fact.Name,
                    Order = fact.Order,
                    IsActive = fact.IsActive
                }
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
