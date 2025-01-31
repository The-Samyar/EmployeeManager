﻿using System;
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
            var employee = db.Employees.Where(u => u.User.Username == User.Identity.Name && u.IsDeleted == false).FirstOrDefault();

            if (employee == null)
            {
                ViewBag.ErrorMessage = "User not found";
                return Redirect("/auth/login");
            }

            if (employee.User.IsManager)
            {
                ViewBag.ErrorMessage = "User is not authorized";
                return Redirect("/auth/login");
            }

            var rewards = db.RewardHistorys.Where(u => u.EmployeeId == employee.EmployeeId && u.IsDeleted == false).ToList();
            var data = new EmployeeHomeViewModel
            {
                Employee = employee,
                Rewards = rewards,
            };

            return View(data);
        }
    }
}