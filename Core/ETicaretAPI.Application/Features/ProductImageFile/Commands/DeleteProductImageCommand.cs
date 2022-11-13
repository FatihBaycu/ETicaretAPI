using ETicaretAPI.Application.Features.ProductImageFile.Models;
using ETicaretAPI.Application.Repositories.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ETicaretAPI.Application.Features.ProductImageFile.Commands
{
    public class DeleteProductImageCommand:IRequest<DeleteProductImageCommandModel>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
     
    public class DeleteProductImageCommandHanlder : IRequestHandler<DeleteProductImageCommand, DeleteProductImageCommandModel>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductImageCommandHanlder(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductImageCommandModel> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            Domain.Entities.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

            if (productImageFile != null)
                product?.ProductImageFiles.Remove(productImageFile);

            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
