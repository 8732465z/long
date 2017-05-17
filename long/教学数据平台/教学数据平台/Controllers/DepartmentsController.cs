using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 教学数据平台.Controllers
{
    public class DepartmentsController : Controller
    {
        //
        // GET: /Departments/
        Models.TeachDBEntities2 tdb = new Models.TeachDBEntities2();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDepartments()
        {
            var deps = from dep in tdb.Departments
                       select new
                       {
                           ID = dep.ID,
                           Name = dep.Name,
                           Status = dep.Status
                       };
            return Json(deps, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddIndex()
        {
            return View();
        }
        public ActionResult EditIndex(int id)
        {

           Models.Departments dep = tdb.Departments.First(t => t.ID == id);
            //string name = tdb.Departments.First(t => t.ID == id).Name;
            //ViewBag.DepartmentName = name;
            return View(dep);
        }
        public ActionResult DeleteDepartement(int id)
        {
            try
            {
                var dep = tdb.Departments.First(t => t.ID == id);
                tdb.Departments.Remove(dep);
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }
        public ActionResult GetDepartmentById(int id)
        {
            try
            {
                var dep1 = from dep in tdb.Departments
                           where dep.ID == id
                           select new { ID = dep.ID, Name = dep.Name, Status = dep.Status };
                return Json(dep1, JsonRequestBehavior.AllowGet);
            }
            catch { return Content("[]"); }

        }
        public ActionResult EditDepartment(int id,string name,int status)
        {
            try
            {
                var dep = tdb.Departments.First(t => t.ID == id);
                dep.Name = name;
                dep.Status = status;
                tdb.SaveChanges();
                return Content("ok");
            }
            catch
            {
                return Content("err");
            }
        }

        public ActionResult AddDepartement(string name,int status)
        {
            try
            {
                var dep = new Models.Departments();
                dep.Name = name;
                dep.Status = status;
                tdb.Departments.Add(dep);
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
