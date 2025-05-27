using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizaa_Restaurant.Entities
{
    internal class OrderItem
    {
        [Key]
       
        public int OrderItemId { get; set; }       // معرف عنصر الطلب

        public int OrderId { get; set; }            // FK للطلب
        public int PizzaId { get; set; }            // FK للبيتزا
        public int Quantity { get; set; }           // الكمية المطلوبة

        public Order Order { get; set; }            // الربط بالطلب Navigational Property 
        public Pizza Pizza { get; set; }            // الربط بالبيتزا Navigational Property


    }
}
