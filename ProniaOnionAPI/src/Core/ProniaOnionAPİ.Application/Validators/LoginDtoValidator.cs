using FluentValidation;
using ProniaOnionAPİ.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.Validators
{
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(256);
        }
    }
}
