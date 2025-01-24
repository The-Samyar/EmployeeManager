using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;

namespace EmployeeManager.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeManagerDatabaseContext db = new EmployeeManagerDatabaseContext();

        // GET: Employee
        [Authorize]
        public ActionResult Home()
        {
            var employee = db.Employees.Where(e => e.User.Username == User.Identity.Name).FirstOrDefault();
            return View(employee);
        }
    }
}