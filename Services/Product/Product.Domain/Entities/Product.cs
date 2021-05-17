using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class Product : Entity
    {
        public int Id { get; private set; }
        public string Definition { get; private set; }
        public int ProductGroupId { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }
    }
}
