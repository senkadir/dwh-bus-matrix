using Microsoft.AspNetCore.Mvc;

namespace Matrix.UI.Controllers
{
    [Route("dimensions")]
    public class DimensionController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
