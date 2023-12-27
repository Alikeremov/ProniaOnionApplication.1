using ProniaOnionAPİ.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.Abstractions.Services
{
    public interface IAutenticationService
    {
        Task Register(RegisterDto registerDto);
        Task Login(LoginDto loginDto);
    }
}
