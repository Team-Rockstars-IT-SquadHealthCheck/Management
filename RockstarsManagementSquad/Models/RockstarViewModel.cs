using System.ComponentModel.DataAnnotations;

namespace RockstarsManagementSquad.Models;

public class RockstarViewModel
{
    public int id { get; set; }
    [Required(ErrorMessage = "Username is nodig!")]
    public string username { get; set; }
    public string email { get; set; }
    public int roleid { get; set; }
    public int squadid { get; set; }
}