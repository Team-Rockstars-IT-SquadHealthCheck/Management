﻿using System.ComponentModel.DataAnnotations;

namespace RockstarsManagementSquad.Models
{
    public class AnswerViewModel
    {
        public int id { get; set; } 
        public string question { get; set; }
        public int answer { get; set; }
        public string answerText { get; set; }
        public string comment { get; set; }
        public int userid { get; set; }
    }
}
