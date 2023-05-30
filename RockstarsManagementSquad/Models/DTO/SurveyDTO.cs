namespace RockstarsManagementSquad.Models.DTO
{
    public class SurveyDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public SurveyDTO()
        {
            
        }

        public SurveyDTO(int _id, string _name, string _description)
        {
            id = _id;
            name = _name;
            description = _description;
        }
    }
}
