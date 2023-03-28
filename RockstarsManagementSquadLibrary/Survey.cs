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
        public Survey()
        {
            
        }

        public Survey(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            questions = new List<Question>();
        }

        // methods
        public string CreateNewSurveyLink(int squadId, int userId)
        {
            string personalisedSurveyLink = "";
            if (SurveyLinkMayBeCreated(squadId, userId))
            {
                Guid myuuid = Guid.NewGuid();
                string uuid = myuuid.ToString();
                personalisedSurveyLink = $"{uuid}&{Convert.ToString(squadId)}&{Convert.ToString(userId)}";
            }
            return personalisedSurveyLink;
        }
        
        private bool SurveyLinkMayBeCreated(int squadId, int userId)
        {
            bool result = false;

            if (squadId != -1 && userId != -1)
            {
                result = true;
            }

            return result;
        }
    }
}
