using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Sign()
        {
            return View();
        }

        public ActionResult AddUserToDatabase(FormCollection fc)
        {
            String name = fc["name"];
            String email = fc["email"];
            String password = fc["password"];
            String user_type = fc["user_type"];
            String gender = fc["gender"];

          user use = new user
            {
                name = name,
                email = email,
                password = password,
                user_type = user_type,
                user_id = 1
            };

            userformEntities fe = new userformEntities();
            fe.users.Add(use);
            fe.SaveChanges();

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        private bool IsUserValid(string email, string password, out string user_type)
        {
            using (var context = new userformEntities())
            {
                var user = context.users.SingleOrDefault(u => u.email == email && u.password == password);

                if (user != null)
                {
                    user_type = user.user_type;
                    return true;
                }
                else
                {
                    user_type = null;
                    return false;
                }
            }
        }

        public ActionResult ValidateUser(FormCollection fc)
        {
            string email = fc["email"];
            string password = fc["password"];

            if (IsUserValid(email, password, out string user_type))
            {
                Session["UserType"] = user_type;

                string dashboardController;
                string actionName;

                switch (user_type)
                {
                    case "admin":
                        dashboardController = "Activity";
                        actionName = "Admin";
                        break;
                    case "user":
                        dashboardController = "Activity";
                        actionName = "User";
                        break;
                    default:
                        return RedirectToAction("Login");
                }

                return RedirectToAction(actionName, dashboardController);
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
                return View("Login");
            }
        }
    }
}