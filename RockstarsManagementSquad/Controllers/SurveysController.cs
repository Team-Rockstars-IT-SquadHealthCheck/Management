using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ISurveyViewModelService _surveyService;
        private readonly IUserViewModelService _userService;
        private readonly ISquadViewModelService _squadService;

        public SurveysController(ISurveyViewModelService surveyService, IUserViewModelService userService, ISquadViewModelService squadService)
        {
            _surveyService = surveyService ?? throw new ArgumentNullException(nameof(surveyService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
        }

        public async Task<IActionResult> Index()
        {
            var products = await _surveyService.Find();
            foreach (var product in products)
            {
                SurveyViewModel surveyViewModel = new SurveyViewModel();
            }
            return View(products);
        }

        public async Task<IActionResult> CreateSurveyLink(int squadId)
        {
            IEnumerable<UserViewModel> UsersInSquad = await _squadService.UsersInSquad(squadId);
            List<UserViewModel> usersList = UsersInSquad.ToList();

            Survey survey = new Survey();
            foreach (var user in usersList)
            {
                int userId = user.Id;
                string surveyLink = survey.CreateNewSurveyLink(squadId, userId);
                _userService.AddSurveyLinkToUser(surveyLink, userId);
            }
            return RedirectToAction("Index");
        }
    }
}
