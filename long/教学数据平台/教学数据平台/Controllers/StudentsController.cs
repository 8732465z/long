using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 教学数据平台.Controllers
{
    public class StudentsController : Controller
    {
        //
        // GET: /Students/
        Models.TeachDBEntities2 tdb = new Models.TeachDBEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add()
        {
            return View();
        }
        public ActionResult editstudent(int id)
        {
            var student = tdb.Students.Single(t => t.ID == id);

            return View(student);
           
        }
        public ActionResult ResetPass(教学数据平台.Models.Students student)
        {
            try
            {
                var d = tdb.Students.Single(t => t.ID==student.ID);
                d.Name = student.Name;
                d.IsLogin = student.IsLogin;
                d.PTelNo1 = student.PTelNo1;
                d.PTelNo2 = student.PTelNo2;
                d.QQ = student.QQ;
                d.Stauts = student.Stauts;
                d.StudentNo = student.StudentNo;
                d.TelNum = student.TelNum;
                d.WeChat = student.WeChat;
                student.Password = "123";
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult Getstudents(int page, int rows)
        {

            int nums = tdb.Students.Count();//总记录数量
            var student = (from t in tdb.Students
                           join d in tdb.Classes on t.ID equals d.ID
                           orderby t.ID
                           select new
                           {
                               ID = t.ID,
                               Classname=d.Name,
                               
                               Name = t.Name,
                               Studentno = t.StudentNo,
                               Password = t.Password,
                               Telnum = t.TelNum,
                               QQ = t.QQ,
                               Wechat = t.WeChat,
                               Ptelno1 = t.PTelNo1,
                               Ptelno2 = t.PTelNo2,
                               Stauts = t.Stauts,
                               Islogin = t.IsLogin
                           }
                           ).Skip((page - 1) * rows).Take(rows)
            ;
            var obj = new { total = nums, rows = student };
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delectstudents(int id)
        {
            try
            {
                var dep = tdb.Students.First(t => t.ID == id);
                tdb.Students.Remove(dep);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("ok");
            }
        }

        public ActionResult addstudent(Models.Students student)
        {
            try
            {
                student.Password = "123";
                tdb.Students.Add(student);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
         
    }
}
