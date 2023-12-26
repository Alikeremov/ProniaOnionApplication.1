using FluentValidation;
using ProniaOnionAPİ.Application.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.Validators
{
    public class ProductCreateDtoValidator:AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(100).WithMessage("You can send max 100 caracters")
                .MinimumLength(2).WithMessage("You must send minumum 2 caracters for product name");
            RuleFor(x => x.SKU)
                .NotEmpty().WithMessage("SKU can't send empty")
                .Must(x => x.Length == 6).WithMessage("SKU only contain 6 caracter");
            RuleFor(x => x.Price).Must(CheckPrice);
            RuleFor(x => x.CategoryId).Must(c => c >= 0);
            RuleForEach(x => x.ColorIds).Must(c => c >= 0);
            RuleForEach(x=>x.TagIds).Must(c => c > 0);
        }
        public bool CheckPrice(decimal price)
        {
            if (price < 10 && price > 999999.99m) return false;
            return true;
        }
    }
}
