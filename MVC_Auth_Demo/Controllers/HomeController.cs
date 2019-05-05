using MVC_Auth_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace MVC_Auth_Demo.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext context;
        ApplicationRoleManager roleManager;
        ApplicationUserManager userManager;

        public ApplicationDbContext Context
        {
            get
            {
                return context ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }

            set
            {
                context = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }

            set
            {
                roleManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>(); ;
            }

            set
            {
                userManager = value;
            }
        }

        public ActionResult Index()
        {
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole manager = new IdentityRole("Manager");
            IdentityRole employee = new IdentityRole("Employee");

            RoleManager.Create(admin);
            RoleManager.Create(manager);
            RoleManager.Create(employee);

            ApplicationUser adminUser = UserManager.Users.FirstOrDefault(u => u.Email.StartsWith("admin"));
            ApplicationUser managerUser = UserManager.Users.FirstOrDefault(u => u.Email.StartsWith("manager"));
            ApplicationUser employeeUser = UserManager.Users.FirstOrDefault(u => u.Email.StartsWith("employee"));

            UserManager.AddToRole(adminUser.Id, admin.Name);
            UserManager.AddToRole(managerUser.Id, manager.Name);
            UserManager.AddToRole(employeeUser.Id, employee.Name);


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}