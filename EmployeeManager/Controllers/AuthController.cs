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
            if (ModelState.IsValid)
            {
                var res = db.Users.Where(e => e.Username == LoginCredentials.Username && e.Password == LoginCredentials.Password && e.IsDeleted == false).FirstOrDefault();
                if (res != null)
                {
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