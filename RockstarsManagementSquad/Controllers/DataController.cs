using Microsoft.AspNetCore.Mvc;

namespace RockstarsManagementSquad.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
