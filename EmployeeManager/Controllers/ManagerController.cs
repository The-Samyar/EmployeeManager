using System;
using System.Collections.Generic;
using System.Linq;
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
            var manager = db.Users.Where(u => u.Username == User.Identity.Name && u.IsDeleted == false).FirstOrDefault();
            if (manager == null)
            {
                ViewBag.ErrorMessage = "User not found";
                return Redirect("/auth/login");
            }

            if (!manager.IsManager)
            {
                ViewBag.ErrorMessage = "User is not authorized";
                return Redirect("/auth/login");
            }
            var employees = db.Employees
            .Where(e => e.IsDeleted == false)
            .ToList();
            var employeesWithRewards = employees.Select(e => new EmployeeRewardViewModel
            {
                Employee = e,
                RewardHistory = db.RewardHistorys
                    .Where(r => r.EmployeeId == e.EmployeeId && r.IsDeleted == false)
                    .OrderByDescending(r => r.CreatedAt)
                    .FirstOrDefault()
            }).ToList();


            var data = new ManagerHomeViewModel
            {
                Manager = manager,
                EmployeesWithRewards = employeesWithRewards
            };
            return View(data);
        }

        public ActionResult CreateEmployee()
        {
            var createEmployeeForm = new CreateEmployeeViewModel();
            ViewBag.Positions = db.Positions.Where(p => p.IsDeleted == false).ToList();
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
            var employee = db.Employees.Where(e => e.EmployeeId == id && e.IsDeleted == false).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            employee.IsDeleted = true;
            employee.User.IsDeleted = true;
            db.SaveChanges();
            return Redirect("/manager");
        }



        [HttpGet]
        public ActionResult EditEmployee(int empId)
        {
            var editEmployeeViewModel = new EditEmployeeViewModel();

            var emp = from a in db.Employees
                      join b in db.Users on a.UserId equals b.UserId
                      join c in db.Positions on a.PositionId equals c.PositionId
                      where a.EmployeeId == empId && a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false
                      select new EditEmployeeViewModel
                      {
                          EmployeeId = empId,
                          FirstName = b.FirstName,
                          LastName = b.LastName,
                          Password = b.Password,
                          SelectedPosition = c.PositionId,
                          Username = b.Username
                      };
            var result = emp.FirstOrDefault();
            if (result != null)
            {
                ViewBag.Positions = db.Positions.Where(p => p.IsDeleted == false).ToList();

                return View(result);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult EditEmployee(EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = db.Employees.FirstOrDefault(x => x.EmployeeId.Equals(model.EmployeeId) && x.IsDeleted == false);
                if (employee == null)
                {
                    return View(model);
                }

                employee.PositionId = model.SelectedPosition;
                employee.User.FirstName = model.FirstName;
                employee.User.LastName = model.LastName;
                employee.User.Password = model.Password;
                employee.User.Username = model.Username;
                db.SaveChanges();

                return Redirect("/manager");
            }
            return View(model);
        }

        public ActionResult AddReward(int empId)
        {
            var employee = db.Employees.FirstOrDefault(x=>x.EmployeeId.Equals(empId) && x.IsDeleted == false);
            var addRewardViewModel = new AddRewardViewModel
            {
                EmployeeId = empId,
                RewardRate = employee.Position.RewardRate,
            };

            return View(addRewardViewModel);
        }

        [HttpPost]
        public ActionResult AddReward(AddRewardViewModel addRewardViewModel)
        {
            if (ModelState.IsValid)
            {
                var rewardHistory = new RewardHistory
                {
                    EmployeeId = addRewardViewModel.EmployeeId,
                    Count = addRewardViewModel.Count,
                    Rate = addRewardViewModel.RewardRate,
                    Message = addRewardViewModel.Message,
                    CreatedAt = DateTime.Now,
                    EditedAt = DateTime.Now,
                    IsDeleted = false
                };

                db.RewardHistorys.Add(rewardHistory);
                db.SaveChanges();
            }
            return Redirect("/manager");
        }

        public ActionResult Positions()
        {
            var positions = db.Positions
            .Where(e => e.IsDeleted == false)
            .ToList();

            return View(positions);
        }

        public ActionResult CreatePosition()
        {
            var createRewardViewModel = new CreatePositionViewModel();
            return View(createRewardViewModel);
        }

        [HttpPost]
        public ActionResult CreatePosition(CreatePositionViewModel data)
        {
            if (ModelState.IsValid)
            {
                var position = new Position()
                {
                    Title = data.Title,
                    RewardRate = data.RewardRate,
                    CreatedAt = DateTime.Now,
                    EditedAt = DateTime.Now,
                    IsDeleted = false
                };
                db.Positions.Add(position);
                db.SaveChanges();
                return Redirect("/manager/positions");
            }
            return Redirect("/manager/positions/createposition");
        }

        public ActionResult DeletePosition(int posId)
        {
            db.Positions.FirstOrDefault(x => x.PositionId.Equals(posId)).IsDeleted = true;
            db.SaveChanges();
            return Redirect("/manager/positions");
        }
    }
}