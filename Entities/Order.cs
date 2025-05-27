using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizaa_Restaurant.Entities
{
    internal class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }         
        public DateTime OrderDate { get; set; }      
        public decimal TotalPrice { get; set; }      
        public string Status { get; set; }          
        public ICollection<OrderItem> OrderItems { get; set; }=new List<OrderItem>();
        public Customer Customer { get; set; } //Navigational Property

       


    }
}
