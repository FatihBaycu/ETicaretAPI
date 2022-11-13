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
    public class DeleteProductCommand:IRequest<DeleteProductCommandModel>
    {
        public string Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandModel>
    {
        readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductCommandModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.RemoveAsync(request.Id);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }

}
