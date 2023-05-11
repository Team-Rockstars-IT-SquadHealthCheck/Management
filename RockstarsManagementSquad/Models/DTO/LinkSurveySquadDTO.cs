namespace RockstarsManagementSquad.Models.DTO
{
    public class LinkSurveySquadDTO
    {
        public int id { get; set; }
        public int surveyId { get; set; }
        public int squadId { get; set; }

        public LinkSurveySquadDTO(int _surveyId, int _squadId)
        {
            surveyId = _surveyId;
            squadId = _squadId;
        }
    }
}
