using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<Question> questions { get; private set; }

        // constructors
        public Survey()
        {
            questions = new List<Question>();
        }

        public Survey(string name, string description)
        {
            Id = 0;
            Name = name;
            Description = description;
            questions = new List<Question>();
        }

        public Survey(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            questions = new List<Question>();
        }

        // methods
        public List<Question> GetDefaultQuestions()
        {
            questions.Add(new Question("Vraag 1: Toegevoegde waarde", "Heeft het team waardevolle resultaten opgeleverd?", 0, "Het team heeft aanzienlijke waarde opgeleverd", "Het team heeft enige waarde opgeleverd, maar er is nog ruimte voor verbetering", "Het team heeft weinig tot geen waarde opgeleverd"));
            questions.Add(new Question("Vraag 2: Release gemak", "Kan het team de applicatie gemakkelijk vrijgeven voor gebruik?", 0, "Het team kan de applicatie gemakkelijk vrijgeven zonder enige problemen", "Het team heeft enige waarde opgeleverd, Het team kan de applicatie vrijgeven, maar er zijn nog enkele zaken die verbeterd moeten worden", "Het team kan de applicatie niet gemakkelijk vrijgeven vanwege grote technische problemen"));
            questions.Add(new Question("Vraag 3: Werkplezier", "Vindt het team het leuk om aan dit project te werken?", 0, "Het team vindt het project erg leuk en is gemotiveerd om eraan te werken", "Het team vindt het project enigszins leuk, maar kan wel wat extra motivatie gebruiken", "Het team vindt het project niet leuk en heeft weinig motivatie om eraan te werken"));
            questions.Add(new Question("Vraag 4: Codekwaliteit", "Is de code van goede kwaliteit en overzichtelijk geschreven?", 0, "Ja, de code is van goede kwaliteit en overzichtelijk geschreven", "De code kan beter, er zijn enkele punten voor verbetering", "Nee, de code is van slechte kwaliteit en chaotisch geschreven"));
            questions.Add(new Question("Vraag 5: Leeropbrengst", "Doet het team nieuwe kennis op door aan dit project te werken?", 0, "Ja, het team verwerft nieuwe kennis door aan dit project te werken", "Het team doet enige nieuwe kennis op, maar het is beperkt", "Nee, het team leert geen nieuwe kennis door aan dit project te werken"));
            questions.Add(new Question("Vraag 6: Missie en doelen", " Heeft het team duidelijke doelen en een visie die alle teamleden begrijpen en ondersteunen?", 0, "Ja, het team heeft duidelijke doelen en een visie die alle teamleden begrijpen en ondersteunen", "De doelen en visie van het team zijn enigszins onduidelijk en sommige teamleden begrijpen ze niet volledig", "Nee, het team heeft geen duidelijke doelen en visie en sommige teamleden begrijpen ze niet of ondersteunen ze niet"));
            questions.Add(new Question("Vraag 7: Eigen inbreng", "Krijgt het team voldoende ruimte om eigen inbreng te hebben in het project?", 0, "Ja, het team krijgt voldoende ruimte om eigen inbreng te hebben in het project", "Het team heeft enige ruimte voor eigen inbreng, maar dit kan beter", "Nee, het team heeft geen duidelijke doelen en visie en sommige teamleden begrijpen ze niet of ondersteunen ze niet"));
            questions.Add(new Question("Vraag 8: Snelheid van werken", "Werkt het team efficiënt en snel aan het project?", 0, "Ja, het team werkt efficiënt en snel aan het project", "Het team werkt redelijk efficiënt en snel aan het project, maar er zijn enkele verbeterpunten", "Nee, het team werkt niet efficiënt en/of snel aan het project"));

            return questions;
        }

        public string CreateNewSurveyLink(int surveyId, int squadId, int userId)
        {
            string personalisedSurveyLink = "";
            if (SurveyLinkMayBeCreated(surveyId, squadId, userId))
            {
                Guid myuuid = Guid.NewGuid();
                string uuid = myuuid.ToString();
                personalisedSurveyLink = $"{surveyId}&{uuid}&{Convert.ToString(squadId)}&{Convert.ToString(userId)}";
            }
            return personalisedSurveyLink;
        }
        
        private bool SurveyLinkMayBeCreated(int surveyId, int squadId, int userId)
        {
            bool result = false;

            if (surveyId != -1 && squadId != -1 && userId != -1)
            {
                result = true;
            }

            return result;
        }
    }
}
