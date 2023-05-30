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
        public string _Question { get; private set; }
        public string Description { get; private set; }
        public int SurveyId { get; private set; }
        public string Desc_good { get; private set; }
        public string Desc_avg { get; private set; }
        public string Desc_bad { get; private set; }

        // constructors
        public Question(int id, string question, string description, int surveyId, string desc_good, string desc_avg, string desc_bad)
        {
            Id = id;
            _Question = question;
            Description = description;
            SurveyId = surveyId;
            Desc_good = desc_good;
            Desc_avg = desc_avg;
            Desc_bad = desc_bad;
        }
        public Question(string question, string description, int surveyId, string desc_good, string desc_avg, string desc_bad)
        {
            Id = 0;
            _Question = question;
            Description = description;
            SurveyId = surveyId;
            Desc_good = desc_good;
            Desc_avg = desc_avg;
            Desc_bad = desc_bad;
        }
    }
}
