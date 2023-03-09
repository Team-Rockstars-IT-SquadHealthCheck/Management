using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class User
    {
        // properties
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string SurveyLink { get; private set; }
        public List<Answer> Answers { get; private set; }

        // constructors
        public User(int id, string username) 
        { 
            Id = id;
            Username = username;
            Answers = new List<Answer>();
        }

        // methods

    }
}
