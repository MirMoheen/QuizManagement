using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizManagement.Customclasses
{
    public class QuestionsModel
    {
        public string Questiontitle { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectAnswer { get; set; }
        public int Quizid { get; set; }
    }
}