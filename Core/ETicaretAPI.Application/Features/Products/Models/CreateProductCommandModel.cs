using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Products.Models
{
    public class CreateProductCommandModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}

