using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using System.Diagnostics;

namespace RockstarsManagementSquad.Controllers
{
    public class DashboardController : Controller
    {
        
        private readonly ILogger<DashboardController> _logger;
        private readonly IAnswerViewModelService _answerViewModelService;
        private readonly IUserViewModelService _userViewModelService;
        private readonly ISquadViewModelService _squadViewModelService;
        private readonly IRockstarViewModelService _rockstarViewModelService;
        public DashboardController(ILogger<DashboardController> logger, IAnswerViewModelService answerViewModelService, IUserViewModelService userViewModelService, ISquadViewModelService squadViewModelService, IRockstarViewModelService rockstarViewModelService)
        {
            _logger = logger;
            _answerViewModelService = answerViewModelService ?? throw new ArgumentNullException(nameof(answerViewModelService));
            _userViewModelService = userViewModelService ?? throw new ArgumentNullException(nameof(userViewModelService));
            _squadViewModelService = squadViewModelService ?? throw new ArgumentNullException(nameof(squadViewModelService));
            _rockstarViewModelService = rockstarViewModelService ?? throw new ArgumentNullException(nameof(rockstarViewModelService));
        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var allSquads = await _squadViewModelService.FindAll();

            DashboardViewModel dashboardViewModel = new DashboardViewModel();

            List<SquadViewModel> NotFinnishedEnquetes = new List<SquadViewModel>();
            List<SquadViewModel> FinnishedEnquetes = new List<SquadViewModel>();

            foreach (var squad in allSquads)
            {
                var answerInSquad = await _answerViewModelService.GetSquadAnswers(squad.id);
                var usersInSquad = await _squadViewModelService.UsersInSquad(squad.id);
                bool squadHasEnquete = false;

                foreach (var user in usersInSquad)
                {
                    if (user.url != null)
                    {
                        squadHasEnquete = true;
                    }
                }

                if (squadHasEnquete && answerInSquad.Count() == usersInSquad.Count())
                {
                    FinnishedEnquetes.Add(squad);
                }
                else if (squadHasEnquete && answerInSquad.Count() != usersInSquad.Count())
                {
                    NotFinnishedEnquetes.Add(squad);
                }
            }

            dashboardViewModel.SquadFinnishedEnquetes = FinnishedEnquetes;
            dashboardViewModel.SquadNotFinnishedEnquetes = NotFinnishedEnquetes;

            return View(dashboardViewModel);
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
