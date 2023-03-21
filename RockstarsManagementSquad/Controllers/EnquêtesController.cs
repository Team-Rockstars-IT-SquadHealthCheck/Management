using Microsoft.AspNetCore.Mvc;

namespace RockstarsManagementSquad.Controllers;

public class EnquêtesController : Controller
{
     public IActionResult Index()
    {
        return View();
    }
}