using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquadLibrary;
using System.Diagnostics;

namespace RockstarsManagementSquad.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public UserViewModel CreateUser(string name, string email, string emailConfirmed, string password)
        {
            UserViewModel user = new UserViewModel(name, email.ToLower(), emailConfirmed.ToLower(), password);

            return user;
        }
    }
}
