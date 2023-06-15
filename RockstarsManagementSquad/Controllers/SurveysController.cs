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
using Org.BouncyCastle.Crypto.Generators;
using MimeKit.Encodings;
using System.Text;

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

            List<SelectListItem> surveys = products.Select(c => new SelectListItem
            {
                Text = c.name.ToString(),
                Value = c.id.ToString()
            }).ToList();

            ViewBag.Surveys = surveys;

            Survey CreateSurvey = new Survey();
            CreateSurvey.GetDefaultQuestions();
            SurveyViewModel CreateSurveyViewModel = CreateSurvey.ConvertSurveyToSurveyViewModel();
            foreach (var question in CreateSurvey.questions)
            {
                CreateSurveyViewModel.Questions.Add(question.ConvertQuestionToQuestionViewModel());
            }

            SurveysViewModel surveysViewModel = new SurveysViewModel();
            surveysViewModel.SurveyViewModels = surveyViewModels;
            surveysViewModel.CreateSurveyViewModel = CreateSurveyViewModel;

            return View(surveysViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SurveysViewModel surveysViewModel)
        {
            Survey survey = new Survey(surveysViewModel.CreateSurveyViewModel.name, surveysViewModel.CreateSurveyViewModel.description);
            foreach (var question in surveysViewModel.CreateSurveyViewModel.Questions)
            {
                survey.questions.Add(question.ConvertQuestionViewModelToQuestion());
            }
            CreateNewSurvey(survey);

            return RedirectToAction("Index");
        }

        private async Task<Survey> CreateNewSurvey(Survey survey)
        {
            List<SurveyDTO> surveyDTOs = new List<SurveyDTO>();
            if (survey.Name != "")
            {
                await _surveyService.Create(survey.ConvertSurveyToSurveyDTO());
                var products = await _surveyService.Find();
                surveyDTOs = products.ToList();
            
                for (int i = surveyDTOs.Count()-1; i < surveyDTOs.Count(); i++)
                {
                    survey.Id = surveyDTOs[i].id;
                }
            }

            if (survey.Id != 0)
            {
                CreateQuestionsToSurvey(survey.Id, survey.questions);
            }

            return survey;
        }

        private async Task<bool> CreateQuestionsToSurvey(int surveyId, List<Question> questions)
        {
            foreach (var question in questions)
            {
                question.SurveyId = surveyId;
                await _surveyService.CreateQuestion(question.ConvertQuestionToQuestionDTO());
            }
            return true;
        }

        public async Task<IActionResult> SendSurveyLink(int surveyId, int squadId)
        {
            _surveyService.CreateLinkSurveySquad(new LinkSurveySquadDTO(surveyId, squadId));

            IEnumerable<RockstarViewModel> UsersInSquad = await _squadService.UsersInSquad(squadId);
            List<RockstarViewModel> usersList = UsersInSquad.ToList();

            Survey survey = _surveyService.FindById(surveyId).Result.ConvertSurveyDTOToSurvey();

            foreach (var user in usersList)
            {
                int userId = user.id;
                string surveyLink = survey.CreateNewSurveyLink(surveyId, squadId, userId);
                
                _userService.AddSurveyLinkToUser(surveyLink, userId);

                byte[] input = Encoding.UTF8.GetBytes(surveyLink);

                surveyLink = Convert.ToBase64String(input);

                await SendMail(
                    new MailData() 
                    { 
                        EmailToAddress = user.email,
                        EmailToName = user.username,
                        EmailSubject = "Squad Health Check",
                        EmailBody = $"This is your personal surveylink: <a href=\"http://138.201.52.251:82/Home/Index/{surveyLink}\">http://138.201.52.251:82/Home/Index/{surveyLink}</ a>"
                    }
                    );
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("SendMail")]
        public async Task<bool> SendMail(MailData mailData)
        {
            return await _mailService.SendMail(mailData);
        }
    }
}
