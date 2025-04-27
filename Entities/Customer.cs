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
        public string Name { get; set; }            // اسم العميل
        public string Phone { get; set; }           // رقم الهاتف
       
        public string Address { get; set; }         // العنوان
        public ICollection<Order> Orders { get; set; }=new List<Order>();

    }
}
