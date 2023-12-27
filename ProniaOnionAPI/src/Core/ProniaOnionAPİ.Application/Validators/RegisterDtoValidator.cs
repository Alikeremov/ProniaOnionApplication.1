using FluentValidation;
using ProniaOnionAPİ.Application.DTOs.User;
using ProniaOnionAPİ.Application.Utilites.Extencions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProniaOnionAPİ.Application.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(regex: new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")).WithMessage("Email name type is not true")
                .MaximumLength(256).WithMessage("max Email length must be 256 carecters")
                .MinimumLength(6).WithMessage("min Email length must be 6 carecters");
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(regex: new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{3,}$")).WithMessage("Your Password minumum must be 1 upper letter,1 lower letter and 1 digit")
                .MinimumLength(8).WithMessage("max Password length must be 8 carecters")
                .MaximumLength(100).WithMessage("min Password length must be 100 carecters");
            RuleFor(x => x.UserName)
                .NotEmpty().
                MaximumLength(256).WithMessage("max UserName length must be 256 carecters")
                .MinimumLength(4).WithMessage("min UserName length must be 4 carecters");
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("max Name length must be 50 carecters")
                .MinimumLength(3).WithMessage("min Name length must be 3 carecters")
                .Must(CheckLetter).WithMessage("Name only contain letters");
            RuleFor(x => x.Surname)
                .MaximumLength(50).WithMessage("max Surname length must be 50 carecters")
                .MinimumLength(3).WithMessage("min Surname length must be 3 carecters")
                .Must(CheckLetter).WithMessage("Surname only contain letters");
            RuleFor(x => x).
                Must(x => x.ConfirmPassword == x.Password);
        }
        public bool CheckLetter(string word)
        {
            int count = 0;
            foreach (char c in word)
            {
                if(Char.IsLetter(c)) count++;
            }
            if(count==word.Length) return true;
            return false;
        }
    }
}
