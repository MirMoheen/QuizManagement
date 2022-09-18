using QuizManagement.Customclasses;
using QuizManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizManagement.Controllers
{
    public class AllQuizController : ApiController
    {
        DbEntities Db = new DbEntities();
        [HttpPost]
        public HttpResponseMessage AddNewQuiz([FromBody] Quiz quiz)
        {
            Db.Quizs.Add(quiz);
            Db.SaveChanges();
            var data = Db.Quizs.ToList().OrderByDescending(x=>x.Qid).Select(z=>z.Qid);
            return Request.CreateResponse(HttpStatusCode.OK, data.Take(1));
        }
        [HttpPost]
        public HttpResponseMessage AddNewQuestions([FromBody] QuestionsModel questionsModel)
        {
            List<string> optlist = new List<string>();
            optlist.Add(questionsModel.Option1);
            optlist.Add(questionsModel.Option2);
            optlist.Add(questionsModel.Option3);
            optlist.Add(questionsModel.Option4);
            string lastCharacter = questionsModel.CorrectAnswer.Substring(questionsModel.CorrectAnswer.Length - 1);
            int answerindex = int.Parse(lastCharacter);
            string answer = optlist[answerindex-1];
            Question question = new Question();
            question.QuestionDescription = questionsModel.Questiontitle;
            question.Quizid = questionsModel.Quizid;
            Db.Questions.Add(question);
            Db.SaveChanges();
            var qid = Db.Questions.ToList().OrderByDescending(x => x.Questid).Select(z => z.Questid).Take(1);
            AddQuestionOPtion(questionsModel.Option1, qid.First());
            AddQuestionOPtion(questionsModel.Option2, qid.First());
            AddQuestionOPtion(questionsModel.Option3, qid.First());
            AddQuestionOPtion(questionsModel.Option4, qid.First());
            int qsid = qid.First();
            var data = Db.QuestionOptions.Where(x => x.QuestionOPtion1 == answer && x.Qsid == qsid).FirstOrDefault();
            correctAnswer correctAnswer = new correctAnswer();
            correctAnswer.Qoid = data.Qoid;
            correctAnswer.Qsid = qid.First();
            Db.correctAnswers.Add(correctAnswer);
            Db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "QuizCreated");
        }
        void AddQuestionOPtion(string opt,int qid)
        {
            QuestionOption questionOption = new QuestionOption();
            questionOption.QuestionOPtion1 = opt;
            questionOption.Qsid = qid;
            Db.QuestionOptions.Add(questionOption);
            Db.SaveChanges();
        }
        [HttpGet]
        public HttpResponseMessage GetAllQuizSectionwise(int ID)
        {
            var user = Db.Students.Where(x => x.uid == ID).FirstOrDefault();
            List<AllQuizModel> quizModels = new List<AllQuizModel>();
            var Quizdetals = Db.Quizs.Where(x => x.Section == user.Section).Select(x=>new {x.Quizdate,x.QuizDuration,x.Qid,x.Section,x.Startingtime,x.CourseName,x.Teacher.ALLUser.Username,IsCanAttemp=false }).ToList();
            if (Quizdetals.Count > 0)
            {
                for (int i = 0; i < Quizdetals.Count; i++)
                {
                    AllQuizModel obj = new AllQuizModel();
                    obj.Quizdate = Quizdetals[i].Quizdate;
                    obj.Section = Quizdetals[i].Section;
                    obj.Startingtime = Quizdetals[i].Startingtime;
                    obj.QuizDuration = Quizdetals[i].QuizDuration;
                    obj.Username = Quizdetals[i].Username;
                    obj.Qid = Quizdetals[i].Qid;
                    obj.Quizdate = Quizdetals[i].Quizdate;
                    if (Quizdetals[i].Quizdate >= DateTime.Now && Quizdetals[i].Startingtime >= new TimeSpan())
                    {
                        obj.IsCanAttempt = true;
                    }
                    else
                    {
                        obj.IsCanAttempt = false;
                    }
                   
                    quizModels.Add(obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK, quizModels);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Nodata");
        }
        [HttpPost]
        public HttpResponseMessage StudentVerfication([FromBody] ImageVerficationModel imageVerficationModel)
        {
            Studentverfication sf = new Studentverfication();
            
            byte[] imgBytes = Convert.FromBase64String(imageVerficationModel.Imagebase);
            string filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            using (var imageFile = new FileStream(@"C:/Users/mirmo/source/repos/QuizManagement/QuizManagement/UploadImages\" + filename + ".png", FileMode.Create))
            {
                imageFile.Write(imgBytes, 0, imgBytes.Length);
                imageFile.Flush();
            }
            sf.sid = imageVerficationModel.Sid;
            sf.Picturename = filename;
            Db.Studentverfications.Add(sf);
            Db.SaveChanges();
            filename = filename + ".png";
            return Request.CreateResponse(HttpStatusCode.OK, filename);
        }
        [HttpGet]
        public HttpResponseMessage getQuizstudent(string Quizid,string uid)
        {
            int qid = int.Parse(Quizid);
            int ud = int.Parse(uid);
            var data = Db.getstudentQuizquestion().Where(x=>x.Quizid==qid &&x.Uid==ud).FirstOrDefault();
            StudentQuizModel obj = new StudentQuizModel();
            if (data!=null)
            {
                var Options = Db.QuestionOptions.Where(x => x.Qsid == data.Qsid).ToList();
               
                obj.Questiondesr = data.QuestionDescription;
                obj.Option1 = Options[0].QuestionOPtion1;
                obj.Option2 = Options[1].QuestionOPtion1;
                obj.Option3 = Options[2].QuestionOPtion1;
                obj.Option4 = Options[3].QuestionOPtion1;
                obj.Qsid = (int)data.Qsid;
           

            }
                //var data = Db.Questions.ToList().Except(Db.AttemptedQuestions.ToList());
            //var data = Db.Questions.ToList().Join(Db.AttemptedQuestions.ToList(), P => P.Questid, M => M.Qsid, (P, M) => new
            //{
            //   P.QuestionDescription,
            //   P.Questid
            //}).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, obj);
        }
        [HttpGet]
        public HttpResponseMessage ConfimPin(int uid, int qid, string pinnumber)
        {
            PinGenrated pinGenrated = new PinGenrated();
            pinGenrated.RandNum = pinnumber;
            pinGenrated.Qid = qid;
            pinGenrated.Userid = uid;
            Db.PinGenrateds.Add(pinGenrated);
            Db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Succes");
        }
        [HttpGet]
        public HttpResponseMessage ValidatePin(string pin)
        {
            var dta = Db.PinGenrateds.Where(x => x.RandNum == pin).FirstOrDefault();
            if (dta != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Succes");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "notfound");
        }
    }
}
