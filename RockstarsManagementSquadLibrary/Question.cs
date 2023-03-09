using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class Question
    {
        // properties
        public int Id { get; private set; }
        public string Question_ { get; private set; }
        public string Description { get; private set; }

        // constructors
        public Question(int id, string question, string description)
        {
            Id = id;
            Question_ = question;
            Description = description;
        }

        // methods

    }
}