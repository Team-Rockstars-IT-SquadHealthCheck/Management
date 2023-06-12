namespace RockstarsManagementSquad.Models
{
    public class SurveysViewModel
    {
        public IEnumerable<SurveyViewModel> SurveyViewModels = new List<SurveyViewModel>();
        public SurveyViewModel CreateSurveyViewModel = new SurveyViewModel(0, "", "");
    }
}
