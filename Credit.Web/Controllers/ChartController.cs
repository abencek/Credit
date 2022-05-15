using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Credit.Web.Controllers
{
    /// <summary>
    /// Controller for Chart view 
    /// Sample charts are created on client side with Chart.js library
    /// </summary>

    [Authorize]
    public class ChartController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.SideBarItemSelected = "Charts";

            return View();
            
        }

    }
}
