using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizManagement.Customclasses
{
    public class StudentQuizModel
    {
        public string Questiondesr { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int Qsid { get; set; }
        public int Uid { get; set; }
    }
}