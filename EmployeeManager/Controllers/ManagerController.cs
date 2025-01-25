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
            var manager = db.Users.Where(u => u.Username == User.Identity.Name).FirstOrDefault();
            if (manager == null) {
                return View(HttpNotFound());
            }

            if (!manager.IsManager)
            {
                return View(HttpNotFound());
            }
            var employees = db.Employees.ToList();
            var data = new ManagerHomeViewModel
            {
                Manager = manager,
                Employees = employees
            };
            return View(data);
        }

        // Update
        public ActionResult Update()
        {
            return View();
        }
    }
}