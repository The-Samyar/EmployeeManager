using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using DataLayer.Models.ViewModels;

namespace EmployeeManager.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeManagerDatabaseContext db = new EmployeeManagerDatabaseContext();

        // GET: Employee
        [Authorize]
        public ActionResult Home()
        {
            var employee = db.Employees.Where(u => u.User.Username == User.Identity.Name).FirstOrDefault();

            if (employee == null)
            {
                return HttpNotFound();
            }

            if (employee.User.IsManager)
            {
                return HttpNotFound();
            }

            var rewards = db.RewardHistorys.Where(u => u.EmployeeId == employee.EmployeeId).ToList();
            var data = new EmployeeHomeViewModel
            {
                Employee = employee,
                Rewards = rewards,
            };

            return View(data);
        }
    }
}