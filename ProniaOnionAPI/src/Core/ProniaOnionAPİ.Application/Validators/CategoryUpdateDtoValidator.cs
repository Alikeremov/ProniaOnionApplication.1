using FluentValidation;
using ProniaOnionAPİ.Application.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.Validators
{
    public class CategoryUpdateDtoValidator:AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).Matches(@"^[a-zA-Z0-9/s]*$");
        }
    }
}
