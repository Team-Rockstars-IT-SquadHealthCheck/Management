﻿using System.ComponentModel.DataAnnotations;

namespace RockstarsManagementSquad.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }

        public UserViewModel(string name, string email, string emailConfirmed, string password, string passwordConfirmed) 
        { 
            Name = name;
            Email = email;
            EmailConfirmed = emailConfirmed;
            Password = password;
            PasswordConfirmed = passwordConfirmed;
        }
    }
}
