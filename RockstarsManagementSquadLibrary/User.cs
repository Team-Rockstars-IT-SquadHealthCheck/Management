using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class User
    {
        // properties
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int? SquadId { get; set; }
        public string Url { get; set; }
        public List<Answer> Answers { get; private set; }

        // constructors
        public User() { }

        public User(string username) 
        { 
            Username = username;
            Answers = new List<Answer>();
        }
        public User(int id, string username, string email, int roleId, int? squadId, string url)
        {
            Id = id;
            Username = username;
            Email = email;
            RoleId = roleId;
            SquadId = squadId;
            Url = url;
            Answers = new List<Answer>();
        }

        // methods
        public bool TryGetAllAnswers()
        {
            bool result = false;
            return result;
        }
    }
}
