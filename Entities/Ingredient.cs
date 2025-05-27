using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizaa_Restaurant.Entities
{
    internal class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        public string Name { get; set; }            // اسم المكون (مثل "جبن موزاريلا")
        public string Description { get; set; }     // وصف للمكون
    }
}
