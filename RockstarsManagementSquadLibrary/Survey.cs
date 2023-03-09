using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class Survey
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<Question> questions { get; private set; }

        // constructors
        public Survey(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            questions = new List<Question>();
        }

        // methods

    }
}
