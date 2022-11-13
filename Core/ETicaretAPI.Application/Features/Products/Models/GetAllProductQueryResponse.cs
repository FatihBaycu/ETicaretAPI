using ETicaretAPI.Application.RequestParametres;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Products.Models
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        public object Products { get; set; }
    }
}
