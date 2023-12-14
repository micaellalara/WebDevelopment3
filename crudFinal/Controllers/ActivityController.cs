using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace crudFinal.Controllers
{
    public class ActivityController : Controller
    {
        private readonly userformEntities _context;

        public ActivityController()
        {
            _context = new userformEntities();
        }

        // GET: Activity
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {

            userformEntities fe = new userformEntities();
            var userList = (from a in fe.users
                                select a).ToList();

            ViewData["UserList"] = userList;
            return View();
        }

        public ActionResult DeleteUser(int user_id)
        {
            var userToDelete = _context.users.Find(user_id);
            if (userToDelete != null)
            {
                _context.users.Remove(userToDelete);
                _context.SaveChanges();
            }

            return Json(new { success = true });
        }
    

    public ActionResult User()
        {
            return View();
        }

        public ActionResult AddActivityToDatabase(FormCollection fc)
        {
            try
            {
                string name = fc["name"];
                string ootd = fc["ootd"];
                string address = fc["address"];

                DateTime date;
                if (!DateTime.TryParse(fc["date"], out date))
                {
                    return RedirectToAction("Error", new { message = "Invalid date format" });
                }

                string status = fc["status"];

                activity act = new activity();
                act.name = name;
                act.ootd = ootd;
                act.address = address;
                act.date = date;
                act.status = status;
                act.user_id = 3;

                using (userformEntities fe = new userformEntities())
                {
                    fe.activities.Add(act);
                    fe.SaveChanges();
                }

                return RedirectToAction("User");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new { message = ex.Message });
            }
        }
        public ActionResult ShowActivity()
        {
            userformEntities fe = new userformEntities();
            var activityList = (from a in fe.activities
                                select a).ToList();

            ViewData["UserList"] = activityList;
            return View();
        }

        
        public ActionResult EditAct(int id)
        {
            userformEntities ae = new userformEntities();
            activity existingActivity = ae.activities.Find(id);
            
            if (existingActivity == null)
            {
                return HttpNotFound();
            }
            
            return View(existingActivity);
        }
    

        [HttpPost]
        public ActionResult Update(activity updatedActivity)
        {
                if (ModelState.IsValid)
                {
                    // Update the existing activity in the database
                    userformEntities ae = new userformEntities();
                    ae.Entry(updatedActivity).State = EntityState.Modified;
                    ae.SaveChanges();

                    return RedirectToAction("ShowActivity");
                }

                return View("EditAct", updatedActivity);
            }
        


    public ActionResult DeleteActivity(int activity_id)
        {
            var activityToDelete = _context.activities.Find(activity_id);
            if (activityToDelete != null)
            {
                _context.activities.Remove(activityToDelete);
                _context.SaveChanges();
            }

            return Json(new { success = true });
        }
    }

}
