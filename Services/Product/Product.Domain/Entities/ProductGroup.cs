using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class ProductGroup : Entity
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public bool Active { get; set; }
    }
}
