using ETicaretAPI.Application.Features.Products.Models;
using ETicaretAPI.Application.Repositories.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Products.Commands
{
    public class UpdateProductCommand:IRequest<UpdateProductCommandModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandModel>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<UpdateProductCommandModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productReadRepository.GetByIdAsync(request.Id.ToString());
            product.Stock = request.Stock;
            product.Name = request.Name;
            product.Price = request.Price;
             await _productWriteRepository.SaveAsync();
            return new();
          
        }
    }


}
