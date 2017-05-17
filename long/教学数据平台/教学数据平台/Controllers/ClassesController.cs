using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 教学数据平台.Controllers
{
    public class ClassesController : Controller
    {
        //
        // GET: /Classes/
        //Models.TeachDBEntities1 tdb = new Models.TeachDBEntities1();
    
        Models.TeachDBEntities2 tdb = App_Start.Helper.tdb;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetClassesList()
        {
            var classes = from a in tdb.Classes
                          select new
                          {
                              ID = a.ID,
                              Name = a.Name
                          };
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetClassListByMajorId(int majorId)
        {
            var classes = from a in tdb.Classes
                          where a.MajorID == majorId
                          select new
                          {
                              ID = a.ID,
                              Name = a.Name
                          };
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        //获取带班主任信息的班级列表
        //majorid 专业的id,如果为-1表示所有专业
        public ActionResult GetClassesListByMajorIdWithDirector(int majorId)
        {
            var classes = from c in tdb.View_TeacherClasses
                          where c.MajorID == majorId
                          select new
                          {
                              ID = c.ID,
                              Name = c.Name,
                              TeacherName = c.TeacherName,
                              TeacherNo = c.TeacherNo,
                              MajorId = c.MajorID
                          };
            if (majorId != -1)
            {
                classes = classes.Where(t => t.MajorId == majorId);
            }
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Addclass(Models.View_TeacherClasses classteacher)
        {
            try
            {
                tdb.AddClasses(classteacher.MajorID, classteacher.Name, classteacher.TeacherClassID);
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult Edit(int id)
        {
            var c = tdb.View_TeacherClasses.Single(t => t.ID == id);
            return View(c);
        }
        public ActionResult EditClass(Models.View_TeacherClasses tc)
        {
            try
            {
                var c = tdb.Classes.Single(t => t.ID == tc.ID);
                var a = tdb.TeacherClasses.Where(t => t.ClassID == tc.ID);
                if (a.Count() == 0)
                {
                    tdb.TeacherClasses.Add(new Models.TeacherClasses() { TeacherID = tc.TeacherID, ClassID = tc.ID, Status = 0 });
                }
                else
                {
                    var teaclass = a.ToList()[0];
                    teaclass.TeacherID = tc.TeacherID;
                    teaclass.Status = 0;
                    teaclass.ClassID = tc.ID;
                }
                c.Name = tc.Name;
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult delete(int id)
        {
            try{
       tdb.TeacherClasses.Remove(tdb.TeacherClasses.Single(t=>t.ClassID==id));
           tdb.Classes.Remove(tdb.Classes.Single(t=>t.ID==id));
                tdb.SaveChanges();
                return Content("ok");
            }
            catch{
                return Content("err");
            }
    }
    }
}
