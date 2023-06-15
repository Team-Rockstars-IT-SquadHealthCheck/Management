namespace RockstarsManagementSquad.Models
{
    public class SurveyViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<QuestionViewModel> Questions { get; set; }


        public SurveyViewModel(int _id, string _name, string _description)
        {
            id = _id;
            name = _name;
            description = _description;
            Questions = new List<QuestionViewModel>();
        }

        public SurveyViewModel()
        {
            Questions = new List<QuestionViewModel>()
            {
                new QuestionViewModel(),
                new QuestionViewModel(),
                new QuestionViewModel(),
                new QuestionViewModel(),
                new QuestionViewModel(),
                new QuestionViewModel(),
                new QuestionViewModel(),
                new QuestionViewModel()
            };
        }
    }
}
