using Dwh.Core.Services;
using Dwh.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dwh.UI.Controllers
{
    [Route("Dwh")]
    public class DwhController : BaseController
    {
        private readonly IFactService _factService;
        private readonly IDimensionService _dimensionService;

        public DwhController(IFactService factService,
                                IDimensionService dimensionService)
        {
            _factService = factService;
            _dimensionService = dimensionService;
        }

        public async Task<IActionResult> Index()
        {
            var dimennsions = await _dimensionService.GetAsync();

            dimennsions = dimennsions.OrderBy(x => x.Order).Where(x => x.IsActive).ToList();

            var facts = await _factService.GetAsync();

            facts = facts.OrderBy(x => x.Order).Where(x => x.IsActive);

            ViewDwhModel model = new()
            {
                Dimensions = dimennsions,
                Facts = facts
            };

            return View(model);
        }
    }
}
