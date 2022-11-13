using ETicaretAPI.Application.Abstraction;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
   

        //public IQueryable<Product> GetAll()
        //{
        //    return _productReadRepository.GetAll();
        //}

        public List<Product> GetProducts()
        =>  new()
        {
            new(){Id=Guid.NewGuid(),Name="Product1",Price=10,Stock=10},
            new(){Id=Guid.NewGuid(),Name="Product2",Price=100,Stock=10},
            new(){Id=Guid.NewGuid(),Name="Product3",Price=90,Stock=10},
            new(){Id=Guid.NewGuid(),Name="Product4",Price=70,Stock=10},
            new(){Id=Guid.NewGuid(),Name="Product5",Price=48,Stock=10}
        };
        
    }
}
