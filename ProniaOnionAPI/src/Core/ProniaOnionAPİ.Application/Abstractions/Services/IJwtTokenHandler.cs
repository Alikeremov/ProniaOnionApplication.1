using ProniaOnionAPİ.Application.DTOs.Tokens;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Application.Abstractions.Services
{
    public interface IJwtTokenHandler
    {
        TokenResponseDto CreateToken(AppUser user,int minutes);
    }
}
