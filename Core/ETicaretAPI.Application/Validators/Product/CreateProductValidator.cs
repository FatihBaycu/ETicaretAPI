using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<VM_Product_Create>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen Ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Lütfen ürün adını 1 ile 150 karakter arasında giriniz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen stok bilgisni boş geçmeyiniz.")
                .Must(s => s >= 0)
                    .WithMessage("stok bilgisi negatif olamaz");


            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen fiyat bilgisni boş geçmeyiniz.")
                .Must(s => s >= 0)
                    .WithMessage("fiyat bilgisi negatif olamaz");




        }
    }
}
