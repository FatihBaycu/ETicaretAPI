using ETicaretAPI.Application.Features.ProductImageFile.Models;
using ETicaretAPI.Application.Repositories.ProductImageFiles;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace ETicaretAPI.Application.Features.ProductImageFile.Commands
{
    public class ChangeShowcaseImageCommand:IRequest<ChangeShowcaseImageCommandResponse>
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
    }

    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommand, ChangeShowcaseImageCommandResponse>
    {
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommand request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table
                      .Include(p => p.Products)
                      .SelectMany(p => p.Products, (pif, p) => new
                      {
                          pif,
                          p
                      });

            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);

            if (data != null)
                data.pif.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));
            if (image != null)
                image.pif.Showcase = true;

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
