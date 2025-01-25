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
            var employees = db.Employees.Where(e=>e.IsDeleted == false).ToList();
            var data = new ManagerHomeViewModel
            {
                Manager = manager,
                Employees = employees
            };
            return View(data);
        }

        public ActionResult CreateEmployee()
        {
            var createEmployeeForm = new CreateEmployeeViewModel();
            ViewBag.Positions = db.Positions.ToList();
            return View(createEmployeeForm);
        }

        [HttpPost]
        public ActionResult CreateEmployee(CreateEmployeeViewModel data)
        {
            if (ModelState.IsValid)
            {
                var employeeUser = new User()
                {
                    Username = data.Username,
                    Password = data.Password,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    IsManager = false,
                    CreatedAt = DateTime.Now,
                    EditedAt = DateTime.Now,
                    IsDeleted = false
                };
                var employee = new Employee()
                {
                    User = employeeUser,
                    PositionId = data.SelectedPosition,
                    HiringDate = data.HiringDate,
                    CreatedAt = DateTime.Now,
                    EditedAt = DateTime.Now,
                    IsDeleted = false
                };

                db.Users.Add(employeeUser);
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            return Redirect("/manager");
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employee = db.Employees.Where(e => e.EmployeeId == id).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            employee.IsDeleted = true;
            employee.User.IsDeleted = true;
            db.SaveChanges();
            return Redirect("/manager");
        }
    }
}