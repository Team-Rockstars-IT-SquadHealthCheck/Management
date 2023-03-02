namespace RockstarsManagementSquad.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }

        public User(string name, string email, string emailConfirmed, string password, string passwordConfirmed) 
        { 
            Name = name;
            Email = email;
            EmailConfirmed = emailConfirmed;
            Password = password;
            PasswordConfirmed = passwordConfirmed;
        }
    }
}
