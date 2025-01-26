using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Models.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

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
            if (manager == null)
            {
                return View(HttpNotFound());
            }

            if (!manager.IsManager)
            {
                return View(HttpNotFound());
            }
            var employees = db.Employees
            .Where(e => e.IsDeleted == false)
            .ToList();
            var employeesWithRewards = employees.Select(e => new EmployeeRewardViewModel
            {
                Employee = e,
                RewardHistory = db.RewardHistorys
                    .Where(r => r.EmployeeId == e.EmployeeId)
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



        [HttpGet]
        public ActionResult EditEmployee(int empId)
        {
            var editEmployeeViewModel = new EditEmployeeViewModel();

            var emp = from a in db.Employees
                      join b in db.Users on a.UserId equals b.UserId
                      join c in db.Positions on a.PositionId equals c.PositionId
                      where a.EmployeeId == empId
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
                ViewBag.Positions = db.Positions.ToList();

                return View(result);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult EditEmployee(EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = db.Employees.FirstOrDefault(x => x.EmployeeId.Equals(model.EmployeeId));
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
            var employee = db.Employees.FirstOrDefault(x=>x.EmployeeId.Equals(empId));
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
    }
}