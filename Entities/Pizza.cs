using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizaa_Restaurant.Entities
{
    internal class Pizza
    {
        [Key]
        public int PizzaId { get; set; }
        public string Name { get; set; }            // اسم البيتزا (مثل "بيتزا مارغريتا")
        public string Description { get; set; }     // وصف للبيتزا
        public decimal Price { get; set; }          // سعر البيتزا
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
