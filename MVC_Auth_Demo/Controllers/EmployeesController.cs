using MVC_Auth_Demo.Models;
using MVC_Auth_Demo.Models.Entities;
using MVC_Auth_Demo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVC_Auth_Demo.Controllers
{
    public class EmployeesController : Controller
    {
        static ApplicationDbContext context = new ApplicationDbContext();
        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.Action = "Add";
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Employees = context.Employees.Include(e => e.Department).ToList();
            EVM.Departments = context.Departments.ToList();
            return View(EVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Add()

        {
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            ViewBag.Action = "Add";
            return View("AddEdit", EVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Add(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                context.Employees.Add(Employee);
                context.SaveChanges();
                return PartialView("_PartialEmployeeRow",Employee);
            }
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            return View("AddEdit", EVM);
        }
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit(int id)
        {
            Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Employee = employee;
            EVM.Departments = context.Departments.ToList();

            if (EVM.Employee != null)
            {
                //ViewBag.Action = "Edit";
                return View("Edit", EVM);
            }
            return RedirectToAction("Index", EVM);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var oldEmployee = context.Employees.FirstOrDefault(e => e.Id == employee.Id);
                if (oldEmployee != null)
                {
                    oldEmployee.Name = employee.Name;
                    oldEmployee.Age = employee.Age;
                    oldEmployee.Gender = employee.Gender;
                    oldEmployee.Email = employee.Email;
                    oldEmployee.Fk_DepartmentId = employee.Fk_DepartmentId;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            EVM.Employee = employee;
            return View("Edit", EVM);
        }

        public ActionResult Details(int id)
        {
            EmployeeViewModel EVM = new EmployeeViewModel();
            Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);
            EVM.Departments = context.Departments.ToList();
            EVM.Employee = employee;
            if (employee != null)
            {
                return View("Details", EVM);
            }
            return RedirectToAction("Index", EVM);
        }

        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete(int id)
        {
            Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return RedirectToAction("Index");

        }


        //public async Task<bool> Delete(int id)
        //{
        //    Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);
        //    if (employee != null)
        //    {
        //        context.Employees.Remove(employee);
        //        await context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;

        //}
    }
}