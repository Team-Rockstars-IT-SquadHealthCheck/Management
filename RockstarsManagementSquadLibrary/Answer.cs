using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class Answer
    {
        // properties
        public int Id { get; private set; }
        public Question Question { get; private set; }
        public string Answer_ { get; private set; }
        public string Comment { get; private set; }

        // constructors
        public Answer(int id, Question question, string answer, string comment)
        {
            Id = id;
            Question = question;
            Answer_ = answer;
            Comment = comment;
        }

        // methods

    }
}
