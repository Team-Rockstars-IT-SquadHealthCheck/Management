using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RockstarsManagementSquad.Configuration;
using RockstarsManagementSquad.Helpers;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using PostmarkDotNet;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Linq;

namespace RockstarsManagementSquad.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ISurveyViewModelService _surveyService;
        private readonly IUserViewModelService _userService;
        private readonly ISquadViewModelService _squadService;
        private readonly IMailService _mailService;

        public SurveysController(ISurveyViewModelService surveyService, IUserViewModelService userService, ISquadViewModelService squadService, IMailService mailService)
        {
            _surveyService = surveyService ?? throw new ArgumentNullException(nameof(surveyService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }
        
        public async Task<IActionResult> Index()
        {
            var squads = await _squadService.FindAll();
            var products = await _surveyService.Find();

            List<SelectListItem> items = squads.Select(c => new SelectListItem
            {
                Text = c.name.ToString(),
                Value = c.id.ToString()
            }).ToList();

            ViewBag.Squads = items;

            List<SurveyViewModel> surveyViewModels = new List<SurveyViewModel>();

            foreach (var product in products)
            {
                SurveyViewModel surveyViewModel = product.ConvertSurveyDTOToSurveyViewModel();
                surveyViewModels.Add(surveyViewModel);
            }

            return View(surveyViewModels);
        }

        public async Task<IActionResult> CreateSurveyLink(string title, string description, int squadId)
        {
            Survey survey = new Survey(title, description);
            survey = CreateAndGetNewSurvey(survey, squadId);

            IEnumerable<RockstarViewModel> UsersInSquad = await _squadService.UsersInSquad(squadId);
            List<RockstarViewModel> usersList = UsersInSquad.ToList();

            foreach (var user in usersList)
            {
                int surveyId = survey.Id;
                int userId = user.id;
                string surveyLink = survey.CreateNewSurveyLink(surveyId, squadId, userId);
                
                _userService.AddSurveyLinkToUser(surveyLink, userId);

                await SendMail(
                    new MailData() 
                    { 
                        EmailToAddress = user.email,
                        EmailToName = user.username,
                        EmailSubject = "Squad Health Check",
                        EmailBody = $"This is your personal surveylink: http://138.201.52.251:82/Home/Index/{surveyLink}"
                    }
                    );
            }
            return RedirectToAction("Index");
        }

        private Survey CreateAndGetNewSurvey(Survey survey, int squadId)
        {
            _surveyService.Create(survey.ConvertSurveyToSurveyDTO());

            Task<IEnumerable<SurveyDTO>> _surveys = _surveyService.Find();
            List<SurveyDTO> surveys = _surveys.Result.ToList();

            for (int i = surveys.Count()-1; i < surveys.Count(); i++)
            {
                survey = surveys[i].ConvertSurveyDTOToSurvey();
            }

            _surveyService.CreateLinkSurveySquad(new LinkSurveySquadDTO(survey.Id, squadId));

            return survey;
        }

        [HttpPost]
        [Route("SendMail")]
        public async Task<bool> SendMail(MailData mailData)
        {
            return await _mailService.SendMail(mailData);
        }
    }
}
