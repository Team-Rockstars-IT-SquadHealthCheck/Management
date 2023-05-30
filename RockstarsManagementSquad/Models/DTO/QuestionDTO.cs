namespace RockstarsManagementSquad.Models.DTO
{
    public class QuestionDTO
    {
        public string question { get; private set; }
        public string description { get; private set; }
        public int surveyid { get; private set; }
        public string desc_good { get; private set; }
        public string desc_avg { get; private set; }
        public string desc_bad { get; private set; }

        // constructors
        public QuestionDTO(string _question, string _description, int _surveyId, string _desc_good, string _desc_avg, string _desc_bad)
        {
            question = _question;
            description = _description;
            surveyid = _surveyId;
            desc_good = _desc_good;
            desc_avg = _desc_avg;
            desc_bad = _desc_bad;
        }
    }
}
