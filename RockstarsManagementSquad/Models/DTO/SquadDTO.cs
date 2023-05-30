namespace RockstarsManagementSquad.Models.DTO
{
    public class SquadDTO
    {
        public int id { get; set; }
        public int surveyId { get; set; }
        public int companyId { get; set; }
        public string name { get; set; }

        public SquadDTO(int _id, int _survey, int _companyId, string _name)
        {
            id = _id;
            surveyId = _survey;
            companyId = _companyId;
            name = _name;
        }
    }
}
