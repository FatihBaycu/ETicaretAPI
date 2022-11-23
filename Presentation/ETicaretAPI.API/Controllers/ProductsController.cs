using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ETicaretAPI.Application.Abstraction;
using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Abstraction.Storage;
using ETicaretAPI.Application.Features.ProductImageFile.Commands;
using ETicaretAPI.Application.Features.ProductImageFile.Models;
using ETicaretAPI.Application.Features.ProductImageFile.Queries;
using ETicaretAPI.Application.Features.Products.Commands;
using ETicaretAPI.Application.Features.Products.Models;
using ETicaretAPI.Application.Features.Products.Queries;
using ETicaretAPI.Application.Repositories.Files;
using ETicaretAPI.Application.Repositories.InvoiceFiles;
using ETicaretAPI.Application.Repositories.ProductImageFiles;
using ETicaretAPI.Application.Repositories.Products;
using ETicaretAPI.Application.RequestParametres;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Repositories.ProductRepo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Net;
using System.Xml.Linq;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;

        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;

        private readonly IInvoiceFileWriteRepository _ınvoiceFileWriteRepository;
        private readonly IInvoiceFileReadRepository _ınvoiceFileReadRepository;

        private readonly IStorageService _storageService;
        readonly IConfiguration configuration;

        readonly IMediator _mediator;


        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IInvoiceFileWriteRepository ınvoiceFileWriteRepository, IInvoiceFileReadRepository ınvoiceFileReadRepository
            , IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _ınvoiceFileWriteRepository = ınvoiceFileWriteRepository;
            _ınvoiceFileReadRepository = ınvoiceFileReadRepository;
            _storageService = storageService;
            this.configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQuery getByIdProductQuery)
        {
            GetByIdProductResponse response = await _mediator.Send(getByIdProductQuery);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Post(CreateProductCommand command)
        {

            CreateProductCommandModel response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Put(UpdateProductCommand command)
        {
            UpdateProductCommandModel response = await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommand command)
        {
            DeleteProductCommandModel model = await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommand command)
        {
            command.Files = Request.Form.Files;
            UploadProductImageCommandModel model = await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("add-photo")]
        [Authorize(AuthenticationSchemes = "Admin")]

        public async Task<IActionResult> AddPhoto([FromForm] IFormFileCollection collection, string id)
        {

            AddPhotoProductImageCommand command = new();
            command.Id = id;
            command.Files = collection;

            AddPhotoProductImageModel model = await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("[action]/{id}")]

        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImageQuery getProductImagesQueryRequest)
        {
            List<GetProductImageQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommand removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            //Ders sonrası not !
            //Burada RemoveProductImageCommandRequest sınıfı içerisindeki ImageId property'sini de 'FromQuery' attribute'u ile işaretleyebilirdik!

            removeProductImageCommandRequest.ImageId = imageId;
            DeleteProductImageCommandModel response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommand changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }

}
