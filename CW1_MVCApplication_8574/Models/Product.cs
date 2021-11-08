using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW1_MVCApplication_8574.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category ProductCategory { get; set; }
    }
}
