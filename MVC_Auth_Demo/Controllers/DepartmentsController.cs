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
    [Authorize(Roles = "Admin,Manager")]
    public class DepartmentsController : Controller
    {
        // GET: Departments

        static ApplicationDbContext context = new ApplicationDbContext();

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Index()
        {
            ViewBag.Action = "Add";
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Employees = context.Employees.Include(e => e.Department).ToList();
            EVM.Departments = context.Departments.ToList();
            return View(EVM);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Department Department)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Add(Department);
                context.SaveChanges();
                return PartialView("_PartialDepartmentRow", Department);
                //return RedirectToAction("Index");
            }
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            return View("Index", EVM);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            EVM.Department = context.Departments.FirstOrDefault(e => e.Id == id);
            if (EVM.Department != null)
            {
                return View("Edit", EVM);
            }
            return RedirectToAction("Index",EVM);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                var oldDepartment = context.Departments.FirstOrDefault(d => d.Id == department.Id);
                if (oldDepartment != null)
                {
                    oldDepartment.Name = department.Name;

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();     
            return View("Edit", EVM);
        }

        public ActionResult Details(int id)
        {
            //EmployeeViewModel EVM = new EmployeeViewModel();
            Department department = context.Departments.FirstOrDefault(d => d.Id == id);
            //EVM.Departments = context.Departments.ToList();

            if (department != null)
            {
                return View("Details", department);
            }
            return RedirectToAction("Index", department);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Department department = context.Departments.FirstOrDefault(e => e.Id == id);
            if (department != null)
            {
                context.Departments.Remove(department);
                context.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}
