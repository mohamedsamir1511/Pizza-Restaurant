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
        public int CustomerId { get; set; }         // FK للعميل
        public DateTime OrderDate { get; set; }     // تاريخ الطلب
        public decimal TotalPrice { get; set; }     // السعر الإجمالي
        public string Status { get; set; }          // حالة الطلب (مثل "قيد التحضير", "جاهز للتسليم")
        public ICollection<OrderItem> OrderItems { get; set; }=new List<OrderItem>();
        public Customer Customer { get; set; } // إضافة العلاقة مع Customer


    }
}
