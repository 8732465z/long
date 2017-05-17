using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace 教学数据平台.Controllers
{
    public class MainController : Controller
    {
          Models.TeachDBEntities2 tdb = new Models.TeachDBEntities2();
        public ActionResult Index()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
        //public ActionResult Login(string username, string password)
        //{
        //    if (username == "admin" && password == "123")
        //        return Content("ok");
        //    else
        //        return Content("err");
        //}
        public ActionResult Main()
        {
            string t = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value; //取到的是加密数据  -- ticket
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(t);
           
            ViewBag.teacherName = ticket.Name;
            return View();
        }
        public ActionResult GetUsers()
        {
            //List<book> bs = new List<book>();
            //bs.Add(new book { no = 1, name = "admin" });
            //bs.Add(new book { no = 2, name = "123" });
            //return Json(bs, JsonRequestBehavior.AllowGet);
            Models.TeachDBEntities2 tdb = new Models.TeachDBEntities2();
          
            return Json(tdb.Departments, JsonRequestBehavior.AllowGet);
        }
        class book
        {
            public int no { get; set; }
            public string name { get; set; }
        }
        public ActionResult AddIndex()
        {
            return View();
        }
        public ActionResult GetDepartments()
        {
            
            Models.TeachDBEntities2 tdb = new Models.TeachDBEntities2();
            var deps = from dep in tdb.Departments select new { ID = dep.ID, Name = dep.Name, Status = dep.Status };
            return Json(tdb.Departments, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login(string username, string password, string role)
        {
            if (role == "t")
            {
                try
                {
                    var teacher=tdb.Teachers.Single(t=>t.TeacherNo==username&&t.Password==password);
                    if(teacher!=null)
                    {
                        FormsAuthenticationTicket t = new FormsAuthenticationTicket(1,teacher.Name, DateTime.Now, DateTime.Now.AddMinutes(30), false, teacher.DeptID + ";" + teacher.ID);
                        var ticket = FormsAuthentication.Encrypt(t);
                        HttpCookie c = new HttpCookie(FormsAuthentication.FormsCookieName,ticket);
                        HttpContext.Response.Cookies.Add(c);
                        return Content("ok");
                    }
                        
                    else
                        return Content("err");
                }
                catch
                {
                    return Content("err");
                }
            }
            else if (role == "s") //学生
            {
                //TODO:学生登录未完成
                return Content("err");
            }
            else
                return Content("err");
           }
        //public ActionResult Main()
        //{
        //    string t = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value; //取到的是加密数据  -- ticket
        //    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(t);
        //    ViewBag.teacherName = ticket.Name;
        //    return View();
        //}

      }
}
