using ETicaretAPI.Application.Features.Products.Models;
using ETicaretAPI.Application.Repositories.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Products.Commands
{
    public class CreateProductCommand:IRequest<CreateProductCommandModel>
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandModel>
    {
        readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await _productWriteRepository.SaveAsync();

            return new();
        }
    }

}
