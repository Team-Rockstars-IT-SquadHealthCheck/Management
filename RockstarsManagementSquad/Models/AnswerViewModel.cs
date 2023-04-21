using System.ComponentModel.DataAnnotations;

namespace RockstarsManagementSquad.Models
{
    public class AnswerViewModel
    {
        public int id { get; set; }
        public int answer { get; set; }
        public string comment { get; set; }
        public int userid { get; set; }
        public int questionid { get; set; }
    }
}
