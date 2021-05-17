using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public int ProductGroupId { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
    }
}
