using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public List<Answer> Answers { get; private set;}
    }
}
