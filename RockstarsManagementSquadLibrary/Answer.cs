using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class Answer
    {
        public int Id { get; private set; }
        public Question Question { get; private set; }
        public string Answer_ { get; private set; }
        public string Comment { get; private set; }
    }
}