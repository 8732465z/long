using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 教学数据平台.Controllers
{
    public class MajorsController : Controller
    {
        //
        // GET: /Majars/
        Models.TeachDBEntities2 tdb = new Models.TeachDBEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMajors()
        {
            var marjors = from m in tdb.Majors
                          join d in tdb.Departments on m.DepartmentID equals d.ID
                          select new
                          {
                              ID = m.ID,
                              departmentName = d.Name,
                              Name = m.Name
                          };
            return Json(marjors);

        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddMajors(Models.Majors major)
        {
            try
            {
                tdb.Majors.Add(major);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult delete()
        {
            return View();
        }
        public ActionResult GetMajor(int deptId)
        {
            var marjors = from m in tdb.Majors
                          where m.DepartmentID == deptId
                          select new
                          {
                              ID = m.ID,
                              Name = m.Name
                          };
            return Json(marjors);
        }

        public ActionResult DeleteMajors(int id)
        {
            try
            {
                var dep = tdb.Majors.First(t => t.ID == id);
                tdb.Majors.Remove(dep);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            } 
        }
        public ActionResult EditMajors(int id, int departmentID, string name, int status)
        {
            try
            {
                var dep = tdb.Majors.First(t => t.ID == id);
               
                dep.Name = name;
                dep.DepartmentID = departmentID;
                dep.Status = status;
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult Edit(int id)
        {
            Models.Majors dep = tdb.Majors.First(t => t.ID == id);
            return View(dep);
        }
    }
}
