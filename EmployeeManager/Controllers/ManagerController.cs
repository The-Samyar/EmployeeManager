using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Models.ViewModels;

namespace EmployeeManager.Controllers
{
    public class ManagerController : Controller
    {
        public EmployeeManagerDatabaseContext db = new EmployeeManagerDatabaseContext();

        // Read
        // GET: Manager
        [Authorize]
        public ActionResult Home()
        {
            var user = db.Users.Where(u => u.Username == User.Identity.Name).FirstOrDefault();

            return View(user);
        }

        // Update
        public ActionResult Update()
        {
            return View();
        }
    }
}