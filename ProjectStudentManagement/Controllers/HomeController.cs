using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectStudentManagement;
using ProjectStudentManagement.Models;

namespace ProjectStudentManagement.Controllers
{
    public class HomeController : Controller
    {
        DBStudentManagementEntities db = new DBStudentManagementEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InsertTBLClassRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertTBLClassRoom( TBLClassRoom tBLClassRoom)
        {
            db.TBLClassRooms.Add(tBLClassRoom);
            db.SaveChanges();
            return View();
        }

        public PartialViewResult prinfTBLClassRoom()
        {
            var listClassRoom = db.TBLClassRooms.ToList();
            return PartialView(listClassRoom);
        }

        public ActionResult DeleteTBLClassRoom(int id)
        {
            try
            {
                var DeleteTBLClassRoom = db.TBLClassRooms.Where(x => x.IdCr == id).FirstOrDefault();
                db.TBLClassRooms.Remove(DeleteTBLClassRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch 
            { 
                return View();
            }
        }

        [HttpGet]
        public ActionResult AddTBLStudent( int id)
        {
            ViewBag.Test = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddTBLStudent(TBLStudent tBLStudent,int id) 
        {
            db.TBLStudents.Add(tBLStudent);
            db.SaveChanges();
            return View();
        }

        public ActionResult ListTBLStudent(int id)
        {
            var listStudent = (from cr in db.TBLClassRooms
                               from st in db.TBLStudents
                               where cr.IdCr == st.IdCr && id == st.IdCr
                               select st).ToList();
            return View(listStudent);
        }

        public ActionResult DeleteTBLStudent(int id)
        {
            try
            {
                var DeleteTBLStudent = db.TBLStudents.Where(x => x.IdSt == id).FirstOrDefault();
                db.TBLStudents.Remove(DeleteTBLStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ShowAllTBLStudent()
        {
            var showAllstudent = db.TBLStudents.ToList();
            return View(showAllstudent);
        }

        public ActionResult OrderByAscId()
        {
            var OrderByAscId = db.TBLStudents.OrderBy(x => x.IdSt).ToList();
            return View(OrderByAscId);
        }

        public ActionResult OrderByDescId()
        {
            var OrderByDescId = db.TBLStudents.OrderByDescending(x => x.IdSt).ToList();
            return View(OrderByDescId);
        }

        public ActionResult IdMax()
        {
            var IdMax = db.TBLStudents.OrderByDescending(x => x.IdSt).Take(1).ToList();
            return View(IdMax);
        }

        public ActionResult IdMin()
        {
            var IdMin = db.TBLStudents.OrderBy(x => x.IdSt).Take(1).ToList();
            return View(IdMin);
        }

        [HttpPost]
        public ActionResult SearchByName(FormCollection f)
        {
            String KeySearchName = f["SearchValue"].ToString();
            List<TBLStudent> ListSearch = db.TBLStudents.Where(x => x.NameSt.Contains(KeySearchName)).ToList();
            if (ListSearch.Count == 0)
            {
                ViewBag.Notification = "No result is found !";
            }
            else 
            { 
                ViewBag.Notification = "Found "+ListSearch.Count+"result";
            }
            return View(ListSearch.OrderBy(x => x.NameSt));
        }
    }
}