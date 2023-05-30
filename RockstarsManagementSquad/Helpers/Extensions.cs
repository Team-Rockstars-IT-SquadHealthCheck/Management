using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Helpers
{
    public static class Extensions
    {
        public static User ConvertRockstarViewModelToUser(this RockstarViewModel rvm)
        {
            return new User(rvm.id, rvm.username, rvm.email, rvm.roleid, rvm.squadid, rvm.url);
        }
        public static SurveyDTO ConvertSurveyToSurveyDTO(this Survey survey)
        {
            return new SurveyDTO(survey.Id, survey.Name, survey.Description);
        }
        public static Survey ConvertSurveyDTOToSurvey(this SurveyDTO sDTO)
        {
            return new Survey(sDTO.id, sDTO.name, sDTO.description);
        }
        public static SurveyViewModel ConvertSurveyDTOToSurveyViewModel(this SurveyDTO sDTO)
        {
            return new SurveyViewModel(sDTO.id, sDTO.name, sDTO.description);
        }
        public static QuestionDTO ConvertQuestionToQuestionDTO(this Question question)
        {
            return new QuestionDTO(question._Question, question.Description, question.SurveyId, question.Desc_good, question.Desc_avg, question.Desc_bad);
        }
    }
}
