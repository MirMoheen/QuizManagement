using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizManagement.Customclasses
{
    public class AllQuizModel
    {
        public int Qid { get; set; }
        public string QTitle { get; set; }
        public Nullable<System.DateTime> Quizdate { get; set; }
        public Nullable<int> Tid { get; set; }
        public Nullable<System.TimeSpan> Startingtime { get; set; }
        public Nullable<int> QuizDuration { get; set; }
        public string CourseName { get; set; }
        public string Section { get; set; }
        public string Username { get; set; }
        public bool IsCanAttempt { get; set; }
    }
}