using ETicaretAPI.Application.Abstraction.Storage;
using ETicaretAPI.Application.Features.ProductImageFile.Models;
using ETicaretAPI.Application.Repositories.ProductImageFiles;
using ETicaretAPI.Application.Repositories.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.ProductImageFile.Commands
{
    public class UploadProductImageCommand:IRequest<UploadProductImageCommandModel>
    {
        public string Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, UploadProductImageCommandModel>
    {
        private readonly IStorageService _storageService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageCommandModel> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {

            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);

            Product product = await _productReadRepository.GetByIdAsync(request.Id);


            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
