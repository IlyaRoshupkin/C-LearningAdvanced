using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework8v6.Models
{
    public class EmployeeDepartmentViewModel
    {
        public List<Employee> Employees { get; set; }
        public SelectList Departments { get; set; }
        public string EmployeeDepartment { get; set; }
        public string SearchString { get; set; }
    }
}
