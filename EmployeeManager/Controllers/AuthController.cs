using DataLayer.Context;
using DataLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using System.Web.Security;

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
            if (ModelState.IsValid)
            {
                Employee res = db.Employees.Where(e => e.User.Username == LoginCredentials.Username && e.User.Password == LoginCredentials.Password).FirstOrDefault();
                if (res != null)
                {
                    FormsAuthentication.SetAuthCookie(LoginCredentials.Username, true);
                    if (res.User.IsManager)
                    {
                        return RedirectToRoute("manager/");
                    }
                    else
                    {
                        return RedirectToRoute("employee/");
                    }
                }
            }
            return HttpNotFound();
        }

        [Authorize]
        public ActionResult Signout()
        {   
            FormsAuthentication.SignOut();
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