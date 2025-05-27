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
<<<<<<< HEAD
        public string Name { get; set; }            
        public string Role { get; set; }    
        public string Username {  get; set; }
        public string Password {  get; set; }

        
=======
        public string Name { get; set; }            // اسم الموظف
        public string Role { get; set; }            // دور الموظف (مثل "طاهي" أو "عامل توصيل")
>>>>>>> ad232dab4c3e600c9aef0dbae34339ccb7d2c587
    }
}
