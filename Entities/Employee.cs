using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizaa_Restaurant.Entities
{
   
    internal class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }            // اسم الموظف
        public string Role { get; set; }            // دور الموظف (مثل "طاهي" أو "عامل توصيل")
    }
}
