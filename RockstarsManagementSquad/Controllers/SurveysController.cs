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

            List<SelectListItem> surveys = products.Select(c => new SelectListItem
            {
                Text = c.name.ToString(),
                Value = c.id.ToString()
            }).ToList();

            ViewBag.Surveys = surveys;

            return View(surveyViewModels);
        }

        public async Task<IActionResult> CreateSurvey(string title, string description)
        {
            Survey survey = new Survey(title, description);
            CreateNewSurvey(survey);

            return RedirectToAction("Index");
        }

        private Survey CreateNewSurvey(Survey survey)
        {
            _surveyService.Create(survey.ConvertSurveyToSurveyDTO());

            List<SurveyDTO> surveyDTOs = _surveyService.Find().Result.ToList();
            
            foreach (var surveyDTO in surveyDTOs)
            {
                survey.Id = surveyDTO.id;
            }

            CreateQuestionsToSurvey(survey.Id);

            return survey;
        }

        private void CreateQuestionsToSurvey(int surveyId)
        {
            List<Question> questions = new List<Question>();
            questions.Add(new Question("Vraag 1: Toegevoegde waarde", "Heeft het team waardevolle resultaten opgeleverd?", surveyId, "Het team heeft aanzienlijke waarde opgeleverd", "Het team heeft enige waarde opgeleverd, maar er is nog ruimte voor verbetering", "Het team heeft weinig tot geen waarde opgeleverd"));
            questions.Add(new Question("Vraag 2: Release gemak", "Kan het team de applicatie gemakkelijk vrijgeven voor gebruik?", surveyId, "Het team kan de applicatie gemakkelijk vrijgeven zonder enige problemen", "Het team heeft enige waarde opgeleverd, Het team kan de applicatie vrijgeven, maar er zijn nog enkele zaken die verbeterd moeten worden", "Het team kan de applicatie niet gemakkelijk vrijgeven vanwege grote technische problemen"));
            questions.Add(new Question("Vraag 3: Werkplezier", "Vindt het team het leuk om aan dit project te werken?", surveyId, "Het team vindt het project erg leuk en is gemotiveerd om eraan te werken", "Het team vindt het project enigszins leuk, maar kan wel wat extra motivatie gebruiken", "Het team vindt het project niet leuk en heeft weinig motivatie om eraan te werken"));
            questions.Add(new Question("Vraag 4: Codekwaliteit", "Is de code van goede kwaliteit en overzichtelijk geschreven?", surveyId, "Ja, de code is van goede kwaliteit en overzichtelijk geschreven", "De code kan beter, er zijn enkele punten voor verbetering", "Nee, de code is van slechte kwaliteit en chaotisch geschreven"));
            questions.Add(new Question("Vraag 5: Leeropbrengst", "Doet het team nieuwe kennis op door aan dit project te werken?", surveyId, "Ja, het team verwerft nieuwe kennis door aan dit project te werken", "Het team doet enige nieuwe kennis op, maar het is beperkt", "Nee, het team leert geen nieuwe kennis door aan dit project te werken"));
            questions.Add(new Question("Vraag 6: Missie en doelen", " Heeft het team duidelijke doelen en een visie die alle teamleden begrijpen en ondersteunen?", surveyId, "Ja, het team heeft duidelijke doelen en een visie die alle teamleden begrijpen en ondersteunen", "De doelen en visie van het team zijn enigszins onduidelijk en sommige teamleden begrijpen ze niet volledig", "Nee, het team heeft geen duidelijke doelen en visie en sommige teamleden begrijpen ze niet of ondersteunen ze niet"));
            questions.Add(new Question("Vraag 7: Eigen inbreng", "Krijgt het team voldoende ruimte om eigen inbreng te hebben in het project?", surveyId, "Ja, het team krijgt voldoende ruimte om eigen inbreng te hebben in het project", "Het team heeft enige ruimte voor eigen inbreng, maar dit kan beter", "Nee, het team heeft geen duidelijke doelen en visie en sommige teamleden begrijpen ze niet of ondersteunen ze niet"));
            questions.Add(new Question("Vraag 8: Snelheid van werken", "Werkt het team efficiënt en snel aan het project?", surveyId, "Ja, het team werkt efficiënt en snel aan het project", "Het team werkt redelijk efficiënt en snel aan het project, maar er zijn enkele verbeterpunten", "Nee, het team werkt niet efficiënt en/of snel aan het project"));
            foreach (var question in questions)
            {
                _surveyService.CreateQuestion(question.ConvertQuestionToQuestionDTO());
            }
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

        [HttpPost]
        [Route("SendMail")]
        public async Task<bool> SendMail(MailData mailData)
        {
            return await _mailService.SendMail(mailData);
        }
    }
}
