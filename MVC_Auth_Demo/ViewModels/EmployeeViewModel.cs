using MVC_Auth_Demo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Auth_Demo.ViewModels
{
    public class EmployeeViewModel
    {
        public List<Department> Departments { get; set; }
        public Employee Employee { get; set; }

        public Department Department { get; set; }
        public List<Employee> Employees { get; set; }
    }
}