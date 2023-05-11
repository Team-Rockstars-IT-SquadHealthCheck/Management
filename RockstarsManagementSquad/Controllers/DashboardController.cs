using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquadLibrary;
using System.Diagnostics;
using RockstarsManagementSquad.Services.Interfaces;
using System.ComponentModel;

namespace RockstarsManagementSquad.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Services.Interfaces.IAnswerViewModelService _answerService;
        private readonly Services.Interfaces.ISquadViewModelService _squadService;
        private readonly Services.Interfaces.IRockstarViewModelService _rockstarsService;

        public DashboardController(RockstarsManagementSquad.Services.Interfaces.IAnswerViewModelService answerService, 
            RockstarsManagementSquad.Services.Interfaces.ISquadViewModelService squadService,
            ILogger<DashboardController> logger)
        {
            _answerService = answerService ?? throw new ArgumentNullException(nameof(answerService));
            _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
			//_rockstarsService = rockstarsService ?? throw new ArgumentNullException(nameof(rockstarsService));
			//_logger = logger;
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

        public async Task<List<int>> GetAllExistingSurveyNumbers()
        {
            var users = await _rockstarsService.Find();

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
