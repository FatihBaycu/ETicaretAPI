using ETicaretAPI.Application.Features.ProductImageFile.Models;
using ETicaretAPI.Application.Repositories.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.ProductImageFile.Queries
{
    public class GetProductImageQuery : IRequest<List<GetProductImageQueryResponse>>
    {
        public string Id { get; set; }
    }
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQuery, List<GetProductImageQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration configuration;

        public GetProductImageQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            this.configuration = configuration;
        }

        public async Task<List<GetProductImageQueryResponse>> Handle(GetProductImageQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                   .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return product?.ProductImageFiles.Select(p => new GetProductImageQueryResponse
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id
            }).ToList();
        }

    }
}

