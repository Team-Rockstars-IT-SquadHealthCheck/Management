namespace RockstarsManagementSquad.Models.DTO
{
    public class SquadDTO
    {
        public int id { get; set; }
        public int surveyId { get; set; }
        public int companyId { get; set; }

        public SquadDTO(int _id, int _survey, int _companyId)
        {
            id = _id;
            surveyId = _survey;
            companyId = _companyId;
        }
    }
}
