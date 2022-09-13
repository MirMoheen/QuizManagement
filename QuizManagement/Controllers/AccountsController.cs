using QuizManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizManagement.Controllers
{
    public class AccountsController : ApiController
    {
        DbEntities Db = new DbEntities();
        [HttpPost]
        public HttpResponseMessage AddnewUser([FromBody] ALLUser user)
        {
            var data = Db.ALLUsers.Where(x => x.Email == user.Email).FirstOrDefault();
            if (data == null)
            {
                Db.ALLUsers.Add(user);
                Db.SaveChanges();
                int dt;
                user.Uid = Db.ALLUsers.Where(x => x.Email == user.Email).Select(x => x.Uid).FirstOrDefault();
                if (user.UserRole == "Teacher")
                {
                    Teacher teacher = new Teacher();
                    teacher.uid = user.Uid;
                    Db.Teachers.Add(teacher);
                    Db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "AlreadyExist");
        }
        [HttpPost]
        public HttpResponseMessage vallidateUser([FromBody] ALLUser user)
        {
            var data = Db.ALLUsers.Where(x => x.Email == user.Email && x.UserPassword == user.UserPassword).Select(x=>new {x.Uid,x.Username,x.UserPassword,x.UserRole,x.Email }).FirstOrDefault();
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Not Found");
        }
    }
}
