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
        private readonly Services.Interfaces.IAnswerViewModelService _answerService;
        private readonly Services.Interfaces.ISquadViewModelService _squadService;

        public DashboardController(RockstarsManagementSquad.Services.Interfaces.IAnswerViewModelService answerService, 
            RockstarsManagementSquad.Services.Interfaces.ISquadViewModelService squadService,
            ILogger<DashboardController> logger)
        {
            _answerService = answerService ?? throw new ArgumentNullException(nameof(answerService));
            _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
            _logger = logger;
        }

        //[Authorize]
        public IActionResult Index()
        {
            GetAllAnswers();
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

        public List<AnswerViewModel> GetAllAnswers()
        {

            return new List<AnswerViewModel>();
        }
    }
}
