using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using System.Diagnostics;
using System.ComponentModel;

namespace RockstarsManagementSquad.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAnswerViewModelService _answerViewModelService;
        private readonly ISquadViewModelService _squadViewModelService;
        private readonly IRockstarViewModelService _rockstarsViewModelService;
        private readonly ILogger _logger;

        public DashboardController(IAnswerViewModelService answerService, ISquadViewModelService squadService, IRockstarViewModelService rockstarViewModelService, ILogger<DashboardController> logger)
        {
            _answerViewModelService = answerService ?? throw new ArgumentNullException(nameof(answerService));
            _squadViewModelService = squadService ?? throw new ArgumentNullException(nameof(squadService));
			_rockstarsViewModelService = rockstarViewModelService ?? throw new ArgumentNullException(nameof(rockstarViewModelService));
			_logger = logger;
        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var allSquads = await _squadViewModelService.FindAll();

            DashboardViewModel dashboardViewModel = new DashboardViewModel();

            List<SquadViewModel> NotFinishedEnquetes = new List<SquadViewModel>();
            List<SquadViewModel> FinishedEnquetes = new List<SquadViewModel>();

            foreach (var squad in allSquads)
            {
                var answerInSquad = await _answerViewModelService.GetSquadFinnishedEnquetes(squad.id);
                var usersInSquad = await _squadViewModelService.UsersInSquad(squad.id);

                if (answerInSquad.Count() > 0) 
                { 
                    if (answerInSquad.Count(x => x.answerText != null && x.answerText != "") == usersInSquad.Count())
                    {
                        FinishedEnquetes.Add(squad);
                    }
                    else if (answerInSquad.Count(x => x.answerText != null && x.answerText != "") != usersInSquad.Count())
                    {
                        NotFinishedEnquetes.Add(squad);
                    }
                }
            }

            dashboardViewModel.SquadFinishedEnquetes = FinishedEnquetes;
            dashboardViewModel.SquadNotFinishedEnquetes = NotFinishedEnquetes;

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

        public async Task<List<int>> GetAllExistingSurveyNumbers()
        {
            var users = await _rockstarsViewModelService.Find();

            int surveyNumber = 0;
            List<int> surveyNumbers = new List<int>();

            foreach (var user in users)
            {
                var url = user.url.Split('&');
                for (int i = 0; i < 1; i++)
                {
                    surveyNumber = Convert.ToInt32(url[i]);
                    if (!surveyNumbers.Contains(surveyNumber))
                    {
                        surveyNumbers.Add(surveyNumber);
                    }
                }
            }
            return surveyNumbers;
        }
    }
}
