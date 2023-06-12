namespace RockstarsManagementSquad.Models
{
    public class QuestionViewModel
    {
        public string question { get; set; }
        public string description { get; set; }
        public int surveyid { get; set; }
        public string desc_good { get; set; }
        public string desc_avg { get; set; }
        public string desc_bad { get; set; }
        public QuestionViewModel(string _question, string _description, int _surveyid, string _desc_good, string _desc_avg, string _desc_bad)
        {
            question = _question;
            description = _description;
            surveyid = _surveyid;
            desc_good = _desc_good;
            desc_avg = _desc_avg;
            desc_bad = _desc_bad;
        }
    }
}
