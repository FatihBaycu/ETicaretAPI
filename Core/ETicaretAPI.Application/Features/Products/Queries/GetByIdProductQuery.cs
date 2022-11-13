using ETicaretAPI.Application.Features.Products.Models;
using ETicaretAPI.Application.Repositories.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Products.Queries
{
    public class GetByIdProductQuery : IRequest<GetByIdProductResponse>
    {
        public string Id { get; set; }
    }
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductResponse> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            Product product=await _productReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Name=product.Name,
                Stock=product.Stock,
                Price=product.Price,

            };
        }
    }
}
