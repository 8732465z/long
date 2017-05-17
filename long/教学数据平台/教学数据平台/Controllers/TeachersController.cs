using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 教学数据平台.Controllers
{
    public class TeachersController : Controller
    {
        //
        // GET: /Teachers/
        Models.TeachDBEntities2 tdb = new Models.TeachDBEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
      
        //public ActionResult GetTeacher()
        //{
        //    var teacher = from t in tdb.Teachers
        //                  join d in tdb.Departments on t.DeptID equals d.ID
        //                  select new
        //                   {
        //                       ID = t.ID,
        //                       departmentName = d.Name,
        //                       Name = t.Name,
        //                       TeacherID = t.TeacherID,
        //                       IsLogin = t.IsLogin,
        //                   };
        //      return Json(teacher);
        //}
        public ActionResult ResetPass(int id)
        {
            try
            {
                var teacher = tdb.Teachers.Single(t => t.ID == id);
                teacher.Password = "123";
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult GetTeacher(int page, int rows)
        {
            int nums = tdb.Teachers.Count();//总记录数量
            var teacher = (from t in tdb.Teachers
                           join d in tdb.Departments on t.DeptID equals d.ID
                           orderby t.ID
                           select new
                            {
                                ID = t.ID,
                                departmentName = d.Name,
                                Name = t.Name,
                                TeacherID = t.TeacherNo,
                                IsLogin = t.IsLogin,
                            }
                           ).Skip((page - 1) * rows).Take(rows)
            ;
            var obj = new { total = nums, rows = teacher };
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DelectTeacher(int id)
        {
            try
            {
                var dep = tdb.Teachers.First(t => t.ID == id);
                tdb.Teachers.Remove(dep);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("ok");
            }
        }

        public ActionResult AddTeacher(Models.Teachers teacher)
        {
            try
            {
                teacher.Password = "123";
                tdb.Teachers.Add(teacher);
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
