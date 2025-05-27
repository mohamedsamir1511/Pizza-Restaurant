using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizaa_Restaurant.Entities
{
    internal class Customer
    {
        [Key]
        public int CustomerId { get; set; }
<<<<<<< HEAD
        public string Name { get; set; }            
        public string Phone { get; set; }        
       
        public string Address { get; set; }        
=======
        public string Name { get; set; }            // اسم العميل
        public string Phone { get; set; }           // رقم الهاتف
       
        public string Address { get; set; }         // العنوان
>>>>>>> ad232dab4c3e600c9aef0dbae34339ccb7d2c587
        public ICollection<Order> Orders { get; set; }=new List<Order>();

    }
}
