using System.ComponentModel.DataAnnotations;

namespace RockstarsManagementSquad.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

		public UserViewModel() { }
		

		public UserViewModel( string username, string email, string emailConfirmed, string password)
		{
			
			this.Username = username;
			this.Email = email;
			this.EmailConfirmed = emailConfirmed;
			this.Password = password;
			
		}
	}
}
