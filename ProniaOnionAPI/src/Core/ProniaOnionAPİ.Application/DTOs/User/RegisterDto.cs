using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.DTOs.User
{
    public record RegisterDto(string UserName,string Email,string Name,string Surname,string Password,string ConfirmPassword);
}
