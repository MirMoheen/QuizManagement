using QuizManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizManagement.Controllers
{
    public class StudentSideController : ApiController
    {
        DbEntities Db = new DbEntities();
        [HttpPost]
        public HttpResponseMessage UpdateStudentProfile([FromBody] Student student)
        {
            var data = Db.Students.Where(x => x.AridNO == student.AridNO).FirstOrDefault();
            if(data!=null)
            {
                student.sid = data.sid;
            }
            Db.Students.AddOrUpdate(student);
            Db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}
