using DataLayer.Context;
using DataLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using System.Web.Security;
using System.Security.Claims;

namespace EmployeeManager.Controllers
{
    public class AuthController : Controller
    {
        public EmployeeManagerDatabaseContext db = new EmployeeManagerDatabaseContext();

        public ActionResult Login()
        {
            var loginForm = new LoginViewModel();
            return View(loginForm);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel LoginCredentials)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Input fields are not valid";
                return View(LoginCredentials);

            }

            var res = db.Users.Where(e => e.Username == LoginCredentials.Username && e.Password == LoginCredentials.Password && e.IsDeleted == false).FirstOrDefault();
            if (res == null)
            {
                ViewBag.ErrorMessage = "User with the following credentials does not exist";
                return View(LoginCredentials);

            }

            FormsAuthentication.SetAuthCookie(LoginCredentials.Username, true);
            if (res.IsManager)
            {
                return RedirectToRoute("Manager");
            }
            else
            {
                return RedirectToRoute("Employee");
            }
        }

        [Authorize]
        public ActionResult Signout()
        {   
            FormsAuthentication.SignOut();

            ViewBag.ErrorMessage = "Some error occured";

            return RedirectToAction("Login");
        }

        public ActionResult Signup()
        {
            var createManagerForm = new CreateManagerViewModel();
            return View(createManagerForm);
        }

        [HttpPost]
        public ActionResult Signup(CreateManagerViewModel managerInfo)
        {
            if (ModelState.IsValid)
            {
                var manager = new User()
                {
                    Username = managerInfo.Username,
                    Password = managerInfo.Password,
                    FirstName = managerInfo.FirstName,
                    LastName = managerInfo.LastName,
                    IsManager = true
                };

                db.Users.Add(manager);
                db.SaveChanges();
            }
            return RedirectToAction("Create");
        }

    }
}